FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG VERSION

WORKDIR /src
COPY ["HelloWorldWebApp/HelloWorldWebApp.csproj", "."]
RUN dotnet restore "./HelloWorldWebApp.csproj"
COPY "HelloWorldWebApp/" .
COPY "stylecop.json" ..
WORKDIR "/src/."
RUN dotnet build "HelloWorldWebApp.csproj" -c Release -o /app/build /p:AssemblyVersion=${VERSION}

FROM build AS publish
ARG VERSION

RUN dotnet publish "HelloWorldWebApp.csproj" -c Release -o /app/publish /p:AssemblyVersion=${VERSION}

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloWorldWebApp.dll"]