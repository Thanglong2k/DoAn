using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    /// <summary>
    /// THông tin phòng ban
    /// </summary>
    /// CreatedBy: TTLONG (25/7/2021)
    public class Category:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Tên danh mục
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Tên danh mục
        /// </summary>
        public string? Meta { get; set; }
        
        /// <summary>  
        /// Ảnh
        /// </summary>
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        /// <summary>
        /// ID danh mục cha
        /// </summary>
        public Guid? CategoryParentID { get; set; }
        /*/// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }*/
        /*/// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }*/
        #endregion
    }
}