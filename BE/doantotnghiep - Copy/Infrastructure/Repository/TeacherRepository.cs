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
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(IConfiguration configuration) : base(configuration)
        {

        }
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true/false</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public bool CheckDuplicateEmployeeCode(string employeeCode)
        {
          

            var sqlCommand = $"Select exists(SELECT EmployeeCode FROM Employee where EmployeeCode = @EmployeeCode)";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@EmployeeCode", employeeCode);

            //Lấy dữ liệu với Dapper:
            var isDuplicate = DbConnection.QueryFirstOrDefault<bool>(sql: sqlCommand, param: paramaters);
            

            return isDuplicate;
        }
        /// <summary>
        /// Lọc dữ liệu
        /// </summary>
        /// <param name="employeeFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public IEnumerable<Teacher> GetFilter(string teacherFilter, Guid? combinationSubjectId)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"TeacherFilter", teacherFilter);
                parameters.Add($"CombinationSubjectId", combinationSubjectId);
            //Khởi tạo các CommandText:
            /*var entitites = DbConnection.Query<Teacher,CombinationSubject,Teacher>("Proc_GetTeachersFilter",(teacher,combination)=> {
                if (combination != null)
                {
                    teacher.CombinationSubject = combination;

                }
                return teacher;

            } 
            ,parameters, commandType: CommandType.StoredProcedure,splitOn: "CombinationSubjectId");*/
            var entitites = DbConnection.Query<Teacher>("Proc_GetTeachersFilter", parameters, commandType: CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            
        }
       /* public IEnumerable<TeacherDepartment> GetTeacher() { }*/
       /// <summary>
       /// kiểm tra tồn tại teacher trong combinationsubject
       /// </summary>
       /// <param name="combinationSubjectId">id tổ bộ môn</param>
       /// <returns>true: có, false:không</returns>
        public bool CheckExistsCombinationSubject(Guid combinationSubjectId)
        {
            var isDuplicate = false;

            var sqlCommand = $"SELECT 1  FROM Teacher where CombinationSubjectId = @CombinationSubjectId";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@CombinationSubjectId", combinationSubjectId);

            //Lấy dữ liệu với Dapper:
            var numberRecord = DbConnection.QueryFirstOrDefault<string>(sql: sqlCommand, param: paramaters);
            if (numberRecord != null)
                isDuplicate = true;

            return isDuplicate;
        }
        
        /*public override int Delete(Guid entityId)
        {
            var rowAffects = 0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    //Xử lý các dữ liệu:
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"@TeacherId", entityId);

                   *//* var count = 0;

                    var rowe = DbConnection.Query<Teacher>($"Proc_GetTeacherDepartmentByTeacherId", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    //THực thi commandText:
                    if (rowe != null)
                    {
                        count = DbConnection.Execute($"Proc_DeleteTeacherDepartmentByTeacherId", parameters, transaction: transaction, null, CommandType.StoredProcedure);
                    }*//*
                    rowAffects = DbConnection.Execute($"Proc_DeleteTeacherById", parameters, transaction: transaction, null, CommandType.StoredProcedure);


                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    //transaction.Dispose();
                    DbConnection.Close();
                }

            }
            //Trả về dữ liệu:

            return rowAffects;

        }*/
        /// <summary>
        /// lấy thông tin teacher theo ngày tạo mới nhất
        /// </summary>
        /// <returns>thông tin teacher</returns>
        public Teacher GetTeacherByCreatedDate()
        {
            var entity = DbConnection.QueryFirstOrDefault<Teacher>($"Proc_GetTeacherByCreatedDate",commandType: CommandType.StoredProcedure);
            return entity;
        }
    }
}