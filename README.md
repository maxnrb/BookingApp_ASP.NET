## Installation

## First pull from git
1. If you have the following error message when you launch the project : 
    ```
    Impossible de démarrer le débogage. Impossible de lance le projet de démarrage. Assuerez-vous que le projet approprié est défini en tant que projet de démarrage... 
    ```
    Juste close the solution (project) and re-open it via the ` BookingApp.sln `

2. [IMPORTANT] Create the dabatase (with the help of ` Migrations ` directory) with Entity Framework :

    Go in ` Tools > NuGet Package Manager > Package Manager Console `, and enter:
    ```powershell
    Update-Database
    ```
    
3. [IMPORTANT] Launch the project and go to ` /InitDB ` in order to create Roles in database

## Installed packages
Via ` Tools > NuGet Package Manager > Package Manager Console `

- EntityFrameworkCore for Microsoft SQL Server : 
```powershell
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

- EntityFrameworkCore Design in order to generate CRUD: 
```powershell
Install-Package Microsoft.EntityFrameworkCore.Design
```
