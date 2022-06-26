
using Core.Entities;
using Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDepartmentRepository:IBaseRepository<Department>
    {
        public List<Department> GetDepartmentsByTeacherId(Guid TeacherId);
        public bool CheckDuplicateDepartmentCode(string departmentCode);
    }
}
