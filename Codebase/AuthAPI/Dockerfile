# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code and build the application
COPY . ./
RUN dotnet build -c Release --no-restore

# Publish the application
RUN dotnet publish -c Release -o out --no-restore

# Use a lighter runtime image as the base image for final deployment
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production
ENV Database__ConnectionString=
ENV Database__Name=
ENV JWT__Secret=
ENV JWT__Issuer=
ENV JWT__Audience=
ENV CrossServiceCommunicationAuthentication__Secret=

# Copy the published output from the build image
COPY --from=build /app/out ./

# Expose the port that the application listens on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "cliph.dll"]
