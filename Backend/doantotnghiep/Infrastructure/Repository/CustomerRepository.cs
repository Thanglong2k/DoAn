using Dapper;
using Microsoft.Extensions.Configuration;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public Customer GetCustomerByPhoneNumber(string phoneNumber)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"PhoneNumber", phoneNumber);

            var entitites = DbConnection.QuerySingleOrDefault<Customer>("Proc_GetCustomerByPhoneNumber", parameters, commandType: CommandType.StoredProcedure);

           
            //Trả về dữ liệu:
            return entitites;
        }

    }
}