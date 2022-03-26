FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LoginService.csproj", "LoginService/"]
RUN dotnet restore "LoginService/LoginService.csproj"
COPY . .
WORKDIR "/src/LoginService"
RUN dotnet build "LoginService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoginService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoginService.dll"]
