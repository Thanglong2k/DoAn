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
using Core.Extensions;

namespace doantotnghiep.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/v1/posts")]
    [ApiController]
    public class PostController : BaseEntityController<Post>
    {
        #region Declare
        IPostService _postService;
        IWebHostEnvironment _webHostEnvironment;
        ServiceResult ServiceResult = new ServiceResult();
        string apiKey = "AIzaSyAPIncDBJN-j0u8UGOqRD8DdfARTPdrtpc";
        string AuthEmail = "thanglong2k@gmail.com";
        string AuthPassword = "thanglong123456";
        string Bucket = "doantotnghiep-c1e69.appspot.com";
        #endregion
        #region Constructor
        public PostController(IPostService postService, IWebHostEnvironment webHostEnvironment) : base(postService)
        {
            _postService = postService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Method
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] PostAdd postAdd)
        {
            if (postAdd.AvatarFile != null)
            {

                postAdd.Avatar = await SaveImage(postAdd.AvatarFile, "");
                FileStream fs;
                FileStream ms;
                //xử lý lưu ảnh vào firebase
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", postAdd.Avatar);
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
                    .Child(postAdd.Avatar)
                    .PutAsync(ms, cancellation.Token);
                postAdd.Avatar = await task;
                //hủy truy cập
                ms.Dispose();
                //xóa ảnh trong project
                System.IO.File.Delete(imagePath);

            }
                var post = Extensions.Map(postAdd, new Post());

                ServiceResult = _postService.Add(post);

                //task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
                return StatusCode(201, ServiceResult);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] PostAdd postAdd, Guid id)
        {
            if (postAdd.AvatarFile != null)
            {

                postAdd.Avatar = await SaveImage(postAdd.AvatarFile, "");
                FileStream fs;
                FileStream ms;
                //xử lý lưu ảnh vào firebase
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", postAdd.Avatar);
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
                    .Child(postAdd.Avatar)
                    .PutAsync(ms, cancellation.Token);
                postAdd.Avatar = await task;
                //hủy truy cập
                ms.Dispose();
                //xóa ảnh trong project
                System.IO.File.Delete(imagePath);

            }
                var post = Extensions.Map(postAdd, new Post());

                ServiceResult = _postService.Update(post, id);

                //task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
                return StatusCode(200, ServiceResult);
        }
        [HttpGet("posts-by-product")]
        public IActionResult GetPostByProductID(Guid productID, int pageIndex, int pageSize)
        {
            try
            {
                ServiceResult = _postService.GetPostByProductID(productID, pageIndex, pageSize);
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
        [HttpGet("post-by-id")]
        public IActionResult GetPostByID(Guid postID, bool admin=false)
        {
            try
            {
                ServiceResult = _postService.GetPostByID(postID, admin);
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

        [HttpGet("posts")]

        public IActionResult GetPosts(string? queryText, int pageIndex, int pageSize)
        {
            try
            {
                ServiceResult = _postService.GetPosts(queryText, pageIndex, pageSize);
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
        #endregion
    }
}
