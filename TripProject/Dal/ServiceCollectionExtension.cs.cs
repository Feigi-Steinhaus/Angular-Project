using Dal.Functions;
using Dal.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public static class ServiceCollectionExtension
    {   
        
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITripDal,TripDal>();
            services.AddScoped<ITripTypeDal, TripTypeDal>();
            services.AddScoped<IBookingPlaceDal, BookingPlaceDal>();
            services.AddScoped<IUsersDal, UsersDal>();

            //services.AddSingleton<IContext, MyTripsContext>();
        }

    }
}
