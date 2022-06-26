using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Const;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Cors;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Core.Model;

namespace doantotnghiep.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/v1/account")]
    [ApiController]
    public class AccountController : BaseEntityController<Account>
    {
        #region Declare
        IAccountService _accountService;
        IWebHostEnvironment _webHostEnvironment;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public AccountController(IAccountService accountService, IWebHostEnvironment webHostEnvironment) : base(accountService)
        {
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Method

        [HttpGet("login")]
        public IActionResult Login([FromQuery] Account account)
        {
            try
            {
                
                ServiceResult = _accountService.Login(account);
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
        [HttpGet("accountID")]
        public IActionResult GetAccountID([FromQuery] Account account)
        {
            try
            {

                ServiceResult = _accountService.GetAccountID(account);
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
        [HttpGet("account")]
        public IActionResult GetAccountByUserName(string userName)
        {
            try
            {

                ServiceResult = _accountService.GetAccountByUserName(userName);
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

        [HttpGet("check-exists")]
        public IActionResult CheckAccountExists(string userName)
        {
            try
            {

                ServiceResult = _accountService.CheckAccountExists(userName);
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
