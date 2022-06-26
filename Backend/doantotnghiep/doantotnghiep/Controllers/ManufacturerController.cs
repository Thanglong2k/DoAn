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
    [Route("api/v1/manufacturer")]
    [ApiController]
    public class ManufacturerController : BaseEntityController<Manufacturer>
    {
        #region Declare
        IManufacturerService _manufacturerService;
        IWebHostEnvironment _webHostEnvironment;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public ManufacturerController(IManufacturerService manufacturerService, IWebHostEnvironment webHostEnvironment) : base(manufacturerService)
        {
            _manufacturerService = manufacturerService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Method

        #endregion
    }
}
