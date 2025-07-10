#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER 0
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true

ARG NUGET_URL
ARG NUGET_TOKEN
ARG NUGET_USR

WORKDIR /src
COPY ["NDD.Api.Mapeamento.API/NDD.Api.Mapeamento.API.csproj", "NDD.Api.Mapeamento.API/"]
COPY ["NDD.Api.Mapeamento.Application/NDD.Api.Mapeamento.Application.csproj", "NDD.Api.Mapeamento.Application/"]
COPY ["NDD.Api.Mapeamento.Base/NDD.Api.Mapeamento.Base.csproj", "NDD.Api.Mapeamento.Base/"]
COPY ["NDD.Api.Mapeamento.Infra.Data/NDD.Api.Mapeamento.Infra.Data.csproj", "NDD.Api.Mapeamento.Infra.Data/"]
COPY ["NDD.Api.Mapeamento.Domain/NDD.Api.Mapeamento.Domain.csproj", "NDD.Api.Mapeamento.Domain/"]

ADD Antivirus.crt /usr/local/share/ca-certificates/Antivirus.crt
ADD Antivirus2.crt /usr/local/share/ca-certificates/Antivirus2.crt
RUN update-ca-certificates

RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done

COPY nuget.config .

RUN dotnet nuget remove source central.nuget
RUN dotnet nuget remove source labs.nuget
#RUN dotnet nuget remove source nuget.org

RUN dotnet nuget add source $NUGET_URL -n central.nuget -u $NUGET_USR -p $NUGET_TOKEN --store-password-in-clear-text --valid-authentication-types "basic"
RUN dotnet restore "NDD.Api.Mapeamento.API/NDD.Api.Mapeamento.API.csproj"

COPY . .

WORKDIR /src/NDD.Api.Mapeamento.API
FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "NDD.Api.Mapeamento.API.dll"]