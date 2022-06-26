using Dapper;
using Microsoft.Extensions.Configuration;
using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #region Methods
        public List<Subject> GetSubjectsByTeacherId(Guid TeacherId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("TeacherId", TeacherId);
            var Subjects = DbConnection.Query<Subject>(@"Proc_GetSubjectsByTeacherId",parameters, commandType: CommandType.StoredProcedure);
            return Enumerable.ToList(Subjects);
        }
        /// <summary>
        /// Hàm kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true- đã tồn tại; false- chưa tồn tại</returns>
        /// CreatedBy: TTLONG (26/7/2021)
        public bool CheckDuplicateSubjectCode(string SubjectCode)
        {
            var isDuplicate = false;

            var sqlCommand = $"SELECT SubjectCode FROM Subject where SubjectCode = @SubjectCode";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@SubjectCode", SubjectCode);

            //Lấy dữ liệu với Dapper:
            var Subjects = DbConnection.QueryFirstOrDefault<string>(sql: sqlCommand, param: paramaters);
            if (Subjects != null)
                isDuplicate = true;

            return isDuplicate;
        }
        #endregion
    }
}
