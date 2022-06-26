using Core.Model;
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

        [DisplayName("Mã sản phẩm")]
        public string? ProductCode { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        [Required]
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
        /// <summary>
        /// Meta
        /// </summary>
        public string? Meta { get; set; }
        /// <summary>
        /// Màn hình
        /// </summary>
        public string? Monitor { get; set; }

        public float? MonitorSize { get; set; }
        /// <summary>
        /// Ram
        /// </summary>
        public int? Ram { get; set; }

        /// <summary>
        /// IMEI
        /// </summary>
        public string? IMEI { get; set; }

        /// <summary>
        /// Giá
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Camera
        /// </summary>
        public string? Camera { get; set; }

        /// <summary>
        /// Pin
        /// </summary>
        public string? Battery { get; set; }

        /// <summary>
        /// Giá khuyến mãi
        /// </summary>
        public decimal? PromotionPrice { get; set; }

        /// <summary>
        /// Ngày bắt đầu khuyến mãi
        /// </summary>
        public DateTime? PromotionStart { get; set; }

        /// <summary>
        /// Ngày kết thúc khuyến mãi
        /// </summary>
        public DateTime? PromotionEnd { get; set; }
        /// <summary>
        /// Đánh giá trung bình
        /// </summary>
        public float? Rate { get; set; }

        /// <summary>
        /// Số lwuojt đánh giá
        /// </summary>
        public int? CountRating { get; set; }

        /// <summary>
        /// Ảnh
        /// </summary>
        public string? Image { get; set; }
        public string? Avatar { get; set; }

        [NotMapped]
        public IFormFile? AvatarFile { get; set; }

        public List<IFormFile>? ImageFile { get; set; }

        /// <summary>
        /// Id danh mục sản phẩm
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Id hãng sản xuất
        /// </summary>
        public Guid ManufacturerID { get; set; }

        public int? Weight { get; set; }
        public int Type { get; set; }
        public string Origin { get; set; }
        public DateTime ReleaseTime { get; set; }
        public string? GPU { get; set; }
        public string? BatteryType { get; set; }
        public string? OperatingSystem { get; set; }
        public string? Size { get; set; }
        public int? Warranty { get; set; }
        public List<ProductAttribute>? ProductAttributes { get; set; }
        public string? ProductAttributeList { get; set; }



        #endregion
    }
    public class ProductPost
    {
        public Product Product { get; set; }
        public int PostQuantity { get; set; }
    }
    public class ProductAdd
    {
        public string? ProductCode { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>

        public string ProductName { get; set; }
        /// <summary>
        /// Meta
        /// </summary>
        public string? Meta { get; set; }
        /// <summary>
        /// Màn hình
        /// </summary>
        public string? Monitor { get; set; }
        public float? MonitorSize { get; set; }
        /// <summary>
        /// Ram
        /// </summary>
        public int? Ram { get; set; }

        /// <summary>
        /// IMEI
        /// </summary>
        public string? IMEI { get; set; }

        /// <summary>
        /// Giá
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Camera
        /// </summary>
        public string? Camera { get; set; }

        /// <summary>
        /// Pin
        /// </summary>
        public string? Battery { get; set; }

        /// <summary>
        /// Giá khuyến mãi
        /// </summary>
        public decimal? PromotionPrice { get; set; }

        /// <summary>
        /// Đánh giá trung bình
        /// </summary>
        public float? Rate { get; set; }

        /// <summary>
        /// Số lwuojt đánh giá
        /// </summary>
        public int? CountRating { get; set; }

        /// <summary>
        /// Ảnh
        /// </summary>
        public string? Avatar { get; set; }
        public List<string>? Image { get; set; }

        public IFormFile? AvatarFile { get; set; }
        public List<IFormFile>? ImageFile { get; set; }

        /// <summary>
        /// Id danh mục sản phẩm
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Id hãng sản xuất
        /// </summary>
        public Guid ManufacturerID { get; set; }

        public int? Weight { get; set; }
        public int Type { get; set; }
        public string Origin { get; set; }
        public string ReleaseTime { get; set; }
        public string? GPU { get; set; }
        public string? BatteryType { get; set; }
        public string? OperatingSystem { get; set; }
        public string? Size { get; set; }
        public int? Warranty { get; set; }

        public string? ProductAttributeList { get; set; }
        public  List<ProductAttribute>? ProductAttributes { get; set; }
    }
    public class ProductAndProductAttribute
    {
        public Product Product { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
    }
}
