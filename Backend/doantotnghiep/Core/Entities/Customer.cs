using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Customer:BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string? CustomerCode { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
    }
}
