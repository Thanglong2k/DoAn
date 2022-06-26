
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Core.Const;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Service;
using static Core.Entities.BaseEntity;

namespace Core.Services
{
    public class TeacherService:BaseService<Teacher>,ITeacherService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private ITeacherRepository _teacherRepository;
        private ITeacherDepartmentRepository _teacherDepartmentRepository;
        private ITeacherSubjectRepository _teacherSubjectRepository;
        private IDepartmentRepository _departmentRepository;
        private ISubjectRepository _subjectRepository;
        #endregion
        #region Constructor
        public TeacherService(ITeacherRepository teacherRepossitory, 
            ITeacherDepartmentRepository teacherDepartmentRepository, 
            ITeacherSubjectRepository teacherSubjectRepository, 
            IDepartmentRepository departmentRepository,
            ISubjectRepository subjectRepository
            ) :base(teacherRepossitory)
        {
            //ServiceResult = new ServiceResult();
            
            _teacherRepository = teacherRepossitory;
            _departmentRepository = departmentRepository;
            _subjectRepository = subjectRepository;
            _teacherDepartmentRepository = teacherDepartmentRepository;
            _teacherSubjectRepository = teacherSubjectRepository;
        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Teacher teacher)
        {
           /* var departmentIds = "";
            var lengthDepartments = teacher.DepartmentList.Count;
            if (lengthDepartments > 0)
            {

                for (int i = 0; i < lengthDepartments - 1; i++)
                {
                    departmentIds += teacher.DepartmentList[i].DepartmentId + ",";
                }
                departmentIds += teacher.DepartmentList[lengthDepartments - 1].DepartmentId;
                teacher.DepartmentIds = departmentIds;
            }


            var subjectIds = "";
            var lengthSubjectList = teacher.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += teacher.SubjectList[i].SubjectId + ",";
                }
                subjectIds += teacher.SubjectList[lengthSubjectList - 1].SubjectId;
                teacher.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(teacher);
            if(isValidate)
            {
                var row = _teacherRepository.Add(teacher);
                *//*if (row > 0)
                {
                    var teacherDepartmentService = new TeacherDepartmentService(_teacherDepartmentRepository);
                    var entity = _teacherRepository.GetTeacherByCreatedDate();
                    for (int i = 0; i < teacher.DepartmentList.Count; i++)
                    {
                        TeacherDepartment teacherDepartment = new TeacherDepartment();
                        teacherDepartment.TeacherId = entity.TeacherId;
                        teacherDepartment.DepartmentId = teacher.DepartmentList[i].DepartmentId;
                        ServiceResult = teacherDepartmentService.Add(teacherDepartment);
                    }
                }*//*

                ServiceResult.Data = row;
                ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            }
            else
            {
                return ServiceResult;
            }
            
            return ServiceResult;

        }*/
        #endregion
        /// <summary>
        /// Cập nhật thông tin teacher
        /// CreatedBy:TTLONG(19/09/2021)
        /// </summary>
        /// <param name="teacher">đối tượng teacher</param>
        /// <param name="teacherId">id teacher</param>
        /// <returns>đối tượng lưu thông tin request</returns>
        public override ServiceResult Update(Teacher teacher, Guid teacherId)
        {
            //cập nhạt teacher
            var row = base.Update(teacher, teacherId);
            //cập nhật phòng ban trong bảng trung gian teacherdepartment
            var teacherDepartmentService = new TeacherDepartmentService(_teacherDepartmentRepository);
            for (int i = 0; i < teacher.DepartmentList.Count; i++)
            {
                
                if (teacher.DepartmentList[i].EntityState == Core.Enum.EntityState.Update)
                {
                    TeacherDepartment teacherDepartment = new TeacherDepartment();
                    teacherDepartment.TeacherId = teacherId;
                    teacherDepartment.DepartmentId = teacher.DepartmentList[i].DepartmentId;
                    ServiceResult = teacherDepartmentService.Add(teacherDepartment);
                }
                else if(teacher.DepartmentList[i].EntityState == Core.Enum.EntityState.Delete)
                {
                    ServiceResult = teacherDepartmentService.Delete(teacherId,teacher.DepartmentList[i].DepartmentId);
                }
                
                
            }
            //cập nhật môn học trong bảng trung gian teachersubject
            var teacherSubjectRepository = new TeacherSubjectService(_teacherSubjectRepository);
            for (int i = 0; i < teacher.SubjectList.Count; i++)
            {

                if (teacher.SubjectList[i].EntityState == Core.Enum.EntityState.Update)
                {
                    TeacherSubject teacherSubject = new TeacherSubject();
                    teacherSubject.TeacherId = teacherId;
                    teacherSubject.SubjectId = teacher.SubjectList[i].SubjectId;
                    ServiceResult = teacherSubjectRepository.Add(teacherSubject);
                }
                else if (teacher.SubjectList[i].EntityState == Core.Enum.EntityState.Delete)
                {
                    ServiceResult = teacherSubjectRepository.Delete(teacherId, teacher.SubjectList[i].SubjectId);
                }


            }
            return ServiceResult;
        }
        /// <summary>
        /// lấy teacher theo id
        /// </summary>
        /// <param name="teacherId">id teacher</param>
        /// <param name="projectPath">đường dẫn của project</param>
        /// <returns>thông tin request</returns>
        public ServiceResult GetById(Guid teacherId,string projectPath)
        {
            var teacher = _teacherRepository.GetById(teacherId);
            teacher.DepartmentList = _departmentRepository.GetDepartmentsByTeacherId(teacherId);
            teacher.SubjectList = _subjectRepository.GetSubjectsByTeacherId(teacherId);
            if (teacher.ImageName != null)
            {

                teacher.ImageName = projectPath+teacher.ImageName;
            }
            
            ServiceResult.Data = teacher;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        /// <summary>
        /// Lọc và phân trang
        /// </summary>
        /// <param name="teacherFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public ServiceResult GetFilter(string teacherFilter, Guid? combinationSubjectId)
        {
            ServiceResult.Data = _teacherRepository.GetFilter(teacherFilter, combinationSubjectId);
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult CheckExistsCombinationSubject(Guid combinationSubjectId)
        {
            ServiceResult.Data = _teacherRepository.CheckExistsCombinationSubject(combinationSubjectId);
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        /// <summary>
        /// Validate tùy chỉnh theo màn hình nhân viên
        /// </summary>
        /// <param name="entity">Thực thể nhân viên</param>
        /// <returns>(true-false)</returns>
        /// CreatedBy: TTLONG(28/07/2021)
        protected override bool ValidateCustom(Teacher teacher)
        {
            bool isValid = true;
            /*var dateOfBirth="";*/
            //1. Đọc các property
            var properties = teacher.GetType().GetProperties();

            /*foreach (var property in properties)
            {             
                if (isValid && property.IsDefined(typeof(Date), false))
                {
                    TimeSpan time = DateTime.Now - Convert.ToDateTime(property.GetValue(teacher));
                    
                    if (DateTime.Now < Convert.ToDateTime(property.GetValue(teacher)))
                    {
                        isValid = false;
                        ServiceResult.Data = property.Name;
                        ServiceResult.MISACode = Core.Const.Const.MISACodeValidate;
                        ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Date);                        
                    }                  
                }              
            }*/
            return isValid;
        }
        /// <summary>
        /// Validate định dạng thuộc tính theo regex
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="propertyInfo"></param>
        /// <returns>(true-đúng false-sai)</returns>
        /// CreatedBy: TTLONG (04/08/2021)
        private bool ValidateProperty(Teacher teacher, PropertyInfo propertyInfo, string regex)
        {
            var isValid = true;

            //1. Tên trường
            var propertyName = propertyInfo.Name;

            //2. Tên hiển thị
            var propertyDisplayName = propertyInfo.GetCustomAttribute(typeof(DisplayName), true);

            //3. Gía trị
            var value = propertyInfo.GetValue(teacher);
            
            //Không validate required
            if (value==null || value.ToString()=="")
                return isValid;

            //var regex = @"^(0|(\+84)){1}(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$";
            var match = Regex.Match(value.ToString(), regex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                isValid = false;
            }
            if (!isValid)
            {              
                ServiceResult.Data = propertyInfo.Name;
                ServiceResult.MISACode = Core.Const.Const.MISACodeValidate;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_NotFormat, (propertyDisplayName as DisplayName).Name);    
            }

            return isValid;
        }
        
        #endregion
    }
}
