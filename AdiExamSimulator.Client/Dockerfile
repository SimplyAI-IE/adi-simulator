# Stage 1: Build the Blazor WASM app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY AdiExamSimulator.Client/*.csproj ./ 
RUN dotnet restore

# Copy all files and build release
COPY AdiExamSimulator.Client/. ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Serve the published files with Nginx
FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
