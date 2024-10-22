FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Appointify.Api/Appointify.Api.csproj", "Appointify.Api/"]
COPY ["Appointify.Application/Appointify.Application.csproj", "Appointify.Application/"]
COPY ["Appointify.Infrastructure/Appointify.Infrastructure.csproj", "Appointify.Infrastructure/"]
COPY ["Appointify.Domain/Appointify.Domain.csproj", "Appointify.Domain/"]
RUN dotnet restore "Appointify.Api/Appointify.Api.csproj"
COPY . .
WORKDIR "/src/Appointify.Api"
RUN dotnet build "Appointify.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Appointify.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Appointify.Api.dll"]