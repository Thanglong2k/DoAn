using Core.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface dùng chung kết nối dữ liệu database
    /// </summary>
    /// <typeparam name="TEntity">ĐỐi tượng</typeparam>
    /// CreatedBy: TTLONG (28/7/2021)
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu:
        /// </summary>
        /// <returns>
        /// Danh sách Object
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// Lấy dữ liệu theo mã
        /// </summary>
        /// <param name="entityId"> Object Id</param>
        /// <returns>
        /// Dữ liệu được lấy theo Id
        /// </returns>
        /// /// CreatedBy: TTLONG (28/7/2021)
        TEntity GetById(Guid entityId);
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">object</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// CreatedBy: TTLONG(28/7/2021)
        int Add(TEntity entity);
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="entityId">Id của Object</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// CreatedBy: TTLONG(28/7/2021)
        int Update(TEntity entity, Guid entityId);
        /// <summary>
        /// Xóa nhiều dữ liệu theo khóa chính
        /// </summary>
        /// <param name="entityId">Mảng Id của Object</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// CreatedBy: TTLONG(28/7/2021)
        int Delete(List<Guid> entityIds);
        /// <summary>
        /// Xóa dữ liệu theo khóa chính
        /// </summary>
        /// <param name="entityId">Id của Object</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// CreatedBy: TTLONG(28/7/2021)
        int Delete(Guid entityId);
        /// <summary>
        /// Lấy mã mới
        /// </summary> 
        /// <returns>
        /// Mã mới
        /// </returns>
        /// CreatedBy: TTLONG(29/7/2021)
        string GetNewCode();
        /// <summary>
        /// Lấy dữ liệu theo thuộc tính bất kì
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <param name="property">Thuộc tính của đối tượng</param>
        /// <returns>Danh sách thông tin</returns>
        /// CreatedBy: TTLONG(02/08/2021)
        TEntity GetEntityByProperty(TEntity entity, PropertyInfo property);
        /// <summary>
        /// Lấy thông tin entity theo thuộc tính
        /// </summary>
        /// <param name="propertyName">Tên thuộc tính</param>
        /// <param name="propertyValue">giá trị thuộc tính</param>
        /// <returns> Danh sách thông tin</returns>
        /// CreatedBy: TTLONG (02/08/2021)
        TEntity GetEntityByProperty(string propertyName, object propertyValue);

    }
}
