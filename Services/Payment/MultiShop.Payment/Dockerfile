FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/MultiShop.Shared/*.csproj Shared/MultiShop.Shared/
COPY Services/Payment/MultiShop.Payment/*.csproj Services/Payment/MultiShop.Payment/
RUN dotnet restore Services/Payment/MultiShop.Payment/*.csproj
COPY . .
RUN dotnet publish Services/Payment/MultiShop.Payment/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","MultiShop.Payment.dll"]

 


