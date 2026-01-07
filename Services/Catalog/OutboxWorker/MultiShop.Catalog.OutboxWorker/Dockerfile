FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80
COPY Services/Catalog/OutboxWorker/MultiShop.Catalog.OutboxWorker/*.csproj Services/Catalog/OutboxWorker/MultiShop.Catalog.OutboxWorker/
RUN dotnet restore Services/Catalog/OutboxWorker/MultiShop.Catalog.OutboxWorker/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/OutboxWorker/MultiShop.Catalog.OutboxWorker/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","MultiShop.Catalog.OutboxWorker.dll"]

 


