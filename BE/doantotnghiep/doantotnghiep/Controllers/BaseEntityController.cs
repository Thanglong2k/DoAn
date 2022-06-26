using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Const;
using Core.Entities;
using Core.Interfaces.Service;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace doantotnghiep.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase
    {
        #region Field
        /// <summary>
        /// Đối tượng chứa thông tin lỗi
        /// </summary>

        protected ServiceResult ServiceResult;
        private IBaseService<TEntity> _baseService;
        #endregion
        #region Constructor
        /// <summary>
        /// Khởi tạo các thông tin cần thiết
        /// </summary>
        public BaseEntityController(IBaseService<TEntity> baseService)
        {

            ServiceResult = new ServiceResult();
            _baseService = baseService;
        }
        #endregion
        #region Methods

        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpGet]
        public IActionResult GetAll()
        {

            try
            {
                ServiceResult = _baseService.GetAll();               
                return Ok(ServiceResult);//StatusCode(200,"MISA")
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
        /// Lấy đối tượng theo Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpGet("{id}")]
        public virtual IActionResult GetById(Guid id)
        {
            try
            {
                ServiceResult = _baseService.GetById(id);
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
        [HttpGet("NewCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                ServiceResult = _baseService.GetNewCode();
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
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (25/7/2021)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                ServiceResult = _baseService.Delete(id);
                
                return Ok(ServiceResult);
               
            }

            catch (Exception e)
            {
                ServiceResult.Success = false;
                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = Const.MISACodeErrorException;
                return StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (25/7/2021)
        [HttpDelete("ids")]
        public IActionResult Delete([FromBody] List<Guid> ids)
        {
            try
            {
                ServiceResult = _baseService.Delete(ids);
                
                return Ok(ServiceResult);
                
            }

            catch (Exception e)
            {
                ServiceResult.Success = false;

                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = Const.MISACodeErrorException;
               
                return StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// Thêm mới đối tượng2
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// 200 - thành công
        /// 201 - thêm thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpPost]
        public virtual IActionResult Post([FromBody] TEntity entity)
        {

            try
            {
                ServiceResult = _baseService.Add(entity);    
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
        /// cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng đối tượng</param>
        /// <param name="id">Id đối tượng</param>
        /// <returns>
        /// 200 - thành công
        /// 201 - thêm thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpPut("id")]
        public virtual IActionResult Update([FromBody] TEntity entity, Guid id)
        {

            try
            {
                ServiceResult = _baseService.Update(entity, id);   
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
        #endregion
    }
}
