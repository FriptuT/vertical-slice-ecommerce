# vertical-slice-ecommerce
Simplified e-commerce online shop with a vertical slice arhitecture built in ASP.NET Core web api, Angular , MS SQL SERVER (with Docker).

# HOW TO RUN THE PROJECT

# STEP 1
if executed first time, just do:
  docker compose up -d
else:
  docker compose down -v
  docker compose up -d

# STEP 2
  Initiate DB
  Run this command in the directory where 'init.sql' exists (command prompt, not powershell)
  
     path: simplifiedEcomm/docker/sqlserver/init.sql

  command: docker exec -i ecommerce-sql //opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P TeodorIsBack!1 -C < init.sql

# STEP 3
Run the backend:

   path:  simplifiedEcomm/src/Ecommerce.Api
command: dotnet run

# STEP 4
Run the frontend:

   path: simplifiedEcomm/frontend/angular-fe
command: ng serve

# STEP 5
Enjoy! 


# NOTE
For adding products to shopping cart we need to register && login . 
