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
    [Route("api/v1/subjects")]
    [ApiController]
    public class SubjectController : BaseEntityController<Subject>
    {
        #region Field
        /// <summary>
        /// Đối tượng chứa thông tin lỗi
        /// </summary>
        private ResponseError _responseError;
        private ISubjectService _subjectService;
        #endregion
        #region Constructor
        /// <summary>
        /// Khởi tạo các thông tin cần thiết
        /// </summary>
        public SubjectController(ISubjectService subjectService) : base(subjectService)
        {
            _responseError = new ResponseError();
            _subjectService = subjectService;
        }
        #endregion
    }
}
