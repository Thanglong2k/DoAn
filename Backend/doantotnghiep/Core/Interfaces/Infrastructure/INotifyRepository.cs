
using Core.Entities;
using Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface Danh mục kết nối dữ liệu database
    /// </summary>
    /// CreatedBy: TTLONG (28/7/2021)
    public interface INotifyRepository : IBaseRepository<Notify>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu:
        /// </summary>
        /// <returns>
        /// Danh sách Object
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        IEnumerable<NotifyList> GetNotifyList();
        int GetNotifyNotViewTotal();
        int UpdateIsRead(Guid notifyID);
        int UpdateIsView();
    }
}
