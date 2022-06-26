using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class TeacherModel : BaseEntity
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
        public string Subjects { get; set; }

        public string DepartmentName { get; set; }
        public string DepartmentIds { get; set; }

        public string Departments { get; set; }


        #endregion

    }
}
