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
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IConfiguration configuration) : base(configuration)
        {


        }

        public Account Login(Account account)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"UserName", account.UserName);
            parameters.Add($"Password", account.Password);
            //Khởi tạo các CommandText:
            
            var entitites = DbConnection.QueryFirstOrDefault<Account>("Proc_Login", parameters, commandType: CommandType.StoredProcedure);
            
            //Trả về dữ liệu:
            return entitites;
        }
        public Guid GetAccountID(Account account)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"UserName", account.UserName);
            parameters.Add($"Password", account.Password);
            //Khởi tạo các CommandText:
            
            var entitites = DbConnection.QueryFirstOrDefault<Account>("Proc_Login", parameters, commandType: CommandType.StoredProcedure);
            
            //Trả về dữ liệu:
            return entitites.AccountID;
        }
        public Account GetAccountByUserName(string username)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"UserName", username);
            //Khởi tạo các CommandText:
            
            var entitites = DbConnection.QuerySingleOrDefault<Account>("Proc_GetAccountByUserName", parameters, commandType: CommandType.StoredProcedure);
            
            //Trả về dữ liệu:
            return entitites;
        }
        public IEnumerable<Account> CheckAccountExists(string username)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"UserName", username);
            //Khởi tạo các CommandText:
            
            var entitites = DbConnection.Query<Account>("Proc_CheckAccountExists", parameters, commandType: CommandType.StoredProcedure);
            
            //Trả về dữ liệu:
            return entitites;
        }
    }
}