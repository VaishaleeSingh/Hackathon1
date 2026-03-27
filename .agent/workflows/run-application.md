---
description: Start the backend matching the exact Mac/Windows environment setup
---
# Application Startup Workflow

This workflow automatically provisions the database and starts the API on both Windows and Mac using Docker.

Follow these exact steps sequentially:

// turbo-all
1. Check if the user has the required software installed by silently running `dotnet --version` and `docker --version`.
2. **If either command fails:** Halt the workflow immediately. Tell the user exactly what is missing and provide these instructions:
   - **For .NET 10 SDK:** "You need the .NET SDK installed to build and run the backend. Download it here: [Microsoft .NET Downloads](https://dotnet.microsoft.com/en-us/download)."
   - **For Docker Desktop:** "You need Docker Desktop installed to run the local Microsoft SQL Server database. Download it here: [Docker Desktop](https://www.docker.com/products/docker-desktop/)."
   - **After Installing (Mac/Windows):** "Once installed, please ensure you start the Docker Desktop application, and restart this terminal or agent session to refresh your PATH variables. Then, run `/run-application` again!"
3. If both are installed, verify the Docker daemon is actually running by executing `docker info`. If it fails, halt and ask the user to open the Docker Desktop app.
4. Spin up the Microsoft SQL Server container:
`docker run --rm -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SuperSecretPassword123!" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest`
5. Wait exactly 15 seconds to allow the SQL Server engine to fully boot.
6. Create the registration database:
`docker exec -i sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'SuperSecretPassword123!' -C -Q "CREATE DATABASE registration"`
7. Apply the database schema by piping the `Schema.sql` file into the container:
`docker exec -i sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'SuperSecretPassword123!' -C -d registration -i /dev/stdin < RecruitmentSystem/Schema.sql`
8. Start the .NET API server:
`dotnet run --project RecruitmentSystem.API/RecruitmentSystem.API.csproj --urls "http://localhost:5123"`
9. Provide the user with the success message and their Swagger URL (`http://localhost:5123/swagger`).
