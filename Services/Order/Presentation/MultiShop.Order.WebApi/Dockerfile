FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80
COPY Services/Order/Presentation/MultiShop.Order.WebApi/*.csproj Services/Order/Presentation/MultiShop.Order.WebApi/

COPY Services/Order/Core/MultiShop.Order.Application/*.csproj Services/Order/Core/MultiShop.Order.Application/
COPY Services/Order/Core/MultiShop.Order.Domain/*.csproj Services/Order/Core/MultiShop.Order.Domain/
COPY Services/Order/Infrastructure/MultiShop.Order.Persistence/*.csproj Services/Order/Infrastructure/MultiShop.Order.Persistence/

RUN dotnet restore Services/Order/Presentation/MultiShop.Order.WebApi/*.csproj
COPY . .
RUN dotnet publish Services/Order/Presentation/MultiShop.Order.WebApi/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","MultiShop.Order.WebApi.dll"]

 


