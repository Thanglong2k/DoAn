using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    /// <summary>
    /// THông tin phòng ban
    /// </summary>
    /// CreatedBy: TTLONG (25/7/2021)
    public class Subject:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid SubjectId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string SubjectCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string SubjectName { get; set; }
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