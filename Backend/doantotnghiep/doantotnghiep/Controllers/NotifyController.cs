using Core.Const;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace doantotnghiep.Controllers
{
    [Route("api/v1/notify")]
    [ApiController]
    public class NotifyController : BaseEntityController<Notify>
    {
        #region Declare
        INotifyService _notifyService;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public NotifyController(INotifyService notifyService) : base(notifyService)
        {
            _notifyService = notifyService;
        }
        #endregion
        #region Method
        [HttpGet("notify")]
        public IActionResult GetNotifyList()
        {
            try
            {
                ServiceResult = _notifyService.GetNotifyList();
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
        [HttpGet("not-view-notify-total")]
        public IActionResult GetNotifyNotViewTotal()
        {
            try
            {
                ServiceResult = _notifyService.GetNotifyNotViewTotal();
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
        [HttpPut("update-read")]
        public IActionResult UpdateIsRead([FromQuery] Guid notifyID)
        {
            try
            {
                ServiceResult = _notifyService.UpdateIsRead(notifyID);
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
        [HttpPut("update-view")]
        public IActionResult UpdateIsView()
        {
            try
            {
                ServiceResult = _notifyService.UpdateIsView();
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
        #endregion
    }
}
