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
    public class ReceiptRepository : BaseRepository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public Guid Add(Receipt receipt)
        {
            var rowAffects = 0;
            Guid receiptID = Guid.Empty;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {

                try
                {
                    //Xử lý các dữ liệu:
                    var parameters = MappingDbType(receipt);
                    parameters.Add($"@ReceiptID", dbType: DbType.Guid, direction: ParameterDirection.Output);

                    rowAffects = DbConnection.Execute($"Proc_InsertReceipt", parameters, transaction: transaction, null, CommandType.StoredProcedure);
                    receiptID = parameters.Get<Guid>("ReceiptID");
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    var t = e.Message;

                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                    DbConnection.Close();
                }

            }
            return receiptID;
        }
        public IEnumerable<Receipt> GetReceiptByPhoneNumber(string phoneNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"PhoneNumber", phoneNumber);

            var customer = DbConnection.QueryFirstOrDefault<Suplier>("Proc_GetSuplierByPhoneNumber", parameters, commandType: CommandType.StoredProcedure);

            parameters = new DynamicParameters();
            parameters.Add($"SuplierID", customer.SuplierID);

            var receipts = DbConnection.Query<Receipt>("Proc_GetReceiptByPhoneNumber", parameters, commandType: CommandType.StoredProcedure);
            foreach (var item in receipts)
            {
                parameters = new DynamicParameters();
                parameters.Add($"ReceiptID", item.ReceiptID);

                var entitites = DbConnection.Query<Receipt>("Proc_GetReceiptByPhoneNumber", parameters, commandType: CommandType.StoredProcedure);
            }

            //Trả về dữ liệu:
            return receipts;
        }
        public OutputFilterPaging<ReceiptList> GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime)
        {
            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@PageIndex", pageIndex);
                parameters.Add($"@PageSize", pageSize);
                parameters.Add($"@StartTime", startTime);
                parameters.Add($"@EndTime", endTime);
                parameters.Add($"@QueryText", queryText);
                parameters.Add($"@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add($"@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //Khởi tạo các CommandText:
                var entitites = DbConnection.Query<Receipt, Suplier, Int64, ReceiptList>($"Proc_GetReceiptsPaging",
                     (c, o, u) =>
            {
                ReceiptList ol = new ReceiptList();
                ol.Receipt = c;
                ol.Suplier = o;
                ol.ReceiptDetailQuantity = (int)u;
                return ol;
            },
                parameters, splitOn: "ReceiptID,cID,ReceiptDetailQuantity", commandType: CommandType.StoredProcedure);

                var output = new OutputFilterPaging<ReceiptList>();
                output.Data = entitites.ToList();
                output.TotalRecord = parameters.Get<int>("TotalRecord");
                output.TotalPage = parameters.Get<int>("TotalPage");
                //Trả về dữ liệu:
                return output;

            }
            catch (Exception e)
            {
                var t = e.Message;

                return null;
            }
        }
        public ReceiptSuplier CheckSuplierPurchasedProduct(string phoneNumber, Guid productAttributeID)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"PhoneNumber", phoneNumber);
            parameters.Add($"ProductAttributeID", productAttributeID);
            parameters.Add($"Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            var customerID = DbConnection.ExecuteScalar<Guid>("Proc_CheckSuplierPurchasedProduct", parameters, commandType: CommandType.StoredProcedure);
            var receiptSuplier = new ReceiptSuplier()
            {
                SuplierID = customerID,
                IsPurchased = parameters.Get<bool>("Result")
            };
            //Trả về dữ liệu:
            return receiptSuplier;
        }

        public IEnumerable<Receipt> PurchasedHistory(string phoneNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"PhoneNumber", phoneNumber);
            var receipts = DbConnection.Query<Receipt>("Proc_GetPurchasedHistoryReceipt", parameters, commandType: CommandType.StoredProcedure);

            //Trả về dữ liệu:
            return receipts;
        }
        public ReceiptSuplierInfo GetSuplierReceipt(Guid receiptID)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"ReceiptID", receiptID);
            var receipts = DbConnection.Query<Suplier,Receipt, Int64, ReceiptSuplierInfo>("Proc_GetSuplierReceipt",
                (i, p,c) =>
                {
                    ReceiptSuplierInfo o = new ReceiptSuplierInfo();
                    o.Suplier = i;
                    o.Receipt = p;
                    o.Quantity = (int)c;
                    return o;
                },
                parameters, splitOn: "ReceiptID,Quantity", commandType: CommandType.StoredProcedure);

            //Trả về dữ liệu:
            return receipts.ToList()[0];
        }

        
    }
}