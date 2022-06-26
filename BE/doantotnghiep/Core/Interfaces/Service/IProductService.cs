using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IProductService:IBaseService<Product>
    {
        ServiceResult GetById(Guid ProductId, string projectPath);
        /// <summary>
        /// Lọc và phân trang
        /// </summary>
        /// <param name="employeeFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        ServiceResult GetFilter(string ProductFilter, Guid? combinationSubjectId);
        /// <summary>
        /// kiểm tra Product tồn tại trong combinationsubject
        /// CreatedBy:TTLONG(19/09/2021)
        /// </summary>
        /// <param name="combinationSubjectId">id tổ bộ môn</param>
        /// <returns>trạng thái request</returns>
        ServiceResult CheckExistsCombinationSubject(Guid combinationSubjectId);
    }
}
