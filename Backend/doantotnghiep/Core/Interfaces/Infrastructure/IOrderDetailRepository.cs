
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
    public interface IOrderDetailRepository:IBaseRepository<OrderDetail>
    {
        /// <summary>
        /// Kiểm tra khách hàng đã mua sản phẩm chưa
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public OrderCustomer CheckCustomerPurchasedProduct(string phoneNumber, Guid productAttributeID);
        public int InsertOrderDetails(List<OrderDetail> orderDetails,Guid orderID);
        public IEnumerable<OrderDetailProductAttribute> PurchasedHistoryByOrderID(Guid? orderID);
        public OutputFilterPaging<OrderDetailProductAttribute> GetOrderDetailByOrderID(Guid orderID, int pageIndex, int pageSize);
        public int UpdateIMEIByOrderID(string imeis, Guid orderDetailID);
    }
}
