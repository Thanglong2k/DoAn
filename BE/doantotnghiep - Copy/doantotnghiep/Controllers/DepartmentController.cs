using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doantotnghiep.Controllers
{
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentController : BaseEntityController<Department>
    {
        #region Field
        /// <summary>
        /// Đối tượng chứa thông tin lỗi
        /// </summary>
        private ResponseError _responseError;
        private IDepartmentService _departmentService;
        #endregion
        #region Constructor
        /// <summary>
        /// Khởi tạo các thông tin cần thiết
        /// </summary>
        public DepartmentController(IDepartmentService departmentService) : base(departmentService)
        {
            _responseError = new ResponseError();
            _departmentService = departmentService;
        }
        #endregion
    }
}
