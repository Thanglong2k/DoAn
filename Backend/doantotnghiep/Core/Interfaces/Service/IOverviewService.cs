using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IOverviewService
    {
       /// <summary>
       /// Đăng nhập
       /// </summary>
       /// <param name="account">Tài khoản</param>
       /// <returns></returns>
        ServiceResult GetTurnoverTotal(int month);
        ServiceResult GetOrderTotal(int month);
        ServiceResult GetReceiptTotal(int month);
        ServiceResult GetNewProductTotal(int month);
        ServiceResult GetMonthlyTurnover();
        ServiceResult GetQuantitySoldByManufacturer(int month);
        ServiceResult GetQuantityOrderCancelled(int month);
        ServiceResult GetCancelledOrders(int month);
        ServiceResult GetProductBestSellingsInMonth(int month,int productNumber);
    }
}
