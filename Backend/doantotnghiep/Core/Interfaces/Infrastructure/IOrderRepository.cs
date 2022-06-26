
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
    public interface IOrderRepository:IBaseRepository<Order>
    {
        public Guid Add(Order order);
        /// <summary>
        /// Lấy dánh sách đơn hàng theo số điện thoại khách hàng
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetOrderByPhoneNumber(string phoneNumber);
        /// <summary>
        /// Kiểm tra khách hàng đã mua sản phẩm chưa
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public OrderCustomer CheckCustomerPurchasedProduct(string phoneNumber,Guid productAttributeID);

        public IEnumerable<Order> PurchasedHistory(string phoneNumber);
        public IEnumerable<Order> FollowOrder(string phoneNumber, int status);
        /// <summary>
        /// Lấy toàn bộ dữ liệu:
        /// </summary>
        /// <returns>
        /// Danh sách Object
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        OutputFilterPaging<OrderList> GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime, int status);
        public OrderCustomerInfo GetCustomerOrder(Guid orderID);
    }
}
