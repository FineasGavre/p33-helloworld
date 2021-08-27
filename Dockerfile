FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

ARG VERSION

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HelloWorldWebApp/HelloWorldWebApp.csproj", "."]
RUN dotnet restore "./HelloWorldWebApp.csproj"
COPY "HelloWorldWebApp/" .
COPY "stylecop.json" ..
WORKDIR "/src/."
RUN dotnet build "HelloWorldWebApp.csproj" -c Release -o /app/build /p:AssemblyVersion=$VERSION

FROM build AS publish
RUN dotnet publish "HelloWorldWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloWorldWebApp.dll"]