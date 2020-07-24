#Following tutorial at https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code

#initial scripts to create dev environment
dotnet new webapi -o TodoApi
cd TodoApi
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.InMemory
code -r ../TodoApi

# can call web api through terminal using curl
curl -k https://localhost:5001/weatherforecast

#Scaffold a controller
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet aspnet-codegenerator controller -name TodoItemsController -async -api -m TodoItem -dc TodoContext -outDir Controllers

# can post data using curl
## post
curl -k -H "Content-Type: application/json" 
-d '
{
  "name":"walk dog",
  "isComplete":true
}' 
https://localhost:5001/api/TodoItems

## get
curl -k https://localhost:5001/api/TodoItems # get list
curl -k https://localhost:5001/api/TodoItems/1  # get TodoItems with id = 1