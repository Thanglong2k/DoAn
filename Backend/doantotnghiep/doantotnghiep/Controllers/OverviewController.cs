using Core.Const;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace doantotnghiep.Controllers
{
    [Route("api/v1/overview")]
    [ApiController]
    public class OverviewController : ControllerBase
    {
        #region Declare
        IOverviewService _overviewService;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public OverviewController(IOverviewService overviewService)
        {
            _overviewService = overviewService;
        }
        #endregion
        #region Method

        [HttpGet("turnover-total")]
        public IActionResult GetTurnoverTotal(int month)
        {
            try
            {

                ServiceResult = _overviewService.GetTurnoverTotal(month);
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
        [HttpGet("order-total")]
        public IActionResult GetOrderTotal(int month)
        {
            try
            {

                ServiceResult = _overviewService.GetOrderTotal(month);
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
        [HttpGet("receipt-total")]
        public IActionResult GetReceiptTotal(int month)
        {
            try
            {

                ServiceResult = _overviewService.GetReceiptTotal(month);
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
        [HttpGet("new-product-total")]
        public IActionResult GetNewProductTotal(int month)
        {
            try
            {

                ServiceResult = _overviewService.GetNewProductTotal(month);
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
        [HttpGet("monthly-turnover")]
        public IActionResult GetMonthlyTurnover()
        {
            try
            {

                ServiceResult = _overviewService.GetMonthlyTurnover();
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
        [HttpGet("quantity-sold-by-manufacturer")]
        public IActionResult GetQuantitySoldByManufacturer(int month)
        {
            try
            {

                ServiceResult = _overviewService.GetQuantitySoldByManufacturer(month);
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
        [HttpGet("quantity-order-cancelled")]
        public IActionResult GetQuantityOrderCancelled(int month)
        {
            try
            {

                ServiceResult = _overviewService.GetQuantityOrderCancelled(month);
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
        [HttpGet("best-selling")]
        public IActionResult GetProductBestSellingsInMonth(int month,int productNumber)
        {
            try
            {

                ServiceResult = _overviewService.GetProductBestSellingsInMonth(month,productNumber);
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
        [HttpGet("cancelled-order")]
        public IActionResult GetCancelledOrders(int month)
        {
            try
            {

                ServiceResult = _overviewService.GetCancelledOrders(month);
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
