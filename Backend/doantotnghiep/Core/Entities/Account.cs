using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    /// <summary>
    /// THông tin tài khoản
    /// </summary>
    /// CreatedBy: TTLONG (25/7/2021)
    public class Account:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid AccountID { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string? Password { get; set; }
        
        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// SĐT
        /// </summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool Status { get; set; }
        public int Permission { get; set; }
        public string? Token { get; set; }

        #endregion
    }
}