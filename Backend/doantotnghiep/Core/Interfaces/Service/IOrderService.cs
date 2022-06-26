using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IOrderService:IBaseService<Order>
    {
        /// <summary>
        /// Lấy thông tin đơn hàng theo số điện thoại của khách hàng
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        ServiceResult GetOrderByPhoneNumber(string phoneNumber);
        /// <summary>
        /// Kiểm tra khách hàng đã mua sản phẩm chưa
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        ServiceResult CheckCustomerPurchasedProduct(string phoneNumber,Guid productAttributeID);

        ServiceResult Add(OrderInfo orderInfo);

        ServiceResult PurchasedHistory(string phoneNumber);
        ServiceResult FollowOrder(string phoneNumber, int status);
        ServiceResult GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime, int status);
        ServiceResult GetCustomerOrder(Guid orderID);
        ServiceResult UpdateIMEIByOrderID(string imeis, Guid orderDetailID);
    }
}
