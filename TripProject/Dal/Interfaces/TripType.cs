using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface ITripTypeDal
    {
        Task<List<TripType>> GetAll();
        Task<TripType> GetByCode(int code);
        Task<bool> Add(TripType t);
        Task<int> Add(string typeName);
        Task<int> add(TripType t);
        Task<bool> Delete(int code);     

    }
}
