FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80
COPY Services/Monitor/MultiShop.Monitor/*.csproj Services/Monitor/MultiShop.Monitor/
RUN dotnet restore Services/Monitor/MultiShop.Monitor/*.csproj
COPY . .
RUN dotnet publish Services/Monitor/MultiShop.Monitor/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","MultiShop.Monitor.dll"]

 


