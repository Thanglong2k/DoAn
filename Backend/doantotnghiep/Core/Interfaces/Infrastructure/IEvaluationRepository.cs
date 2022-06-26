
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
    public interface IEvaluationRepository:IBaseRepository<Evaluation>
    {
        /// <summary>
        /// Lấy chi tiết số lượng của từng mức đánh giá của sản phẩm
        /// </summary>
        /// <param name="productID">ID sản phẩm</param>
        /// <returns></returns>
        public IEnumerable<ProductRatingQuantity> GetProductRatingDetail(Guid productID);
        /// <summary>
        /// Lấy danh sách đánh giá theo sản phẩm
        /// </summary>
        /// <param name="productID">ID sản phẩm</param>
        /// <returns></returns>
        public OutputFilterPaging<Evaluation> GetEvaluationByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize);
        public OutputFilterPaging<EvaluationProductCustomer> GetEvaluations(string? queryText, int pageIndex, int pageSize);
    }
}
