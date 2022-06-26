using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IProductService:IBaseService<Product>
    {
        /// <summary>
        /// Lấy danh sách tất cả sản phẩm
        /// </summary>
        /// <param name="projectPath">Nguồn lưu ảnh</param>
        /// <returns></returns>
        ServiceResult GetAllProducts();
        /// <summary>
        /// Lấy danh sách tất cả sản phẩm
        /// </summary>
        /// <param name="projectPath">Nguồn lưu ảnh</param>
        /// <returns></returns>
        ServiceResult GetAllProductsPaging(int pageIndex, int pageSize, string queryText);
        ServiceResult GetProductDetailPaging(int pageIndex, int pageSize, string? queryText, Guid productID);
        ServiceResult GetWarrentyLookup(string imei);
        /// <summary>
        /// Lấy dánh sách sản phẩm mới
        /// </summary>
        /// <param name="projectPath">nguồn lưu ảnh</param>
        /// <param name="productNumber">số sản phẩm lấy ra</param>
        /// <returns></returns>
        ServiceResult GetNewProducts(int productNumber);
        ServiceResult GetNewProductPosts(Guid productID, int pageIndex, int pageSize);
        /// <summary>
        /// Lấy dánh sách sản phẩm bán chạy nhất
        /// </summary>
        /// <param name="projectPath">nguồn lưu ảnh</param>
        /// <param name="productNumber">số sản phẩm lấy ra</param>
        /// <param name="stage">khoảng thời gian</param>
        /// <returns></returns>
        ServiceResult GetBestSellingProducts(int productNumber,int stage);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="productNumber"></param>
        /// <param name="manufacturerID"></param>
        /// <returns></returns>
        ServiceResult GetRelatedProducts(int productNumber,Guid manufacturerID, Guid productID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPath"></param>
        /// <param name="price"></param>
        /// <param name="manufacturerID"></param>
        /// <returns></returns>
        ServiceResult GetRecommandProducts(Guid manufacturerID, Guid productID, decimal price);
        ServiceResult GetProductAttributeByID(Guid productAttributeID);
        
        /// <summary>
        /// Lấy danh sách sản phẩm theo danh mục
        /// </summary>
        /// <param name="categoryID">ID danh mục</param>
        /// <param name="sortBy">kiểu sắp xếp</param>
        /// <param name="pageIndex">trang hiện tại</param>
        /// <param name="pageSize">số sản phẩm 1 trang</param>
        /// <returns></returns>
        ServiceResult GetProductsByCategory(Guid? categoryID, string? queryText, List<string>? filter, int sortBy, int pageIndex, int pageSize);
        /// <summary>
        /// Lấy danh sách sản phẩm theo từng attribute
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ServiceResult GetProductAttributeAndProduct(string? queryText, int? pageIndex, int? pageSize);
        ServiceResult GetById(Guid productId);

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
