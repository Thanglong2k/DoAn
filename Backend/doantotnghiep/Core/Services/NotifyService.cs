
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
    public class NotifyService : BaseService<Notify>, INotifyService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private INotifyRepository _notifyRepository;

        #endregion
        #region Constructor
        public NotifyService(INotifyRepository notifyRepository) :base(notifyRepository)
        {
            //ServiceResult = new ServiceResult();

            _notifyRepository = notifyRepository;

        }
        #endregion
        #region Method
        public ServiceResult GetNotifyList()
        {
            ServiceResult.Data = _notifyRepository.GetNotifyList();
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult GetNotifyNotViewTotal()
        {
            ServiceResult.Data = _notifyRepository.GetNotifyNotViewTotal();
            ServiceResult.MISACode = Core.Const.Const.MISACodeSuccess;
            ServiceResult.UserMsg = string.Format(Core.Properties.Resources.Msg_Success);
            return ServiceResult;
        }
        public ServiceResult UpdateIsRead(Guid notifyID)
        {
            ServiceResult.Data = _notifyRepository.UpdateIsRead(notifyID);
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
        public ServiceResult UpdateIsView()
        {
            ServiceResult.Data = _notifyRepository.UpdateIsView();
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

        #endregion
    }
}
