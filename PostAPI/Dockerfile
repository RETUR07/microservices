FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PostAPI/PostAPI.csproj", "PostAPI/"]
COPY ["PostAPI.Application/PostAPI.Application.csproj", "PostAPI.Application/"]
COPY ["PostAPI.Messaging.Send/PostAPI.Messaging.Send.csproj", "PostAPI.Messaging.Send/"]
COPY ["PostAPI.Entities/PostAPI.Entities.csproj", "PostAPI.Entities/"]
COPY ["PostAPI.Repository/PostAPI.Repository.csproj", "PostAPI.Repository/"]
COPY ["PostAPI.Messaging.Recieve/PostAPI.Messaging.Recieve.csproj", "PostAPI.Messaging.Recieve/"]
RUN dotnet restore "PostAPI/PostAPI.csproj"
COPY . .
WORKDIR "/src/PostAPI"
RUN dotnet build "PostAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostAPI.dll"]