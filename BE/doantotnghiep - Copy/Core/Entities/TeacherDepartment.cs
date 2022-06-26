using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class TeacherDepartment:BaseEntity
    {

        public Guid TeacherId { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
