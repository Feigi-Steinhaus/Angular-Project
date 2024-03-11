using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Functions
{
    public class TripDal:ITripDal
    {
        
        MyTripsContext Context;
        /*
        public TripDal(MyTripsContext db)
        { 
            this.Context = db; 
        }
        */
        public TripDal()
        {
            MyTripsContext context = new MyTripsContext();
            this.Context = context;
        }

        //--get
        public async Task<List<Trip>> GetAll()
        {
            return await Context.Trips.Include(t=> t.TypeCodeNavigation).ToListAsync();


        }

        //--get
        public async Task<Trip> GetByCode(int code)
        {
            var data =await Context.Trips.Include(t => t.TypeCodeNavigation)
                .FirstOrDefaultAsync(x => x.TripCode == code);
            return data;
        }

        //--add
        public async Task<bool> Add(Trip trip)
        {
            try
            {
                await Context.Trips.AddAsync(trip);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //--add
        public async Task<int> add(Trip trip)
        {
            try
            {
                await Context.Trips.AddAsync(trip);
                await Context.SaveChangesAsync();
                return trip.TripCode;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        //--delete
        public async Task<bool> Delete(int tripCode)
        {
            try
            {
                var data =await Context.Trips.FirstOrDefaultAsync(x => x.TripCode == tripCode);
                if (data == null)
                {
                    return false;
                }
                Context.Trips.Remove(data);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //--update
        public async Task<bool> Update(Trip trip)
        {
            try
            {
                if (trip.TripDate > new DateTime())
                {
                    var data=await Context.Trips.FirstOrDefaultAsync(x=>x.TripCode== trip.TripCode);
                    if(data != null)
                    {
                        data.AvailablePlaces = trip.AvailablePlaces;
                        await Context.SaveChangesAsync();
                        return true;
                    }  
                }
                return false;            
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //--update
        public async Task<bool> UpdatePlaces(int code,int count)
        {
            try
            {
                var data =await Context.Trips.FirstOrDefaultAsync(x => x.TripCode == code);
                if (data!=null)
                {
                    if (data.TripDate > new DateTime())
                    {
                        data.AvailablePlaces = count;
                    }
                }
                //Context.Trips.Update(data);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<BookingPlace>> GetInvitesToTrip(int tripCode)
        {
            //מחזיר רשימת הזמנות לטיול כולל שם המזמין
            return await Context.BookingPlaces.Where(x=>x.TripCode==tripCode).ToListAsync();
        }


    }
}
