using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class TeacherDepartmentService: BaseService<TeacherDepartment>, ITeacherDepartmentService
    {
        #region Declare

        //public ServiceResult ServiceResult;
        private ITeacherDepartmentRepository _teacherDepartmentRepository;
        #endregion
        #region Constructor
        public TeacherDepartmentService(ITeacherDepartmentRepository teacherDepartmentRepository) : base(teacherDepartmentRepository)
        {
            //ServiceResult = new ServiceResult();

            _teacherDepartmentRepository = teacherDepartmentRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Xóa thông tin teacherdepartment theo  id teacher và id deparment
        /// CreatedBy:TTLONG(20/09/2021)
        /// </summary>
        /// <param name="teacherId">id giáo viên</param>
        /// <param name="departmentId">id phòng ban</param>
        /// <returns>trạng thái request</returns>
        public ServiceResult Delete(Guid teacherId, Guid departmentId)
        {
            ServiceResult.Data = _teacherDepartmentRepository.DeleteTeacherDepartment(teacherId, departmentId);
            return ServiceResult;
        }
        #endregion
    }
}
