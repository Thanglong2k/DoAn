using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Const;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Cors;
using Core.Extensions;

namespace doantotnghiep.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : BaseEntityController<Category>
    {
        #region Declare
        ICategoryService _categoryService;
        IWebHostEnvironment _webHostEnvironment;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public CategoryController(ICategoryService categoryService, IWebHostEnvironment webHostEnvironment) : base(categoryService)
        {
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Method
        [HttpGet]
        public override IActionResult GetAll()
        {
            try
            {
                var projectPath = _webHostEnvironment.ContentRootPath;
                projectPath = String.Format("{0}://{1}{2}/Images/", Request.Scheme, Request.Host, Request.PathBase);
                ServiceResult = _categoryService.GetAllCategories(projectPath);
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
        /// <summary>
        /// Lấy Category theo Id
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="id">Id teacehr</param>
        /// <returns>Thông tin Category</returns>
        /*[HttpGet("{id}")]
        public override IActionResult GetById(Guid id)
        {
            try
            {
                var projectPath = _webHostEnvironment.ContentRootPath;
                projectPath = String.Format("{0}://{1}{2}/Images/", Request.Scheme, Request.Host, Request.PathBase);
                ServiceResult = _CategoryService.GetById(id, projectPath);
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
        }*/
        /// <summary>
        /// Thêm mới Category
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="Category">đối tượng Category</param>
        /// <returns>Trạng thái request</returns>
        [HttpPost]
        public override async Task<IActionResult> Post([FromBody] Category category)
        {
            try
            {
                if (category.ImageFile != null)
                {

                    category.Image = SaveImage(category.ImageFile, category.CategoryName);
                }
                category.Meta = category.CategoryName.ToLower().Replace(" ", "-").NonUnicode();
                ServiceResult = _categoryService.Add(category);
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
        /// Cập nhật thông tin Category
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="Category">đối tượng Category</param>
        /// <param name="id">id Category</param>
        /// <returns>Trạng thái request</returns>
        [HttpPut("id")]
        public override IActionResult Update([FromBody] Category category, Guid id)
        {

            try
            {
                if (category.ImageFile != null)
                {

                    category.Image = SaveImage(category.ImageFile, category.CategoryName);
                }
                category.Meta = category.CategoryName.ToLower().Replace(" ", "-").NonUnicode();
                ServiceResult = _categoryService.Update(category, id);
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
        /// thiết lập tên ảnh và lưu ảnh vào project
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="files">file ảnh</param>
        /// <param name="CategoryCode">Mã Category</param>
        /// <returns>tên ảnh</returns>
        [NonAction]
        public string SaveImage(IFormFile files, string categoryName)
        {

            //string Image = new String(Path.GetFileNameWithoutExtension(files.FileName).Take(10).ToArray()).Replace(' ', '-');
            string Image = categoryName + DateTime.Now.ToString("yymmddssfff") + Path.GetExtension(files.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Image);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                files.CopyTo(fileStream);
            }
            return Image;

        }
        #endregion
    }
}
