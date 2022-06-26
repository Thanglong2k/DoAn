using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Service
{
    public interface IReceiptDetailService:IBaseService<ReceiptDetail>
    {



        ServiceResult Add(ReceiptDetail receiptDetail);
        ServiceResult InsertReceiptDetails(List<ReceiptDetail> receiptDetails,Guid receiptID);
        ServiceResult PurchasedHistoryByReceiptID(Guid? receiptID);
        ServiceResult GetReceiptDetailByReceiptID(Guid receiptID, int pageIndex, int pageSize);
    }
}
