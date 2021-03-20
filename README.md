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
    
3. [IMPORTANT] Launch the project and go to ` <your url>/InitDB ` in order to create Roles in database

## Installed packages
Already installed via ` Tools > NuGet Package Manager > Package Manager Console `

- EntityFrameworkCore for Microsoft SQL Server: ` Microsoft.EntityFrameworkCore.SqlServer `
- EntityFrameworkCore Design in order to generate CRUD: `  Microsoft.EntityFrameworkCore.Design `
- Identity UI for login/register interfaces: ` Microsoft.AspNetCore.Identity.UI `
