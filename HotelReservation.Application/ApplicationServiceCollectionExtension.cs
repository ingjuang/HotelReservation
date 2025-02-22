using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;


namespace HotelReservation.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register services
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IGuestService, GuestService>();

            // Register MediatR
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            return services;
        }
    }
}
