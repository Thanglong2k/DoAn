using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    /// <summary>
    /// THông tin vị trí
    /// </summary>
    /// CreatedBy: TTLONG (25/7/2021)
    public class CombinationSubject:BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CombinationSubjectId { get; set; }
        /*/// <summary>
        /// Mã vị trí
        /// </summary>
        public string CombinationSubjectCode { get; set; }*/
        /// <summary>
        /// Tên vị trí
        /// </summary>
        [Duplicate]
        public string CombinationSubjectName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Note { get; set; }
        public string Not { get; set; }

        /*public virtual ICollection<Teacher> Teacher { get; set; }*/
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
