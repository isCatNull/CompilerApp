## CompilerApp
Simple one page web service, allows user to call JDoodle compiler API, execute code and return results back to user. Also, stores each compilation to database.

## Running locally
- Set `Credentials:ClientId` and `Credentials:ClientSecret` with values from JDoodle account in appsettings.json or in your local secrets.json file
- Run `dotnet ef database update`