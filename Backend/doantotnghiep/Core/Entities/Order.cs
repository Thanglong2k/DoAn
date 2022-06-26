using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid? OrderID { get; set; }
        
        /// <summary>
        /// Id khách hàng
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// Mã đơn hàng
        /// </summary>

/*        [Required]
        [Duplicate]
        [DisplayName("Mã đơn đặt")]
        public string OrderCode { get; set; }*/

/// <summary>
/// trạng thái đơn hàng
/// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
/// trạng thái thanh toán
/// </summary>
        public bool PaymentStatus { get; set; }
        /// <summary>
        /// Tổng giá
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// Địa chỉ giao hàng
        /// </summary>
        public string? DeliveryAddress { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }
        /// <summary>
        /// Danh sách chi tiết đơn hàng
        /// </summary>
        public List<OrderDetail>? OrderDetails { get; set; }
        

        
        #endregion
    }

    public class OrderCustomer
    {
        public Guid CustomerID { get; set; }
        public bool IsPurchased { get; set; }
    }
    public class OrderCustomerInfo
    {
        public Order Order { get; set; }
        public Customer Customer { get; set; }
       public int Quantity { get; set; }
    }

    public class OrderInfo
    {
        public Order order { get; set; }
        public List<OrderDetail> orderDetails { get; set; }

    }
    public class OrderCart
    {
        public Order Order { get; set; }
        public List<OrderDetailProductAttribute> OrderDetails { get; set; }

    }
    public class OrderList
    {
        public Order Order { get; set; }
        public Customer Customer { get; set; }
        public int ProductQuantity { get; set; }
    }
}
