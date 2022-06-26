using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class TeacherSubject:BaseEntity
    {

        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
    }
}
