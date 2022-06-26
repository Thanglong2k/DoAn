using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ReceiptDetail:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid? ReceiptDetailID { get; set; }
        /// <summary>
        /// ID đơn hàng  
        /// </summary>
        public Guid ReceiptID { get; set; }


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
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }

        #endregion
    }
    public class ReceiptDetailProductAttribute
    {
        public  ReceiptDetail ReceiptDetail { get; set; }
        public  ProductAttribute ProductAttribute { get; set; }
        public  Product Product { get; set; }
    }
}
