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
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : BaseEntityController<Product>
    {
        #region Declare
        IProductService _productService;
        IWebHostEnvironment _webHostEnvironment;
        ServiceResult ServiceResult = new ServiceResult();
        #endregion
        #region Constructor
        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment) : base(productService)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Method
        [HttpGet("new-product")]
        public IActionResult GetNewProducts()
        {
            try
            {
                var projectPath = _webHostEnvironment.ContentRootPath;
                projectPath = String.Format("{0}://{1}{2}/Images/", Request.Scheme, Request.Host, Request.PathBase);
                ServiceResult = _productService.GetNewProducts(projectPath);
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
        /// Lấy Product theo Id
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="id">Id teacehr</param>
        /// <returns>Thông tin Product</returns>
        [HttpGet("{id}")]
        public override IActionResult GetById(Guid id)
        {
            try
            {
                var projectPath = _webHostEnvironment.ContentRootPath;
                projectPath = String.Format("{0}://{1}{2}/Images/", Request.Scheme, Request.Host, Request.PathBase);
                ServiceResult = _productService.GetById(id, projectPath);
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
        /// Thêm mới Product
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="Product">đối tượng Product</param>
        /// <returns>Trạng thái request</returns>
        [HttpPost]
        public override IActionResult Post([FromForm] Product Product)
        {
            try
            {
                if (Product.ImageFile != null)
                {

                    Product.Image = SaveImage(Product.ImageFile, Product.ProductCode);
                }
                ServiceResult = _productService.Add(Product);
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
        /// Cập nhật thông tin Product
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="Product">đối tượng Product</param>
        /// <param name="id">id Product</param>
        /// <returns>Trạng thái request</returns>
        [HttpPut("id")]
        public override IActionResult Update([FromForm] Product Product, Guid id)
        {

            try
            {
                /*JavaScriptSerializer js = new JavaScriptSerializer();*/
                //Product.SubjectList = JsonConvert.DeserializeObject<List<Subject>>(Product.Subjects);
                //Product.DepartmentList = JsonConvert.DeserializeObject<List<Department>>(Product.Departments);
                //Product Product = new Product();

                if (Product.ImageFile != null)
                {

                    Product.Image = SaveImage(Product.ImageFile, Product.ProductCode);
                }
                /*else if(Product.Image!=null)
                {
                    
                    var image = Product.Image.Split("/");
                    Product.Image = image[image.Length - 1];
                }*/
                ServiceResult = _productService.Update(Product, id);
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
        /// Lọc và phân trang
        /// </summary>
        /// <param name="ProductFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="combinationSubjectId">id tổ bộ môn</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        [HttpGet("ProductFilter")]

        public IActionResult GetFilter(string ProductFilter, Guid? combinationSubjectId)
        {
            try
            {
                ProductFilter = ProductFilter == null ? "" : ProductFilter;
                ServiceResult = _productService.GetFilter(ProductFilter, combinationSubjectId);
                return Ok(ServiceResult);
            }

            catch (Exception e)
            {
                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = Const.MISACodeErrorException;

                return base.StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// kiểm tra Product tồn tại trong combinationsubject
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="combinationSubjectId">id tổ hợp môn</param>
        /// <returns>trạng thái request</returns>
        [HttpGet("existsCombinationSubject")]
        public IActionResult CheckExistsCombinationSubject(Guid combinationSubjectId)
        {
            try
            {
                ServiceResult = _productService.CheckExistsCombinationSubject(combinationSubjectId);
                return Ok(ServiceResult);
            }

            catch (Exception e)
            {
                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = Const.MISACodeErrorException;

                return base.StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// thiết lập tên ảnh và lưu ảnh vào project
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="files">file ảnh</param>
        /// <param name="ProductCode">Mã Product</param>
        /// <returns>tên ảnh</returns>
        [NonAction]
        public string SaveImage(IFormFile files, string ProductCode)
        {

            //string Image = new String(Path.GetFileNameWithoutExtension(files.FileName).Take(10).ToArray()).Replace(' ', '-');
            string Image = ProductCode + DateTime.Now.ToString("yymmddssfff") + Path.GetExtension(files.FileName);
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
