using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Receipt:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid? ReceiptID { get; set; }
        
        /// <summary>
        /// Id khách hàng
        /// </summary>
        public Guid SuplierID { get; set; }

        /// <summary>
/// trạng thái thanh toán
/// </summary>
        public bool PaymentStatus { get; set; }
        /// <summary>
        /// Tổng giá
        /// </summary>
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }
        

        
        #endregion
    }

    public class ReceiptSuplier
    {
        public Guid SuplierID { get; set; }
        public bool IsPurchased { get; set; }
    }
    public class ReceiptSuplierInfo
    {
        public Receipt Receipt { get; set; }
        public Suplier Suplier { get; set; }
       public int Quantity { get; set; }
    }

    public class ReceiptInfo
    {
        public Receipt Receipt { get; set; }
        public List<ReceiptDetail> ReceiptDetails { get; set; }

    }
    public class ReceiptCart
    {
        public Receipt Receipt { get; set; }
        public List<ReceiptDetailProductAttribute> ReceiptDetails { get; set; }

    }
    public class ReceiptList
    {
        public Receipt Receipt { get; set; }
        public Suplier Suplier { get; set; }
        public int ReceiptDetailQuantity { get; set; }
    }
}
