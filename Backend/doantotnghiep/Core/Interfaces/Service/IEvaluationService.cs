using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IEvaluationService:IBaseService<Evaluation>
    {
        /// <summary>
        /// Lấy chi tiết số lượng của từng mức đánh giá của sản phẩm
        /// </summary>
        /// <param name="productID">ID sản phẩm</param>
        /// <returns></returns>
        ServiceResult GetProductRatingDetail(Guid productID);
        /// <summary>
        /// Lấy danh sách đánh giá theo sản phẩm
        /// </summary>
        /// <param name="productID">ID sản phẩm</param>
        /// <returns></returns>
        ServiceResult GetEvaluationByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize);
        ServiceResult GetEvaluations(string? queryText, int pageIndex, int pageSize);
    }
}
