using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Const;
using Core.Entities;
using Core.Interfaces.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace doantotnghiep.Controllers
{
    [Route("api/v1/teachers")]
    [ApiController]
    public class TeacherController: BaseEntityController<Teacher>
    {
        #region Declare
        ITeacherService _teacherService;
        IWebHostEnvironment _webHostEnvironment;
        ServiceResult ServiceResult=new ServiceResult();
        #endregion
        #region Constructor
        public TeacherController(ITeacherService teacherService, IWebHostEnvironment webHostEnvironment) :base(teacherService)
        {
            _teacherService = teacherService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy teacher theo Id
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="id">Id teacehr</param>
        /// <returns>Thông tin teacher</returns>
        [HttpGet("{id}")]
        public override IActionResult GetById(Guid id)
        {
            try
            {
                var projectPath = _webHostEnvironment.ContentRootPath;
                projectPath = String.Format("{0}://{1}{2}/Images/", Request.Scheme,Request.Host,Request.PathBase);
                ServiceResult = _teacherService.GetById(id,projectPath);
                return Ok(ServiceResult);
                
            }

            catch (Exception e)
            {
                ServiceResult.Success = false;

                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = Const.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// Thêm mới teacher
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="teacher">đối tượng teacher</param>
        /// <returns>Trạng thái request</returns>
        [HttpPost]
        public override IActionResult Post([FromForm] Teacher teacher)
        {    
            try
            {
                if (teacher.ImageFile != null)
                {
                    
                    teacher.ImageName = SaveImage(teacher.ImageFile, teacher.TeacherCode);
                }
                ServiceResult = _teacherService.Add(teacher);
                return StatusCode(201, ServiceResult);
            }
            catch (Exception e)
            {
                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeErrorException;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.DevMsg = e.Message;
                return StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// Cập nhật thông tin teacher
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="teacher">đối tượng teacher</param>
        /// <param name="id">id teacher</param>
        /// <returns>Trạng thái request</returns>
        [HttpPut("id")]
        public override IActionResult Update([FromForm] Teacher teacher, Guid id)
        {

            try
            {
                /*JavaScriptSerializer js = new JavaScriptSerializer();*/
                teacher.SubjectList = JsonConvert.DeserializeObject<List<Subject>>(teacher.Subjects);
                teacher.DepartmentList = JsonConvert.DeserializeObject<List<Department>>(teacher.Departments);
                //Teacher teacher = new Teacher();

                if (teacher.ImageFile != null)
                {

                    teacher.ImageName = SaveImage(teacher.ImageFile, teacher.TeacherCode);
                }
                /*else if(teacher.ImageName!=null)
                {
                    
                    var image = teacher.ImageName.Split("/");
                    teacher.ImageName = image[image.Length - 1];
                }*/
                ServiceResult=_teacherService.Update(teacher, id);
                return StatusCode(200, ServiceResult);
            }
            catch (Exception e)
            {

                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeErrorException;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.DevMsg = e.Message;
                return StatusCode(500, ServiceResult);
            }

        }
        /// <summary>
        /// Lọc và phân trang
        /// </summary>
        /// <param name="teacherFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="combinationSubjectId">id tổ bộ môn</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        [HttpGet("teacherFilter")]

        public IActionResult GetFilter(string teacherFilter, Guid? combinationSubjectId)
        {
            try
            {
                teacherFilter = teacherFilter == null ? "" : teacherFilter;
                ServiceResult = _teacherService.GetFilter(teacherFilter, combinationSubjectId);
                return Ok(ServiceResult);
            }

            catch (Exception e)
            {
                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = Const.MISACodeErrorException;
                
                return base.StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// kiểm tra teacher tồn tại trong combinationsubject
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="combinationSubjectId">id tổ hợp môn</param>
        /// <returns>trạng thái request</returns>
        [HttpGet("existsCombinationSubject")]
        public IActionResult CheckExistsCombinationSubject(Guid combinationSubjectId)
        {
            try
            {
                ServiceResult = _teacherService.CheckExistsCombinationSubject(combinationSubjectId);
                return Ok(ServiceResult);
            }

            catch (Exception e)
            {
                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = Const.MISACodeErrorException;

                return base.StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// thiết lập tên ảnh và lưu ảnh vào project
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="files">file ảnh</param>
        /// <param name="teacherCode">Mã teacher</param>
        /// <returns>tên ảnh</returns>
        [NonAction]
        public string SaveImage(IFormFile files,string teacherCode)
        {
           
                //string imageName = new String(Path.GetFileNameWithoutExtension(files.FileName).Take(10).ToArray()).Replace(' ', '-');
                string imageName = teacherCode+DateTime.Now.ToString("yymmddssfff") + Path.GetExtension(files.FileName);
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imageName);
                using (var fileStream=new FileStream(imagePath, FileMode.Create))
                {
                    files.CopyTo(fileStream);
                }
                return imageName;
            
        }
        #endregion
    }
}
