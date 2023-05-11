# Base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy the csproj files
COPY *.csproj ./

# Restore NuGet packages
RUN dotnet restore

# Copy the project files
COPY . .

# Build the app
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 80 for HTTP traffic
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "Api.dll"]