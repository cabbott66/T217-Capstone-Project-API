# StakeholderRisk API

## MariaDB

Change the connection string in apssettings.json to your local MariaDB information when running locally.

MariaDB: https://mariadb.org/

The API uses EFCore Migrations to update and manage the database schema, and **a new migration must be made** and the database updated before any changes to the mdoels are reflected on the database.

**To create a new migration:**

.NET CLI: `dotnet ef migrations add <migrationName>`

Visual Studio: `Add-Migration <migrationName>`

**To update the database:**

.NET CLI: `dotnet ef database update`

Visual Studio: `Update-Database`

The application is designed to automatically migrate on startup, this should mean as long as you have migrations in your Migrations folder, the database should automatically be build from your migrations, assuming the connection string has been properly configured.

More information on how to work with [Entitiy Framework](https://learn.microsoft.com/en-us/ef/).

## DocFX

DocFX is used to process the XML commenting of the code and create a documentation website. A GitHub action has been set up to automatically handle DocFX creation whenever a push is made to the master branch, and will automatically update the associated GitHub Pages page.

More information on [DocFX](https://dotnet.github.io/docfx/)

Guide on how to create [XML comments](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/)
