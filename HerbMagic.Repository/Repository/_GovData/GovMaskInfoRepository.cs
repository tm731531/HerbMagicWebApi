using Dapper;
using HerbMagic.Repository.Common.Interface;
using HerbMagic.Repository.DTO.GovData;
using System;
using System.Collections.Generic;
using System.Data;

namespace HerbMagic.Repository.GovData
{
    /// <summary>
    /// Pchome資料Repository
    /// </summary>
    public class GovMaskInfoRepository : IDisposable
    {
        private IDatabaseConnectionHelper _DatabaseConnection { get; }

        internal GovMaskInfoRepository(IDatabaseConnectionHelper databaseConnectionHelper)
        {
            this._DatabaseConnection = databaseConnectionHelper;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    return;
                }
                disposedValue = true;
            }
        }
        ~GovMaskInfoRepository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// Get All Pchome data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GovMaskHospitalInfoDto> GetGovMaskInfoDtos(string city, string distict, string road)
        {
            string sqlCommand = $@"SELECT 
                                	    [GovHospitalInfo].hospital_name
                                      , [GovHospitalInfo].hospital_address
                                      , [GovHospitalInfo].hospital_cellphone
                                      , [audit_mask_count]
                                      , [child_mask_count]
                                      , [dataTime]
                                  FROM [dbo].[GovMaskInfo] (nolock)
                                  join [GovHospitalInfo](nolock) on  [GovMaskInfo].[hospital_id] = [GovHospitalInfo].[hospital_id]
                                  where 
                                   [hospital_name] like N'%{city}%' 
                                  order by [dataTime] desc   ";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<GovMaskHospitalInfoDto>(sqlCommand);

                return result;
            }
        }

        public IEnumerable<GovMaskHospitalInfoDto> GetGovMaskInfoDtosbySP(double longitude, double latitude)
        {

            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<GovMaskHospitalInfoDto>("GetHospital", new { Longitude = longitude, Latitude = latitude, RankType = 1 },
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        public IEnumerable<GovMaskHospitalInfoDto> GetGovMaskInfoDtosbySPWithName(string name)
        {

            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<GovMaskHospitalInfoDto>("GetHospitalByName", new { Name = name },
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public bool InsertGovMaskInfoDtos(GovMaskInfoDto govMaskInfoDto)
        {
            string sqlCommand = @"
                            INSERT INTO [dbo].[GovMaskInfo]
                                       ([hospital_id]
                                       ,[audit_mask_count]
                                       ,[child_mask_count]
                                       ,[dataTime])
                                 VALUES
                                       (@hospital_id
                                       ,@audit_mask_count
                                       ,@child_mask_count
                                       ,@dataTime)";

            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Execute(sqlCommand,

                    new
                    {
                        hospital_id = govMaskInfoDto.hospital_id,
                        audit_mask_count = govMaskInfoDto.audit_mask_count,
                        child_mask_count = govMaskInfoDto.child_mask_count,
                        dataTime = govMaskInfoDto.dataTime

                    });
                return (int)result > 0;
            }

        }

        public bool BulkInsertGovMaskInfoDtos(List<GovMaskInfoDto> lgovMaskInfoDto)
        {
            string sqlCommand = @"
                            INSERT INTO [dbo].[GovMaskInfo]
                                       ([hospital_id]
                                       ,[audit_mask_count]
                                       ,[child_mask_count]
                                       ,[dataTime])
                                 VALUES
                                       (@hospital_id
                                       ,@audit_mask_count
                                       ,@child_mask_count
                                       ,@dataTime)";

            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Execute(sqlCommand, lgovMaskInfoDto);
                return (int)result > 0;
            }

        }

    }
}
