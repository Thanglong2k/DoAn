using Dapper;
using Microsoft.Extensions.Configuration;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySqlConnector;

namespace Infrastructure.Repository
{
    public class OverviewRepository : IOverviewRepository
    {
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        string _className;
        //Khởi tạo kết nối
        protected IDbConnection DbConnection;
        public OverviewRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
            DbConnection = new MySqlConnection(_connectionString);
        }

        public int GetTurnoverTotal(int month)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@Month", month);
            parameters.Add($"@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //Khởi tạo các CommandText:

            var entitites = DbConnection.QueryFirstOrDefault<object>("Proc_GetTurnoverTotal", parameters, commandType: CommandType.StoredProcedure);
            int total = parameters.Get<int>("Total");
            //Trả về dữ liệu:
            return total;
        }
        public int GetOrderTotal(int month)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@Month", month);
            parameters.Add($"@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //Khởi tạo các CommandText:

            var entitites = DbConnection.QueryFirstOrDefault<object>("Proc_GetOrderTotal", parameters, commandType: CommandType.StoredProcedure);
            int total = parameters.Get<int>("Total");
            //Trả về dữ liệu:
            return total;
        }
        public int GetReceiptTotal(int month)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@Month", month);
            parameters.Add($"@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //Khởi tạo các CommandText:

            var entitites = DbConnection.QueryFirstOrDefault<object>("Proc_GetReceiptTotal", parameters, commandType: CommandType.StoredProcedure);
            int total = parameters.Get<int>("Total");
            //Trả về dữ liệu:
            return total;
        }
        public int GetNewProductTotal(int month)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@Month", month);
            parameters.Add($"@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //Khởi tạo các CommandText:

            var entitites = DbConnection.QueryFirstOrDefault<object>("Proc_GetNewProductTotal", parameters, commandType: CommandType.StoredProcedure);
            int total = parameters.Get<int>("Total");
            //Trả về dữ liệu:
            return total;
        }
        public IEnumerable<QuantitySoldOfManufacturer> GetQuantitySoldByManufacturer(int month)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@Month", month);
            //Khởi tạo các CommandText:

            var entitites = DbConnection.Query<Manufacturer,Int64, QuantitySoldOfManufacturer>("Proc_GetQuantitySoldByManufacturer",
                (m, q) =>
                {
                    QuantitySoldOfManufacturer quantitySoldOfManufacturer = new QuantitySoldOfManufacturer();
                    quantitySoldOfManufacturer.Manufacturer = m;
                    quantitySoldOfManufacturer.Quantity = (int)q;
                    return quantitySoldOfManufacturer;
                },
                parameters,splitOn: "Quantity",commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return entitites;
        }
        public MonthlyTurnover GetMonthlyTurnover()
        {

            var entitites = DbConnection.QueryFirstOrDefault<MonthlyTurnover>("Proc_GetMonthlyTurnover", commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return entitites;
        }
        public int GetQuantityOrderCancelled(int month)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@Month", month);
            parameters.Add($"@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //Khởi tạo các CommandText:

            var entitites = DbConnection.QueryFirstOrDefault<object>("Proc_GetQuantityOrderCancelled", parameters, commandType: CommandType.StoredProcedure);
            int total = parameters.Get<int>("Total");
            //Trả về dữ liệu:
            return total;
        }
        public IEnumerable<ProductBestSelling> GetProductBestSellingsInMonth(int month,int productNumber)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"Month", month);
            parameters.Add($"ProductNumber", productNumber);

            var entitites = DbConnection.Query<Product, ProductAttribute, Int64, ProductBestSelling>("Proc_GetProductBestSellingsInMonth",
                (p,pa,q) =>
                {
                    ProductBestSelling o=new ProductBestSelling();
                    o.Product = p;
                    o.ProductAttribute = pa;
                    o.SellingQuantity = (int)q;
                    return o;
                },

                parameters, splitOn: "ProductAttributeID,SellingQuantity", commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return entitites;
        }
        public IEnumerable<OrderCustomerInfo> GetCancelledOrders(int month)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@Month", month);
            var entitites = DbConnection.Query<Customer, Order, OrderCustomerInfo>("Proc_GetCancelledOrders",
                (c,o) =>
                {
                    OrderCustomerInfo u =new OrderCustomerInfo();
                    u.Customer = c;
                    u.Order = o;
                    return u;
                },

                 parameters, splitOn: "OrderID", commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return entitites;
        }
        
    }
}