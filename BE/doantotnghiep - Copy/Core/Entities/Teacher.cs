

using Microsoft.AspNetCore.Http;
using Core.Enum;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// CreatedBy TTLONG (23/7/2021)
    /// </summary>
    public class Teacher : BaseEntity
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
        public Guid TeacherId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        
        [Required]
        [Duplicate]
        [DisplayName("Số hiệu cán bộ")]
        public string TeacherCode { get; set; }
        /// <summary>
        /// HỌ và tên
        /// </summary>
        [Required]
        [DisplayName("Họ và tên")]
        public string TeacherName { get; set; }
        /// <summary>
        /// Id Phòng ban
        /// </summary>
        /*[Required]
        [DisplayName("Mã kho/phòng")]
        public Guid? DepartmentId { get; set; }*/

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [DisplayName("Số điện thoại di động")]
        [APhoneNumber]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// email
        /// </summary>
        
        [DisplayName("Email")]
        [AEmail]
        public string Email { get; set; }

        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public bool EMProfessionalQualifications { get; set; }
        public bool WorkStatus { get; set; }
        public DateTime? QuitWorkDate { get; set; }

        public Guid? CombinationSubjectId { get; set; }
        /*[Computed]*/
        [Write(false)]
        public string CombinationSubjectName { get; set; }

        /*public virtual CombinationSubject CombinationSubject { get; set; }*/
        public string SubjectName { get; set; }
        public string SubjectIds { get; set; }
        public List<Subject> SubjectList { get; set; }
        public string Subjects { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentIds { get; set; }

        public string Departments { get; set; }
        public List<Department> DepartmentList { get; set; }


        #endregion

    }
}
