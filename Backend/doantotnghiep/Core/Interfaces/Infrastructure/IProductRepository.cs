
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
        public Guid Add(Product product);
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true/false</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public bool CheckDuplicateEmployeeCode(string employeeCode);
        
        /// <summary>
        /// Lấy danh sách sản phẩm mới
        /// </summary>
        /// <param name="productNumber"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetNewProducts(int productNumber);
        public OutputFilterPaging<ProductPost> GetNewProductPosts(Guid productID, int pageIndex, int pageSize);
        public OutputFilterPaging<ProductProductDetail> GetProductDetailPaging(int pageIndex, int pageSize, string? queryText, Guid productID);
        public ProductWarrenty GetWarrentyLookup(string imei);
        /// <summary>
        /// Lấy danh sách sản phẩm bán chạy nhất
        /// </summary>
        /// <param name="productNumber"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        public IEnumerable<ProductAttribute> GetBestSellingProducts(int productNumber,int stage);
        public IEnumerable<ProductAttribute> GetProductAttributeByProductID(Guid productID);

        /// <summary>
        /// Lấy danh sách sản phẩm theo danh mục
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public OutputFilterPaging<Product> GetProductsByCategory(string whereClause, int sortBy, int pageIndex, int pageSize);
        /// <summary>
        /// Lấy danh sách sản phẩm gợi ý s\o sánh
        /// </summary>

        /// <param name="manufacturerID">ID hãng sản xuất</param>
        /// <param name="price">Giá</param>
        /// <returns></returns>
        public IEnumerable<Product> GetRecommandProducts( Guid manufacturerID, Guid productID, decimal price);
        /// <summary>
        /// Lấy danh sách sản phẩm cả attribute
        /// </summary>
        /// <param name="manufacturerID"></param>
        /// <param name="productID"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public OutputFilterPaging<ProductAndProductAttribute> GetProductAttributeAndProduct(string? queryText, int? pageIndex, int? pageSize);
        /// <summary>
        /// Lấy danh sách sản phẩm liên quan
        /// </summary>
        /// <param name="productNumber">Số lượng lấy ra</param>
        /// <param name="manufacturerID">Id hãng sản xuất</param>
        /// <param name="productID">id sán phẩm</param>
        /// <returns></returns>
        public IEnumerable<Product> GetRelatedProducts(int productNumber, Guid manufacturerID, Guid productID);
        /// <summary>
        /// Lấy thông tin sản phẩm theo sản phẩm thuộc tính
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Product GetProductByProductAttribute(Guid productID);


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
