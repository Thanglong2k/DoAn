using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface INotifyService:IBaseService<Notify>
    {
        public ServiceResult GetNotifyList();
        public ServiceResult GetNotifyNotViewTotal();
        public ServiceResult UpdateIsRead(Guid notifyID);
        public ServiceResult UpdateIsView();
    }
}
