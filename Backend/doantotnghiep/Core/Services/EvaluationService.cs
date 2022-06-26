
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
    public class EvaluationService:BaseService<Evaluation>,IEvaluationService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IEvaluationRepository _evaluationRepository;
        private INotifyRepository _notifyRepository;

        #endregion
        #region Constructor
        public EvaluationService(IEvaluationRepository evaluationRepository, INotifyRepository notifyRepository) :base(evaluationRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _evaluationRepository = evaluationRepository;
            _notifyRepository = notifyRepository;

        }
        #endregion
        #region Method
        #region
        public override ServiceResult Add(Evaluation evaluation)
        {

            evaluation.EntityState = Enum.EntityState.AddNew;
            var isValidate = Validate(evaluation);

            if (isValidate)
            {
                var data = _evaluationRepository.Add(evaluation);
                ServiceResult.Data = data;
                if ((int)ServiceResult.Data > 0)
                {
                    Notify notify = new Notify();
                    notify.ProductAttributeID = evaluation.ProductAttributeID;
                    notify.CustomerID = evaluation.CustomerID;
                    notify.Type = Enum.Notify.Evaluation;
                    var notifyResult = _notifyRepository.Add(notify);
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
        #endregion
        public ServiceResult GetProductRatingDetail(Guid productID)
        {
            var productRatingDetail= _evaluationRepository.GetProductRatingDetail(productID);

            ServiceResult.Data = productRatingDetail;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetEvaluationByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize)
        {
            var evaluationByProductID= _evaluationRepository.GetEvaluationByProductAttributeID(productAttributeID, pageIndex,pageSize);

            ServiceResult.Data = evaluationByProductID;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetEvaluations(string? queryText, int pageIndex, int pageSize)
        {
            var commentByProductID = _evaluationRepository.GetEvaluations(queryText, pageIndex, pageSize);

            ServiceResult.Data = commentByProductID;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }


        #endregion
    }
}
