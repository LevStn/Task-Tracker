# Task-Tracker

## Stack
  ASP .NET Core 6
  
  Entity Framework
  
  MSSQL
  
## Launch instructions
-Run Task_Tracker.sln
-At the first start, you need to change connection string in ***appsetings.json*** that file stored in ***Task_Tracker.API***
```
"ConnectionStrings": {
    "ConnectionString": "Server=.;Database=Task-Tracking;Trusted_Connection=True;TrustServerCertificate=true"
  }
 ```
-Apply database migrations, open ***Package Manager Console*** in ***Visual Studio*** and run the command
```
update-database
 ```
 -Run the project
