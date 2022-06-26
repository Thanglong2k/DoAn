using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class TeacherSubjectService: BaseService<TeacherSubject>, ITeacherSubjectService
    {
        #region Declare

        //public ServiceResult ServiceResult;
        private ITeacherSubjectRepository _teacherSubjectRepository;
        #endregion
        #region Constructor
        public TeacherSubjectService(ITeacherSubjectRepository teacherSubjectRepository) : base(teacherSubjectRepository)
        {
            //ServiceResult = new ServiceResult();

            _teacherSubjectRepository = teacherSubjectRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Xóa teacherSubject theo id teacher và id subject
        /// CreatedBy:TTLONG(20/09/2021)
        /// </summary>
        /// <param name="teacherId">id giáo viên</param>
        /// <param name="SubjectId">id môn học</param>
        /// <returns>Trạng thái request</returns>
        public ServiceResult Delete(Guid teacherId, Guid SubjectId)
        {
            ServiceResult.Data = _teacherSubjectRepository.DeleteTeacherSubject(teacherId, SubjectId);
            return ServiceResult;
        }
        #endregion
    }
}
