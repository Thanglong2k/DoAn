using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Const;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using Core.Services;
using Core.Interfaces;

namespace doantotnghiep.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/v1/comments")]
    [ApiController]
    public class CommentController : BaseEntityController<Comment>
    {
        #region Declare
        ICommentService _commentService;
        ICommentRepository _commentRepo;
        private readonly IHubContext<ProductHub, IProductHub> _hubContext;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public CommentController(ICommentService commentService, ICommentRepository commentRepo, IHubContext<ProductHub, IProductHub> hubContext) : base(commentService)
        {
            _commentService = commentService;
            _commentRepo = commentRepo;
            _hubContext = hubContext;
        }
        #endregion
        #region Method

        [HttpPost("add-comment")]
        public async Task<IActionResult> Post([FromBody] Comment comment)
        {

            try
            {
                ServiceResult = _commentService.Add(comment);
                var commentCustomer = _commentRepo.GetCommentByProductAttributeID(comment.ProductAttributeID, 1, 5);
                await _hubContext
                .Clients
                .All
                .NewCommentAddedToProduct(commentCustomer.Data);
                var commentCustomerProduct = _commentRepo.GetComments(null, 1, 10);
                await _hubContext
                .Clients
                .All
                .NewCommentAddedToAdmin(commentCustomerProduct.Data);
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

        [HttpGet("comments-by-product")]
        public IActionResult GetCommentByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize)
        {
            try
            {
                ServiceResult = _commentService.GetCommentByProductAttributeID(productAttributeID, pageIndex, pageSize);
                return Ok(ServiceResult);

            }

            catch (Exception e)
            {
                ServiceResult.Success = false;

                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = Const.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, ServiceResult);
            }
        }

        [HttpGet("comments")]

        public IActionResult GetComments(string? queryText, int pageIndex, int pageSize)
        {
            try
            {
                ServiceResult = _commentService.GetComments(queryText, pageIndex, pageSize);
                return Ok(ServiceResult);

            }

            catch (Exception e)
            {
                ServiceResult.Success = false;

                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = Const.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, ServiceResult);
            }
        }
        #endregion
    }
}
