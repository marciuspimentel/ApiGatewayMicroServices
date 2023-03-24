FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["MF.Example.Auth/MF.Example.Auth.csproj", "MF.Example.Auth/"]
RUN dotnet restore "MF.Example.Auth/MF.Example.Auth.csproj"
COPY . .
WORKDIR "/src/MF.Example.Auth"
RUN dotnet build "MF.Example.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MF.Example.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MF.Example.Auth.dll"]