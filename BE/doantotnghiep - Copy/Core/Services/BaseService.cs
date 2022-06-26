
using Core.Const;
using Core.Entities;
using Core.Enum;
using Core.Interfaces.Infrastructure;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Core.Entities.BaseEntity;

namespace Core.Services
{
    public class BaseService<TEntity>:IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Declare
        private ResponseError _responseError;
        public ServiceResult ServiceResult;
        private IBaseRepository<TEntity> _baseRepository;
        #endregion
        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepossitory)
        {
            ServiceResult = new ServiceResult();
            _responseError = new ResponseError();
            _baseRepository = baseRepossitory;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy tát cả đối tượng
        /// </summary>
        /// <returns>
        /// Danh sách đối tượng
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        public ServiceResult GetAll()
        {

            ServiceResult.Data = _baseRepository.GetAll();
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        /// <summary>
        /// Lấy đối tượng theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// THông tin đối tượng đưuọc lấy theo Id
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        public virtual ServiceResult GetById(Guid entityId)
        {

            ServiceResult.Data = _baseRepository.GetById(entityId);
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        /// <summary>
        /// Service thêm mới đối tượng
        /// </summary>
        /// <param name="entity"> đối tượng đối tượng</param>
        /// <returns>
        /// Kết quả thêm đối tượng
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public virtual ServiceResult Add(TEntity entity)
        {
            entity.EntityState = Enum.EntityState.AddNew;
            var isValidate = Validate(entity);
            
            if (isValidate)
            {
                var data = _baseRepository.Add(entity);
                ServiceResult.Data = data;
                if ((int)ServiceResult.Data > 0)
                {
                    ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                    ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
                }
                else
                {
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
        /// <summary>
        /// Service cập nhật đối tượng
        /// </summary>
        /// <param name="entity"> đối tượng đối tượng</param>
        /// <param name="entityId"> Id đối tượng</param>
        /// <returns>
        /// Kết quả cập nhật đối tượng
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public virtual ServiceResult Update(TEntity entity, Guid entityId)
        {
            entity.EntityState = Enum.EntityState.Update;
            var isValidate = Validate(entity);
            
            if (isValidate)
            {
                ServiceResult.Data = _baseRepository.Update(entity,entityId);
                if ((int)ServiceResult.Data > 0)
                {
                    ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                    ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
                }
                else
                {
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

        /// <summary>
        /// Service xóa đối tượng
        /// </summary>
        /// <param name="entityId"> Id đối tượng</param>
        /// <returns>
        /// Kết quả xóa đối tượng
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Delete(Guid entityId)
        {
            ServiceResult.Data = _baseRepository.Delete(entityId);
            if ((int)ServiceResult.Data > 0)
            {
                ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);               
            }
            else
            {
                ServiceResult.MISACode = Core.Const.Const.MISACodeInValid;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_NotValid);
            }  
            return ServiceResult;
        }
        /// <summary>
        /// Service xóa nhiều đối tượng
        /// </summary>
        /// <param name="entityId">Mảng Id đối tượng</param>
        /// <returns>
        /// Kết quả xóa đối tượng
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Delete(List<Guid> entityIds)
        {
            ServiceResult.Data = _baseRepository.Delete(entityIds);

            if ((int)ServiceResult.Data == entityIds.Count)
            {
                ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
                
            }
            else
            {
                ServiceResult.MISACode = Core.Const.Const.MISACodeInValid;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_NotValid);
            }
            return ServiceResult;
        }
        /// <summary>
        /// Service lấy mã đối tượng mới
        /// </summary>
        /// <returns>
        /// MÃ đối tượng mới
        /// </returns>
        /// CreatedBy: TTLONG (1/8/2021)
        public ServiceResult GetNewCode()
        {

            ServiceResult.Data = _baseRepository.GetNewCode();
            
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            
            return ServiceResult;
        }
        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <returns>
        /// true/false
        /// </returns>
        /// CreatedBy: TTLONG (3/8/2021)
        protected bool Validate(TEntity entity)
        {
            var isValid = true;
            var mesArrayError = new List<string>();
            //1. Đọc các property
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var displayName = propertyName;
                var displayNameAttributes = (DisplayName)property.GetCustomAttribute(typeof(DisplayName), true);
                if (displayNameAttributes!=null && displayNameAttributes.ToString().Length > 0)
                {
                    displayName = (displayNameAttributes as DisplayName).Name;
                }
                //kiểm tra property có attribute Required không:
                if (isValid && property.IsDefined(typeof(Required), false))
                {
                    //1.1.1 Check bắt buộc nhập
                    isValid = ValidateRequired(entity, property);
 
                }
                //kiểm tra property có attribute Duplicate không:

                if (isValid && property.IsDefined(typeof(Duplicate), false))
                {
                    //1.1.2 Check trùng
                    //isValid = validateDuplicate(entity, property, entityDbSource);
                    var entityDuplicate = _baseRepository.GetEntityByProperty(entity, property);
                    if (entityDuplicate != null)
                    {
                        var propertyValue = property.GetValue(entity);
                        isValid = false;
                       
                        ServiceResult.Data = property.Name;
                        ServiceResult.MISACode = Core.Const.Const.MISACodeDuplicate;
                        ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Duplicated, displayName, propertyValue);
                        
                    }
                }
                //kiểm tra property có attribute RangeLength không:

                if (isValid && property.IsDefined(typeof(RangeLength), false))
                {
                    //1.1.3 Check length
                    //lấy độ dài đã khai báo
                    var propertyValue = property.GetValue(entity);
                    var attributeRangeLength = property.GetCustomAttribute(typeof(RangeLength), true);
                    var minLength = (attributeRangeLength as RangeLength).MinLength;
                    var maxLength = (attributeRangeLength as RangeLength).MaxLength;
                    var errorMsg = (attributeRangeLength as RangeLength).ErrorMsg;
                    if(propertyValue.ToString().Trim().Length<minLength || propertyValue.ToString().Trim().Length > maxLength)
                    {
                        isValid = false;
                        mesArrayError.Add(errorMsg);
                        ServiceResult.Data = property.Name;
                        ServiceResult.MISACode = Core.Const.Const.MISACodeValidate;
                        ServiceResult.UserMsg = errorMsg;                        
                        
                    }
                }
            }
            //ServiceResult.Data = mesArrayError;
            //check validate của lớp con
            if (isValid)
            {
                isValid = ValidateCustom(entity);
            }
            return isValid;
        }
        /// <summary>
        /// Kiểm tra thuộc tính có bắt buộc nhập không
        /// </summary>
        /// <returns>
        /// true/false
        /// </returns>
        /// CreatedBy: TTLONG (3/8/2021)
        private bool ValidateRequired(TEntity entity, PropertyInfo propertyInfo)
        {
            bool isValid = true;

            var propertyName = propertyInfo.Name;
            var propertyValue = propertyInfo.GetValue(entity);
            var propertyDisplayName = getAttributeDisplayName(propertyName);

            if (propertyValue == null)
            {
                isValid = false;
                ServiceResult.Data = propertyInfo.Name;
                
                ServiceResult.MISACode = Core.Const.Const.MISACodeEmpty;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Required, propertyDisplayName);
                ServiceResult.DevMsg = null;
                
            }

            return isValid;
        }
        /// <summary>
        /// Kiểm tra thuộc tính đã có trong hệ thống chưa
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="eSource">TEntity</param>
        /// <returns></returns>
        private bool ValidateDuplicate(TEntity entity, PropertyInfo propertyInfo, entitySource eSource)
        {
            bool isValid = true;

            var propertyName = propertyInfo.Name;
            var propertyDisplayName = getAttributeDisplayName(propertyName);

            //Tùy chỉnh nguồn dữ liệu để validate, trạng thái thêm hoắc sửa
            var entityDuplicate = eSource(entity, propertyInfo);

            if (entityDuplicate != null)
            {
                

                isValid = false;
                
                ServiceResult.MISACode = Core.Const.Const.MISACodeDuplicate;
                ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Duplicated, propertyDisplayName);
                ServiceResult.DevMsg = null;
            }

            return isValid;
        }
        /// <summary>
        /// Lấy tên hiển thị của trường trong entity
        /// </summary>
        /// <param name="attributeName">Tên thuộc tính</param>
        /// <returns>Tên hiển thị</returns>
        private String getAttributeDisplayName(string attributeName)
        {
            var res = attributeName;
            try
            {
                res = typeof(TEntity).GetProperty(attributeName).GetCustomAttributes(typeof(DisplayName),
                                               false).Cast<DisplayName>().Single().Name;
            }
            catch { }
            return res;
        }
        /// <summary>
        /// Điều chỉnh nguồn dữ liệu để validate
        /// </summary>
        /// <returns>TEntity</returns>
        /// CreatedBy: TTLONG (01/08/2021)
        public delegate TEntity entitySource(TEntity entity, PropertyInfo propertyInfo);
        
        /// <summary>
        /// Hàm thực hiện kiểm tra dữ liệu/nghiệp vụ tùy chỉnh
        /// </summary>
        /// <returns>TEntity</returns>
        /// CreatedBy: TTLONG (04/08/2021)
        protected virtual bool ValidateCustom(TEntity entity)
        {
            var isValid = true;
            //Validate chung:

            //Validate riêng:
            return isValid;
        }
        #endregion
    }
}
