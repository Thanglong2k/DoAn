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
    [Route("api/v1/supliers")]
    [ApiController]
    public class SuplierController : BaseEntityController<Suplier>
    {
        #region Declare
        ISuplierService _suplierService;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public SuplierController(ISuplierService suplierService) : base(suplierService)
        {
            _suplierService = suplierService;
        }
        #endregion
        #region Method
        [HttpGet("suplier-by-phonenumber")]
        public IActionResult GetSuplierByPhoneNumber(string phoneNumber)
        {
            ServiceResult = _suplierService.GetSuplierByPhoneNumber(phoneNumber);
            return Ok(ServiceResult);
        }
        #endregion
    }
}
