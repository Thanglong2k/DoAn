using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Const;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Cors;
using Firebase.Auth;
using Firebase.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Core.Model;

namespace doantotnghiep.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/v1/products")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    /*[Authorize]*/
    public class ProductController : BaseEntityController<Product>
    {
        #region Declare
        IProductService _productService;
        IWebHostEnvironment _webHostEnvironment;
        ServiceResult ServiceResult = new ServiceResult();

        string apiKey = "AIzaSyAPIncDBJN-j0u8UGOqRD8DdfARTPdrtpc";
        string AuthEmail = "thanglong2k@gmail.com";
        string AuthPassword = "thanglong123456";
        string Bucket = "doantotnghiep-c1e69.appspot.com";
        #endregion
        #region Constructor
        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment) : base(productService)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Method
        [HttpGet]
        public override IActionResult GetAll()
        {
            try
            {
                ServiceResult = _productService.GetAllProducts();
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
        [HttpGet("product-paging")]
        public override IActionResult GetAllPaging(int pageIndex, int pageSize, string? queryText)
        {
            try
            {
                ServiceResult = _productService.GetAllProductsPaging(pageIndex, pageSize, queryText);
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
        /// Danh sách sản phẩm mới
        /// </summary>
        /// <param name="productNumber">Số sản phẩm lấy ra</param>
        /// <returns></returns>
        [HttpGet("new-product")]
        public IActionResult GetNewProducts(int productNumber)
        {
            try
            {
                ServiceResult = _productService.GetNewProducts(productNumber);
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
        [HttpGet("new-product-post")]
        public IActionResult GetNewProductPosts(Guid productID, int pageIndex, int pageSize)
        {
            try
            {
                ServiceResult = _productService.GetNewProductPosts(productID, pageIndex, pageSize);
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
        /// Danh sách sản phẩm bán chạy nhất trong tháng
        /// </summary>
        /// <param name="productNumber">Số sản phẩm lấy ra</param>
        /// <param name="stage">Khoảng thời gian</param>
        /// <returns></returns>
        [HttpGet("best-selling-product")]
        public IActionResult GetBestsellingProducts(int productNumber, int stage)
        {
            try
            {
                ServiceResult = _productService.GetBestSellingProducts(productNumber,stage);
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
        /// Lấy danh sách sản phẩm liên quan thoe hãng sản xuất
        /// </summary>
        /// <param name="productNumber">Số sản phẩm</param>
        /// <param name="manufacturerID">ID hãng sản xuất</param>
        /// <returns></returns>
        [HttpGet("related-product")]
        public IActionResult GetRelatedProducts(int productNumber, Guid manufacturerID, Guid productID)
        {
            try
            {

                ServiceResult = _productService.GetRelatedProducts( productNumber, manufacturerID, productID);
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
        [HttpGet("product-attribute-id")]
        public IActionResult GetProductAttributeByID(Guid productAttributeID)
        {
            try
            {

                ServiceResult = _productService.GetProductAttributeByID(productAttributeID);
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
        /// Lấy danh sách sản phẩm gợi ý so sánh
        /// </summary>
        /// <param name="price">Giá</param>
        /// <param name="manufacturerID">ID hãng sản xuất</param>
        /// <returns></returns>
        [HttpGet("recommand-product")]
        public IActionResult GetRecommandProducts(Guid manufacturerID, Guid productID, decimal price)
        {
            try
            {
                ServiceResult = _productService.GetRecommandProducts(manufacturerID, productID, price);
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
        /// Danh sách sản phẩm bán theo danh mục
        /// </summary>
        /// <param name="productNumber">Số sản phẩm lấy ra</param>
        /// <param name="stage">Khoảng thời gian</param>
        /// <returns></returns>
        [HttpPost("products-by-category")]
        public IActionResult GetProductsByCategory(Guid? categoryID,string? queryText, [FromBody] List<string>? filter, int sortBy,int pageIndex, int pageSize)
        {
            try 
            {
                ServiceResult = _productService.GetProductsByCategory(categoryID, queryText, filter, sortBy, pageIndex, pageSize);
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
        [HttpGet("product-attribute")]
        public IActionResult GetProductAttributeAndProduct(string? queryText, int? pageIndex, int? pageSize)
        {
            try 
            {
                ServiceResult = _productService.GetProductAttributeAndProduct(queryText, pageIndex, pageSize);
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
                ServiceResult = _productService.GetById(id);
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
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] ProductAdd productAdd)
        {
            try
            {
                var imageFullPath = new List<string>();
                if (productAdd.AvatarFile != null)
                {

                    productAdd.Avatar = await SaveImage(productAdd.AvatarFile, productAdd.ProductName);
                }
                if (productAdd.ImageFile!= null&&productAdd.ImageFile.Count>0)
                {
                    var imageFiles = new List<string>();
                    
                    var imageInfo=await SaveImageList(productAdd.ImageFile, productAdd.ProductName);
                    imageFiles = imageInfo.ImageList;
                    imageFullPath = imageInfo.ImageFullPath;


                    productAdd.Image = imageFiles;
                }
                //convert data từ JSON về dạng đúng chuẩn
                var productAttributesDto = JsonConvert.DeserializeObject<List<ProductAttributeDto>>(productAdd.ProductAttributeList);
                productAttributesDto.ForEach(x => {
                    if (x.RangeDate != null)
                    {
                        if (x.RangeDate.StartDate != null)
                        {

                            x.PromotionStart = x.RangeDate.StartDate;
                        }
                        if (x.RangeDate.StartDate != null)
                        {

                            x.PromotionEnd = x.RangeDate.EndDate;
                        }
                    }


            });
                var productAttributes = new List<ProductAttribute>();
                //convert từ 1 kiểu Class sang thành object
                var objectList=productAttributesDto.Cast<object>().ToList();
                //tự động map dữ liệu giữa 2 Object
                productAdd.ProductAttributes = Extensions.MapList(objectList, productAttributes);

                if (productAdd.AvatarFile != null)
                {
                    FileStream fs;
                    FileStream ms;
                    //xử lý lưu ảnh vào firebase
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", productAdd.Avatar);
                    ms = new FileStream(imagePath, FileMode.Open);
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                    var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
                    // you can use CancellationTokenSource to cancel the upload midway
                    var cancellation = new CancellationTokenSource();

                    var task = new FirebaseStorage(
                        Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                        .Child("receipts")
                        .Child("test")
                        .Child(productAdd.Avatar)
                        .PutAsync(ms, cancellation.Token);
                    productAdd.Avatar = await task;
                    //hủy truy cập
                    ms.Dispose();
                    //xóa ảnh trong project
                    System.IO.File.Delete(imagePath);



                }
                    productAdd.Image = imageFullPath;
                
                
                var product = Extensions.Map(productAdd, new Product());
                if (productAdd.Image != null)
                {

                    product.Image=String.Join(",", productAdd.Image);
                }
                product.ReleaseTime = JsonConvert.DeserializeObject<DateTime>(productAdd.ReleaseTime);
                product.Meta = product.ProductName.ToLower().Replace(" ", "-").NonUnicode();
                var productPriceMin = product.ProductAttributes.OrderBy(x => x.Price);
                product.Price = productPriceMin.ToList()[0].Price;
                ServiceResult = _productService.Add(product);

                //task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
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
        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ProductAdd productAdd)
        {

            try
            {
                var imageFullPath = new List<string>();
                if (productAdd.AvatarFile != null)
                {

                    productAdd.Avatar = await SaveImage(productAdd.AvatarFile, productAdd.ProductName);
                }
                if (productAdd.ImageFile!=null && productAdd.ImageFile.Count > 0)
                {

                    var imageFiles = new List<string>();

                    var imageInfo = await SaveImageList(productAdd.ImageFile, productAdd.ProductName);
                    imageFiles = imageInfo.ImageList;
                    imageFullPath = imageInfo.ImageFullPath;


                    productAdd.Image.AddRange(imageFiles);

                }
                //convert data từ JSON về dạng đúng chuẩn
                var productAttributesDto = JsonConvert.DeserializeObject<List<ProductAttributeDto>>(productAdd.ProductAttributeList);
                productAttributesDto.ForEach(x => {
                if (x.RangeDate != null)
                    {
                        if (x.RangeDate.StartDate != null)
                        {

                            x.PromotionStart = x.RangeDate.StartDate;
                        }
                        if (x.RangeDate.StartDate != null)
                        {

                            x.PromotionEnd = x.RangeDate.EndDate;
                        }
                    }
                    
                    
                });
                var productAttributes = new List<ProductAttribute>();
                //convert từ 1 kiểu Class sang thành object
                var objectList = productAttributesDto.Cast<object>().ToList();
                //tự động map dữ liệu giữa 2 Object
                productAdd.ProductAttributes = Extensions.MapList(objectList, productAttributes);


                FileStream fs;
                FileStream ms;
                var imagePath = "";
                //xử lý lưu ảnh vào firebase
                if (productAdd.AvatarFile != null)
                {
                    imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", productAdd.Avatar);
                    ms = new FileStream(imagePath, FileMode.Open);
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                    var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
                    // you can use CancellationTokenSource to cancel the upload midway
                    var cancellation = new CancellationTokenSource();

                    var task = new FirebaseStorage(
                        Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                        .Child("receipts")
                        .Child("test")
                        .Child(productAdd.Avatar)
                        .PutAsync(ms, cancellation.Token);
                    productAdd.Avatar = await task;
                    //hủy truy cập
                    ms.Dispose();
                    //xóa ảnh trong project
                    System.IO.File.Delete(imagePath);
                }
                
                
                
               
                

                var product = Extensions.Map(productAdd, new Product());
                
                    product.Image = String.Join(",", productAdd.Image);
                
                /*productAdd.ReleaseTime = String.Concat("\"", productAdd.ReleaseTime,"\"");*/
                product.ReleaseTime = JsonConvert.DeserializeObject<DateTime>(productAdd.ReleaseTime);
                product.Meta = product.ProductName.ToLower().Replace(" ", "-").NonUnicode();
                var productPriceMin = product.ProductAttributes.OrderBy(x => x.Price);
                product.Price = productPriceMin.ToList()[0].Price;
                ServiceResult = _productService.Update(product,id);

                //task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
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
        /// Lọc và phân trang
        /// </summary>
        /// <param name="ProductFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="combinationSubjectId">id tổ bộ môn</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        [HttpGet("ProductFilter")]

        public IActionResult GetFilter(string productFilter, Guid? combinationSubjectId)
        {
            try
            {
                productFilter = productFilter == null ? "" : productFilter;
                ServiceResult = _productService.GetFilter(productFilter, combinationSubjectId);
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
        [HttpGet("product-detail-paging")]
        public IActionResult GetProductDetailPaging(int pageIndex, int pageSize, string? queryText, Guid productID)
        {
            try
            {
                ServiceResult = _productService.GetProductDetailPaging(pageIndex, pageSize, queryText, productID);
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
        [HttpGet("warrenty-lookup")]
        public IActionResult GetWarrentyLookup(string imei)
        {
            try
            {
                ServiceResult = _productService.GetWarrentyLookup(imei);
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
        /// thiết lập tên ảnh và lưu ảnh vào project
        /// CreatedBy:TTLONG(18/09/2021)
        /// </summary>
        /// <param name="files">file ảnh</param>
        /// <param name="ProductCode">Mã Product</param>
        /// <returns>tên ảnh</returns>
        [NonAction]
        public async Task<string> SaveImage(IFormFile files, string productName)
        {

            //string Image = new String(Path.GetFileNameWithoutExtension(files.FileName).Take(10).ToArray()).Replace(' ', '-');
            string Image = productName + DateTime.Now.ToString("yymmddssfff") + Path.GetExtension(files.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Image);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                files.CopyTo(fileStream);
                fileStream.Dispose();
            }
            
            return Image;

        }
        public async Task<SaveImageInfo> SaveImageList(List<IFormFile> files, string productName)
        {
            var imageFullPath = new List<string>();
            var imageList = new List<string>();
            foreach (var item in files)
            {
                //string Image = new String(Path.GetFileNameWithoutExtension(files.FileName).Take(10).ToArray()).Replace(' ', '-');
                string Image = productName + DateTime.Now.ToString("yymmddssfff") + Path.GetExtension(item.FileName);
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Image);
                await using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    item.CopyTo(fileStream);
                    
                    fileStream.Dispose();
                }
                await using (var fileStream = new FileStream(imagePath, FileMode.Open))
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                    var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
                    // you can use CancellationTokenSource to cancel the upload midway
                    var cancellation = new CancellationTokenSource();

                    var task = new FirebaseStorage(
                        Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                        })
                        .Child("receipts")
                        .Child("test")
                        .Child(Image)
                        .PutAsync(fileStream, cancellation.Token);
                    imageFullPath.Add(await task);
                    fileStream.Dispose();
                }
                    imageList.Add(Image);
                

            }
            var imageInfo = new SaveImageInfo();
            imageInfo.ImageList = imageList;
            imageInfo.ImageFullPath = imageFullPath;
            return imageInfo;

        }
        #endregion
    }
}
