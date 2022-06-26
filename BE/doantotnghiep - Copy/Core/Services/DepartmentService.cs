

using Core.Const;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Service;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DepartmentService: BaseService<Department>, IDepartmentService
    {
        #region Declare
        private ResponseError _responseError;
        public ServiceResult ServiceResult;
        IDepartmentRepository _departmentRepository;
        #endregion
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            ServiceResult = new ServiceResult();
            _responseError = new ResponseError();
            _departmentRepository = departmentRepository;
        }
        #endregion
        #region Method
        /*/// <summary>
        /// Lấy tát cả phòng ban
        /// </summary>
        /// <returns>
        /// Danh sách phòng ban
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult GetAll()
        {
            ServiceResult.Data = _departmentRepository.GetAll();
            return ServiceResult;
        }
        /// <summary>
        /// Lấy phòng ban theo Id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>
        /// THông tin phòng ban đưuọc lấy theo Id
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        public ServiceResult GetById(Guid departmentId)
        {

            ServiceResult.Data = _departmentRepository.GetById(departmentId);
            return ServiceResult;
        }
        //THêm mới phòng ban:
        /// <summary>
        /// Service thêm mới phòng ban
        /// </summary>
        /// <param name="department"> đối tượng phòng ban</param>
        /// <returns>
        /// Kết quả thêm phòng ban
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Add(Department department)
        {

            //validate dữ liệu:
            //1. Kiểm tta xem đã có thông tin mã phòng ban chưa?:
            if (string.IsNullOrEmpty(department.DepartmentCode))
            {

                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeEmpty;
                ServiceResult.UserMsg = Core.Properties.Resources.Msg_Required;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }
            //2. Mã phòng ban có trùng hay không? - Không được phép để trùng
            if (_departmentRepository.CheckDuplicateDepartmentCode(department.DepartmentCode))
            {
                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeDuplicate;
                ServiceResult.UserMsg = Core.Properties.Resources.Msg_Duplicated;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }
            //3. Email có đúng định dạng hay không?
            department.DepartmentId = new Guid();
            ServiceResult.Data = _departmentRepository.Add(department);
            return ServiceResult;
            
           
        }
        //Sửa phòng ban:
        /// <summary>
        /// Service cập nhật phòng ban
        /// </summary>
        /// <param name="department"> đối tượng phòng ban</param>
        /// <param name="departmentId"> Id phòng ban</param>
        /// <returns>
        /// Kết quả cập nhật phòng ban
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Update(Department department, Guid departmentId)
        {

            //validate dữ liệu:
            //1. Kiểm tta xem đã có thông tin mã phòng ban chưa?:
            if (string.IsNullOrEmpty(department.DepartmentCode))
            {

                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeEmpty;
                ServiceResult.UserMsg = Core.Properties.Resources.Msg_Required;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }

            //3. Email có đúng định dạng hay không?

            ServiceResult.Data = _departmentRepository.Update(department, departmentId);
            return ServiceResult;
            
        }
        //Xóa phòng ban
        /// <summary>
        /// Service xóa phòng ban
        /// </summary>
        /// <param name="departmentId"> Id phòng ban</param>
        /// <returns>
        /// Kết quả xóa phòng ban
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Delete(Guid departmentId)
        {

            ServiceResult.Data = _departmentRepository.Delete(departmentId);
            return ServiceResult;
        }

        public ServiceResult GetNewCode()
        {
            ServiceResult.Data = _departmentRepository.GetNewCode();
            return ServiceResult;
        }*/
        
        #endregion
    }
}
