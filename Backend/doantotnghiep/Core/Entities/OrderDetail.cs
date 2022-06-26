using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OrderDetail:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid? OrderDetailID { get; set; }
        /// <summary>
        /// ID đơn hàng  
        /// </summary>
        public Guid OrderID { get; set; }


        /// <summary>
        /// ID sản phẩm
        /// </summary>
        public Guid ProductAttributeID { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal UnitPrice { get; set; }

        public string? IMEI { get; set; }

        #endregion
    }
    public class OrderDetailProductAttribute
    {
        public  OrderDetail OrderDetail { get; set; }
        public  ProductAttribute ProductAttribute { get; set; }
        public  Product Product { get; set; }
    }
}
