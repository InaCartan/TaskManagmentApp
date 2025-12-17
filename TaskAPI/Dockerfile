# Build stage (Linux images)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG BUILD_CONFIGURATION=Release

# copy project files and restore
COPY *.sln ./
COPY TaskAPI/TaskAPI.csproj TaskAPI/
COPY BusinessLogic/BusinessLogic.csproj BusinessLogic/
COPY DataAccess/DataAccess.csproj DataAccess/
COPY DTO/DTO.csproj DTO/
RUN dotnet restore "./TaskAPI/TaskAPI.csproj"

# copy rest and build/publish
COPY . .
WORKDIR /src/TaskAPI
RUN dotnet publish "./TaskAPI.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

# Runtime stage (Linux)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "TaskAPI.dll"]