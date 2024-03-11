using Dal;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IUserBll
    {
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> GetById(int id);
        Task<UserDTO> GetByMailPass(string mail, string pass);
        Task<List<TripsDTO>> GetAllTrips(int userId);
        Task<bool> Add(UserDTO u);
        Task<int> add(UserDTO u);
        Task<bool> Delete(string mail,string pass);
        Task<bool> DeleteById(int id);
        Task<bool> Update(UserDTO u);
    }
}
