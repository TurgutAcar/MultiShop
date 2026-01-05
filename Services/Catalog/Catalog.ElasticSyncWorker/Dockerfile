FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/MultiShop.Shared/*.csproj Shared/MultiShop.Shared/
COPY Services/Catalog/Catalog.ElasticSyncWorker/*.csproj Services/Catalog/Catalog.ElasticSyncWorker/
RUN dotnet restore Services/Catalog/Catalog.ElasticSyncWorker/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/Catalog.ElasticSyncWorker/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","Catalog.ElasticSyncWorker.dll"]

 


