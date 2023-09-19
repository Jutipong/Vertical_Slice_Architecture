set host=172.17.9.83
set port=1433
set timeout=30
set dbname=Go_Test
set user=sa
set password=p@ssw0rd

set context_name_space=Databases
set context_dir=.\Databases
set context_name=SqlContext

set output_dir=.\Entities
set name_space=Entities

dotnet ef dbcontext scaffold "Server=%host%,%port%;Initial Catalog=%dbname%;User ID=%user%; Password=%password%; Timeout=%timeout%; TrustServerCertificate=True" ^
Microsoft.EntityFrameworkCore.SqlServer ^
--use-database-names ^
--data-annotations ^
--no-onconfiguring ^
--context-dir %context_dir% ^
--context %context_name% ^
--context-namespace %context_name_space% ^
--namespace %name_space% ^
--output-dir %output_dir% ^
--no-pluralize ^
--force