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
    public class PchomePriceRepository : IDisposable
    {
        private IDatabaseConnectionHelper _DatabaseConnection { get; }

        internal PchomePriceRepository(IDatabaseConnectionHelper databaseConnectionHelper)
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
        ~PchomePriceRepository()
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
        public IEnumerable<PchomePriceDto> GetPchomePriceDtos()
        {
            string sqlCommand = @"SELECT [Id]
                                  ,[name]
                                  ,[originprice]
                                  ,[pics]
                                  ,[picb]
                                  ,[key_word]
                                  ,[data_from]
                              FROM [dbo].[PchomePrice] with(nolock)";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<PchomePriceDto>(sqlCommand);
                return result;
            }
        }

        /// <summary>
        /// Get MixGovPc data by key word
        /// </summary>
        /// <param name="keyWord">關鍵字</param>
        /// <returns></returns>
        public IEnumerable<PchomePriceDto> GetMixGovPcDataDtosByKeyWord(string keyWord)
        {
            string sqlCommand = @"SELECT [Id]
                                    ,[name]
                                    ,[originprice]
                                    ,[pics]
                                    ,[picb]
                                    ,[key_word]
                                  ,[data_from]
                                FROM [dbo].[PchomePrice] with(nolock)
                                Where [key_word] =@keyWord";
            using (var conn = _DatabaseConnection.Create())
            {
                var result = conn.Query<PchomePriceDto>(sqlCommand, new { keyWord = keyWord }, commandType: CommandType.Text);
                return result;
            }
        }
    }
}
