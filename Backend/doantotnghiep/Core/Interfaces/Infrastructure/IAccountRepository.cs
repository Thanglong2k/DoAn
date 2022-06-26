
using Core.Entities;
using Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface Danh mục kết nối dữ liệu database
    /// </summary>
    /// CreatedBy: TTLONG (28/7/2021)
    public interface IAccountRepository:IBaseRepository<Account>
    {
        public Account Login(Account account);
        public Guid GetAccountID(Account account);
        public Account GetAccountByUserName(string username);
        public IEnumerable<Account> CheckAccountExists(string username);
    }
}
