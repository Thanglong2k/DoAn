using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface ICategoryService:IBaseService<Category>
    {
        /// <summary>
        /// Lấy danh sách tất cả sản phẩm
        /// </summary>
        /// <param name="projectPath">Nguồn lưu ảnh</param>
        /// <returns></returns>
        ServiceResult GetAllCategories(string projectPath);
        ServiceResult GetCategoryIDByCategoryParentID(Guid categoryParentID);
    }
}
