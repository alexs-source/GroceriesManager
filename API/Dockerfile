FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY ./app .
COPY *.db .
ENTRYPOINT ["dotnet", "API.dll"]