# timeoff

A holiday and 'time off' application where users can request whole days and half days off.

This is open source so anyone can contribute to it and make it better. All Pull Requests will be given a helpful and non-scary code review.

It is an ASP .Net 5 MVC & Razor Pages application set up with PostgreSql, a registration and login system using Identity and full logging using ILogger with Serilog and Elasticsearch, plus email sending using Gmail.

tl;dr key technical features:

- Uses PostgreSql instead of MS SQL Server
- A working registration and login system with Gmail to send emails
- Logging to daily rolling text documents (cleared weekly) using ILogger
- Logging to Elasticsearch using ILogger

## Initial setup

### Customising the appsettings.json

You will need to set up your PostgreSQL database connection string, your Elasticsearch URL, and your Gmail account details to enable the database, logging and sending emails respectively.

### Getting the database set up

Install the dotnet-ef tool globally, if you don't already have it using `dotnet tool install --global dotnet-ef` then run `dotnet ef database update` in a terminal from the Jcreek.Timeoff project folder. This will create the initial database and seed the initial test data.

## Making data changes

When you make changes to it, add a migration with the command `dotnet ef migrations add AddStuff`, for example `dotnet ef migrations add AddBaseDataSeeding`. To undo a migration, use `dotnet ef migrations remove`
