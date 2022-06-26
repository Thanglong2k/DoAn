using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IAccountService:IBaseService<Account>
    {
       /// <summary>
       /// Đăng nhập
       /// </summary>
       /// <param name="account">Tài khoản</param>
       /// <returns></returns>
        ServiceResult Login(Account account);
        ServiceResult GetAccountID(Account account);
        ServiceResult GetAccountByUserName(string username);
        ServiceResult CheckAccountExists(string username);
    }
}
