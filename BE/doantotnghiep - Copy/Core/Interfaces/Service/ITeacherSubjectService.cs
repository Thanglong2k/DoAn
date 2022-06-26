using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface ITeacherSubjectService : IBaseService<TeacherSubject>
    {
        public ServiceResult Delete(Guid teacherId, Guid subjectId);
    }
}
