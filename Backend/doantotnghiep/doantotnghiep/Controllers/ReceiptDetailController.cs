using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Const;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Cors;

namespace doantotnghiep.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/v1/receiptdetails")]
    [ApiController]
    public class ReceiptDetailController : BaseEntityController<ReceiptDetail>
    {
        #region Declare
        IReceiptDetailService _receiptDetailService;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public ReceiptDetailController(IReceiptDetailService receiptDetailService) : base(receiptDetailService)
        {
            _receiptDetailService = receiptDetailService;
        }
        #endregion
        #region Method
        [HttpGet("receipt-detail")]
        public IActionResult GetReceiptDetailByReceiptID(Guid receiptID,int pageIndex, int pageSize)
        {
            try
            {
                ServiceResult = _receiptDetailService.GetReceiptDetailByReceiptID(receiptID, pageIndex, pageSize);
                return StatusCode(200, ServiceResult);
            }
            catch(Exception e)
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
