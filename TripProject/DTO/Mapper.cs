using AutoMapper;
using Dal;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DTO
{
    public class MapperTrips : Profile
    {
        public MapperTrips()
        {
            //TripType
            CreateMap<Dal.TripType, TripTypeDTO>().ReverseMap();

            //Users
            CreateMap<UserDTO, Dal.User>()
                  //.ForMember(dest => dest.UserCode, opt => opt.Ignore())
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                  .ForMember(dest => dest.Family, opt => opt.MapFrom(src => src.LastName))
                  .ForMember(dest => dest.LoginPassword, opt => opt.MapFrom(src => src.Password));
            CreateMap<Dal.User, UserDTO>()
                  .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                  .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Family))
                  .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.LoginPassword));

            //Trips
            CreateMap<TripsDTO, Dal.Trip>()
                 .ForMember(dest => dest.TripCode, opt => opt.Ignore());

            CreateMap<Dal.Trip, TripsDTO>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.TypeCodeNavigation.TypeName));

            //BookingPlace
            CreateMap<BookingPlacesDTO, Dal.BookingPlace>();
               // .ForMember(dest => dest.BookingCode, opt => opt.Ignore());

            CreateMap<Dal.BookingPlace, BookingPlacesDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserCodeNavigation.Name + " " + src.UserCodeNavigation.Family))
                .ForMember(dest => dest.DestinationTrip, opt => opt.MapFrom(src => src.TripCodeNavigation.TripDestination))
                .ForMember(dest => dest.TripDate, opt => opt.MapFrom(src => src.TripCodeNavigation.TripDate));
        }
    }
}
