# Docker/Podman

sudo podman pull mcr.microsoft.com/mssql/server:2022-latest

sudo podman run --replace -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Passw0rd" -p 1433:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest

sudo podman exec -it sql1 "bash"

/opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "Passw0rd" -C

SELECT Name from sys.Databases
GO


More info: https://computingpost.medium.com/run-microsoft-sql-server-in-podman-docker-container-7d270e9c97b0



# Entity Framework

"dotnet_init" (to get dotnet on $PATH)

dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

"AppDbContext setup + Program.cs"

dotnet tool install --global dotnet-ef

dotnet restore

dotnet ef migrations add <name>

dotnet ef database update
