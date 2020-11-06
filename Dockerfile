# Dockerfile

FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY . .
RUN dotnet build pocker-backend-core/pocker-backend-core.csproj -c Release
CMD dotnet pocker-backend-core/bin/Release/netcoreapp3.1/pocker-backend-core.dll "$PORT"