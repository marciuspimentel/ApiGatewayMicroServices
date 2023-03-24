#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MF.Example.Geral/MF.Example.Geral.csproj", "MF.Example.Geral/"]
RUN dotnet restore "MF.Example.Geral/MF.Example.Geral.csproj"
COPY . .
WORKDIR "/src/MF.Example.Geral"
RUN dotnet build "MF.Example.Geral.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MF.Example.Geral.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MF.Example.Geral.dll"]