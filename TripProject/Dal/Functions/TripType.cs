using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Dal.Functions
{
    public class TripTypeDal:ITripTypeDal
    {
        
        MyTripsContext Context;

        /*
        public TripTypeDal(MyTripsContext db)
        {
            this.Context = db;
        }*/

        public TripTypeDal()
        {
            MyTripsContext db=new MyTripsContext();
            this.Context = db;
        }

        //--get
        public async Task<List<TripType>> GetAll()
        {
            return await Context.TripTypes.ToListAsync();
        }

        //--get
        public async Task<TripType> GetByCode(int code)
        {
            return await Context.TripTypes.FirstOrDefaultAsync(x => x.TypeCode == code);
        }

        //--add
        public async Task<int> Add(string typeName)
        {
            TripType t = new TripType();
            t.TypeName = typeName;
            var data =await Context.TripTypes.FirstOrDefaultAsync(x => x.TypeName == typeName);
            if (data != null)
                return -1;
            await Context.TripTypes.AddAsync(t);
            await Context.SaveChangesAsync();
            data =await Context.TripTypes.FirstOrDefaultAsync(x => x.TypeName == typeName);
            return data.TypeCode;
        }

        //--add
        public async Task<bool> Add(TripType t)
        {
            try
            {
                await Context.TripTypes.AddAsync(t);
                await Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //--add
        public async Task<int> add(TripType t)
        {
            try
            {
                await Context.TripTypes.AddAsync(t);
                await Context.SaveChangesAsync();
                return t.TypeCode;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        //--delete
        public async Task<bool> Delete(int code)
        {
            try
            {
                var data = await Context.TripTypes.FirstOrDefaultAsync(x => x.TypeCode == code);
                if (data == null)
                {
                    return false;
                }
                Context.TripTypes.Remove(data);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
