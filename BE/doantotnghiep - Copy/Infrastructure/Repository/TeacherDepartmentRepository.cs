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
    public class TeacherDepartmentRepository : BaseRepository<TeacherDepartment>, ITeacherDepartmentRepository
    {
        public TeacherDepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public int DeleteTeacherDepartment(Guid teacherId, Guid departmentId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("TeacherId", teacherId);
            parameters.Add("DepartmentId", departmentId);
            var rowAffect = DbConnection.Execute("Proc_DeleteTeacherDepartmentByTeacherIdAndDepartmentId", parameters, commandType: CommandType.StoredProcedure);
            return rowAffect;
        }
    }
}