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
    [Route("api/v1/evaluations")]
    [ApiController]
    public class EvaluationController : BaseEntityController<Evaluation>
    {
        #region Declare
        IEvaluationService _evaluationService;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public EvaluationController(IEvaluationService evaluationService) : base(evaluationService)
        {
            _evaluationService = evaluationService;
        }
        #endregion
        #region Method
       
        [HttpGet("product-rating-detail")]
        public IActionResult GetProductRatingDetail(Guid productID)
        {
            try
            {
                ServiceResult = _evaluationService.GetProductRatingDetail(productID);
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
        [HttpGet("evaluations-by-product")]
        public IActionResult GetEvaluationByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize)
        {
            try
            {
                ServiceResult = _evaluationService.GetEvaluationByProductAttributeID(productAttributeID, pageIndex, pageSize);
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

        [HttpGet("evaluations")]

        public IActionResult GetEvaluations(string? queryText, int pageIndex, int pageSize)
        {
            try
            {
                ServiceResult = _evaluationService.GetEvaluations(queryText, pageIndex, pageSize);
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
