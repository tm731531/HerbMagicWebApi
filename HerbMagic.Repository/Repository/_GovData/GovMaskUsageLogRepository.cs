using Dapper;
using HerbMagic.Repository.Common.Interface;
using HerbMagic.Repository.DTO.GovData;
using System;
using System.Collections.Generic;
using System.Data;

namespace HerbMagic.Repository.GovData
{
    public class GovMaskUsageLogRepository : IDisposable
    {
        private IDatabaseConnectionHelper _DatabaseConnection { get; }

        internal GovMaskUsageLogRepository(IDatabaseConnectionHelper databaseConnectionHelper)
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
        ~GovMaskUsageLogRepository()
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
        /// Get All Log data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GovMaskHospitalInfoDto> GovMaskUsageLogDtos(string city, string distict, string road)
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
                                   hospital_address like N'%{city}%' and
                                  hospital_address like N'%{distict}%' and
                                  hospital_address like N'%{road}%' 
                                  order by [dataTime] desc   ";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<GovMaskHospitalInfoDto>(sqlCommand);

                return result;
            }
        }

        public bool InsertMaskUsageLogDtos(GovMaskUsageLogDto govMaskUsageLog)
        {
            string sqlCommand = @"
                                    INSERT INTO [dbo].[GovMaskUsageLog]
                                               ([line_id]
                                               ,[group_id]
                                               ,[room_id]
                                               ,[user_longitude]
                                               ,[user_latitude]
                                               ,[create_time])
                                         VALUES
                                               (@line_id
                                               ,@group_id
                                               ,@room_id
                                               ,@user_longitude
                                               ,@user_latitude
                                               ,@create_time )

";

            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Execute(sqlCommand,

                    new
                    {
                        line_id = govMaskUsageLog.line_id,
                        group_id = govMaskUsageLog.group_id,
                        room_id = govMaskUsageLog.room_id,
                        user_longitude = govMaskUsageLog.user_longitude,
                        user_latitude = govMaskUsageLog.user_latitude,
                        create_time= govMaskUsageLog.create_time

                    });
                return (int)result > 0;
            }

        }


    }
}
