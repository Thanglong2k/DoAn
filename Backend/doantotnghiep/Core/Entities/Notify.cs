using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.Entities
{
    /// <summary>
    /// THông báo
    /// </summary>
    /// CreatedBy: TTLONG (25/7/2021)
    public class Notify : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid NotifyID { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public Enum.Notify Type { get; set; }
        
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public Guid? CustomerID { get; set; }
        
        /// <summary>
        /// Tên người dùng
        /// </summary>
        public Guid? OrderID { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public Guid? ProductAttributeID { get; set; }
        public bool? IsRead { get; set; }

        public bool? IsView { get; set; }

        /// <summary>
        /// SĐT
        /// </summary>
        public DateTime CreatedDate { get; set; }

        #endregion
    }
    public class NotifyList
    {
        public Notify Notify { get; set; }
        public Product Product { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
        public Order Order { get; set; }
        public Customer Customer { get; set; }
    }
}