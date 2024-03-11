using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Dal.Functions
{
    public class BookingPlaceDal :IBookingPlaceDal
    {
        MyTripsContext Context;
        
        public BookingPlaceDal()
        {
            MyTripsContext context = new MyTripsContext();
            this.Context = context;
        }
        /*
        public BookingPlaceDal(MyTripsContext db)
        {
            this.Context = db;
        }
        */
        
        //--get
        public async Task<List<BookingPlace>> GetAll()
        {
            var data=await Context.BookingPlaces
                .Include(x=>x.TripCodeNavigation)
                //.ThenInclude(x=>x.TypeCodeNavigation)
                .Include(x=>x.UserCodeNavigation)
                 .ToListAsync();
            return data;
        }

        //--getByUserId
        public async Task<List<BookingPlace>> GetById(int id)
        {
            return await Context.BookingPlaces.Where(obj => obj.UserCode == id).ToListAsync();
        }

        //--getByTripCode
        public async Task<List<BookingPlace>> GetByTrip(int id)
        {
            return await Context.BookingPlaces.Where(obj => obj.TripCode == id).ToListAsync();
        }

        //--getByCode
        public async Task<BookingPlace> GetByCode(int code)
        {
            return await Context.BookingPlaces.FirstOrDefaultAsync(obj => obj.BookingCode == code);
        }

        //--add
        public async Task<bool> Add(BookingPlace newb)
        {
            try
            {
                await Context.BookingPlaces.AddAsync(newb);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //--add
        public async Task<int> add(BookingPlace newb)
        {
            try
            {
                await Context.BookingPlaces.AddAsync(newb);
                await Context.SaveChangesAsync();
                return newb.BookingCode;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        //--delete
        public async Task<bool> Delete(int id)
        {
            try
            {
                var data =await Context.BookingPlaces.FirstOrDefaultAsync(x => x.BookingCode == id);
                if (data == null)
                {
                    return false;
                }
                Context.BookingPlaces.Remove(data);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //--update
        public async Task<bool> Update(BookingPlace b)
        {
            try
            {
                var data = await Context.BookingPlaces.FirstOrDefaultAsync(x => x.BookingCode == b.BookingCode);
                if (data != null)
                {
                    data.NumOfPlaces = b.NumOfPlaces;
                    await Context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}