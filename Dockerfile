# Use the official ASP.NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Use the .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the csproj and restore dependencies
COPY BackendApi.csproj ./
RUN dotnet restore ./BackendApi.csproj

# Now copy everything else
COPY . ./

# Build and publish the specific project
RUN dotnet publish ./BackendApi.csproj -c Release -o /app

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=build /app .

# Expose port 3000
EXPOSE 3000

ENTRYPOINT ["dotnet", "BackendApi.dll"]
