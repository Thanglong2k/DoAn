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
    [Route("api/v1/receipts")]
    [ApiController]
    public class ReceiptController : BaseEntityController<Receipt>
    {
        #region Declare
        IReceiptService _receiptService;
        IWebHostEnvironment _webHostEnvironment;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public ReceiptController(IReceiptService receiptService, IWebHostEnvironment webHostEnvironment) : base(receiptService)
        {
            _receiptService = receiptService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy thông tin đơn hàng theo số điện thoại của khách hàng
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("receipt-by-phonenumber")]
        public IActionResult GetReceiptByPhoneNumber(string phoneNumber)
        {
            try
            {
                ServiceResult = _receiptService.GetReceiptByPhoneNumber(phoneNumber);
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
        /// Lấy thông tin phiếu nhập theo id
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("id/{id}")]
        public IActionResult GetReceiptByID(Guid id)
        {
            try
            {
                ServiceResult = _receiptService.GetReceiptByID(id);
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
        
        [HttpGet("receipt-paging")]
        public IActionResult GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                ServiceResult = _receiptService.GetAllPaging(pageIndex, pageSize, queryText, startTime, endTime);
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
        /// Thêm mới Receipt
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="Receipt">đối tượng Receipt</param>
        /// <returns>Trạng thái request</returns>
        [HttpPost("insert")]
        public IActionResult Post([FromBody] ReceiptInfo receiptInfo)
        {
            try
            {
                
                ServiceResult = _receiptService.Add(receiptInfo);
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
        /// Cập nhật thông tin Receipt
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="Receipt">đối tượng Receipt</param>
        /// <param name="id">id Receipt</param>
        /// <returns>Trạng thái request</returns>
        [HttpPut("update")]
        public IActionResult Update([FromBody] Receipt receipt, Guid id)
        {

            try
            {
                /*JavaScriptSerializer js = new JavaScriptSerializer();*/
                //Receipt.SubjectList = JsonConvert.DeserializeObject<List<Subject>>(Receipt.Subjects);
                //Receipt.CategoryList = JsonConvert.DeserializeObject<List<Category>>(Receipt.Categorys);
                //Receipt Receipt = new Receipt();

                                                                                  
                /*else if(Receipt.Image!=null)
                {
                    
                    var image = Receipt.Image.Split("/");
                    Receipt.Image = image[image.Length - 1];
                }*/
                ServiceResult = _receiptService.Update(receipt, id);
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
                ServiceResult = _receiptService.PurchasedHistory(phoneNumber);
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
        [HttpGet("suplier-receipt")]
        public IActionResult GetCustomerOrder(Guid receiptID)
        {
            try
            {
                ServiceResult = _receiptService.GetSuplierReceipt(receiptID);
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
