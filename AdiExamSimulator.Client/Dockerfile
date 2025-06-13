# Stage 1: Build the Blazor WASM app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all client source files
COPY . ./

# Publish the client app
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Serve with Nginx
FROM nginx:alpine

# Copy published files from build stage
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
