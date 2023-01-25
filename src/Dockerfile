#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TickdMeterReading.API/TickdMeterReading.API.csproj", "TickdMeterReading.API/"]
RUN dotnet restore "TickdMeterReading.API/TickdMeterReading.API.csproj"
COPY . .
WORKDIR "/src/TickdMeterReading.API"
RUN dotnet build "TickdMeterReading.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TickdMeterReading.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TickdMeterReading.API.dll"]