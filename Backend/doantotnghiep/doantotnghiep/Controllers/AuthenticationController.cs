using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace doantotnghiep.Controllers
{
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
		private readonly IJWTManagerRepository _jWTManager;
		public AuthenticationController(IJWTManagerRepository jWTManager)
		{
			this._jWTManager = jWTManager;
		}
		[HttpPost("authenticate")]
		public IActionResult Authenticate(Account usersdata)
		{
			var token = _jWTManager.Authenticate(usersdata);

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(token);
		}
	}
}
