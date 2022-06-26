using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IReceiptService:IBaseService<Receipt>
    {
        /// <summary>
        /// Lấy thông tin đơn hàng theo số điện thoại của khách hàng
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        ServiceResult GetReceiptByPhoneNumber(string phoneNumber);
        ServiceResult GetReceiptByID(Guid receiptID);


        ServiceResult Add(ReceiptInfo receiptInfo);

        ServiceResult PurchasedHistory(string phoneNumber);
        ServiceResult GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime);
        ServiceResult GetSuplierReceipt(Guid receiptID);
    }
}
