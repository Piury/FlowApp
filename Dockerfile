FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /src

COPY ["FlowAPI/FlowAPI.csproj", "FlowAPI/"]

RUN dotnet restore "FlowAPI/FlowAPI.csproj"

COPY . .

WORKDIR "/src/FlowAPI"

RUN dotnet build "FlowAPI.csproj" -c Release -o /app/build

FROM build-env AS publish

RUN dotnet publish "FlowAPI.csproj" -c Release -o /app/publish

FROM build-env AS final

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 5000

WORKDIR /app/FlowAPIcode
COPY . .
# ENV DOTNET_CLI_TOOL_CHAIN=Bundled
# RUN dotnet tool install --global dotnet-ef
# ENV PATH="$PATH:/root/.dotnet/tools"

# RUN mkdir -p /obj
# WORKDIR /app/FlowAPIcode/FlowAPI
# RUN ls -l
# RUN dotnet ef database update --project "FlowAPI.csproj" -c AppDbContext

WORKDIR /app
RUN chmod +x /app/FlowAPIcode/FlowAPI/generate_migrations.sh
# RUN sleep 30 && /app/FlowAPIcode/FlowAPI/generate_migrations.sh
ENTRYPOINT ["dotnet", "FlowAPI.dll"]





# # # Base image for building the application
# # FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# # # Working directory for building the application
# # WORKDIR /FlowAPI

# # # Copy all project files from the current directory to /FlowAPI within the container
# # COPY . .

# # # Restore dependencies using dotnet restore command
# # RUN dotnet restore

# # # Publish the application in Release configuration and copy the output to /FlowApp/FlowAPI/bin/Release/net8.0
# # RUN dotnet publish -c Release -o /FlowApp/FlowAPI/bin/Release/net8.0

# # # Base image for running the application
# # FROM mcr.microsoft.com/dotnet/aspnet:8.0

# # # Working directory for running the application
# # WORKDIR /FlowAPI

# # # Copy the published application from the build stage to the running stage
# # COPY --from=build-env /FlowApp/FlowAPI/bin/Release/net8.0 .


# # # Expose port 5000 for incoming traffic
# # EXPOSE 5000

# # # Entrypoint command to execute the application
# # ENTRYPOINT ["dotnet", "/FlowApp/FlowAPI/bin/Release/net8.0/FlowAPI.dll"]