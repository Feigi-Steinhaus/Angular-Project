using Bll.Functions;
using Bll.Interfaces;
using Dal;
using Dal.Functions;
using Dal.Interfaces;
using DTO;
using DTO.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
       
        ITripBll Bll;
      
        public TripController(ITripBll bll)
        {
            this.Bll = bll;
        }
        
        [HttpGet]
        public async Task<List<TripsDTO>> Get()
        {
            return await Bll.GetAll();
        }

        [HttpGet]
        [Route("getByCode/{code:int}")]
        public async Task<TripsDTO> GetByCode(int code)
        {
            return await Bll.GetByCode(code);
        }

        [HttpPost]
        public async Task<bool> Add(TripsDTO trip)
        {
            return await Bll.Add(trip);
        }

        [HttpPost]
        [Route("AddByInt")]
        public async Task<int> AddByInt(TripsDTO trip)
        {
            return await Bll.add(trip);
        }

        [HttpDelete]
        public async Task<bool> Delete(int code)
        {
            return await Bll.Delete(code);
        }

        [HttpPut]
        public async Task<bool> Update(TripsDTO trip)
        {
            return await Bll.Update(trip);
        }
        
        [HttpPut]
        [Route("Places/{code:int}/{count:int}")]
        public async Task<bool> UpdatePlaces(int code,int count)
        {
            return await Bll.UpdatePlaces(code,count);
        }
    }
}