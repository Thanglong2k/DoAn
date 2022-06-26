using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface ICommentService : IBaseService<Comment>
    {

        /// <summary>
        /// Lấy danh sách comment theo sản phẩm
        /// </summary>
        /// <param name="productID">ID sản phẩm</param>
        /// <returns></returns>
        ServiceResult GetCommentByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize);
        ServiceResult GetComments(string? queryText, int pageIndex, int pageSize);
    }
}
