FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

ARG PORTHTTP=25100
ENV PORTHTTP=${PORTHTTP}

ARG PORTHTTPS=25101
ENV PORTHTTPS=${PORTHTTPS}

ENV ASPNETCORE_HTTP_PORTS=${PORTHTTP}

ARG ASPNETCORE_ENVIRONMENT="Development"
ENV ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT}

#ENV ASPNETCORE_URLS=http://+:${PORTHTTP}
EXPOSE ${PORTHTTP} ${PORTHTTPS}

ENTRYPOINT ["dotnet", "Ploomes.API.dll"]