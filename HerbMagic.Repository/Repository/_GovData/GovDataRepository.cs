using Dapper;
using HerbMagic.Repository.Common.Interface;
using HerbMagic.Repository.DTO.GovData;
using System;
using System.Collections.Generic;
using System.Data;

namespace HerbMagic.Repository.GovData
{
    /// <summary>
    /// 政府開放資料Repository
    /// </summary>
    public class GovDataRepository:IDisposable
    {
        private IDatabaseConnectionHelper _DatabaseConnection { get; }

        internal GovDataRepository(IDatabaseConnectionHelper databaseConnectionHelper)
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
        ~GovDataRepository()
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
        /// Get All Gov data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GovDataDto> GetGovDataDtos()
        {
            string sqlCommand = @"SELECT [product_model]
                                ,[brand_name]
                                ,[labeling_company]
                                ,[from_date_of_expiration]
                                ,[to_date_of_expiration]
                                ,[efficiency_benchmark]
                                ,[test_report_of_energy_efficiency]
                                ,[annual_power_consumption_degrees_dive_year]
                                ,[Id]
                                ,[login_number]
                                ,[detailUri]
                                ,[efficiency_rating]
                                ,[key_word]
                                FROM [dbo].[GovData] with(nolock)";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<GovDataDto>(sqlCommand);
                return result;
            }
        }

        /// <summary>
        /// Get Gov data by key word
        /// </summary>
        /// <param name="keyWord">關鍵字</param>
        /// <returns></returns>
        public IEnumerable<GovDataDto> GetGovDataDtosByKeyWord(string keyWord)
        {
            string sqlCommand = @"SELECT [product_model]
                                ,[brand_name]
                                ,[labeling_company]
                                ,[from_date_of_expiration]
                                ,[to_date_of_expiration]
                                ,[efficiency_benchmark]
                                ,[test_report_of_energy_efficiency]
                                ,[annual_power_consumption_degrees_dive_year]
                                ,[Id]
                                ,[login_number]
                                ,[detailUri]
                                ,[efficiency_rating]
                                ,[key_word]
                                FROM [dbo].[GovData] with(nolock)
                                Where [key_word] =@keyWord";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<GovDataDto>(sqlCommand, new { keyWord= keyWord}, commandType:CommandType.Text);
                return result;
            }
        }

        /// <summary>
        /// Get Gov data By manufacturer
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        public IEnumerable<GovDataDto> GetGovDataDtosByManufacturer(string manufacturer)
        {
            string sqlCommand = @"SELECT [product_model]
                                ,[brand_name]
                                ,[labeling_company]
                                ,[from_date_of_expiration]
                                ,[to_date_of_expiration]
                                ,[efficiency_benchmark]
                                ,[test_report_of_energy_efficiency]
                                ,[annual_power_consumption_degrees_dive_year]
                                ,[Id]
                                ,[login_number]
                                ,[detailUri]
                                ,[efficiency_rating]
                                ,[key_word]
                                FROM [dbo].[GovData] with(nolock)
                                Where [brand_name] =@brand_name";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<GovDataDto>(sqlCommand, new { brand_name = manufacturer }, commandType: CommandType.Text);
                return result;
            }
        }
    }
}
