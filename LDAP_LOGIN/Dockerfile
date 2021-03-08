#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LDAP_LOGIN/LDAP_LOGIN.csproj", "LDAP_LOGIN/"]
RUN dotnet restore "LDAP_LOGIN/LDAP_LOGIN.csproj"
COPY . .
WORKDIR "/src/LDAP_LOGIN"
RUN dotnet build "LDAP_LOGIN.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LDAP_LOGIN.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LDAP_LOGIN.dll"]