using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IUsersDal
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByMailPass(string mail, string pass);
        Task<List<Trip>> GetAllTrips(int userId);
        Task<bool> Add(User user);
        Task<int> add(User u);
        Task<bool> Delete(string mail,string pass);
        Task<bool> DeleteById(int id);
        Task<bool> Update(User user); 
    }
}
