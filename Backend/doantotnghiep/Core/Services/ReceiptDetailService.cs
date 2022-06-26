
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
    public class ReceiptDetailService:BaseService<ReceiptDetail>,IReceiptDetailService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IReceiptDetailRepository _receiptDetailRepository;

        #endregion
        #region Constructor
        public ReceiptDetailService(IReceiptDetailRepository receiptDetailRepository) :base(receiptDetailRepository)
        {
            //ServiceResult = new ServiceResult();

            _receiptDetailRepository = receiptDetailRepository;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(ReceiptDetail ReceiptDetail)
        {
           /* var ReceiptDetailIds = "";
            var lengthReceiptDetails = ReceiptDetail.ReceiptDetailList.Count;
            if (lengthReceiptDetails > 0)
            {

                for (int i = 0; i < lengthReceiptDetails - 1; i++)
                {
                    ReceiptDetailIds += ReceiptDetail.ReceiptDetailList[i].ReceiptDetailId + ",";
                }
                ReceiptDetailIds += ReceiptDetail.ReceiptDetailList[lengthReceiptDetails - 1].ReceiptDetailId;
                ReceiptDetail.ReceiptDetailIds = ReceiptDetailIds;
            }


            var subjectIds = "";
            var lengthSubjectList = ReceiptDetail.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += ReceiptDetail.SubjectList[i].SubjectId + ",";
                }
                subjectIds += ReceiptDetail.SubjectList[lengthSubjectList - 1].SubjectId;
                ReceiptDetail.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(ReceiptDetail);
            if(isValidate)
            {
                var row = _ReceiptDetailRepository.Add(ReceiptDetail);
                *//*if (row > 0)
                {
                    var ReceiptDetailReceiptDetailService = new ReceiptDetailReceiptDetailService(_ReceiptDetailReceiptDetailRepository);
                    var entity = _ReceiptDetailRepository.GetReceiptDetailByCreatedDate();
                    for (int i = 0; i < ReceiptDetail.ReceiptDetailList.Count; i++)
                    {
                        ReceiptDetailReceiptDetail ReceiptDetailReceiptDetail = new ReceiptDetailReceiptDetail();
                        ReceiptDetailReceiptDetail.ReceiptDetailId = entity.ReceiptDetailId;
                        ReceiptDetailReceiptDetail.ReceiptDetailId = ReceiptDetail.ReceiptDetailList[i].ReceiptDetailId;
                        ServiceResult = ReceiptDetailReceiptDetailService.Add(ReceiptDetailReceiptDetail);
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
        public ServiceResult InsertReceiptDetails(List<ReceiptDetail> receiptDetails,Guid receiptID)
        {
            var result = _receiptDetailRepository.InsertReceiptDetails(receiptDetails, receiptID);
            bool data = true;
            if(result!= receiptDetails.Count)
            {
                data = false;
            }
            ServiceResult.Data = data;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }

        public ServiceResult PurchasedHistoryByReceiptID(Guid? receiptID)
        {
            var resultCheck = _receiptDetailRepository.PurchasedHistoryByReceiptID(receiptID);

            ServiceResult.Data = resultCheck;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetReceiptDetailByReceiptID(Guid receiptID, int pageIndex, int pageSize)
        {
            var resultCheck = _receiptDetailRepository.GetReceiptDetailByReceiptID(receiptID,pageIndex,pageSize);

            ServiceResult.Data = resultCheck;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }




        #endregion
    }
}
