
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
using Core.Interfaces;
using Core.Interfaces.Service;
using static Core.Entities.BaseEntity;

namespace Core.Services
{
    public class ProductService:BaseService<Product>,IProductService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IProductRepository _ProductRepository;
        //private IProductDepartmentRepository _ProductDepartmentRepository;
        //private IProductSubjectRepository _ProductSubjectRepository;
        //private IDepartmentRepository _departmentRepository;
        //private ISubjectRepository _subjectRepository;
        #endregion
        #region Constructor
        public ProductService(IProductRepository ProductRepossitory 
            //IProductDepartmentRepository ProductDepartmentRepository, 
            //IProductSubjectRepository ProductSubjectRepository, 
            //IDepartmentRepository departmentRepository,
            //ISubjectRepository subjectRepository
            ) :base(ProductRepossitory)
        {
            //ServiceResult = new ServiceResult();
            
            _ProductRepository = ProductRepossitory;
            //_departmentRepository = departmentRepository;
            //_subjectRepository = subjectRepository;
            //_ProductDepartmentRepository = ProductDepartmentRepository;
            //_ProductSubjectRepository = ProductSubjectRepository;
        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Product Product)
        {
           /* var departmentIds = "";
            var lengthDepartments = Product.DepartmentList.Count;
            if (lengthDepartments > 0)
            {

                for (int i = 0; i < lengthDepartments - 1; i++)
                {
                    departmentIds += Product.DepartmentList[i].DepartmentId + ",";
                }
                departmentIds += Product.DepartmentList[lengthDepartments - 1].DepartmentId;
                Product.DepartmentIds = departmentIds;
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
                    var ProductDepartmentService = new ProductDepartmentService(_ProductDepartmentRepository);
                    var entity = _ProductRepository.GetProductByCreatedDate();
                    for (int i = 0; i < Product.DepartmentList.Count; i++)
                    {
                        ProductDepartment ProductDepartment = new ProductDepartment();
                        ProductDepartment.ProductId = entity.ProductId;
                        ProductDepartment.DepartmentId = Product.DepartmentList[i].DepartmentId;
                        ServiceResult = ProductDepartmentService.Add(ProductDepartment);
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
        /// <summary>
        /// Cập nhật thông tin Product
        /// CreatedBy:TTLONG(19/09/2021)
        /// </summary>
        /// <param name="Product">đối tượng Product</param>
        /// <param name="ProductId">id Product</param>
        /// <returns>đối tượng lưu thông tin request</returns>
        /*public override ServiceResult Update(Product Product, Guid ProductId)
        {
            //cập nhạt Product
            var row = base.Update(Product, ProductId);
            //cập nhật phòng ban trong bảng trung gian Productdepartment
            var ProductDepartmentService = new ProductDepartmentService(_ProductDepartmentRepository);
            for (int i = 0; i < Product.DepartmentList.Count; i++)
            {
                
                if (Product.DepartmentList[i].EntityState == Core.Enum.EntityState.Update)
                {
                    ProductDepartment ProductDepartment = new ProductDepartment();
                    ProductDepartment.ProductId = ProductId;
                    ProductDepartment.DepartmentId = Product.DepartmentList[i].DepartmentId;
                    ServiceResult = ProductDepartmentService.Add(ProductDepartment);
                }
                else if(Product.DepartmentList[i].EntityState == Core.Enum.EntityState.Delete)
                {
                    ServiceResult = ProductDepartmentService.Delete(ProductId,Product.DepartmentList[i].DepartmentId);
                }
                
                
            }
            //cập nhật môn học trong bảng trung gian Productsubject
            var ProductSubjectRepository = new ProductSubjectService(_ProductSubjectRepository);
            for (int i = 0; i < Product.SubjectList.Count; i++)
            {

                if (Product.SubjectList[i].EntityState == Core.Enum.EntityState.Update)
                {
                    ProductSubject ProductSubject = new ProductSubject();
                    ProductSubject.ProductId = ProductId;
                    ProductSubject.SubjectId = Product.SubjectList[i].SubjectId;
                    ServiceResult = ProductSubjectRepository.Add(ProductSubject);
                }
                else if (Product.SubjectList[i].EntityState == Core.Enum.EntityState.Delete)
                {
                    ServiceResult = ProductSubjectRepository.Delete(ProductId, Product.SubjectList[i].SubjectId);
                }


            }
            return ServiceResult;
        }*/
        /// <summary>
        /// lấy Product theo id
        /// </summary>
        /// <param name="ProductId">id Product</param>
        /// <param name="projectPath">đường dẫn của project</param>
        /// <returns>thông tin request</returns>
        /*public ServiceResult GetById(Guid ProductId,string projectPath)
        {
            var Product = _ProductRepository.GetById(ProductId);
            Product.DepartmentList = _departmentRepository.GetDepartmentsByProductId(ProductId);
            Product.SubjectList = _subjectRepository.GetSubjectsByProductId(ProductId);
            if (Product.ImageName != null)
            {

                Product.ImageName = projectPath+Product.ImageName;
            }
            
            ServiceResult.Data = Product;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }*/
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
            ServiceResult.Data = _ProductRepository.GetFilter(ProductFilter, combinationSubjectId);
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult CheckExistsCombinationSubject(Guid combinationSubjectId)
        {
            ServiceResult.Data = _ProductRepository.CheckExistsCombinationSubject(combinationSubjectId);
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

        public ServiceResult GetById(Guid ProductId, string projectPath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
