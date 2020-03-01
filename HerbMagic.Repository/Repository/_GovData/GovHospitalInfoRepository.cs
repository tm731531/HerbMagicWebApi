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
    public class GovHospitalInfoRepository : IDisposable
    {
        private IDatabaseConnectionHelper _DatabaseConnection { get; }

        internal GovHospitalInfoRepository(IDatabaseConnectionHelper databaseConnectionHelper)
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
        ~GovHospitalInfoRepository()
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
        /// Get All GovHospital data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GovHospitalInfoDto> GetGovHospitalInfoDtos()
        {
            string sqlCommand = @"SELECT [hospital_id]
                                        ,[hospital_name]
                                        ,[hospital_address]
                                        ,[hospital_cellphone]
                              FROM [dbo].[GovHospitalInfo] with(nolock)";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<GovHospitalInfoDto>(sqlCommand);
                return result;
            }
        }

        public bool InsertGovHospitalInfoDtos(GovHospitalInfoDto  govHospitalInfoDto)
        {
            string sqlCommand = @"INSERT INTO [dbo].[GovHospitalInfo]
                                                   ([hospital_id]
                                                   ,[hospital_name]
                                                   ,[hospital_address]
                                                   ,[hospital_cellphone])
                                             VALUES
                                                   (@hospital_id
                                                   ,@hospital_name
                                                   ,@hospital_address
                                                   ,@hospital_cellphone)";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Execute(sqlCommand,

                    new {
                        hospital_id = govHospitalInfoDto.hospital_id,
                        hospital_name = govHospitalInfoDto.hospital_name,
                        hospital_address = govHospitalInfoDto.hospital_address,
                        hospital_cellphone = govHospitalInfoDto.hospital_cellphone

                    });
                return (int)result>0;
            }
        }

    }
}
