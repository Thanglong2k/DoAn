using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface ISuplierService:IBaseService<Suplier>
    {
        /// <summary>
        /// Lấy danh thông tin khách hàng bằng số điện thoại
        /// </summary>
        /// <param name="phoneNummber">Số điện thoại</param>
        /// <returns></returns>
        ServiceResult GetSuplierByPhoneNumber(string phoneNummber);
    }
}
