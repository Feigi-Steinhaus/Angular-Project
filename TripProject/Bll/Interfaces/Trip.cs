using Dal;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface ITripBll
    {
        Task<List<TripsDTO>> GetAll();
        Task<TripsDTO> GetByCode(int code);
        Task<bool> Add(TripsDTO t);
        Task<int> add(TripsDTO t);
        Task<bool> Update(TripsDTO t);
        Task<bool> UpdatePlaces(int code, int count);
        Task<bool> Delete(int t);
        Task<List<BookingPlacesDTO>> GetInvitesToTrip(int code);

    }
}
