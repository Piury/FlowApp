#!/bin/bash

# Install dotnet-ef tool (if not already installed)
if ! command -v dotnet-ef &> /dev/null; then
  echo "Installing dotnet-ef tool..."
  dotnet tool install --global dotnet-ef
fi

# Generate migrations
# echo "Generating migrations..."
echo "Applying migrations..."
dotnet ef database update --project "FlowAPI.csproj" #-c AppDbContext

# Apply migrations (optional)
# dotnet ef database update -c <YourDbContextClass>  # Uncomment to apply migrations automatically

# Make the script executable
chmod +x generate_migrations.sh