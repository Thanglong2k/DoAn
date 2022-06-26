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
    public class Product : BaseEntity
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
        public Guid ProductID { get; set; }
        /// <summary>
        /// Mã sản phẩm
        /// </summary>

        [Required]
        [Duplicate]
        [DisplayName("Mã sản phẩm")]
        public string ProductCode { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        [Required]
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }

        /// <summary>
        /// IMEI
        /// </summary>
        public string IMEI { get; set; }

        /// <summary>
        /// Giá
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Camera
        /// </summary>
        public string Camera { get; set; }

        /// <summary>
        /// Pin
        /// </summary>
        public string Battery { get; set; }

        /// <summary>
        /// Giá khuyến mãi
        /// </summary>
        public decimal PromotionPrice { get; set; }

        /// <summary>
        /// Ngày bắt đầu khuyến mãi
        /// </summary>
        public DateTime PromotionStart { get; set; }

        /// <summary>
        /// Ngày kết thúc khuyến mãi
        /// </summary>
        public DateTime PromotionEnd { get; set; }

        /// <summary>
        /// Ảnh
        /// </summary>
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// Id danh mục sản phẩm
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Id hãng sản xuất
        /// </summary>
        public Guid ManufacturerID { get; set; }



        #endregion
    }
}
