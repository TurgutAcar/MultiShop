FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/MultiShop.Shared/*.csproj Shared/MultiShop.Shared/
COPY Services/Checkout/MultiShop.Checkout/*.csproj Services/Checkout/MultiShop.Checkout/
RUN dotnet restore Services/Checkout/MultiShop.Checkout/*.csproj
COPY . .
RUN dotnet publish Services/Checkout/MultiShop.Checkout/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","MultiShop.Checkout.dll"]

 


