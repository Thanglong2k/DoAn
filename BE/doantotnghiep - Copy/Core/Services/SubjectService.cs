

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
    public class SubjectService: BaseService<Subject>, ISubjectService
    {
        #region Declare
        private ResponseError _responseError;
        public ServiceResult ServiceResult;
        ISubjectRepository _SubjectRepository;
        #endregion
        #region Constructor
        public SubjectService(ISubjectRepository SubjectRepository) : base(SubjectRepository)
        {
            ServiceResult = new ServiceResult();
            _responseError = new ResponseError();
            _SubjectRepository = SubjectRepository;
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
            ServiceResult.Data = _SubjectRepository.GetAll();
            return ServiceResult;
        }
        /// <summary>
        /// Lấy phòng ban theo Id
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns>
        /// THông tin phòng ban đưuọc lấy theo Id
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        public ServiceResult GetById(Guid SubjectId)
        {

            ServiceResult.Data = _SubjectRepository.GetById(SubjectId);
            return ServiceResult;
        }
        //THêm mới phòng ban:
        /// <summary>
        /// Service thêm mới phòng ban
        /// </summary>
        /// <param name="Subject"> đối tượng phòng ban</param>
        /// <returns>
        /// Kết quả thêm phòng ban
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Add(Subject Subject)
        {

            //validate dữ liệu:
            //1. Kiểm tta xem đã có thông tin mã phòng ban chưa?:
            if (string.IsNullOrEmpty(Subject.SubjectCode))
            {

                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeEmpty;
                ServiceResult.UserMsg = Core.Properties.Resources.Msg_Required;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }
            //2. Mã phòng ban có trùng hay không? - Không được phép để trùng
            if (_SubjectRepository.CheckDuplicateSubjectCode(Subject.SubjectCode))
            {
                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeDuplicate;
                ServiceResult.UserMsg = Core.Properties.Resources.Msg_Duplicated;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }
            //3. Email có đúng định dạng hay không?
            Subject.SubjectId = new Guid();
            ServiceResult.Data = _SubjectRepository.Add(Subject);
            return ServiceResult;
            
           
        }
        //Sửa phòng ban:
        /// <summary>
        /// Service cập nhật phòng ban
        /// </summary>
        /// <param name="Subject"> đối tượng phòng ban</param>
        /// <param name="SubjectId"> Id phòng ban</param>
        /// <returns>
        /// Kết quả cập nhật phòng ban
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Update(Subject Subject, Guid SubjectId)
        {

            //validate dữ liệu:
            //1. Kiểm tta xem đã có thông tin mã phòng ban chưa?:
            if (string.IsNullOrEmpty(Subject.SubjectCode))
            {

                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeEmpty;
                ServiceResult.UserMsg = Core.Properties.Resources.Msg_Required;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }

            //3. Email có đúng định dạng hay không?

            ServiceResult.Data = _SubjectRepository.Update(Subject, SubjectId);
            return ServiceResult;
            
        }
        //Xóa phòng ban
        /// <summary>
        /// Service xóa phòng ban
        /// </summary>
        /// <param name="SubjectId"> Id phòng ban</param>
        /// <returns>
        /// Kết quả xóa phòng ban
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Delete(Guid SubjectId)
        {

            ServiceResult.Data = _SubjectRepository.Delete(SubjectId);
            return ServiceResult;
        }

        public ServiceResult GetNewCode()
        {
            ServiceResult.Data = _SubjectRepository.GetNewCode();
            return ServiceResult;
        }*/
        
        #endregion
    }
}
