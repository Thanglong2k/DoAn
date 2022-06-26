
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
    public class ReceiptService:BaseService<Receipt>,IReceiptService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IReceiptRepository _receiptRepository;
        private IReceiptDetailService _receiptDetailService;

        #endregion
        #region Constructor
        public ReceiptService(IReceiptRepository receiptRepository, IReceiptDetailService receiptDetailService) :base(receiptRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _receiptRepository = receiptRepository;
            _receiptDetailService = receiptDetailService;

        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Receipt Receipt)
        {
           /* var ReceiptIds = "";
            var lengthReceipts = Receipt.ReceiptList.Count;
            if (lengthReceipts > 0)
            {

                for (int i = 0; i < lengthReceipts - 1; i++)
                {
                    ReceiptIds += Receipt.ReceiptList[i].ReceiptId + ",";
                }
                ReceiptIds += Receipt.ReceiptList[lengthReceipts - 1].ReceiptId;
                Receipt.ReceiptIds = ReceiptIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Receipt.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Receipt.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Receipt.SubjectList[lengthSubjectList - 1].SubjectId;
                Receipt.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Receipt);
            if(isValidate)
            {
                var row = _ReceiptRepository.Add(Receipt);
                *//*if (row > 0)
                {
                    var ReceiptReceiptService = new ReceiptReceiptService(_ReceiptReceiptRepository);
                    var entity = _ReceiptRepository.GetReceiptByCreatedDate();
                    for (int i = 0; i < Receipt.ReceiptList.Count; i++)
                    {
                        ReceiptReceipt ReceiptReceipt = new ReceiptReceipt();
                        ReceiptReceipt.ReceiptId = entity.ReceiptId;
                        ReceiptReceipt.ReceiptId = Receipt.ReceiptList[i].ReceiptId;
                        ServiceResult = ReceiptReceiptService.Add(ReceiptReceipt);
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
        public ServiceResult GetReceiptByPhoneNumber(string phoneNumber)
        {
            var Receipt = _receiptRepository.GetReceiptByPhoneNumber(phoneNumber);

            ServiceResult.Data = Receipt;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
       

        public ServiceResult Add(ReceiptInfo receiptInfo)
        {
            
            var receiptID = _receiptRepository.Add(receiptInfo.Receipt);
            if (receiptID != Guid.Empty)
            {

                ServiceResult = _receiptDetailService.InsertReceiptDetails(receiptInfo.ReceiptDetails, receiptID);
            }
            ServiceResult.Data = null;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult PurchasedHistory(string phoneNumber)
        {
            List<ReceiptCart> receiptInfos = new List<ReceiptCart>();
            var receipts = _receiptRepository.PurchasedHistory(phoneNumber);
            foreach (var receipt in receipts)
            {
                var receiptDetails = _receiptDetailService.PurchasedHistoryByReceiptID(receipt.ReceiptID);
                receiptInfos.Add(new ReceiptCart { Receipt = receipt, ReceiptDetails = (List<ReceiptDetailProductAttribute>)receiptDetails.Data });
            }
            ServiceResult.Data = receiptInfos;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime)
        {
            var receipts = _receiptRepository.GetAllPaging(pageIndex, pageSize, queryText, startTime, endTime);
            
            ServiceResult.Data = receipts;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetSuplierReceipt(Guid receiptID)
        {
            var receiptSuplier = _receiptRepository.GetSuplierReceipt(receiptID);

            ServiceResult.Data = receiptSuplier;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }

        public ServiceResult GetReceiptByID(Guid receiptID)
        {
            var receipt = _receiptRepository.GetById(receiptID);
            ServiceResult receiptDetails=null;
            if (receipt != null)
            {
                receiptDetails = _receiptDetailService.GetReceiptDetailByReceiptID(receiptID,0,0);
            }
            var result = new ReceiptCart();
            result.Receipt = receipt;
            result.ReceiptDetails = ((OutputFilterPaging<ReceiptDetailProductAttribute>) receiptDetails.Data).Data;
            ServiceResult.Data = result;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        #endregion
    }
}
