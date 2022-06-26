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
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomerController : BaseEntityController<Customer>
    {
        #region Declare
        ICustomerService _customerService;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public CustomerController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }
        #endregion
        #region Method
        [HttpGet("customer-by-phonenumber")]
        public IActionResult GetCustomerByPhoneNumber(string phoneNumber)
        {
            ServiceResult = _customerService.GetCustomerByPhoneNumber(phoneNumber);
            return Ok(ServiceResult);
        }
        #endregion
    }
}
