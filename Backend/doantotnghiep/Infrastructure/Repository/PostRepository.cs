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
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public OutputFilterPaging<AccountPost> GetPostByProductID(Guid productID, int pageIndex, int pageSize)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"ProductID", productID);
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var entitites = DbConnection.Query<Account, Post, AccountPost>("Proc_GetPostByProductID",
                (a, p) =>
                {
                    AccountPost accountPost=new AccountPost();
                    accountPost.Post = p;
                    accountPost.Account = a;
                    return accountPost;
                },
                parameters, splitOn:"PostID", commandType: CommandType.StoredProcedure);

            var output = new OutputFilterPaging<AccountPost>();
            output.Data = entitites.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;
        }
        public AccountPost GetPostByID(Guid postID, bool admin)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"PostID", postID);
            parameters.Add($"Admin", admin);
            

            var entitites = DbConnection.Query<Account, Post, AccountPost>("Proc_GetPostByID",
                (a, p) =>
                {
                    AccountPost accountPost=new AccountPost();
                    accountPost.Post = p;
                    accountPost.Account = a;
                    return accountPost;
                },
                parameters, splitOn:"PostID", commandType: CommandType.StoredProcedure);
            //Trả về dữ liệu:
            return entitites.ToList().Count>0? entitites.ToList()[0]:null;
        }
        public OutputFilterPaging<PostProductAccount> GetPosts(string? queryText, int pageIndex, int pageSize)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"QueryText", queryText);
            parameters.Add($"PageIndex", pageIndex);
            parameters.Add($"PageSize", pageSize);
            parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var entitites = DbConnection.Query<Account,Product, Post, PostProductAccount>("Proc_GetPostProductAccount",
                (a,pd,p) =>
                {
                    PostProductAccount o = new PostProductAccount();
                    o.Post = p;
                    o.Account = a;
                    o.Product = pd;

                    return o;
                },
                parameters, splitOn: "ProductID,PostID", commandType: CommandType.StoredProcedure);

            var output = new OutputFilterPaging<PostProductAccount>();
            output.Data = entitites.ToList();
            output.TotalRecord = parameters.Get<int>("TotalRecord");
            output.TotalPage = parameters.Get<int>("TotalPage");
            //Trả về dữ liệu:
            return output;
        }

    }
}