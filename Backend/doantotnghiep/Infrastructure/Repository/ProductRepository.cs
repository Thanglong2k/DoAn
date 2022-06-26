using Dapper;
using Microsoft.Extensions.Configuration;
using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public Guid Add(Product product)
        {
            var rowAffects = 0;
            var productID = Guid.Empty;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    //Xử lý các dữ liệu:
                    var parameters = MappingDbType(product);
                    parameters.Add($"ProductIDOut", dbType: DbType.Guid, direction: ParameterDirection.Output);
                    //THực thi commandText:

                    rowAffects = DbConnection.Execute($"Proc_InsertProduct", parameters, transaction: transaction, null, CommandType.StoredProcedure);
                    if (rowAffects > 0)
                    {
                        productID= parameters.Get<Guid>("ProductIDOut");

                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    var t = e.Message;

                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                    DbConnection.Close();
                }

            }

            //Trả vệ số bản ghi được thêm
            return productID;
        }
        public int Update(Product product,Guid id)
        {
            var rowAffects = 0;
            var productID = Guid.Empty;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    //Xử lý các dữ liệu:
                    var parameters = MappingDbType(product);
                    parameters.Add($"@ProductID", id);

                    //THực thi commandText:

                    rowAffects = DbConnection.Execute($"Proc_UpdateProduct", parameters, transaction: transaction, null, CommandType.StoredProcedure);
                    
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    var t = e.Message;

                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                    DbConnection.Close();
                }

            }

            //Trả vệ số bản ghi được thêm
            return rowAffects;
        }
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true/false</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public bool CheckDuplicateEmployeeCode(string employeeCode)
        {
          

            var sqlCommand = $"Select exists(SELECT EmployeeCode FROM Employee where EmployeeCode = @EmployeeCode)";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@EmployeeCode", employeeCode);

            //Lấy dữ liệu với Dapper:
            var isDuplicate = DbConnection.QueryFirstOrDefault<bool>(sql: sqlCommand, param: paramaters);
            

            return isDuplicate;
        }
        /// <summary>
        /// Lọc dữ liệu
        /// </summary>
        /// <param name="employeeFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public IEnumerable<Product> GetFilter(string ProductFilter, Guid? combinationSubjectId)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"ProductFilter", ProductFilter);
                parameters.Add($"CombinationSubjectId", combinationSubjectId);
            //Khởi tạo các CommandText:
            /*var entitites = DbConnection.Query<Product,CombinationSubject,Product>("Proc_GetProductsFilter",(Product,combination)=> {
                if (combination != null)
                {
                    Product.CombinationSubject = combination;

                }
                return Product;

            } 
            ,parameters, commandType: CommandType.StoredProcedure,splitOn: "CombinationSubjectId");*/
            var entitites = DbConnection.Query<Product>("Proc_GetProductsFilter", parameters, commandType: CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            
        }
        public IEnumerable<Product> GetNewProducts(int productNumber)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"ProductNumber", productNumber);
            
            var entitites = DbConnection.Query<Product>("Proc_GetProductNews", parameters, commandType: CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            
        }
        public OutputFilterPaging<ProductPost> GetNewProductPosts(Guid productID, int pageIndex, int pageSize)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"PageIndex", pageIndex);
                parameters.Add($"PageSize", pageSize);
                parameters.Add($"ProductID", productID);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var entitites = DbConnection.Query<Product,Int64, ProductPost>("Proc_GetProductNewPosts",
                (p, q) =>
                {
                    ProductPost productPost = new ProductPost();
                    productPost.Product = p;
                    productPost.PostQuantity = (int)q;
                    return productPost;
                },
                parameters, splitOn: "PostQuantity", commandType: CommandType.StoredProcedure);

            var output = new OutputFilterPaging<ProductPost>();
            output.Data = entitites.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;

            
        }
        public OutputFilterPaging<ProductProductDetail> GetProductDetailPaging(int pageIndex, int pageSize, string? queryText, Guid productID)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"PageIndex", pageIndex);
                parameters.Add($"PageSize", pageSize);
                parameters.Add($"QueryText", queryText);
                parameters.Add($"ProductID", productID);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var entitites = DbConnection.Query<Product,ProductAttribute,ProductDetail,ProductProductDetail>("Proc_GetProductDetailPaging",
                (p, pa, pd) =>
                {
                    ProductProductDetail productProductDetail = new ProductProductDetail();
                    productProductDetail.ProductDetail = pd;
                    productProductDetail.Product = p;
                    productProductDetail.ProductAttribute = pa;
                    return productProductDetail;
                },
                parameters,splitOn:"ProductID,ProductAttributeID,ProductDetailID", commandType: CommandType.StoredProcedure);
            var output= new OutputFilterPaging<ProductProductDetail>();
            output.Data = entitites.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;

            
        }
        public ProductWarrenty GetWarrentyLookup(string imei)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"IMEI", imei);
                
            var entitites = DbConnection.Query<Product,ProductAttribute, Customer, Order, OrderDetail, ProductWarrenty>("Proc_GetWarrentyLookup",
                (p, pa,c,o,od) =>
                {
                    ProductWarrenty productWarrenty = new ProductWarrenty();
                    productWarrenty.Product = p;
                    productWarrenty.ProductAttribute = pa;
                    productWarrenty.Customer = c;
                    productWarrenty.Order = o;
                    productWarrenty.OrderDetail = od;
                    return productWarrenty;
                },
                parameters,splitOn:"ProductID,ProductAttributeID,CustomerID,OrderID,OrderDetailID", commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return entitites.ToList().Count>0? entitites.ToList()[0]:null;

            
        }
        public IEnumerable<ProductAttribute> GetBestSellingProducts(int productNumber,int stage)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"ProductNumber", productNumber);
                parameters.Add($"Stage", stage);
            
            var entitites = DbConnection.Query<ProductAttribute>("Proc_GetProductBestSellings", parameters, commandType: CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            
        }
        public Product GetProductByProductAttribute(Guid productID)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"ProductID", productID);
            
                var entitites = DbConnection.QueryFirstOrDefault<Product>("Proc_GetProductByProductAttribute", parameters, commandType: CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            
        }
        public IEnumerable<Product> GetRelatedProducts(int productNumber,Guid manufacturerID, Guid productID)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"ProductNumber", productNumber);
                parameters.Add($"ManufacturerID", manufacturerID);
                parameters.Add($"ProductID", productID);
            
            var entitites = DbConnection.Query<Product>("Proc_GetProductRelateds", parameters, commandType: CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            
        }
        public IEnumerable<Product> GetRecommandProducts(Guid manufacturerID, Guid productID, decimal price)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"ManufacturerID", manufacturerID);
                parameters.Add($"ProductID", productID);
                parameters.Add($"Price", price);
            
            var entitites = DbConnection.Query<Product>("Proc_GetProductRecommands", parameters, commandType: CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            
        }
        public IEnumerable<ProductAttribute> GetProductAttributeByProductID(Guid productID)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"ProductID", productID);
            
            var entitites = DbConnection.Query<ProductAttribute>("Proc_GetProductAttributeByProductID", parameters, commandType: CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            
        }
        
        public OutputFilterPaging<Product> GetProductsByCategory(string whereClause, int sortBy, int pageIndex, int pageSize)
        {
            

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"WhereClause", whereClause);
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"SortBy", sortBy);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var entitites = DbConnection.Query<Product>("Proc_GetProductsByCategory", parameters, commandType: CommandType.StoredProcedure);
            var output = new OutputFilterPaging<Product>();
            output.Data = entitites.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;   
        }
       /* public IEnumerable<ProductCategory> GetProduct() { }*/
       /// <summary>
       /// kiểm tra tồn tại Product trong combinationsubject
       /// </summary>
       /// <param name="combinationSubjectId">id tổ bộ môn</param>
       /// <returns>true: có, false:không</returns>
        public bool CheckExistsCombinationSubject(Guid combinationSubjectId)
        {
            var isDuplicate = false;

            var sqlCommand = $"SELECT 1  FROM Product where CombinationSubjectId = @CombinationSubjectId";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@CombinationSubjectId", combinationSubjectId);

            //Lấy dữ liệu với Dapper:
            var numberRecord = DbConnection.QueryFirstOrDefault<string>(sql: sqlCommand, param: paramaters);
            if (numberRecord != null)
                isDuplicate = true;

            return isDuplicate;
        }
        
        /*public override int Delete(Guid entityId)
        {
            var rowAffects = 0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    //Xử lý các dữ liệu:
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"@ProductId", entityId);

                   *//* var count = 0;

                    var rowe = DbConnection.Query<Product>($"Proc_GetProductCategoryByProductId", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    //THực thi commandText:
                    if (rowe != null)
                    {
                        count = DbConnection.Execute($"Proc_DeleteProductCategoryByProductId", parameters, transaction: transaction, null, CommandType.StoredProcedure);
                    }*//*
                    rowAffects = DbConnection.Execute($"Proc_DeleteProductById", parameters, transaction: transaction, null, CommandType.StoredProcedure);


                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    //transaction.Dispose();
                    DbConnection.Close();
                }

            }
            //Trả về dữ liệu:

            return rowAffects;

        }*/
        /// <summary>
        /// lấy thông tin Product theo ngày tạo mới nhất
        /// </summary>
        /// <returns>thông tin Product</returns>
        public Product GetProductByCreatedDate()
        {
            var entity = DbConnection.QueryFirstOrDefault<Product>($"Proc_GetProductByCreatedDate",commandType: CommandType.StoredProcedure);
            return entity;
        }

        public OutputFilterPaging<ProductAndProductAttribute> GetProductAttributeAndProduct(string? queryText, int? pageIndex, int? pageSize)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"QueryText", queryText);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var products = DbConnection.Query<Product, ProductAttribute, ProductAndProductAttribute>("Proc_GetProductAttributeAndProduct",
                (p, pa) =>
                {
                    ProductAndProductAttribute o = new ProductAndProductAttribute();
                    o.Product = p;
                    o.ProductAttribute = pa;
                    
                    return o;
                },
                parameters, splitOn: "ProductAttributeID", commandType: CommandType.StoredProcedure);
            var output = new OutputFilterPaging<ProductAndProductAttribute>();
            output.Data = products.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");

            //Trả về dữ liệu:
            return output;
        }
    }
}