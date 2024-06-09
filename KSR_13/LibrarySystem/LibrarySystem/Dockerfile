FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app



FROM microsoft/dotnet:2-2-aspnetcore-runtime
WORKDIR /app



ENTRYPOINT ["dotnet","Library.Web.dll"]