using Bll.Functions;
using Bll.Interfaces;
using Bll;
using Bll.Functions;
using Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DTO.Classes;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingPlaceController : ControllerBase
    {
        IBookingPlaceBll Bll;
        
        public BookingPlaceController(IBookingPlaceBll Bll)
        {
            this.Bll = Bll;
        }

        [HttpGet]
        public async Task<List<BookingPlacesDTO>> Get()
        {
            return await Bll.GetAll();
        }
       
        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<List<BookingPlacesDTO>> GetById(int id)
        {
            return await Bll.GetById(id);
        }
       
       [HttpGet]
       [Route("GetByCode/{code:int}")]
       public async Task<BookingPlacesDTO> GetByCode(int code)
       {
           return await Bll.GetByCode(code);
       }

       [HttpGet]
       [Route("GetByTrip/{code:int}")]
       public async Task<List<BookingPlacesDTO>> GetByTrip(int code)
       {
           return await Bll.GetByTrip(code);
       }

       [HttpPost]
       public async Task<bool> Add(BookingPlacesDTO b)
       {
           return await Bll.Add(b);
       }

       [HttpPost]
       [Route("AddToInt")]
       public async Task<int> AddToInt(BookingPlacesDTO b)
       {
           return await Bll.add(b);
       }

       [HttpDelete]
        [Route("Delete/{code:int}")]
       public async Task<bool> Delete(int code)
       {
           return await Bll.Delete(code);
       }

       [HttpPut]
       public async Task<bool> Update(BookingPlacesDTO b)
       {
           return await Bll.Update(b);
       }  
    }
}
