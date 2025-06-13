# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the server project files first (for faster caching)
COPY ["AdiExamSimulator.Server/AdiExamSimulator.Server.csproj", "AdiExamSimulator.Server/"]
RUN dotnet restore "AdiExamSimulator.Server/AdiExamSimulator.Server.csproj"

# Copy the rest of the source code
COPY . .

WORKDIR "/src/AdiExamSimulator.Server"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80 (default)
EXPOSE 80

ENTRYPOINT ["dotnet", "AdiExamSimulator.Server.dll"]
