using AutoMapper;
using Bll.Interfaces;
using Dal;
using Dal.Functions;
using Dal.Interfaces;
using DTO;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Functions
{
    public class TripTypeBll : ITripTypeBll
    {
        ITripTypeDal dal;
        IMapper mapper;

        public TripTypeBll(ITripTypeDal dal)
        {
            this.dal = dal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperTrips>();
            });

            mapper = config.CreateMapper();
        }
        public async Task<List<TripTypeDTO>> GetAll()
        {
            var answer =await dal.GetAll();
            return mapper.Map<List<TripTypeDTO>>(answer);
        }

        public async Task<TripTypeDTO> GetByCode(int code)
        {
            var answer =await dal.GetByCode(code);
            return mapper.Map<TripTypeDTO>(answer);
        }

        public async Task<bool> Add(TripTypeDTO t)
        {
            var newTripType = mapper.Map<TripType>(t);
            return await dal.Add(newTripType);
        }

        public async Task<int> Add(string type)
        {
            return await dal.Add(type);
        }

        public async Task<int> add(TripTypeDTO t)
        {
            var newTripType = mapper.Map<TripType>(t);
            return await dal.add(newTripType);
        }

        public async Task<bool> Delete(int code)
        {
            return await dal.Delete(code);
        }
    }
}
