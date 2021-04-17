# Booking Web App - ASP.NET

## Functionality
- Home page:
  - Research offers
  - View last offers
- Login & Register:
  - External authentification (Google)
  - 2FA authentication
- CRUD (with research, edit and delete):
  - Accommodation   
  - Offer
  - Booking (no edition and deletion)
  - User (for Administrator) 
- Add form:
  - Accommodation:
    - General informations
    - House rules
    - Manage pictures
    - Manage rooms and amenities 
  - Offer
- Booking page with accommodation and offer informations
- Offers bookmark
- E-Wallet and transaction

### Home Page Preview

![Home Page Screenshot](https://raw.githubusercontent.com/maximenrb/BookingApp_ASP.NET/master/git_img/screencapture-home_page.png)

## Installation

### First pull from git
1. If you have the following error message when you launch the project : 
    ```
    Impossible de démarrer le débogage. Impossible de lance le projet de démarrage. Assurez-vous que le projet approprié est défini en tant que projet de démarrage... 
    ```
    Juste close the solution (project) and re-open it via the ` BookingApp.sln `

2. **[IMPORTANT]** Create the dabatase (with the help of ` Migrations ` directory) with Entity Framework :

    Go in ` Tools > NuGet Package Manager > Package Manager Console `, and enter:
    ```powershell
    Update-Database
    ```
    
3. **[IMPORTANT]** Launch the project and go to ` <your url>/InitDB ` *(ex: ` https://localhost:44369/InitDB ` )* in order to create Roles and default Users in database

    The following users are created **(delete these users or change passwords for production !)** :
    
    - User > email : ` user@user.fr ` - password : ` 123aA_ `
    - Host > email : ` host@host.fr ` - password : ` 123aA_ `
    - Admin > email : ` admin@admin.fr ` - password : ` 123aA_ `

### Add External Login/Register
Firstly, you must enable secret storage, for this open .NET Core CLI in the project directory, go in ` View > Terminal ` or use ` Ctrl+ù `.

Then, run the following command : 
```powershell
dotnet user-secrets init 
```

#### Google ([See documentation for setup](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-5.0)) :
* Store the Google client ID and secret :
    
    ```powershell
    dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
    dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"
    ```
        
## Built With
- [ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-5.0) - ASP.NET Core MVC is a rich framework for building web apps and APIs using the Model-View-Controller design pattern.
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - Entity Framework Core is a modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations. EF Core works with many databases, including SQL Database (on-premises and Azure), SQLite, MySQL, PostgreSQL, and Azure Cosmos DB.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) - Microsoft SQL Server is a relational database management system developed by Microsoft.
- [Bootstrap](https://getbootstrap.com/) - Bootstrap is a free and open-source CSS framework directed at responsive, designed for front-end web development.


## Authors
- [MaximeNrb](https://github.com/maximenrb)
- [Amot98](https://github.com/Amot98)
- [Lekolix](https://github.com/Lekolix)


## License

<img align="left" width= "150" alt="CC Attribution-NonCommercial-NoDerivatives 4.0 International Public License Logo" src="https://raw.githubusercontent.com/maximenrb/BookingApp_ASP.NET/master/git_img/by-nc-nd.eu.png">

This project is licensed under the CC Attribution-NonCommercial-NoDerivatives 4.0 International Public License

See the [LICENSE.md](https://github.com/maximenrb/BookingApp_ASP.NET/blob/master/LICENSE.md) file for details
