using Dal;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface ITripTypeBll
    {
        Task<List<TripTypeDTO>> GetAll();
        Task<TripTypeDTO> GetByCode(int code);
        Task<bool> Add(TripTypeDTO tt);
        Task<int> Add(string type);
        Task<int> add(TripTypeDTO t);
        Task<bool> Delete(int code);
    }
}
