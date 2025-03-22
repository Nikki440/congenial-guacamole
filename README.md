
To crate database, open Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console) and run: 
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
To drop database, open Package Manager Console and run:
```
dotnet ef database drop --force
```
