using Dal;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IBookingPlaceBll
    {
        Task<List<BookingPlacesDTO>> GetAll();
        Task<List<BookingPlacesDTO>> GetById(int id);
        Task<BookingPlacesDTO> GetByCode(int code);
        Task<List<BookingPlacesDTO>> GetByTrip(int tripCode);
        Task<bool> Add(BookingPlacesDTO bp);
        Task<int> add(BookingPlacesDTO b);
        Task<bool> Delete(int id);
        Task<bool> Update(BookingPlacesDTO b);

    }
}
