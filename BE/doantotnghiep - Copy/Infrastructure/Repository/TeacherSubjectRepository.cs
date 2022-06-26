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
    public class TeacherSubjectRepository : BaseRepository<TeacherSubject>, ITeacherSubjectRepository
    {
        public TeacherSubjectRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public int DeleteTeacherSubject(Guid teacherId, Guid SubjectId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("TeacherId", teacherId);
            parameters.Add("SubjectId", SubjectId);
            var rowAffect = DbConnection.Execute("Proc_DeleteTeacherSubjectByTeacherIdAndSubjectId", parameters, commandType: CommandType.StoredProcedure);
            return rowAffect;
        }
    }
}