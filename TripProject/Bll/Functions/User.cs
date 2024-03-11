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
    public class UserBll : IUserBll
    {
        IUsersDal dal;
        IMapper mapper;

        public UserBll(IUsersDal dal)
        {
            this.dal = dal;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperTrips>();
            });

            mapper = config.CreateMapper();
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var answer =await dal.GetAll();
            return mapper.Map<List<UserDTO>>(answer);
        }

        public async Task<UserDTO> GetById(int id)
        {
            var answer =await dal.GetById(id);
            return mapper.Map<UserDTO>(answer);
        }

        public async Task<UserDTO> GetByMailPass(string mail, string pass)
        {
            var answer =await dal.GetByMailPass(mail, pass);
            return mapper.Map<UserDTO>(answer);
        }

        public async Task<List<TripsDTO>> GetAllTrips(int id)
        {
            var answer =await dal.GetAllTrips(id);
            return mapper.Map<List<TripsDTO>>(answer);
        }

        public async Task<bool> Add(UserDTO t)
        {
            try
            {
                var newUser = mapper.Map<User>(t);
                /*
                if (ValidationTests.IsHebrewString(newUser.Name) &&
                ValidationTests.IsHebrewString(newUser.Family) &&
                ValidationTests.IsPhoneNumberValid(newUser.Phone) &&
                ValidationTests.IsPasswordValid(newUser.LoginPassword))
                */
                    return await dal.Add(newUser);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<int> add(UserDTO t)
        {
            try
            {
                var newUser = mapper.Map<User>(t);
                /*
                if (ValidationTests.IsHebrewString(newUser.Name) &&
                ValidationTests.IsHebrewString(newUser.Family) &&
                ValidationTests.IsPhoneNumberValid(newUser.Phone) &&
                ValidationTests.IsPasswordValid(newUser.LoginPassword))
                */
                    return await dal.add(newUser);
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<bool> Delete(string mail, string pass)
        {
            return await dal.Delete(mail, pass);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await dal.DeleteById(id);
        }

        public async Task<bool> Update(UserDTO u)
        {
            try
            {
                var newUser = mapper.Map<User>(u);
                return await dal.Update(newUser);
            }
            catch
            {
                return false;
            }
        }
    }
}
