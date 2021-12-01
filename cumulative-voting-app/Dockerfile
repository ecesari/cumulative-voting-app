#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["cumulative-voting-app/cumulative-voting-app.csproj", "cumulative-voting-app/"]
RUN dotnet restore "cumulative-voting-app/cumulative-voting-app.csproj"
COPY . .
WORKDIR "/src/cumulative-voting-app"
RUN dotnet build "cumulative-voting-app.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cumulative-voting-app.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cumulative-voting-app.dll"]