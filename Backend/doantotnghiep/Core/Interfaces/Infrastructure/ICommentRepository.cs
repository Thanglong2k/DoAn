
using Core.Entities;
using Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface Danh mục kết nối dữ liệu database
    /// </summary>
    /// CreatedBy: TTLONG (28/7/2021)
    public interface ICommentRepository:IBaseRepository<Comment>
    {

        /// <summary>
        /// Lấy danh sách bình luận theo sản phẩm
        /// </summary>
        /// <param name="productID">ID sản phẩm</param>
        /// <returns></returns>
        public OutputFilterPaging<CustomerComment> GetCommentByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize);
        public OutputFilterPaging<CommentProductCustomer> GetComments(string? queryText, int pageIndex, int pageSize);
    }
}
