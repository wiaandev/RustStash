#!/bin/bash
set -e
set -x

dotnet ef database drop --startup-project=src/RustStash.Web/RustStash.Web.csproj --project=src/RustStash.Core/RustStash.Core.csproj --force
dotnet ef database update --startup-project=src/RustStash.Web/RustStash.Web.csproj --project=src/RustStash.Core/RustStash.Core.csproj

cd ./src/RustStash.Seed
dotnet run