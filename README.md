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

## Add External Connexion
- Google ([See documentation](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-5.0)) :
    1. Open .NET Core CLI in the project directory, for this go in ` View > Terminal ` or use ` Ctrl+ù `
    2. Enable secret storage, run the following command : ` dotnet user-secrets init `
    3. Store the Google client ID and secret : 
        ```powershell
        dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
        dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"
        ```

## Installed packages
Already installed via ` Tools > NuGet Package Manager > Package Manager Console `

- EntityFrameworkCore for Microsoft SQL Server: ` Microsoft.EntityFrameworkCore.SqlServer `
- EntityFrameworkCore Design in order to generate CRUD: `  Microsoft.EntityFrameworkCore.Design `
- Identity UI for login/register interfaces: ` Microsoft.AspNetCore.Identity.UI `
