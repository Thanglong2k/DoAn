
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
    public class ProductAttributeService:BaseService<ProductAttribute>, IProductAttributeService
    {
        #region Declare
        

        private IProductAttributeRepository _productAttributeRepository;

        #endregion
        #region Constructor
        public ProductAttributeService(
            IProductAttributeRepository productAttributeRepository
            ) :base(productAttributeRepository)
        {
            _productAttributeRepository = productAttributeRepository;

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

        public ServiceResult GetProductAttributeIDsByProductID(Guid productID)
        {


            var productAttributeIDs = _productAttributeRepository.GetProductAttributeIDsByProductID(productID);

            ServiceResult.Data = productAttributeIDs;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetProductAttributeByID(Guid productAttributeID)
        {


            var productAttributeIDs = _productAttributeRepository.GetProductAttributeByID(productAttributeID);

            ServiceResult.Data = productAttributeIDs;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }

        #endregion
    }
}
