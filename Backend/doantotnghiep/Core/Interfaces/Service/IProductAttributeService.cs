using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IProductAttributeService : IBaseService<ProductAttribute>
    {
        /// <summary>
        /// Lấy danh sách productAttributeID theo productID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        ServiceResult GetProductAttributeIDsByProductID(Guid productID);
    }
}
