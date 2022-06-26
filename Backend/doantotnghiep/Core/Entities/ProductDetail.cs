using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductDetail
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid ProductDetailID { get; set; }
        /// <summary>
        /// ID sản phẩm
        /// </summary>
        public Guid ProductAttributeID { get; set; }

        /// <summary>
        /// Sản phẩm thứ
        /// </summary>
        public int DetailNumber { get; set; }
        /// <summary>
        /// IMEI
        /// </summary>
        public string IMEI { get; set; }
    }
    public class ProductProductDetail
    {
        public ProductDetail ProductDetail { get; set; }
        public Product Product { get; set; }
        public ProductAttribute ProductAttribute { get; set; }

    }
    public class ProductWarrenty
    {
        public Product Product { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
