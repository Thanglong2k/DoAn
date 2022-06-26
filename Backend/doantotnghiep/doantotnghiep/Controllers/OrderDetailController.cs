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
    [Route("api/v1/orderdetails")]
    [ApiController]
    public class OrderDetailController : BaseEntityController<OrderDetail>
    {
        #region Declare
        IOrderDetailService _orderDetailService;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public OrderDetailController(IOrderDetailService orderDetailService) : base(orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        #endregion
        #region Method
        [HttpGet("order-detail")]
        public IActionResult GetOrderDetailByOrderID(Guid orderID,int pageIndex, int pageSize)
        {
            try
            {
                ServiceResult = _orderDetailService.GetOrderDetailByOrderID(orderID, pageIndex, pageSize);
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
