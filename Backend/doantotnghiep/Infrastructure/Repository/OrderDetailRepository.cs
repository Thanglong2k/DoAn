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
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IConfiguration configuration) : base(configuration)
        {          
            
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

        public int InsertOrderDetails(List<OrderDetail> orderDetails, Guid orderID)
        {
            var rowAffects = 0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    foreach(var item in orderDetails)
                    {
                        //Xử lý các dữ liệu:
                        var parameters = MappingDbType(item);
                        parameters.Add($"OrderID", orderID);
                        var rowAffect = DbConnection.Execute($"Proc_InsertOrderDetail", parameters, transaction: transaction, null, CommandType.StoredProcedure);
                        if (rowAffect > 0)
                        {
                            rowAffects++;
                        }
                    }
                    if (rowAffects == orderDetails.Count)
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
        public IEnumerable<OrderDetailProductAttribute> PurchasedHistoryByOrderID(Guid? orderID)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"OrderID", orderID);
            /*string sql = string.Format("SELECT  p.* ,o.* FROM  orderdetail o left JOIN productattribute p ON o.ProductAttributeID = p.ProductAttributeID WHERE o.OrderID = '{0}'; ", orderID);*/
            //var orderDetails = DbConnection.Query("Proc_GetPurchasedHistoryOrderDetail",parameters,  commandType: CommandType.StoredProcedure);
            var orderDetails = DbConnection.Query<ProductAttribute, OrderDetail, Product, OrderDetailProductAttribute>("Proc_GetPurchasedHistoryOrderDetail",
                ( i, p, u) =>
            {
                OrderDetailProductAttribute o = new OrderDetailProductAttribute();
                o.OrderDetail = p;
                o.ProductAttribute = i;
                o.Product = u;
                return o;
            }, parameters, splitOn: "OrderDetailID,pID", commandType: CommandType.StoredProcedure);

            //Trả về dữ liệu:
            return orderDetails;
        }
        public OutputFilterPaging<OrderDetailProductAttribute> GetOrderDetailByOrderID(Guid orderID, int pageIndex, int pageSize)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"OrderID", orderID);
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            /*string sql = string.Format("SELECT  p.* ,o.* FROM  orderdetail o left JOIN productattribute p ON o.ProductAttributeID = p.ProductAttributeID WHERE o.OrderID = '{0}'; ", orderID);*/
            //var orderDetails = DbConnection.Query("Proc_GetPurchasedHistoryOrderDetail",parameters,  commandType: CommandType.StoredProcedure);
            var orderDetails = DbConnection.Query<OrderDetail, ProductAttribute, Product, OrderDetailProductAttribute>("Proc_GetOrderDetailByOrderID",
                ( i, p, u) =>
            {
                OrderDetailProductAttribute o = new OrderDetailProductAttribute();
                o.OrderDetail = i;
                o.ProductAttribute = p;
                o.Product = u;
                return o;
            }, parameters, splitOn: "paID,pID", commandType: CommandType.StoredProcedure);
            var output = new OutputFilterPaging<OrderDetailProductAttribute>();
            output.Data = orderDetails.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;
        }
        public int UpdateIMEIByOrderID(string imeis, Guid orderDetailID)
        {
            
            var rowAffect = 0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add($"IMEIS", imeis);
                    parameters.Add($"OrderDetailID", orderDetailID);
                    rowAffect = DbConnection.Execute($"Proc_UpdateIMEI", parameters, transaction: transaction, null, CommandType.StoredProcedure);
                        
                    if (rowAffect >0)
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
            return rowAffect;
        }

    }
}