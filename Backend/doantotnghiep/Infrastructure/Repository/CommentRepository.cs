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
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public OutputFilterPaging<CustomerComment> GetCommentByProductAttributeID(Guid productAttributeID, int pageIndex, int pageSize)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"ProductAttributeID", productAttributeID);
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var entitites = DbConnection.Query<Comment,string,string, CustomerComment>("Proc_GetCommentByProductAttributeID",
                (c, cn, an) =>
                {
                    CustomerComment o = new CustomerComment();
                    o.Comment = c;
                    o.CustomerName = cn;
                    o.AccountName = an;
                    return o;
                },
                parameters, splitOn: "CustomerName,AccountName", commandType: CommandType.StoredProcedure);

            var output = new OutputFilterPaging<CustomerComment>();
            output.Data = entitites.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;
        }
        public OutputFilterPaging<CommentProductCustomer> GetComments(string? queryText, int pageIndex, int pageSize)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"QueryText", queryText);
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var entitites = DbConnection.Query<Comment,Customer,Account,Product,Guid,string,CommentProductCustomer>("Proc_GetCommentProductCustomer",
                (co,cu,a,p,paID, pc) =>
                {
                    CommentProductCustomer o = new CommentProductCustomer();
                    o.Comment = co;
                    o.Customer = cu;
                    o.Account = a;
                    o.Product = p;
                    o.ProductAttributeID = paID;
                    o.ParentComment = pc;

                    return o;
                },
                parameters, splitOn: "cuID,aID,ProductID,ProductAttributeID,ParentComment", commandType: CommandType.StoredProcedure);

            var output = new OutputFilterPaging<CommentProductCustomer>();
            output.Data = entitites.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;
        }

    }
}