using Dapper;
using Microsoft.Extensions.Configuration;
using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CombinationSubjectRepository:BaseRepository<CombinationSubject>, ICombinationSubjectRepository
    {
        public CombinationSubjectRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #region Methods

        /// <summary>
        /// Hàm kiểm tra mã phòng bahn đã tồn tại hay chưa
        /// </summary>
        /// <param name="positionCode">Mã phòng ban</param>
        /// <returns>true- đã tồn tại; false- chưa tồn tại</returns>
        /// CreatedBy: TTLONG (26/7/2021)
        public bool CheckDuplicatePositionCode(string positionCode)
        {
            var isDuplicate = false;

            var sqlCommand = $"SELECT PositionCode FROM CombinationSubject where PositionCode = @PositionCode";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@PositionCode", positionCode);

            //Lấy dữ liệu với Dapper:
            var positions = DbConnection.QueryFirstOrDefault<string>(sql: sqlCommand, param: paramaters);
            if (positions != null)
                isDuplicate = true;

            return isDuplicate;
        }
        #endregion
    }
}
