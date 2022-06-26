

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
    public class CombinationSubjectService: BaseService<CombinationSubject>, ICombinationSubjectService
    {
        #region Declare
        private ResponseError _responseError;
        public ServiceResult ServiceResult;
        ICombinationSubjectRepository _combinationSubjectRepository;
        #endregion
        #region Constructor
        public CombinationSubjectService(ICombinationSubjectRepository combinationSubjectRepository) :base(combinationSubjectRepository)
        {
            ServiceResult = new ServiceResult();
            _responseError = new ResponseError();
            _combinationSubjectRepository = combinationSubjectRepository;
        }
        #endregion
        #region Method
        /*/// <summary>
        /// Lấy tát cả vị trí
        /// </summary>
        /// <returns>
        /// Danh sách vị trí
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult GetAll()
        {
            ServiceResult.Data = _combinationSubjectRepository.GetAll();
            return ServiceResult;
        }
        /// <summary>
        /// Lấy vị trí theo Id
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns>
        /// THông tin vị trí đưuọc lấy theo Id
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        public ServiceResult GetById(Guid positionId)
        {

            ServiceResult.Data = _combinationSubjectRepository.GetById(positionId);
            return ServiceResult;
        }
        //THêm mới vị trí:
        /// <summary>
        /// Service thêm mới vị trí
        /// </summary>
        /// <param name="position"> đối tượng vị trí</param>
        /// <returns>
        /// Kết quả thêm vị trí
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Add(CombinationSubject position)
        {

            //validate dữ liệu:
            //1. Kiểm tta xem đã có thông tin mã vị trí chưa?:
            if (string.IsNullOrEmpty(position.PositionCode))
            {

                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeEmpty;
                ServiceResult.UserMsg = Core.Properties.Resources.Msg_Required;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }
            //2. Mã vị trí có trùng hay không? - Không được phép để trùng
            if (_combinationSubjectRepository.CheckDuplicatePositionCode(position.PositionCode))
            {
                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeDuplicate;
                ServiceResult.UserMsg = Core.Properties.Resources.Msg_Duplicated;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }
            //3. Email có đúng định dạng hay không?
            position.PositionId = new Guid();
            ServiceResult.Data = _combinationSubjectRepository.Add(position);
            return ServiceResult;
            
        }
        //Sửa vị trí:
        /// <summary>
        /// Service cập nhật vị trí
        /// </summary>
        /// <param name="position"> đối tượng vị trí</param>
        /// <param name="positionId"> Id vị trí</param>
        /// <returns>
        /// Kết quả cập nhật vị trí
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Update(CombinationSubject position, Guid positionId)
        {

            //validate dữ liệu:
            //1. Kiểm tta xem đã có thông tin mã vị trí chưa?:
            if (string.IsNullOrEmpty(position.PositionCode))
            {

                ServiceResult.Success = false;
                ServiceResult.MISACode = Const.MISACodeEmpty;
                ServiceResult.UserMsg = Core.Properties.Resources.Msg_Required;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }

            //3. Email có đúng định dạng hay không?

            ServiceResult.Data = _combinationSubjectRepository.Update(position, positionId);
            return ServiceResult;
            
        }
        //Xóa vị trí
        /// <summary>
        /// Service xóa vị trí
        /// </summary>
        /// <param name="positionId"> Id vị trí</param>
        /// <returns>
        /// Kết quả xóa vị trí
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Delete(Guid positionId)
        {

            ServiceResult.Data = _combinationSubjectRepository.Delete(positionId);
            return ServiceResult;
        }
        public ServiceResult GetNewCode()
        {

            ServiceResult.Data = _combinationSubjectRepository.GetNewCode();
            return ServiceResult;
        }*/
        #endregion
    }
}
