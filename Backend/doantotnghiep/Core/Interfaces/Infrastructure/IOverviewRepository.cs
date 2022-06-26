
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
    public interface IOverviewRepository
    {
        public int GetTurnoverTotal(int month);
        public int GetOrderTotal(int month);
        public int GetReceiptTotal(int month);
        public int GetNewProductTotal(int month);
        public MonthlyTurnover GetMonthlyTurnover();
        public IEnumerable<QuantitySoldOfManufacturer> GetQuantitySoldByManufacturer(int month);
        public IEnumerable<OrderCustomerInfo> GetCancelledOrders(int month);
        public IEnumerable<ProductBestSelling> GetProductBestSellingsInMonth(int month,int productNumber);
        public int GetQuantityOrderCancelled(int month);
    }
}
