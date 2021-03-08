using Microsoft.AspNetCore.Components;
using System;
using System.DirectoryServices;

namespace LDAP_LOGIN.Data
{
    public class LoginActiveDirectory
    {
        public bool loggedIn = false;
        public string role = "";
        public string username = "";
        public string password = "";
        public SearchResultCollection results = null;
        public DirectorySearcher ds = null;

        public void AuthCheck()
        {
            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://192.168.1.154", username, password);

                if (de.Properties.Count == 0) { }

                ds = new DirectorySearcher(de);
                getUserInfo();

                loggedIn = true;
            }
            catch
            {
                loggedIn = false;
                username = "";
                password = "";
            }
        }

        public void getUserInfo()
        {
            try
            {
                // Full Name
                ds.PropertiesToLoad.Add("name");

                // Email Address
                ds.PropertiesToLoad.Add("mail");

                // First Name
                ds.PropertiesToLoad.Add("givenname");

                // Last Name (Surname)
                ds.PropertiesToLoad.Add("sn");

                // Login Name
                ds.PropertiesToLoad.Add("userPrincipalName");

                // Distinguished Name
                ds.PropertiesToLoad.Add("distinguishedName");

                ds.Filter = "(&(userprincipalname="+username+"@os.local)(objectCategory=User)(objectClass=person))";
                results = ds.FindAll();

                if(results[0].Properties["name"].Count > 0)
                    username = results[0].Properties["name"][0].ToString();

                role = results[0].Properties["adspath"][0].ToString().Split(",")[1].Split("=")[1];

                Console.WriteLine("2");

            } catch { }
        }

        public void Logout()
        {
            loggedIn = false;
            username = "";
            password = "";
        }
    }
}
