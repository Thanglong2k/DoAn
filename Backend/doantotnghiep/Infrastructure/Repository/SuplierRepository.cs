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
    public class SuplierRepository : BaseRepository<Suplier>, ISuplierRepository
    {
        public SuplierRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public Suplier GetSuplierByPhoneNumber(string phoneNumber)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"PhoneNumber", phoneNumber);

            var entitites = DbConnection.QuerySingleOrDefault<Suplier>("Proc_GetSuplierByPhoneNumber", parameters, commandType: CommandType.StoredProcedure);

           
            //Trả về dữ liệu:
            return entitites;
        }

    }
}