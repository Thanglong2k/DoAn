using Dapper;
using Microsoft.Extensions.Configuration;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Infrastructure.Repository
{
    public class ReceiptDetailRepository : BaseRepository<ReceiptDetail>, IReceiptDetailRepository
    {
        public ReceiptDetailRepository(IConfiguration configuration) : base(configuration)
        {          
            
        }

     

        public int InsertReceiptDetails(List<ReceiptDetail> receiptDetails, Guid receiptID)
        {
            var rowAffects = 0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    foreach(var item in receiptDetails)
                    {
                        //Xử lý các dữ liệu:
                        var parameters = MappingDbType(item);
                        parameters.Add($"ReceiptID", receiptID);
                        var rowAffect = DbConnection.Execute($"Proc_InsertReceiptDetail", parameters, transaction: transaction, null, CommandType.StoredProcedure);
                        if (rowAffect > 0)
                        {
                            rowAffects++;
                        }
                    }
                    if (rowAffects == receiptDetails.Count)
                    {

                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                
            }
            return rowAffects;
        }
        public IEnumerable<ReceiptDetailProductAttribute> PurchasedHistoryByReceiptID(Guid? receiptID)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"ReceiptID", receiptID);
            /*string sql = string.Format("SELECT  p.* ,o.* FROM  receiptdetail o left JOIN productattribute p ON o.ProductAttributeID = p.ProductAttributeID WHERE o.ReceiptID = '{0}'; ", receiptID);*/
            //var receiptDetails = DbConnection.Query("Proc_GetPurchasedHistoryReceiptDetail",parameters,  commandType: CommandType.StoredProcedure);
            var receiptDetails = DbConnection.Query<ProductAttribute, ReceiptDetail, Product, ReceiptDetailProductAttribute>("Proc_GetPurchasedHistoryReceiptDetail",
                ( i, p, u) =>
            {
                ReceiptDetailProductAttribute o = new ReceiptDetailProductAttribute();
                o.ReceiptDetail = p;
                o.ProductAttribute = i;
                o.Product = u;
                return o;
            }, parameters, splitOn: "ReceiptDetailID,pID", commandType: CommandType.StoredProcedure);

            //Trả về dữ liệu:
            return receiptDetails;
        }
        public OutputFilterPaging<ReceiptDetailProductAttribute> GetReceiptDetailByReceiptID(Guid receiptID, int pageIndex, int pageSize)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"ReceiptID", receiptID);
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            /*string sql = string.Format("SELECT  p.* ,o.* FROM  receiptdetail o left JOIN productattribute p ON o.ProductAttributeID = p.ProductAttributeID WHERE o.ReceiptID = '{0}'; ", receiptID);*/
            //var receiptDetails = DbConnection.Query("Proc_GetPurchasedHistoryReceiptDetail",parameters,  commandType: CommandType.StoredProcedure);
            var receiptDetails = DbConnection.Query<ReceiptDetail, ProductAttribute, Product, ReceiptDetailProductAttribute>("Proc_GetReceiptDetailByReceiptID",
                ( i, p, u) =>
            {
                ReceiptDetailProductAttribute o = new ReceiptDetailProductAttribute();
                o.ReceiptDetail = i;
                o.ProductAttribute = p;
                o.Product = u;
                return o;
            }, parameters, splitOn: "paID,pID", commandType: CommandType.StoredProcedure);
            var output = new OutputFilterPaging<ReceiptDetailProductAttribute>();
            output.Data = receiptDetails.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;
        }

    }
}