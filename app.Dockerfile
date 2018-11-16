# FROM microsoft/aspnetcore-build:lts
# COPY ../../. /app
# WORKDIR /app
# RUN ["dotnet", "restore"]
# RUN ["dotnet", "build"]
# EXPOSE 80/tcp
# RUN chmod +x ./entrypoint.sh
# CMD /bin/bash ./entrypoint.sh

FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
COPY appsettings.json ./
RUN dotnet restore

# Copy everything else and build
COPY . ./

RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY entrypoint.sh . 
COPY --from=build-env /app/out .
EXPOSE 80
# CMD /bin/bash ./entrypoint.sh
# ENTRYPOINT ["/bin/bash", "./entrypoint.sh"]
ENTRYPOINT ["dotnet", "docker_app_compose.dll"]  