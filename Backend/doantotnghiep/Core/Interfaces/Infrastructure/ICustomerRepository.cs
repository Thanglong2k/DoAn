
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
    public interface ICustomerRepository:IBaseRepository<Customer>
    {

        /// <summary>
        /// Lấy thông tin khách hàng bằng số điện thoại
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns></returns>
        public Customer GetCustomerByPhoneNumber(string phoneNumber);
    }
}
