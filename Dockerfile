FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish

WORKDIR /src
COPY ConfigureKestrelCallOrder.csproj .
COPY Program.cs .

WORKDIR /src
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0.9 AS final
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "ConfigureKestrelCallOrder.dll"]
