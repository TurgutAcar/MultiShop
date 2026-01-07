FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80
COPY Services/Stock/Presentation/MultiShop.Stock.WebApi/*.csproj Services/Stock/Presentation/MultiShop.Stock.WebApi/

COPY Services/Stock/Core/MultiShop.Stock.Application/*.csproj Services/Stock/Core/MultiShop.Stock.Application/
COPY Services/Stock/Infrastructure/MultiShop.Stock.Persistence/*.csproj Services/Stock/Infrastructure/MultiShop.Stock.Persistence/

RUN dotnet restore Services/Stock/Presentation/MultiShop.Stock.WebApi/*.csproj
COPY . .
RUN dotnet publish Services/Stock/Presentation/MultiShop.Stock.WebApi/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","MultiShop.Stock.WebApi.dll"]

 


