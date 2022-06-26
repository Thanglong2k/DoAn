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
    public class Comment : BaseEntity
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
        public Guid CommentID { get; set; }

        /// <summary>
        /// ID sản phẩm thuộc tính
        /// </summary>
        public Guid ProductAttributeID { get; set; }

        /// <summary>
        /// ID khách hàng
        /// </summary>
        public Guid? CustomerID { get; set; }
        public Guid? AccountID { get; set; }

        /// <summary>
        /// Số lwuojt đánh giá
        /// </summary>
        public string? Content { get; set; }
        public bool Status { get; set; }
        public Guid? ParentCommentID { get; set; }
        #endregion
    }
    public class CustomerComment
    {
        public Comment Comment { get; set; }
        public string? CustomerName { get; set; }
        public string? AccountName { get; set; }
    }
    public class CommentProductCustomer
    {
        public Comment Comment { get; set; }
        public Customer Customer { get; set; }
        public Account Account { get; set; }
        public Product Product { get; set; }
        public Guid ProductAttributeID { get; set; }
        public string ParentComment { get; set; }
    }
}
