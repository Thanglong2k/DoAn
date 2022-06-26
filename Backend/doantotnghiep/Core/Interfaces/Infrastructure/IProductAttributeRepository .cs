
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
    /// Interface Employee kết nối dữ liệu database
    /// </summary>
    /// CreatedBy: TTLONG (28/7/2021)
    public interface IProductAttributeRepository:IBaseRepository<ProductAttribute>
    {
        /// <summary>
        /// Lấy danh sách productAttributeIDs theo productID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public IEnumerable<Guid> GetProductAttributeIDsByProductID(Guid productID);
        public ProductAndProductAttribute GetProductAttributeByID(Guid productAttributeID);
        /// <summary>
        /// Xóa thuộc tính sản phẩm theo ID sản phẩm
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public int DeleteByProductID(Guid productID);
    }
}
