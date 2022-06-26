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
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #region Methods
        public List<Department> GetDepartmentsByTeacherId(Guid TeacherId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("TeacherId", TeacherId);
            var departments = DbConnection.Query<Department>(@"Proc_GetDepartmentsByTeacherId",parameters, commandType: CommandType.StoredProcedure);
            return Enumerable.ToList(departments);
        }
        /// <summary>
        /// Hàm kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true- đã tồn tại; false- chưa tồn tại</returns>
        /// CreatedBy: TTLONG (26/7/2021)
        public bool CheckDuplicateDepartmentCode(string departmentCode)
        {
            var isDuplicate = false;

            var sqlCommand = $"SELECT DepartmentCode FROM Department where DepartmentCode = @DepartmentCode";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@DepartmentCode", departmentCode);

            //Lấy dữ liệu với Dapper:
            var departments = DbConnection.QueryFirstOrDefault<string>(sql: sqlCommand, param: paramaters);
            if (departments != null)
                isDuplicate = true;

            return isDuplicate;
        }
        #endregion
    }
}
