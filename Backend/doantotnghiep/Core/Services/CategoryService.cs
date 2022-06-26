
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
    public class CategoryService:BaseService<Category>,ICategoryService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private ICategoryRepository _categoryRepository;

        #endregion
        #region Constructor
        public CategoryService(ICategoryRepository categoryRepository) :base(categoryRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _categoryRepository = categoryRepository;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Category Category)
        {
           /* var CategoryIds = "";
            var lengthCategorys = Category.CategoryList.Count;
            if (lengthCategorys > 0)
            {

                for (int i = 0; i < lengthCategorys - 1; i++)
                {
                    CategoryIds += Category.CategoryList[i].CategoryId + ",";
                }
                CategoryIds += Category.CategoryList[lengthCategorys - 1].CategoryId;
                Category.CategoryIds = CategoryIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Category.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Category.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Category.SubjectList[lengthSubjectList - 1].SubjectId;
                Category.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Category);
            if(isValidate)
            {
                var row = _CategoryRepository.Add(Category);
                *//*if (row > 0)
                {
                    var CategoryCategoryService = new CategoryCategoryService(_CategoryCategoryRepository);
                    var entity = _CategoryRepository.GetCategoryByCreatedDate();
                    for (int i = 0; i < Category.CategoryList.Count; i++)
                    {
                        CategoryCategory CategoryCategory = new CategoryCategory();
                        CategoryCategory.CategoryId = entity.CategoryId;
                        CategoryCategory.CategoryId = Category.CategoryList[i].CategoryId;
                        ServiceResult = CategoryCategoryService.Add(CategoryCategory);
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
        public ServiceResult GetAllCategories(string projectPath)
        {
            var Categories = _categoryRepository.GetAll();
            foreach (var Category in Categories)
            {
                if (Category.Image!=null)
                {
                    Category.Image = projectPath + Category.Image;
                }
                   
            }

            ServiceResult.Data = Categories;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }

        public ServiceResult GetCategoryIDByCategoryParentID(Guid categoryParentID)
        {
            var Categories = _categoryRepository.GetCategoryIDByCategoryParentID(categoryParentID);
            

            ServiceResult.Data = Categories;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }


        #endregion
    }
}
