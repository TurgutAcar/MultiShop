FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80
COPY ApiGateway/MultiShop.OcelotGateway/*.csproj ApiGateway/MultiShop.OcelotGateway/
RUN dotnet restore ApiGateway/MultiShop.OcelotGateway/*.csproj
COPY . .
RUN dotnet publish ApiGateway/MultiShop.OcelotGateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","MultiShop.OcelotGateway.dll"]

 


