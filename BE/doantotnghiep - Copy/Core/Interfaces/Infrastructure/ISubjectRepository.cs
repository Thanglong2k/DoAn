
using Core.Entities;
using Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISubjectRepository:IBaseRepository<Subject>
    {
        public List<Subject> GetSubjectsByTeacherId(Guid TeacherId);
        public bool CheckDuplicateSubjectCode(string SubjectCode);
    }
}
