
using Core.Entities;
using Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface Employee kết nối dữ liệu database
    /// </summary>
    /// CreatedBy: TTLONG (28/7/2021)
    public interface ITeacherSubjectRepository:IBaseRepository<TeacherSubject>
    {
        int DeleteTeacherSubject(Guid teacherId, Guid SubjectId);
    }
}
