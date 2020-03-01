using System.Data;

namespace HerbMagic.Repository.Common.Interface
{
    public interface IDatabaseConnectionHelper
    {
        IDbConnection Create();
    }
}
