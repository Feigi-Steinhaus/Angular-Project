using Bll.Functions;
using Bll.Interfaces;
using Dal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Services
{
    public static class ServiceCollectionExtension
    {
       public static void AddServices(this IServiceCollection services)
        {
            //dal זימון של פונקציית ההרחבה מה
            services.AddRepositories();
            //טיפול בהזרקות של שכבה זו
            services.AddScoped<ITripBll, TripBll>();
            services.AddScoped<ITripTypeBll, TripTypeBll>();
            services.AddScoped<IBookingPlaceBll, BookingPlaceBll>();
            services.AddScoped<IUserBll, UserBll>();

            //services.AddSingleton<IContext, MyTripsContext>();

            services.AddAutoMapper(typeof(DTO.MapperTrips));
        }

    }
}
