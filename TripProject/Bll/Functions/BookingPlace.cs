using AutoMapper;
using Bll.Interfaces;
using Dal;
using Dal.Functions;
using Dal.Interfaces;
using DTO;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Bll.Functions
{
    public class BookingPlaceBll:IBookingPlaceBll
    {
        IBookingPlaceDal dal;
        IMapper mapper;

        
        public BookingPlaceBll(IBookingPlaceDal dal)
        {
            this.dal = dal;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperTrips>();
            });

            mapper = config.CreateMapper();
        }

        public async Task<List<BookingPlacesDTO>> GetAll()
        {
            var answer =await dal.GetAll();
            return mapper.Map<List<BookingPlacesDTO>>(answer);
        }

        public async Task<List<BookingPlacesDTO>> GetById(int id)
        {
            var answer =await dal.GetById(id);
            return mapper.Map<List<BookingPlacesDTO>>(answer);
        }

        public async Task<BookingPlacesDTO> GetByCode(int code)
        {
            var answer =await dal.GetByCode(code);
            return mapper.Map<BookingPlacesDTO>(answer);
        }

        public async Task<List<BookingPlacesDTO>> GetByTrip(int tripCode)
        {
            var answer =await dal.GetByTrip(tripCode);
            return mapper.Map<List<BookingPlacesDTO>>(answer);
        }

        //--post
        public async Task<bool> Add(BookingPlacesDTO b)
        {
            //עדכון תאריך ושעת הזמנה לזמן הנוכחי
            var newBooking = mapper.Map<BookingPlace>(b);
            newBooking.BookingTime = (TimeSpan?)DateTime.Now.TimeOfDay;
            newBooking.BookingDate = DateTime.Now;
            return await dal.Add(newBooking);
        }

        public async Task<int> add(BookingPlacesDTO b)
        {
            //עדכון תאריך ושעת הזמנה לזמן הנוכחי
            var newBooking = mapper.Map<BookingPlace>(b);
            newBooking.BookingTime = (TimeSpan?)DateTime.Now.TimeOfDay;
            newBooking.BookingDate =DateTime.Now;
            return await dal.add(newBooking);
        }

        //--delete
        public async Task<bool> Delete(int id)
        {
            return await dal.Delete(id);
        }

        //--update
        public async Task<bool> Update(BookingPlacesDTO b)
        {
            var newBooking = mapper.Map<BookingPlace>(b);
            return await dal.Update(newBooking);
        }
    }
}
