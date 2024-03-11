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

namespace Bll.Functions
{
    public class TripBll : ITripBll
    {
        ITripDal dal;
        IUsersDal users;
        IBookingPlaceDal booking;
        IMapper mapper;

        public TripBll(ITripDal dal, IBookingPlaceDal booking, IUsersDal users)
        {
            this.dal = dal;
            this.users = users;
            this.booking = booking;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperTrips>();
            });

            mapper = config.CreateMapper();
        }

        public async Task<List<TripsDTO>> GetAll()
        {
            var answer =await dal.GetAll();
            var answerDTO=mapper.Map<List<TripsDTO>>(answer);
            foreach(var trip in answerDTO)
            {
                trip.IsMedicNeeded = isMedic(trip.TripCode);
            }
            return answerDTO;
        }

        public async Task<TripsDTO> GetByCode(int code)
        {
            var answer =await dal.GetByCode(code);
            var answerDTO=mapper.Map<TripsDTO>(answer);
            answerDTO.IsMedicNeeded=isMedic(answer.TripCode);
            return answerDTO;
        }

        public async Task<bool> Add(TripsDTO t)
        {
            try
            {
                var newTrip = mapper.Map<Trip>(t);
                if (ValidationTests.IsValidHours(newTrip.TripDurationHours) &&
                    ValidationTests.IsValidplaces(newTrip.AvailablePlaces) &&
                    ValidationTests.IsValidPrice(newTrip.Price) &&
                    ValidationTests.IsValidDate(newTrip.TripDate))
                    return await dal.Add(newTrip);
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> add(TripsDTO t)
        {
            var newTrip = mapper.Map<Trip>(t);
            if (ValidationTests.IsValidHours(newTrip.TripDurationHours) &&
                ValidationTests.IsValidplaces(newTrip.AvailablePlaces) &&
                ValidationTests.IsValidPrice(newTrip.Price) &&
                ValidationTests.IsValidDate(newTrip.TripDate))
                return await dal.add(newTrip);
            return -1;
        }

        public async Task<bool> Update(TripsDTO t)
        {
            try
            {
                var newTrip = mapper.Map<Trip>(t);
                if (ValidationTests.IsValidHours(newTrip.TripDurationHours) &&
                    ValidationTests.IsValidplaces(newTrip.AvailablePlaces) &&
                    ValidationTests.IsValidPrice(newTrip.Price) &&
                    ValidationTests.IsValidDate(newTrip.TripDate))
                    return await dal.Update(newTrip);
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePlaces(int code, int count)
        {
            return await dal.UpdatePlaces(code, count);
        }
        public async Task<bool> Delete(int t)
        {
            return await dal.Delete(t);
        }
        //------
        public async Task<List<BookingPlacesDTO>> GetInvitesToTrip(int code)
        {
            var answer =await dal.GetInvitesToTrip(code);
            return mapper.Map<List<BookingPlacesDTO>>(answer);
        }

        
        //פונקציית חישוב האם יש צורך בחובש
        public bool isMedic(int code)
        {
            var listUsers =users.GetAll();
            //סינון רשימת ההזמנות לטיול זה בלבד
            var listBooking = booking.GetAll().Result.Where(x => x.TripCode == code).ToList();
            //מעבר על רשימת הלקוחות ובדיקה האם יש מישהו שיש לו תעודה
            foreach(var book in listBooking)
            {
                var user = listUsers.Result.FirstOrDefault(x=>x.UserCode==book.UserCode);
                if (user.FirstAidCertificate == true)
                    return true;
            }
            return false;
        }
    }
}
