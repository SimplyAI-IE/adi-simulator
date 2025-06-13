# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore only server project
COPY ["AdiExamSimulator.Server.csproj", "./"]
RUN dotnet restore "AdiExamSimulator.Server.csproj"

# Copy all source files from server folder
COPY . ./

# Publish the server app
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "AdiExamSimulator.Server.dll"]
