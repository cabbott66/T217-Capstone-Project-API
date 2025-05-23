# StakeholderRisk API 

Code documentation is automatically created and published to a [GitHub Page](https://cabbott66.github.io/T217-Capstone-Project-API/) 
> [!WARNING]
> Update the GitHub Page URL when transferring ownership of the repository.

## MariaDB

Change the connection string in apssettings.json to your local MariaDB information when running locally.

MariaDB: https://mariadb.org/

The API uses EFCore Migrations to update and manage the database schema. 
> [!IMPORTANT]
> A new migration must be made and the database updated before any changes to the mdoels are reflected on the database.

**To create a new migration:**

.NET CLI: `dotnet ef migrations add <migrationName>`

Visual Studio: `Add-Migration <migrationName>`

**To update the database:**

.NET CLI: `dotnet ef database update`

Visual Studio: `Update-Database`

The application is designed to automatically migrate on startup, this should mean as long as you have migrations in your Migrations folder, the database should automatically be build from your migrations, assuming the connection string has been properly configured.

More information on how to work with [Entitiy Framework](https://learn.microsoft.com/en-us/ef/).

> [!IMPORTANT]
> When adding a new table to the database schema, to work with it through Entity Framework, you must first create a DbSet of the model in the StakeholderRisksContext file.

## Dependency Injection

The application makes use of dependency injection in the controllers to use the repositories.
> [!IMPORTANT]
> When creating a new repository, the repository and interface must both be added to the scope in the Program file.


## DocFX

DocFX is used to process the XML commenting of the code and create a documentation website. A GitHub action has been set up to automatically handle DocFX creation whenever a push is made to the master branch, and will automatically update the associated GitHub Pages page.

More information on [DocFX](https://dotnet.github.io/docfx/)

Guide on how to create [XML comments](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/)

## Useful Links

[ASP.NET Documentation](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-9.0)

[GitHub README syntax](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax)

## Dalek

[It's just a dalek.](https://en.wikipedia.org/wiki/Dalek)

```
            (\. -- ./)
        O-0)))--|     \
          |____________|
           -|--|--|--|-
           _T~_T~_T~_T_
          |____________|
          |_o_|____|_o_|
       .-~/  :  |   %  \
.-..-~   /  :   |  %:   \
`-'     /   :   | %  :   \
       /   :    |#   :    \
      /    :    |     :    \
     /    :     |     :     \
 . -/     :     |      :     \- .
|\  ~-.  :      |      :   .-~  /|
\ ~-.   ~ - .  _|_  . - ~   .-~ /
  ~-.  ~ -  . _ _ _ .  - ~  .-~
       ~ -  . _ _ _ .  - ~
