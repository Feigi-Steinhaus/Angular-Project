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
    public class TripTypeController : ControllerBase
    {
        ITripTypeBll Bll;
        
        public TripTypeController(ITripTypeBll bll)
        {
            this.Bll = bll;
        }
        
        [HttpGet]
        public async Task<List<TripTypeDTO>> Get()
        {
            return await Bll.GetAll();
        }

        [HttpGet]
        [Route("GetByCode/{code:int}")]
        public async Task<TripTypeDTO> GetByCode(int code)
        {
            return await Bll.GetByCode(code);
        }

        [HttpPost]
        public async Task<bool> Add(TripTypeDTO triptype)
        {
            return await Bll.Add(triptype);
        }
        [HttpPost]
        [Route("AddByInt")]
        public async Task<int> AddByInt(TripTypeDTO TripType)
        {
            return await Bll.add(TripType);
        }

        [HttpPost]
        [Route("/{nameType}")]
        public async Task<int> Add(string nameType)
        {
            return await Bll.Add(nameType);
        }

        [HttpDelete]
        public async Task<bool> Delete(int code)
        {
            return await Bll.Delete(code);
        }

    }
}
