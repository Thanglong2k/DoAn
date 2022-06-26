
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
    public interface IProductRepository:IBaseRepository<Product>
    {
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true/false</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public bool CheckDuplicateEmployeeCode(string employeeCode);


        /// <summary>
        /// Lọc dữ liệu
        /// </summary>
        /// <param name="employeeFilter">Dữ liệu tìm kiếm theo mã, tên</param>

        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        IEnumerable<Product> GetFilter(string ProductFilter, Guid? combinationSubjectId);
        public bool CheckExistsCombinationSubject(Guid combinationSubjectId);
        public Product GetProductByCreatedDate();
    }
}
