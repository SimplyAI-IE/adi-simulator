# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["AdiExamSimulator.Server.csproj", "./"]
RUN dotnet restore "AdiExamSimulator.Server.csproj"

COPY . .

WORKDIR "/src"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "AdiExamSimulator.Server.dll"]
