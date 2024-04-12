#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Appointify.Application/Appointify.Application.csproj", "Appointify.Application/"]
COPY ["Appointify.Domain/Appointify.Domain.csproj", "Appointify.Domain/"]
RUN dotnet restore "Appointify.Application/Appointify.Application.csproj"
COPY . .
WORKDIR "/src/Appointify.Application"
RUN dotnet build "Appointify.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Appointify.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Appointify.Application.dll"]