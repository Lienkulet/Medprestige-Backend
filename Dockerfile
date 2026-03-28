FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY MedPrestige.Models/MedPrestige.Models.csproj MedPrestige.Models/
COPY MedPrestige.DAL/MedPrestige.DAL.csproj MedPrestige.DAL/
COPY MedPrestige.BLL/MedPrestige.BLL.csproj MedPrestige.BLL/
COPY MedPrestige.UI/MedPrestige.UI.csproj MedPrestige.UI/

RUN dotnet restore MedPrestige.UI/MedPrestige.UI.csproj

COPY . .

RUN dotnet publish MedPrestige.UI/MedPrestige.UI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "MedPrestige.UI.dll"]
