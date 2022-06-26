using Dapper;
using Microsoft.Extensions.Configuration;
using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Infrastructure.Repository
{
    public class ProductAttributeRepository : BaseRepository<ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        
        public IEnumerable<Guid> GetProductAttributeIDsByProductID(Guid productID)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"ProductID", productID);
            
            var productAttributeIDs = DbConnection.Query<Guid>("Proc_GetProductAttributeIDsByProductID", parameters, commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return productAttributeIDs;
        }
        public ProductAndProductAttribute GetProductAttributeByID(Guid productAttributeID)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"ProductAttributeID", productAttributeID);
            
            var product = DbConnection.Query<Product, ProductAttribute, ProductAndProductAttribute>("Proc_GetProductAttributeByID",
                (p, pa) =>
                {
                    ProductAndProductAttribute o = new ProductAndProductAttribute();
                    o.Product = p;
                    o.ProductAttribute = pa;

                    return o;
                }
                , parameters, splitOn: "ProductAttributeID", commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return product.ToList().Count>0? product.ToList()[0]:null;
        }
        public int DeleteByProductID(Guid productID)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"ProductID", productID);
            
            var rowAffect = DbConnection.Execute("Proc_DeleteProductAttributeIDByProductID", parameters, commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return rowAffect;
        }
    }
}