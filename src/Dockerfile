# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build

WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY aspnetapi/*.csproj ./aspnetapi/
RUN dotnet restore

# copy everything else and build app
COPY aspnetapi/. ./aspnetapi/
WORKDIR /source/aspnetapi
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime

# A privileged port is some port number less than 1024.
# If no port is mentioned, the app will be exposed at port 80 (which is a privileged port)
# K8s platform like OpenShift will not allow the app to run on port 80, so exposed at port 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Add bash
RUN apk update && apk add bash
# Add curl
RUN apk --no-cache add curl
# Copy the image built in previously
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "aspnetapi.dll"]
