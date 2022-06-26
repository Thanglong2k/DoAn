using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IPostService : IBaseService<Post>
    {

        /// <summary>
        /// Lấy danh sách comment theo sản phẩm
        /// </summary>
        /// <param name="productID">ID sản phẩm</param>
        /// <returns></returns>
        ServiceResult GetPostByProductID(Guid productID, int pageIndex, int pageSize);
        ServiceResult GetPostByID(Guid postID, bool admin);
        ServiceResult GetPosts(string? queryText, int pageIndex, int pageSize);
    }
}
