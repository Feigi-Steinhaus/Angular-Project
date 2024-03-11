using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Functions
{
    public class UsersDal : IUsersDal
    {

        MyTripsContext Context;
        /*
        public UsersDal(MyTripsContext db)
        {
            this.Context = db;
        }*/
     
        public UsersDal()
        {
            MyTripsContext context = new MyTripsContext();
            this.Context = context;
        }

        //--get
        public async Task<List<User>> GetAll()
        {
            return await Context.Users.ToListAsync();
        }

        //--get
        public async Task<User> GetById(int id)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.UserCode == id);
        }

        //--get
        public async Task<User> GetByMailPass(string mail, string pass)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.LoginPassword == pass && x.Email == mail);
        }

        //--get
        public async Task<List<Trip>> GetAllTrips(int userId)
        {
            List<Trip> lt = new List<Trip>();
            var data =await Context.BookingPlaces.Where(x => x.UserCode == userId).ToListAsync();
            foreach (var trip in data)
            {
                lt.Add(await Context.Trips.FirstOrDefaultAsync(x => x.TripCode == trip.TripCode));
            }
            return lt;
        }

        //--add
        public async Task<bool> Add(User u)
        {
            try
            {
                await Context.Users.AddAsync(u);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //--add
        public async Task<int> add(User u)
        {
            try
            {
                await Context.Users.AddAsync(u);
                await Context.SaveChangesAsync();
                return u.UserCode;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        //--delete
        public async Task<bool> Delete(string mail, string pass)
        {
            try
            {
                //MyTripsContext Context = new MyTripsContext();
                //מבצע מחיקה רק אם אין לו טיולים
                var data =await Context.Users.FirstOrDefaultAsync(x => x.LoginPassword == pass && x.Email == mail);
                if (data == null)
                {
                    return false;
                }
                var search =await Context.BookingPlaces.FirstOrDefaultAsync(x => x.UserCode == data.UserCode);
                if (search != null)
                    return false;
                Context.Users.Remove(data);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //--delete
        public async Task<bool> DeleteById(int id)
        {
            try
            {
                //MyTripsContext Context = new MyTripsContext();
                //מבצע מחיקה רק אם אין לו טיולים עתידיים
                var data =await Context.Users.FirstOrDefaultAsync(x => x.UserCode == id);
                if (data == null)
                {
                    return false;
                }
                var search =await Context.BookingPlaces.FirstOrDefaultAsync(x => x.UserCode == data.UserCode && x.TripCodeNavigation.TripDate>new DateTime()) ;
                if (search != null)
                    return false;
                Context.Users.Remove(data);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //--update
        public async Task<bool> Update(User u)
        {
            try
            {
                var data =await Context.Users.FirstOrDefaultAsync(x => x.UserCode == u.UserCode);
                if (data != null)
                {
                    data.FirstAidCertificate = u.FirstAidCertificate;
                    data.Name = u.Name;
                    data.Family = u.Family;
                    data.LoginPassword = u.LoginPassword;
                    data.Email = u.Email;
                    data.Phone = u.Phone;
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
