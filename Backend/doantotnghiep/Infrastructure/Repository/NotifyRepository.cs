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
    public class NotifyRepository : BaseRepository<Notify>, INotifyRepository
    {
        public NotifyRepository(IConfiguration configuration) : base(configuration)
        {
            

        }

        public IEnumerable<NotifyList> GetNotifyList()
        {
            try
            {

                //Khởi tạo các CommandText:
                var entitites = DbConnection.Query<Product, ProductAttribute, Customer, Order, Notify, NotifyList>($"Proc_GetNotifys",
                    (p1, p, c, o, n) =>
                    {
                        NotifyList notify = new NotifyList();
                        notify.Product = p1;
                        notify.Customer = c;
                        notify.Order = o;
                        notify.ProductAttribute = p;
                        notify.Notify = n;
                        return notify;

                    },
                    null,splitOn:"ProductID,ProductAttributeID,CustomerID,OrderID, NotifyID",commandType: CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            }
            catch (Exception e)
            {
                var t = e.Message;

                return null;
            }


        }
        public int GetNotifyNotViewTotal()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //Khởi tạo các CommandText:
                var entitites = DbConnection.Query<Notify>($"Proc_GetNotifyNotViewTotal",
                    parameters,commandType: CommandType.StoredProcedure);
                int result= parameters.Get<int>("Total");
                //Trả về dữ liệu:
                return result;

            }
            catch (Exception e)
            {
                var t = e.Message;

                return 0;
            }


        }
        public int UpdateIsRead(Guid notifyID)
        {
            int rowAffect = 0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("NotifyID", notifyID);
                    //Khởi tạo các CommandText:
                    rowAffect = DbConnection.Execute($"Proc_UpdateIsReadNotify", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    var t = e.Message;

                    transaction.Rollback();
                }
                finally
                {
                    //transaction.Dispose();
                    DbConnection.Close();
                }
                return rowAffect;
            }

        }
        public int UpdateIsView()
        {
            int rowAffect = 0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    //Khởi tạo các CommandText:
                    rowAffect = DbConnection.Execute($"Proc_UpdateIsViewNotify", null, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    var t = e.Message;

                    transaction.Rollback();
                }
                finally
                {
                    //transaction.Dispose();
                    DbConnection.Close();
                }
                return rowAffect;
            }

        }
    }
}