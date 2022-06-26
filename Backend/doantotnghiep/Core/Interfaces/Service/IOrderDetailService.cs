using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IOrderDetailService:IBaseService<OrderDetail>
    {

        /// <summary>
        /// Kiểm tra khách hàng đã mua sản phẩm chưa
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        ServiceResult CheckCustomerPurchasedProduct(string phoneNumber,Guid productAttributeID);

        ServiceResult Add(OrderDetail orderDetail);
        ServiceResult InsertOrderDetails(List<OrderDetail> orderDetails,Guid orderID);
        ServiceResult PurchasedHistoryByOrderID(Guid? orderID);
        ServiceResult GetOrderDetailByOrderID(Guid orderID, int pageIndex, int pageSize);

        ServiceResult UpdateIMEIByOrderID(string imeis, Guid orderDetailID);
    }
}
