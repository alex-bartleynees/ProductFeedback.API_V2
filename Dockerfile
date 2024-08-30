FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-amd64 AS base
WORKDIR /
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-amd64 AS build
WORKDIR /
COPY ["MinimalApi/MinimalApi.csproj", "MinimalApi/"]
RUN dotnet restore "MinimalApi/MinimalApi.csproj"
COPY . .
RUN dotnet build "MinimalApi/MinimalApi.csproj" -c Release -o /build

FROM build AS publish
RUN dotnet publish "MinimalApi/MinimalApi.csproj" -c Release -o /publish /p:UseAppHost=false

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:5185
EXPOSE 5185

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-amd64 AS final
WORKDIR /MinimalApi
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "MinimalApi.dll"]
