export host=172.17.9.83
export port=1433
export timeout=30
export dbname=TDRCollectionDev
export user=sa
export password=p@ssw0rd

export context_name_space=DAL.DbContexts
export context_dir=./DbContexts
export context_name=SqlContext

export output_dir=./EntitiesV2
export name_space=DAL.Entities

dotnet ef dbcontext scaffold "Server=$host,$port;Initial Catalog=$dbname;User ID=$user; Password=$password; Timeout=$timeout; TrustServerCertificate=True" \
Microsoft.EntityFrameworkCore.SqlServer \
--use-database-names \
--data-annotations \
--no-onconfiguring \
--context-dir $context_dir \
--context $context_name \
--context-namespace $context_name_space \
--namespace $name_space \
--output-dir $output_dir \
--no-pluralize \
--force
