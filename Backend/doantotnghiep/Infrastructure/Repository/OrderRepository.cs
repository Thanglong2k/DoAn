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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public Guid Add(Order order)
        {
            var rowAffects = 0;
            Guid orderID = Guid.Empty;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {

                try
                {
                    //Xử lý các dữ liệu:
                    var parameters = MappingDbType(order);
                    parameters.Add($"@OrderID", dbType: DbType.Guid, direction: ParameterDirection.Output);

                    rowAffects = DbConnection.Execute($"Proc_InsertOrder", parameters, transaction: transaction, null, CommandType.StoredProcedure);
                    orderID = parameters.Get<Guid>("OrderID");
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
            return orderID;
        }
        public IEnumerable<Order> GetOrderByPhoneNumber(string phoneNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"PhoneNumber", phoneNumber);

            var customer = DbConnection.QueryFirstOrDefault<Customer>("Proc_GetCustomerByPhoneNumber", parameters, commandType: CommandType.StoredProcedure);

            parameters = new DynamicParameters();
            parameters.Add($"CustomerID", customer.CustomerID);

            var orders = DbConnection.Query<Order>("Proc_GetOrderByPhoneNumber", parameters, commandType: CommandType.StoredProcedure);
            foreach (var item in orders)
            {
                parameters = new DynamicParameters();
                parameters.Add($"OrderID", item.OrderID);

                var entitites = DbConnection.Query<Order>("Proc_GetOrderByPhoneNumber", parameters, commandType: CommandType.StoredProcedure);
            }

            //Trả về dữ liệu:
            return orders;
        }
        public OutputFilterPaging<OrderList> GetAllPaging(int pageIndex, int pageSize, string? queryText, DateTime? startTime, DateTime? endTime, int status)
        {
            try
            {
                if (startTime != null)
                {

                    startTime = DateTime.SpecifyKind((DateTime)startTime, DateTimeKind.Local);
                    var t = DateTime.SpecifyKind((DateTime)startTime, DateTimeKind.Utc);
                    var te = DateTime.SpecifyKind((DateTime)startTime, DateTimeKind.Unspecified);
                }
                if (endTime != null)
                {

                    endTime = DateTime.SpecifyKind((DateTime)endTime, DateTimeKind.Local);
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@PageIndex", pageIndex);
                parameters.Add($"@PageSize", pageSize);
                parameters.Add($"@StartTime", startTime);
                parameters.Add($"@EndTime", endTime);
                parameters.Add($"@QueryText", queryText);
                parameters.Add($"@Status", status);
                parameters.Add($"@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add($"@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //Khởi tạo các CommandText:
                var entitites = DbConnection.Query<Order, Customer, Int64, OrderList>($"Proc_GetOrdersPaging",
                     (c, o, u) =>
            {
                OrderList ol = new OrderList();
                ol.Order = c;
                ol.Customer = o;
                ol.ProductQuantity = (int)u;
                return ol;
            },
                parameters, splitOn: "OrderID,cID,ProductQuantity", commandType: CommandType.StoredProcedure);

                var output = new OutputFilterPaging<OrderList>();
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
        public OrderCustomer CheckCustomerPurchasedProduct(string phoneNumber, Guid productAttributeID)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"PhoneNumber", phoneNumber);
            parameters.Add($"ProductAttributeID", productAttributeID);
            parameters.Add($"Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            var customerID = DbConnection.ExecuteScalar<Guid>("Proc_CheckCustomerPurchasedProduct", parameters, commandType: CommandType.StoredProcedure);
            var orderCustomer = new OrderCustomer()
            {
                CustomerID = customerID,
                IsPurchased = parameters.Get<bool>("Result")
            };
            //Trả về dữ liệu:
            return orderCustomer;
        }

        public IEnumerable<Order> PurchasedHistory(string phoneNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"PhoneNumber", phoneNumber);
            var orders = DbConnection.Query<Order>("Proc_GetPurchasedHistoryOrder", parameters, commandType: CommandType.StoredProcedure);

            //Trả về dữ liệu:
            return orders;
        }
        public IEnumerable<Order> FollowOrder(string phoneNumber, int status)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"PhoneNumber", phoneNumber);
            parameters.Add($"Status", status);
            var orders = DbConnection.Query<Order>("Proc_GetFollowOrder", parameters, commandType: CommandType.StoredProcedure);

            //Trả về dữ liệu:
            return orders;
        }
        public OrderCustomerInfo GetCustomerOrder(Guid orderID)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"OrderID", orderID);
            var orders = DbConnection.Query<Customer,Order, Int64, OrderCustomerInfo>("Proc_GetCustomerOrder",
                (i, p,c) =>
                {
                    OrderCustomerInfo o = new OrderCustomerInfo();
                    o.Customer = i;
                    o.Order = p;
                    o.Quantity = (int)c;
                    return o;
                },
                parameters, splitOn: "OrderID,Quantity", commandType: CommandType.StoredProcedure);

            //Trả về dữ liệu:
            return orders.ToList()[0];
        }

    }
}