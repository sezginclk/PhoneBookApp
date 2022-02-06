#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PhoneBookApp/PhoneBookApp.csproj", "PhoneBookApp/"]
COPY ["PhoneBook.Service/PhoneBook.Service.csproj", "PhoneBook.Service/"]
COPY ["PhoneBook.Data/PhoneBook.Data.csproj", "PhoneBook.Data/"]
COPY ["PhoneBook.Data.Model/PhoneBook.Data.Model.csproj", "PhoneBook.Data.Model/"]
COPY ["PhoneBook.Core/PhoneBook.Core.csproj", "PhoneBook.Core/"]
RUN dotnet restore "PhoneBookApp/PhoneBookApp.csproj"
COPY . .
WORKDIR "/src/PhoneBookApp"
RUN dotnet build "PhoneBookApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhoneBookApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBookApp.dll"]