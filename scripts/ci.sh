#!/bin/bash
set -e
set -x


dotnet restore
dotnet tool restore
export ConnectionStrings__RustStashDatabase="Server=${MSSQL_PORT_1433_TCP_ADDR},${MSSQL_PORT_1433_TCP_PORT};Database=RustStash;User Id=sa;password=Strong_!_Password;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;"
dotnet ef database update --verbose --startup-project=src/RustStash.Web/RustStash.Web.csproj --project=src/RustStash.Core/RustStash.Core.csproj
dotnet build --configuration=Release --no-restore /warnaserror
dotnet ef database update --no-build --startup-project=src/RustStash.Web/RustStash.Web.csproj --project=src/RustStash.Core/RustStash.Core.csproj --configuration=Release

dotnet test ./tests/RustStash.Test.Unit --configuration=Release --no-build --no-restore
dotnet test ./tests/RustStash.Test.Integration --configuration=Release --no-build --no-restore
dotnet test ./tests/RustStash.Test.GraphQL --configuration=Release --no-build --no-restore