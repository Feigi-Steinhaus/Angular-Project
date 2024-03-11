using Bll.Functions;
using Bll.Interfaces;
using Dal;
using Dal.Functions;
using Dal.Interfaces;
using DTO.Classes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBll Bll;
        
        public UserController(IUserBll bll)
        {
            this.Bll = bll;
        }

        [HttpGet]
        public async Task<List<UserDTO>> GetAll()
        {
            return await Bll.GetAll();
        }

        [HttpGet]
        [Route("/{id:int}")]
        public async Task<UserDTO> GetById(int id)
        {
            return await Bll.GetById(id);
        }

        [HttpGet]
        [Route("GetByMailAndPass")]
        public async Task<UserDTO> GetByMailAndPass(string mail,string pass)
        {
            return await Bll.GetByMailPass(mail,pass);
        }

        [HttpGet]
        [Route("GetAllTrips/{userCode:int}")]
        public async Task<List<TripsDTO>> GetAllTrips(int userCode)
        {
            return await Bll.GetAllTrips(userCode);
        }

        [HttpPost]
        public async Task<bool> Add(UserDTO u)
        {
            return await Bll.Add(u);
        }
        [HttpPost]
        [Route("AddByInt")]
        public async Task<int> AddByInt(UserDTO u)
        {
            return await Bll.add(u);
        }

        [HttpDelete]
        public async Task<bool> Delete(string mail,string pass )
        {
            return await Bll.Delete(mail,pass);
        }


        [HttpDelete]
        [Route("/{id:int}")]
        public async Task<bool> DeleteById(int id)
        {
            return await Bll.DeleteById(id);
        }

        [HttpPut]
        public async Task<bool> Update(UserDTO u)
        {
            return await Bll.Update(u);
        }
    }
}
