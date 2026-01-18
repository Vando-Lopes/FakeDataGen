# ================================
# Build stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar solution
COPY FakeDataGen.slnx .

# Copiar csproj (respeitando sua estrutura REAL)
COPY Core/FakeDataGen.Core/FakeDataGen.Core.csproj Core/FakeDataGen.Core/
COPY Application/FakeDataGen.Application/FakeDataGen.Application.csproj Application/FakeDataGen.Application/
COPY Api/FakeDataGen.Api/FakeDataGen.Api.csproj Api/FakeDataGen.Api/

# Restore
RUN dotnet restore Api/FakeDataGen.Api/FakeDataGen.Api.csproj

# Copiar tudo
COPY . .

# Publish
RUN dotnet publish Api/FakeDataGen.Api/FakeDataGen.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ================================
# Runtime stage
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FakeDataGen.Api.dll"]
