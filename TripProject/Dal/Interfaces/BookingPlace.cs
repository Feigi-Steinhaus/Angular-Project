using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IBookingPlaceDal
    {
        Task<List<BookingPlace>> GetAll();
        Task<List<BookingPlace>> GetById(int id);
        Task<List<BookingPlace>> GetByTrip(int id);
        Task<BookingPlace> GetByCode(int code);
        Task<bool> Add(BookingPlace bp);
        Task<int> add(BookingPlace bp);
        Task<bool> Delete(int bookCode);
        Task<bool> Update(BookingPlace b);
    }
}
