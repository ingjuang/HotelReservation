# Utiliza la imagen base de .NET SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia los archivos de proyecto y restaura las dependencias
COPY ["HotelReservation.API/HotelReservation.API.csproj", "HotelReservation.API/"]
COPY ["HotelReservation.Application/HotelReservation.Application.csproj", "HotelReservation.Application/"]
COPY ["HotelReservation.Domain/HotelReservation.Domain.csproj", "HotelReservation.Domain/"]
COPY ["HotelReservation.Infraestructure/HotelReservation.Infraestructure.csproj", "HotelReservation.Infraestructure/"]
RUN dotnet restore "HotelReservation.API/HotelReservation.API.csproj"

# Copia el resto de los archivos y compila la aplicación
COPY . .
WORKDIR "/src/HotelReservation.API"
RUN dotnet publish -c Release -o /app/publish

# Utiliza la imagen base de .NET Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Expone el puerto en el que la aplicación escuchará
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "HotelReservation.API.dll"]