using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface ITripDal
    {
        Task<List<Trip>> GetAll();
        Task<Trip> GetByCode(int code);
        Task<bool> Add(Trip t);
        Task<int> add(Trip t);
        Task<bool> Delete(int tripCode);
        Task<bool> Update(Trip t);
        Task<bool> UpdatePlaces(int code, int count);
        Task<List<BookingPlace>> GetInvitesToTrip(int code);

    }
}
