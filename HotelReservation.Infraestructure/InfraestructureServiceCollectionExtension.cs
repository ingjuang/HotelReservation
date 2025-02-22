
using Microsoft.Extensions.Configuration;
using HotelReservation.Domain.Repositories;
using HotelReservation.Infraestructure.DBContext;
using HotelReservation.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HotelReservation.Infraestructure
{
    public static class InfraestructureServiceCollectionExtension
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register MongoDB
            var mongoDBSettings = configuration.GetSection("ConnectionStrings").Get<MongoDBSettings>();
            services.AddSingleton(mongoDBSettings);
            services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoDBSettings.ConnectionString));
            services.AddScoped<MongoDBContext>();
            // Register Repositories
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            return services;
        }
    }
}
