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
    public class EvaluationRepository : BaseRepository<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(IConfiguration configuration) : base(configuration)
        {
            
        }
        public IEnumerable<ProductRatingQuantity> GetProductRatingDetail(Guid productID)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"ProductAttributeID", productID);

            var entitites = DbConnection.Query<ProductRatingQuantity>("Proc_GetRatingOfProduct", parameters, commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return entitites;
        }
        public OutputFilterPaging<Evaluation> GetEvaluationByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"ProductAttributeID", productAttributeID);
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var entitites = DbConnection.Query<Evaluation>("Proc_GetEvaluationByProductAttributeID", parameters, commandType: CommandType.StoredProcedure);

            var output = new OutputFilterPaging<Evaluation>();
            output.Data = entitites.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;
        }
        public OutputFilterPaging<EvaluationProductCustomer> GetEvaluations(string? queryText, int pageIndex, int pageSize)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"QueryText", queryText);
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var entitites = DbConnection.Query<Evaluation, Customer, Product, Guid, EvaluationProductCustomer>("Proc_GetEvaluationProductCustomer",
                (co, cu, p, paID) =>
                {
                    EvaluationProductCustomer o = new EvaluationProductCustomer();
                    o.Evaluation = co;
                    o.Customer = cu;
                    
                    o.Product = p;
                    o.ProductAttributeID = paID;

                    return o;
                },
                parameters, splitOn: "cuID,ProductID,ProductAttributeID", commandType: CommandType.StoredProcedure);

            var output = new OutputFilterPaging<EvaluationProductCustomer>();
            output.Data = entitites.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;
        }

    }
}