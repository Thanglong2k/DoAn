
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
    public class AccountService:BaseService<Account>,IAccountService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IAccountRepository _accountRepository;
        private IJWTManagerRepository _jWTManagerRepository;

        #endregion
        #region Constructor
        public AccountService(IAccountRepository accountRepository,IJWTManagerRepository jWTManagerRepository) :base(accountRepository)
        {
            //ServiceResult = new ServiceResult();
            
            _accountRepository = accountRepository;
            _jWTManagerRepository = jWTManagerRepository;

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
        public ServiceResult Login(Account account)
        {
            account.Password= System.Text.Encoding.UTF8.EncodeBase64(account.Password);
            var _account = _accountRepository.Login(account);
            
            string misaCode = Core.Const.Login.Success;
            if (_account == null)
            {
                misaCode = Core.Const.Login.NotExists;
            }else
            {
                _account.Token = _jWTManagerRepository.Authenticate(_account).Token;
                if (!_account.Status)
                {

                    misaCode = Core.Const.Login.Locked;
                }
            }

            ServiceResult.Data = _account;
            ServiceResult.MISACode = misaCode;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetAccountID(Account account)
        {
            account.Password= System.Text.Encoding.UTF8.EncodeBase64(account.Password);
            var _account = _accountRepository.GetAccountID(account);
            
            

            ServiceResult.Data = _account;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetAccountByUserName(string username)
        {
            
            var _account = _accountRepository.GetAccountByUserName(username);
            
            

            ServiceResult.Data = _account;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult CheckAccountExists(string username)
        {
            
            var _account = _accountRepository.CheckAccountExists(username);
            
            

            ServiceResult.Data = _account;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public override ServiceResult GetAllPaging(int pageIndex, int pageSize, string queryText)
        {
            var accounts = _accountRepository.GetAllPaging(pageIndex, pageSize, queryText);
            foreach (var account in accounts.Data)
            {
                account.Password= System.Text.Encoding.UTF8.DecodeBase64(account.Password);
            }
            ServiceResult.Data = accounts;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult Add(Account account)
        {
            account.Password= System.Text.Encoding.UTF8.EncodeBase64(account.Password);
            var _account = _accountRepository.Add(account);
            
            ServiceResult.Data = _account;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult Update(Account account,Guid id)
        {
            account.Password= System.Text.Encoding.UTF8.EncodeBase64(account.Password);
            var _account = _accountRepository.Update(account,id);
            
            ServiceResult.Data = _account;
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }

        public HashSalt EncryptPassword(string password)
        {
            byte[] salt = new byte[128 / 8]; // Generate a 128-bit salt using a secure PRNG
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return new HashSalt() { Hash = encryptedPassw, Salt = salt };
        }
        public bool VerifyPassword(string enteredPassword, byte[] salt, string storedPassword)
        {
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return encryptedPassw == storedPassword;
        }
        #endregion
    }
}
