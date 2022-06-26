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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public IEnumerable<Guid> GetCategoryIDByCategoryParentID(Guid categoryParentID)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"CategoryID", categoryParentID);

            var entitites = DbConnection.Query<Guid>("Proc_GetCategoryIDByCategoryParentID",
                parameters, commandType: CommandType.StoredProcedure);

            //Trả về dữ liệu:
            return entitites;
        }

    }
}