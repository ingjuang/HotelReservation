FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["HotelReservation.API/HotelReservation.API.csproj", "HotelReservation.API/"]
COPY ["HotelReservation.Application/HotelReservation.Application.csproj", "HotelReservation.Application/"]
COPY ["HotelReservation.Domain/HotelReservation.Domain.csproj", "HotelReservation.Domain/"]
COPY ["HotelReservation.Infraestructure/HotelReservation.Infraestructure.csproj", "HotelReservation.Infraestructure/"]
RUN dotnet restore "HotelReservation.API/HotelReservation.API.csproj"

COPY . .
WORKDIR "/src/HotelReservation.API"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "HotelReservation.API.dll"]