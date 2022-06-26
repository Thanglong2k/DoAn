
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
using Core.Model;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Core.Extensions;

namespace Core.Services
{
    public class OverviewService: IOverviewService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IOverviewRepository _overviewRepository;
        private IProductRepository _productRepository;
        private ServiceResult _serviceResult;
        #endregion
        #region Constructor
        public OverviewService(IOverviewRepository overviewRepository, IProductRepository productRepository)
        {
            //ServiceResult = new ServiceResult();

            _overviewRepository = overviewRepository;
            _productRepository = productRepository;
            _serviceResult = new ServiceResult();
        }
        #endregion
        #region Method
        #region
        /*public override ServiceResult Add(Account Account)
        {
           /* var AccountIds = "";
            var lengthAccounts = Account.AccountList.Count;
            if (lengthAccounts > 0)
            {

                for (int i = 0; i < lengthAccounts - 1; i++)
                {
                    AccountIds += Account.AccountList[i].AccountId + ",";
                }
                AccountIds += Account.AccountList[lengthAccounts - 1].AccountId;
                Account.AccountIds = AccountIds;
            }


            var subjectIds = "";
            var lengthSubjectList = Account.SubjectList.Count;
            if (lengthSubjectList > 0)
            {

                for (int i = 0; i < lengthSubjectList - 1; i++)
                {
                    subjectIds += Account.SubjectList[i].SubjectId + ",";
                }
                subjectIds += Account.SubjectList[lengthSubjectList - 1].SubjectId;
                Account.SubjectIds = subjectIds;
            }*//*
            var isValidate = base.Validate(Account);
            if(isValidate)
            {
                var row = _AccountRepository.Add(Account);
                *//*if (row > 0)
                {
                    var AccountAccountService = new AccountAccountService(_AccountAccountRepository);
                    var entity = _AccountRepository.GetAccountByCreatedDate();
                    for (int i = 0; i < Account.AccountList.Count; i++)
                    {
                        AccountAccount AccountAccount = new AccountAccount();
                        AccountAccount.AccountId = entity.AccountId;
                        AccountAccount.AccountId = Account.AccountList[i].AccountId;
                        ServiceResult = AccountAccountService.Add(AccountAccount);
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
        public ServiceResult GetTurnoverTotal(int month)
        {
            
            var data = _overviewRepository.GetTurnoverTotal(month);



            _serviceResult.Data = data;
            _serviceResult.MISACode = Core.Const.Const.MISACodeSuccess; ;
            _serviceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return _serviceResult;
        }
        public ServiceResult GetOrderTotal(int month)
        {
            
            var data = _overviewRepository.GetOrderTotal(month);



            _serviceResult.Data = data;
            _serviceResult.MISACode = Core.Const.Const.MISACodeSuccess; ;
            _serviceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return _serviceResult;
        }
        public ServiceResult GetReceiptTotal(int month)
        {

            var data = _overviewRepository.GetReceiptTotal(month);



            _serviceResult.Data = data;
            _serviceResult.MISACode = Core.Const.Const.MISACodeSuccess; ;
            _serviceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return _serviceResult;
        }
        public ServiceResult GetNewProductTotal(int month)
        {

            var data = _overviewRepository.GetNewProductTotal(month);



            _serviceResult.Data = data;
            _serviceResult.MISACode = Core.Const.Const.MISACodeSuccess; ;
            _serviceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return _serviceResult;
        }
        public ServiceResult GetMonthlyTurnover()
        {

            var data = _overviewRepository.GetMonthlyTurnover();



            _serviceResult.Data = data;
            _serviceResult.MISACode = Core.Const.Const.MISACodeSuccess; ;
            _serviceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return _serviceResult;
        }
        public ServiceResult GetQuantitySoldByManufacturer(int month)
        {

            var data = _overviewRepository.GetQuantitySoldByManufacturer(month);



            _serviceResult.Data = data;
            _serviceResult.MISACode = Core.Const.Const.MISACodeSuccess; ;
            _serviceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return _serviceResult;
        }
        public ServiceResult GetQuantityOrderCancelled(int month)
        {

            var data = _overviewRepository.GetQuantityOrderCancelled(month);



            _serviceResult.Data = data;
            _serviceResult.MISACode = Core.Const.Const.MISACodeSuccess; ;
            _serviceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return _serviceResult;
        }
        public ServiceResult GetProductBestSellingsInMonth(int month,int productNumber)
        {
            var data = _overviewRepository.GetProductBestSellingsInMonth(month,productNumber);



            _serviceResult.Data = data;
            _serviceResult.MISACode = Core.Const.Const.MISACodeSuccess; ;
            _serviceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return _serviceResult;
        }
        public ServiceResult GetCancelledOrders(int month)
        {
            var data = _overviewRepository.GetCancelledOrders(month);

            _serviceResult.Data = data;
            _serviceResult.MISACode = Core.Const.Const.MISACodeSuccess; ;
            _serviceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return _serviceResult;
        }

        #endregion
    }
}
