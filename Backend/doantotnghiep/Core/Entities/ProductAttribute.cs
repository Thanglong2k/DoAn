using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductAttribute:BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid ProductAttributeID { get; set; }
        /// <summary>
        /// Mã sản phẩm
        /// </summary>

        [Required]
        [Duplicate]
        public Guid ProductID { get; set; }

        public int Memory { get; set; }
        /// <summary>
        /// Giá
        /// </summary>
        public decimal Price { get; set; }
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
        public float Rate { get; set; }

        /// <summary>
        /// Số lwuojt đánh giá
        /// </summary>
        public int CountRating { get; set; }
        public string Description { get; set; }
    }
    public class ProductAttributeDto {

        public Guid ProductAttributeID { get; set; }
        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        public Guid ProductID { get; set; }

        public int Memory { get; set; }
        /// <summary>
        /// Giá
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Giá khuyến mãi
        /// </summary>
        public decimal? PromotionPrice { get; set; }
        /// <summary>
        /// Ngày bắt đầu khuyến mãi
        /// </summary>
        public RangeDate? RangeDate { get; set; }
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
        public float Rate { get; set; }

        /// <summary>
        /// Số lwuojt đánh giá
        /// </summary>
        public int CountRating { get; set; }
        public string Description { get; set; }
    }
}
