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
    public class Evaluation : BaseEntity
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
        public Guid EvaluationID { get; set; }

        /// <summary>
        /// ID sản phẩm thuôc tính
        /// </summary>
        public Guid ProductAttributeID { get; set; }

        /// <summary>
        /// ID khách hàng
        /// </summary>
        public Guid CustomerID { get; set; }


        /// <summary>
        /// Đánh giá trung bình
        /// </summary>
        public float Rate { get; set; }

        /// <summary>
        /// Số lwuojt đánh giá
        /// </summary>
        public int? CountRating { get; set; }
        /// <summary>
        /// Số lwuojt đánh giá
        /// </summary>
        public string? Content { get; set; }
        public bool Status { get; set; }
        public string? CustomerName { get; set; }

        #endregion
    }
    public class ProductRatingQuantity
    {
        public int Rate { get; set; }
        public int Quantity { get; set; }
    }
    public class CustomerEvaluation
    {
        public Evaluation Evaluation { get; set; }
        public string CustomerName { get; set; }
    }
    public class EvaluationProductCustomer
    {
        public Evaluation Evaluation { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public Guid ProductAttributeID { get; set; }
    }
}
