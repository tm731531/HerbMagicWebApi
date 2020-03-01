using Dapper;
using HerbMagic.Repository.Common.Interface;
using HerbMagic.Repository.DTO.GovData;
using HerbMagic.Repository.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using Z.Dapper.Plus;

namespace HerbMagic.Repository.GovData
{
    /// <summary>
    /// 混和政府資料與Pchome資料Repository
    /// </summary>
    public class MixGovPcDataRepository : IDisposable
    {
        private IDatabaseConnectionHelper _DatabaseConnection { get; }

        internal MixGovPcDataRepository(IDatabaseConnectionHelper databaseConnectionHelper)
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
        ~MixGovPcDataRepository()
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
        /// Get all MixGovPc data 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MixGovPcDataDto> GetMixGovPcDataDtos()
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
                                  ,[name]
                                  ,[originprice]
                                  ,[pics]
                                  ,[picb]
                                  ,[login_number]
                                  ,[detailUri]
                                  ,[efficiency_rating]
                                  ,[key_word]
                                  ,[Pchome_Id]
                                  ,[data_from]
                                  ,[MothlyCost]
                                  ,[DailyCost]
                              FROM [dbo].[MixGovPcData] with(nolock)";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<MixGovPcDataDto>(sqlCommand);
                return result;
            }
        }

        /// <summary>
        /// Get MixGovPc data by key word
        /// </summary>
        /// <param name="keyWord">關鍵字</param>
        /// <returns></returns>
        public IEnumerable<MixGovPcDataDto> GetMixGovPcDataDtosByKeyWord(string keyWord)
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
                                  ,[name]
                                  ,[originprice]
                                  ,[pics]
                                  ,[picb]
                                  ,[login_number]
                                  ,[detailUri]
                                  ,[efficiency_rating]
                                  ,[key_word]
                                  ,[Pchome_Id]
                                  ,[data_from]
                                  ,[MothlyCost]
                                  ,[DailyCost]
                                FROM [dbo].[MixGovPcData] with(nolock)
                                Where [key_word] =@keyWord";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<MixGovPcDataDto>(sqlCommand, new { keyWord = keyWord }, commandType: CommandType.Text);
                return result;
            }
        }


        /// <summary>
        /// Get MixGovPc data By manufacturer
        /// </summary>
        /// <param name="manufacturer">製造商</param>
        /// <returns></returns>
        public IEnumerable<MixGovPcDataDto> GetMixGovPcDtosByManufacturer(string manufacturer)
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
                                ,[Pchome_Id]
                                ,[data_from]
                                FROM [dbo].[MixGovPcData] with(nolock)
                                Where [brand_name] =@brand_name";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<MixGovPcDataDto>(sqlCommand, new { brand_name = manufacturer }, commandType: CommandType.Text);
                return result;
            }
        }

        /// <summary>
        /// 取得關鍵字
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetKeyWord()
        {
            string sqlCommand = @"SELECT DISTINCT  [key_word] FROM [dbo].[GovData] with(nolock)";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<string>(sqlCommand,commandType: CommandType.Text);
                return result;
            }
        }

        public bool UpdateElectricityFee(IEnumerable<MixGovPcDataDto> mixGovPcDataDtos)
        {
            string sqlCommand = @"UPDATE [dbo].[MixGovPcData]
                                                               SET [MothlyCost] =@MothlyCost
                                                                  ,[DailyCost] = @DailyCost
                                                             WHERE [product_model] =@product_model and [data_from]=@data_from";
            bool isSuccess = true;
            

                using (var conn = _DatabaseConnection.Create())
                {
                    mixGovPcDataDtos.AsList().ForEach( item =>
                    {
                        conn.Execute(sqlCommand, item);
                    });
                }

            return isSuccess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public Select3CDataNumber GetSelect3CDataNumber(string keyWord)
        {
            string countMixDataSql = @"SELECT count(*) FROM [dbo].[MixGovPcData] with(nolock)  Where [key_word] =@keyWord";
            string countGovDataSql = @"SELECT count(*) FROM [dbo].[GovData] with(nolock)  Where [key_word] =@keyWord";
            string countMallDataSql = @"SELECT count(*) FROM [dbo].[PchomePrice] with(nolock) Where [key_word] =@keyWord";
            Select3CDataNumber select3CDataNumber = new Select3CDataNumber();
            using (var conn = _DatabaseConnection.Create())
            {
                select3CDataNumber.MixDataNumber= conn.QueryFirst<int>(countMixDataSql, new { keyWord }, commandType: CommandType.Text);
                select3CDataNumber.GovDataNumber = conn.QueryFirst<int>(countGovDataSql, new { keyWord }, commandType: CommandType.Text);
                select3CDataNumber.MallDataNumber = conn.QueryFirst<int>(countMallDataSql, new { keyWord }, commandType: CommandType.Text);
            }
            return select3CDataNumber;
        }
    }
}
