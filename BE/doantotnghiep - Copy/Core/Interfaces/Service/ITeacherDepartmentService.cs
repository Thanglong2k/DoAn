using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface ITeacherDepartmentService : IBaseService<TeacherDepartment>
    {
        /// <summary>
        /// Xóa TeacherDepartment theo TeacherId và DepartmentId
        /// </summary>
        /// <param name="teacherId">Id giáo viên</param>
        /// <param name="departmentId">Id phòng kho</param>
        /// <returns></returns>
        public ServiceResult Delete(Guid teacherId, Guid departmentId);
    }
}
