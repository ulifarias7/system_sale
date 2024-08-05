FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "APIstokeo.sln"
COPY . .
RUN dotnet build "APIstokeo.sln" -c Release -o /app/build    #Release: asegura que la aplicación esté optimizada para el rendimiento y lista para su despliegue 

FROM build AS publish
RUN dotnet publish "APIstokeo.sln" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SistemaStokeo.API.dll"]    


#app
 #src
 #build
 #pushish 