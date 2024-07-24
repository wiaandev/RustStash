#!/bin/bash
set -e
set -x

rm -rf ./src/RustStash.Core/Migrations || 0

dotnet ef database drop --startup-project=src/RustStash.Web/RustStash.Web.csproj --project=src/RustStash.Core/RustStash.Core.csproj --force
dotnet ef migrations add --startup-project=src/RustStash.Web/RustStash.Web.csproj --project=src/RustStash.Core/RustStash.Core.csproj InitialCreate
dotnet ef database update --startup-project=src/RustStash.Web/RustStash.Web.csproj --project=src/RustStash.Core/RustStash.Core.csproj