using HerbMagic.Repository.DTO.GovData;
using HerbMagic.Repository.Factory;
using HerbMagicWebApi.Common;
using HerbMagicWebApi.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace HerbMagicWebApi.Controllers.ForGovData
{
    /// <summary>
    /// 
    /// </summary>
    public class GovDataController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IEnumerable<GovDataModels> GetGovDataByKeyword(string keyWord)
        {
            var dtoResult = GovDataFactory.GovDataRepository.GetGovDataDtosByKeyWord(keyWord);
            return MapperHelper.MapperProperties<GovDataDto, GovDataModels>(dtoResult);
        }
    }
}