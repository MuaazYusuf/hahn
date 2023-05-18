FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy all the layers' csproj files into respective folders
COPY ["./Application/Application.csproj", "src/Application/"]
COPY ["./Domain/Domain.csproj", "src/Domain/"]
COPY ["./Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["./Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["./Api/Api.csproj", "src/Api/"]
COPY ["./Test/Test.csproj", "src/Test/"]

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "src/Api/Api.csproj"

COPY . .

# run build over the API project
WORKDIR "/src/Api/"
RUN dotnet build -c Release -o /app/build

FROM build AS test
WORKDIR /src/Test
CMD ["dotnet", "test", "--logger:trx"]

# run the unit tests
FROM build AS test
WORKDIR /src/Test
RUN dotnet test --logger:trx

# run publish over the API project
FROM build AS publish
WORKDIR "/src/Api/"
RUN dotnet publish -c Release -o /app/publish
EXPOSE 80

FROM base AS runtime
WORKDIR /app

COPY --from=publish /app/publish .
RUN ls -l
ENTRYPOINT [ "dotnet", "Api.dll" ]