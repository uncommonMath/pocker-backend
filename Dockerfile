# Dockerfile

FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

CMD dotnet out/pocker-backend-core.dll "$PORT"