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
    [Route("api/v1/orders")]
    [ApiController]
    public class OrderController : BaseEntityController<Order>
    {
        #region Declare
        IOrderService _orderService;
        IWebHostEnvironment _webHostEnvironment;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public OrderController(IOrderService orderService, IWebHostEnvironment webHostEnvironment) : base(orderService)
        {
            _orderService = orderService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy thông tin đơn hàng theo số điện thoại của khách hàng
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("order-by-phonenumber")]
        public IActionResult GetOrderByPhoneNumber(string phoneNumber)
        {
            try
            {
                ServiceResult = _orderService.GetOrderByPhoneNumber(phoneNumber);
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
        /// Kiểm tra khách hàng đã mua sản phẩm chưa
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("check-purchased-product")]
        public IActionResult CheckCustomerPurchasedProduct(string phoneNumber, Guid productAttributeID)
        {
            try
            {
                ServiceResult = _orderService.CheckCustomerPurchasedProduct(phoneNumber, productAttributeID);
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
        [HttpGet("order-paging")]
        public IActionResult GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime, int status)
        {
            try
            {
                ServiceResult = _orderService.GetAllPaging(pageIndex, pageSize, queryText, startTime, endTime, status);
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
        /// Thêm mới Order
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="Order">đối tượng Order</param>
        /// <returns>Trạng thái request</returns>
        [HttpPost("insert")]
        public IActionResult Post([FromBody] OrderInfo orderInfo)
        {
            try
            {
                
                ServiceResult = _orderService.Add(orderInfo);
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
        /// Cập nhật thông tin Order
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="Order">đối tượng Order</param>
        /// <param name="id">id Order</param>
        /// <returns>Trạng thái request</returns>
        [HttpPut("update")]
        public IActionResult Update([FromBody] Order order, Guid id)
        {

            try
            {
                /*JavaScriptSerializer js = new JavaScriptSerializer();*/
                //Order.SubjectList = JsonConvert.DeserializeObject<List<Subject>>(Order.Subjects);
                //Order.CategoryList = JsonConvert.DeserializeObject<List<Category>>(Order.Categorys);
                //Order Order = new Order();

                                                                                  
                /*else if(Order.Image!=null)
                {
                    
                    var image = Order.Image.Split("/");
                    Order.Image = image[image.Length - 1];
                }*/
                ServiceResult = _orderService.Update(order, id);
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
        [HttpPut("update-imei")]
        public IActionResult UpdateIMEIByOrderID([FromBody] string imeis, Guid orderDetailID)
        {

            try
            {
                
                ServiceResult = _orderService.UpdateIMEIByOrderID(imeis, orderDetailID);
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
        [HttpGet("purchased-history")]
        public IActionResult PurchasedHistory(string phoneNumber)
        {
            try
            {
                ServiceResult = _orderService.PurchasedHistory(phoneNumber);
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
        }[HttpGet("follow-order")]
        public IActionResult FollowOrder(string phoneNumber, int status=-1)
        {
            try
            {
                ServiceResult = _orderService.FollowOrder(phoneNumber, status);
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
        [HttpGet("customer-order")]
        public IActionResult GetCustomerOrder(Guid orderID)
        {
            try
            {
                ServiceResult = _orderService.GetCustomerOrder(orderID);
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
