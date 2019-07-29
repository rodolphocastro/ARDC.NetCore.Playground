FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ARDC.NetCore.Playground.API/ARDC.NetCore.Playground.API.csproj", "ARDC.NetCore.Playground.API/"]
COPY ["ARDC.NetCore.Playground.Persistance.Mock/ARDC.NetCore.Playground.Persistance.Mock.csproj", "ARDC.NetCore.Playground.Persistance.Mock/"]
COPY ["ARDC.NetCore.Playground.Domain/ARDC.NetCore.Playground.Domain.csproj", "ARDC.NetCore.Playground.Domain/"]
COPY ["ARDC.NetCore.Playground.API.ViewModels/ARDC.NetCore.Playground.API.ViewModels.csproj", "ARDC.NetCore.Playground.API.ViewModels/"]
COPY ["ARDC.NetCore.Playground.Persistance.Memory/ARDC.NetCore.Playground.Persistance.Memory.csproj", "ARDC.NetCore.Playground.Persistance.Memory/"]
RUN dotnet restore "ARDC.NetCore.Playground.API/ARDC.NetCore.Playground.API.csproj"
COPY . .
WORKDIR "/src/ARDC.NetCore.Playground.API"
RUN dotnet build "ARDC.NetCore.Playground.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ARDC.NetCore.Playground.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ARDC.NetCore.Playground.API.dll"]