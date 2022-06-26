
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
    public interface IReceiptRepository:IBaseRepository<Receipt>
    {
        public Guid Add(Receipt receipt);
        /// <summary>
        /// Lấy dánh sách đơn hàng theo số điện thoại khách hàng
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public IEnumerable<Receipt> GetReceiptByPhoneNumber(string phoneNumber);


        public IEnumerable<Receipt> PurchasedHistory(string phoneNumber);
        /// <summary>
        /// Lấy toàn bộ dữ liệu:
        /// </summary>
        /// <returns>
        /// Danh sách Object
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        OutputFilterPaging<ReceiptList> GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime);
        public ReceiptSuplierInfo GetSuplierReceipt(Guid receiptID);

    }
}
