
## Getting Started

1. Install dotnet 7.0 or higher (https://dotnet.microsoft.com)
2. Connection to MongoDB (local/cloud)
3. Clone this repo
4. Go to `src` folder
5. Change `Mongodb` settings inside `appsettings.Development.json`

```json
    {
        "ConnectionStrings": {
            "Mongodb": "mongodb://localhost:27017"
        }
    }
```
6. Open terminal/CMD and run the following command:

    `dotnet run`
7. Open swagger (eg: http://localhost:5183/swagger) or you can use Postman collection below

## Testing with Postman

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/580677-1d294ded-1964-422a-b2cf-3ab3202ff99a?action=collection%2Ffork&collection-url=entityId%3D580677-1d294ded-1964-422a-b2cf-3ab3202ff99a%26entityType%3Dcollection%26workspaceId%3D65cade96-470b-4547-b69a-e5704d735eea#?env%5BLocal%5D=W3sia2V5IjoiYmFzZVVybCIsInZhbHVlIjoiaHR0cDovL2xvY2FsaG9zdDo1MTgzIiwiZW5hYmxlZCI6dHJ1ZSwidHlwZSI6ImRlZmF1bHQiLCJzZXNzaW9uVmFsdWUiOiJodHRwOi8vbG9jYWxob3N0OjUxODMiLCJzZXNzaW9uSW5kZXgiOjB9XQ==)

## Run Unit/Integration Testing

1. Follow `Getting Started` section from step #1 to step #3
2. Go to `test` folder
3. Change `Mongodb` settings inside `appsettings.Integration.json`

```json
    {
        "ConnectionStrings": {
            "Mongodb": "mongodb://localhost:27017"
        }
    }
```
4. Open terminal/CMD and run the following command:

    `dotnet test --no-build --verbosity normal`

### Please submit your solution as a GitHub repository with the following files:

- [x] A C# class for the spin game model.
- [x] A C# class for the prize model.
- [x] A C# interface for the spin game repository.
- [x] A C# implementation of the spin game repository using MongoDB.
- [x] A C# class for the spin result model.
- [x] A C# interface for the spin result repository.
- [x] A C# implementation of the spin result repository using MongoDB.
- [x] Test case for each function you make 
- [x] Create a README file that provides instructions on how to run the script