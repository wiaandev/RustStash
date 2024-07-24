FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY bin/Release/net8.0/linux-x64/publish/ /app/
ENTRYPOINT ["dotnet", "RustStash.Web.dll"]
