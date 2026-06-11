FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY WebApiSolution.sln .
COPY Data/Data.csproj Data/
COPY Domain/Domain.csproj Domain/
COPY Services/Services.csproj Services/
COPY WebApiSolution/WebApiSolution.csproj WebApiSolution/

RUN dotnet restore WebApiSolution/WebApiSolution.csproj

COPY Data/ Data/
COPY Domain/ Domain/
COPY Services/ Services/
COPY WebApiSolution/ WebApiSolution/

RUN dotnet publish WebApiSolution/WebApiSolution.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "WebApiSolution.dll"]
