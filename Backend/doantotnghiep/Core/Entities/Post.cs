using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{

    /// <summary>
    /// Thông tin sản phẩm
    /// CreatedBy TTLONG (19/02/2022)
    /// </summary>
    public class Post : BaseEntity
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class Date : Attribute
        {

        }
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid PostID { get; set; }

        /// <summary>
        /// ID thông tin chung sản phẩm
        /// </summary>
        public Guid ProductID { get; set; }

        public Guid AccountID { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Trạng thái hiển thị
        /// </summary>
        public bool Status { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Avatar { get; set; }
        public string Description { get; set; }
        #endregion
    }
    public class PostAdd
    {

        /// <summary>
        /// ID thông tin chung sản phẩm
        /// </summary>
        public Guid ProductID { get; set; }

        public Guid AccountID { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Trạng thái hiển thị
        /// </summary>
        public bool Status { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Avatar { get; set; }
        public IFormFile? AvatarFile { get; set; }
        public string Description { get; set; }
    }
    public class AccountPost
    {
        public Post Post { get; set; }
        public Account Account { get; set; }
    }
    public class PostProductAccount
    {
        public Post Post { get; set; }
        public Account Account { get; set; }
        public Product Product { get; set; }
    }
}
