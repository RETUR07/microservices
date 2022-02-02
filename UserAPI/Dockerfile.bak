#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UserAPI/UserAPI.csproj", "UserAPI/"]
COPY ["UserAPI.Message.Recieve/UserAPI.Messaging.Recieve.csproj", "UserAPI.Message.Recieve/"]
COPY ["UserAPI.Application/UserAPI.Application.csproj", "UserAPI.Application/"]
COPY ["UserAPI.Messaging.Send/UserAPI.Messaging.Send.csproj", "UserAPI.Messaging.Send/"]
COPY ["Entities/UserAPI.Entities.csproj", "Entities/"]
COPY ["UserAPI.Repository/UserAPI.Repository.csproj", "UserAPI.Repository/"]
RUN dotnet restore "UserAPI/UserAPI.csproj"
COPY . .
WORKDIR "/src/UserAPI"
RUN dotnet build "UserAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserAPI.dll"]