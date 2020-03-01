using HerbMagic.Repository.Common.Enum;
using HerbMagic.Repository.Common.Interface;
using System.Data;
using System.Data.SqlClient;

namespace HerbMagic.Repository.Common.Helper
{
    public  class BookStoreDbConnectionHelper : IDatabaseConnectionHelper
    {
        private readonly string _connectionString;

        public BookStoreDbConnectionHelper()
        {
            this._connectionString = new ConnectionStringFactory().GetConnectionString(DataBaseEnum.BookStore);
        }

        /// <summary>
        /// Create DbConnection
        /// </summary>
        /// <returns></returns>
        public IDbConnection Create()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            return sqlConnection;
        }
    }
}
