
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Core.Const;
using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Core.Interfaces.Service;
using static Core.Entities.BaseEntity;

namespace Core.Services
{
    public class ProductService:BaseService<Product>,IProductService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IProductRepository _productRepository;
        private IProductAttributeRepository _productAttributeRepository;
        private ICategoryRepository _categoryRepository;
        //private IProductCategoryRepository _ProductCategoryRepository;
        //private IProductSubjectRepository _ProductSubjectRepository;
        //private ICategoryRepository _CategoryRepository;
        //private ISubjectRepository _subjectRepository;
        #endregion
        #region Constructor
        public ProductService(IProductRepository productRepossitory,
            IProductAttributeRepository productAttributeRepository,
            ICategoryRepository categoryRepository
            ) :base(productRepossitory)
        {
            _productRepository = productRepossitory;
            _productAttributeRepository = productAttributeRepository;
            _categoryRepository = categoryRepository;
        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Product Product)
        {
           /* var CategoryIds = "";
            var lengthCategorys = Product.CategoryList.Count;
            if (lengthCategorys > 0)
            {

                for (int i = 0; i < lengthCategorys - 1; i++)
                {
                    CategoryIds += Product.CategoryList[i].CategoryId + ",";
                }
                CategoryIds += Product.CategoryList[lengthCategorys - 1].CategoryId;
                Product.CategoryIds = CategoryIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Product.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Product.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Product.SubjectList[lengthSubjectList - 1].SubjectId;
                Product.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Product);
            if(isValidate)
            {
                var row = _ProductRepository.Add(Product);
                *//*if (row > 0)
                {
                    var ProductCategoryService = new ProductCategoryService(_ProductCategoryRepository);
                    var entity = _ProductRepository.GetProductByCreatedDate();
                    for (int i = 0; i < Product.CategoryList.Count; i++)
                    {
                        ProductCategory ProductCategory = new ProductCategory();
                        ProductCategory.ProductId = entity.ProductId;
                        ProductCategory.CategoryId = Product.CategoryList[i].CategoryId;
                        ServiceResult = ProductCategoryService.Add(ProductCategory);
                    }
                }*//*

                ServiceResult.Data = row;
                ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            }
            else
            {
                return ServiceResult;
            }
            
            return ServiceResult;

        }*/
        #endregion

        public override ServiceResult Add(Product product)
        {
            product.EntityState = Enum.EntityState.AddNew;
            var isValidate = Validate(product);

            if (isValidate)
            {
                var productID = _productRepository.Add(product);
                var dataDetail = 0;
                ServiceResult.Data = productID;
                if(productID != Guid.Empty)
                {
                    foreach(var item in product.ProductAttributes)
                    {
                        item.ProductID = productID;

                        dataDetail = _productAttributeRepository.Add(item);
                        if (dataDetail == 0)
                        {
                            break;
                        }
                    }
                }
                if (productID != Guid.Empty && dataDetail>0)
                {
                    ServiceResult.Data = productID;
                    ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                    ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
                }
                else
                {
                    ServiceResult.Data = 0;
                    ServiceResult.MISACode = Core.Const.Const.MISACodeInValid;
                    ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_NotValid);
                }
            }
            else
            {
                return ServiceResult;
            }
            return ServiceResult;
        }
        public override ServiceResult Update(Product product,Guid id)
        {
            product.EntityState = Enum.EntityState.Update;
            var isValidate = Validate(product);

            if (isValidate)
            {
                var rowAffect = _productRepository.Update(product,id);
                var rowAttributeAffects = 0;
                ServiceResult.Data = rowAffect;
                if(rowAffect>0)
                {
                    var productAttributeIDs = _productAttributeRepository.GetProductAttributeIDsByProductID(id);
                    //Update and add
                    foreach (var item in product.ProductAttributes)
                    {
                        var rowAttributeAffect = 0;
                        if (item.ProductAttributeID != Guid.Empty)
                        {
                            
                                rowAttributeAffect = _productAttributeRepository.Update(item, item.ProductAttributeID);
                                                    
                        }
                        
                        else
                        {
                            if(item.ProductID== Guid.Empty)
                            {
                                item.ProductID = id;
                            }
                            rowAttributeAffect = _productAttributeRepository.Add(item);
                        }
                        rowAttributeAffects += rowAttributeAffect;
                    }
                    //Delete
                    foreach(var productAttributeID in productAttributeIDs)
                    {
                        var exists = false;
                        foreach (var item in product.ProductAttributes)
                        {
                            if (item.ProductAttributeID == productAttributeID)
                            {
                                exists=true;
                                break;
                            }
                            
                        }
                        if (!exists){
                            var rowAttributeAffect = _productAttributeRepository.Delete(productAttributeID);
                            rowAttributeAffects += rowAttributeAffect;
                        }
                        
                    }
                    /*var productAttributeIDs = _productAttributeRepository.GetProductAttributeIDsByProductID(id);
                    if (productAttributeIDs.ToList().Count > 0)
                    {
                        foreach(var item in productAttributeIDs)
                        {
                            var productAttribute = product.ProductAttributes.Where(x => x.ProductAttributeID == item).FirstOrDefault();
                            var rowAttributeAffect = _productAttributeRepository.Update(productAttribute, item);
                        } 
                    }*/
                }
                if (rowAffect > 0 && rowAttributeAffects > 0)
                {
                    ServiceResult.Data = true;
                    ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                    ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
                }
                else
                {
                    ServiceResult.Data = false;
                    ServiceResult.MISACode = Core.Const.Const.MISACodeInValid;
                    ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_NotValid);
                }
            }
            else
            {
                return ServiceResult;
            }
            return ServiceResult;
        }

        public override ServiceResult Delete(Guid id)
        {
            /*var rowProductAttributeDeleted = _productAttributeRepository.DeleteByProductID(id);
            var rowAffect = 0;
            if (rowProductAttributeDeleted > 0)
            {
                rowAffect = _productRepository.Delete(id);
            }*/
            ServiceResult.Data = false;
            var rowAffect = _productRepository.Delete(id);
            if (rowAffect > 0)
            {
                ServiceResult.Data = true;
            }
            
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;

        }
        public ServiceResult GetAllProducts()
        {
            var Products = _productRepository.GetAll();
            
            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetAllProductsPaging(int pageIndex, int pageSize, string queryText)
        {
            var Products = _productRepository.GetAllPaging(pageIndex, pageSize, queryText);
            
            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetProductDetailPaging(int pageIndex, int pageSize, string queryText, Guid productID)
        {
            var Products = _productRepository.GetProductDetailPaging(pageIndex, pageSize, queryText, productID);
            
            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetWarrentyLookup(string imei)
        {
            var Products = _productRepository.GetWarrentyLookup(imei);
            
            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetNewProducts(int productNumber)
        {
            var Products = _productRepository.GetNewProducts(productNumber);
            foreach (var Product in Products)
            {               
                Product.ProductAttributes = _productRepository.GetProductAttributeByProductID(Product.ProductID).ToList();
            }

            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetNewProductPosts(Guid productID, int pageIndex, int pageSize)
        {
            var Products = _productRepository.GetNewProductPosts(productID, pageIndex, pageSize);
            foreach (var Product in Products.Data)
            {               
                Product.Product.ProductAttributes = _productRepository.GetProductAttributeByProductID(Product.Product.ProductID).ToList();
            }

            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetBestSellingProducts(int productNumber,int stage)
        {
            var ProductAttributes = _productRepository.GetBestSellingProducts(productNumber, stage);
            var Products = new List<Product>();
            foreach (var ProductAttribute in ProductAttributes)
            {
                
                var Product= _productRepository.GetProductByProductAttribute(ProductAttribute.ProductID);
                Product.ProductAttributes = new List<ProductAttribute>();
                Product.ProductAttributes.Add(ProductAttribute);
                Products.Add(Product);
            }



            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetRelatedProducts( int productNumber,Guid manufacturerID, Guid productID)
        {
            var Products = _productRepository.GetRelatedProducts(productNumber, manufacturerID, productID);
            foreach (var Product in Products)
            {
                Product.ProductAttributes = _productRepository.GetProductAttributeByProductID(Product.ProductID).ToList();
            }

            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetRecommandProducts(Guid manufacturerID, Guid productID, decimal price)
        {
            var Products = _productRepository.GetRecommandProducts(manufacturerID, productID, price);
            foreach (var item in Products)
            {
                item.ProductAttributes = _productRepository.GetProductAttributeByProductID(item.ProductID).ToList();
            }

            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetProductAttributeByID(Guid productAttributeID)
        {
            var Products = _productAttributeRepository.GetProductAttributeByID(productAttributeID);

            ServiceResult.Data = Products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetProductsByCategory(Guid? categoryID, string? queryText, List<string>? filter, int sortBy, int pageIndex, int pageSize) {

            var whereClause=convertToWhereClause(categoryID, queryText, filter);
            var objectFilter = _productRepository.GetProductsByCategory(whereClause, sortBy, pageIndex, pageSize);
            var Products = (List < Product >) objectFilter.Data.AsEnumerable();
            foreach (var Product in Products)
            {
                
                Product.ProductAttributes = _productRepository.GetProductAttributeByProductID(Product.ProductID).ToList();
            }

            ServiceResult.Data = objectFilter;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }

        
        /// <summary>
        /// Tạo chuỗi mệnh để where để truy vấn
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="queryText"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string convertToWhereClause(Guid? categoryID,string queryText, List<string>? filter)
        {
            var manufacturer = filter.Count > 0 ? filter[0] : "";
            var price = filter.Count > 0 ? filter[1] : "";
            var battery = filter.Count > 0 ? filter[2] : "";
            var monitor = filter.Count > 0 ? filter[3] : "";

            string whereClause = "";
            if (categoryID != null)
            {
                var categoryByParent = _categoryRepository.GetCategoryIDByCategoryParentID((Guid)categoryID);
                if (categoryByParent.ToList().Count > 0)
                {
                    foreach(var item in categoryByParent)
                    {
                        whereClause = String.Concat(whereClause, " categoryID = '", item, "' or");
                    }
                    whereClause = whereClause.Substring(0, whereClause.Length - 4);
                    whereClause = String.Concat(whereClause, "' and");
                }
                else
                {

                    whereClause = String.Concat(whereClause, " categoryID = '", categoryID,"' and");
                }
            }
            if (queryText != "" && queryText != "null" && queryText != "undefined") {

                whereClause = String.Concat(whereClause, " UPPER(productName) like CONCAT('%',UPPER('", queryText, "'),'%') and");
        }
            if (manufacturer != "")
            {
                whereClause = String.Concat(whereClause, " locate(manufacturer,'",manufacturer,"') > 0 and");
            }

            //set điều kiện giá sản phẩm
            if (price != "")
            {
                var priceNew = Array.ConvertAll(price.Split(','), item => int.Parse(item));
                var wherePrice = " (";

                for (int i=0;i< priceNew.Count();i++)
                {
                    int testp= priceNew[i];
                    switch (priceNew[i])
                    {
                        case (int)ProductPrices.LessThanTwo:
                            wherePrice = String.Concat(wherePrice, " price < 2000000");
                            break;
                        case (int)ProductPrices.TwoToFour:
                            
                            wherePrice = String.Concat(wherePrice, " (price >= 2000000 and price < 4000000)");
                            break;
                        case (int)ProductPrices.FourToSeven:
                            wherePrice = String.Concat(wherePrice, " (price >= 4000000 and price < 7000000)");
                            break;
                        case (int)ProductPrices.SevenToFifteen:
                            wherePrice = String.Concat(wherePrice, " (price >= 7000000 and price < 15000000)");
                            break;
                        case (int)ProductPrices.OverFifteen:
                            wherePrice = String.Concat(wherePrice, " price >= 15000000");
                            break;
                     
                    }
                    if(i == priceNew.Count()-1)
                    {
                        wherePrice += ") and";
                    }
                    else
                    {
                        wherePrice += " or";
                    }
                }
                whereClause = String.Concat(whereClause, wherePrice);
            }
            //set điều kiện pin
            if (battery != "")
            {
                var batteryNew = Array.ConvertAll(battery.Split(','), item => int.Parse(item));
                var whereBattery = " (";
                for (int i = 0; i < batteryNew.Count(); i++)
                {
                    switch (batteryNew[i])
                    {
                        case (int)Batteries.LessThanThree:
                            whereBattery = String.Concat(whereBattery, " battery < 3000");
                            break;
                        case (int)Batteries.ThreeToFive:
                            whereBattery = String.Concat(whereBattery, " (battery >= 3000 and battery < 5000)");
                            break;
                        case (int)Batteries.OverFive:
                            whereBattery = String.Concat(whereBattery, " battery >= 5000");
                            break;

                    }
                    if (i == batteryNew.Count() - 1)
                    {
                        whereBattery += ") and";
                    }
                    else
                    {
                        whereBattery += " or";
                    }
                }
                whereClause = String.Concat(whereClause, whereBattery);
            }
            //set điều kiện màn hình
            if (monitor != "")
            {
                var monitorNew = Array.ConvertAll(monitor.Split(','), item => int.Parse(item));
                var whereMonitor = " (";
                for (int i = 0; i < monitorNew.Count(); i++)
                {
                    switch (monitorNew[i])
                    {
                        case (int)Monitors.LessThanSix:
                            whereMonitor = String.Concat(whereMonitor, " monitorsize < 6");
                            break;
                        case (int)Monitors.OverSix:
                            whereMonitor = String.Concat(whereMonitor, " monitorsize >= 6");
                            break;

                    }
                    if (i == monitorNew.Count() - 1)
                    {
                        whereMonitor += ") and";
                    }
                    else
                    {
                        whereMonitor += " or";
                    }
                }
                whereClause = String.Concat(whereClause, whereMonitor);
            }
            if (whereClause.Length >= 4)
            {

                 whereClause = whereClause.Substring(0, whereClause.Length - 4);
            }
            return whereClause;
        }
        public ServiceResult GetProductAttributeAndProduct(string? queryText, int? pageIndex, int? pageSize)
        {
            var products = _productRepository.GetProductAttributeAndProduct(queryText, pageIndex, pageSize);
            

            ServiceResult.Data = products;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }

        /// <summary>
        /// lấy Product theo id
        /// </summary>
        /// <param name="ProductId">id Product</param>
        /// <param name="projectPath">đường dẫn của project</param>
        /// <returns>thông tin request</returns>
        public ServiceResult GetById(Guid productId)
        {
            var Product = _productRepository.GetById(productId);

            Product.ProductAttributes= _productRepository.GetProductAttributeByProductID(Product.ProductID).ToList();
            ServiceResult.Data = Product;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        /// <summary>
        /// Lọc và phân trang
        /// </summary>
        /// <param name="ProductFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public ServiceResult GetFilter(string ProductFilter, Guid? combinationSubjectId)
        {
            ServiceResult.Data = _productRepository.GetFilter(ProductFilter, combinationSubjectId);
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult CheckExistsCombinationSubject(Guid combinationSubjectId)
        {
            ServiceResult.Data = _productRepository.CheckExistsCombinationSubject(combinationSubjectId);
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        /// <summary>
        /// Validate tùy chỉnh theo màn hình nhân viên
        /// </summary>
        /// <param name="entity">Thực thể nhân viên</param>
        /// <returns>(true-false)</returns>
        /// CreatedBy: TTLONG(28/07/2021)
        protected override bool ValidateCustom(Product Product)
        {
            bool isValid = true;
            /*var dateOfBirth="";*/
            //1. Đọc các property
            var properties = Product.GetType().GetProperties();

            /*foreach (var property in properties)
            {             
                if (isValid && property.IsDefined(typeof(Date), false))
                {
                    TimeSpan time = DateTime.Now - Convert.ToDateTime(property.GetValue(Product));
                    
                    if (DateTime.Now < Convert.ToDateTime(property.GetValue(Product)))
                    {
                        isValid = false;
                        ServiceResult.Data = property.Name;
                        ServiceResult.MISACode = Core.Const.Const.MISACodeValidate;
                        ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Date);                        
                    }                  
                }              
            }*/
            return isValid;
        }
        /// <summary>
        /// Validate định dạng thuộc tính theo regex
        /// </summary>
        /// <param name="Product"></param>
        /// <param name="propertyInfo"></param>
        /// <returns>(true-đúng false-sai)</returns>
        /// CreatedBy: TTLONG (04/08/2021)
        private bool ValidateProperty(Product Product, PropertyInfo propertyInfo, string regex)
        {
            var isValid = true;

            //1. Tên trường
            var propertyName = propertyInfo.Name;

            //2. Tên hiển thị
            var propertyDisplayName = propertyInfo.GetCustomAttribute(typeof(DisplayName), true);

            //3. Gía trị
            var value = propertyInfo.GetValue(Product);
            
            //Không validate required
            if (value==null || value.ToString()=="")
                return isValid;

            //var regex = @"^(0|(\+84)){1}(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$";
            var match = Regex.Match(value.ToString(), regex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                isValid = false;
            }
            if (!isValid)
            {              
                ServiceResult.Data = propertyInfo.Name;
                ServiceResult.MISACode = Core.Const.Const.MISACodeValidate;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_NotFormat, (propertyDisplayName as DisplayName).Name);    
            }

            return isValid;
        }

        


        #endregion
    }
}
