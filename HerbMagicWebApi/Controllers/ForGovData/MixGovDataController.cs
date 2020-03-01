using AutoMapper;
using HerbMagic.Repository.DTO.GovData;
using HerbMagic.Repository.Factory;
using HerbMagicWebApi.Models;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web.Http;
using HerbMagicWebApi.Common;
using HerbMagic.Repository.Model;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace HerbMagicWebApi.Controllers.ForGovData
{
    /// <summary>
    /// 
    /// </summary>
    public class MixGovDataController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IEnumerable<MixGovPcModels> GetMixGovDataByKeyword(string keyWord)
        {
            var dtoResult = GovDataFactory.MixGovPcDataRepository.GetMixGovPcDataDtosByKeyWord(keyWord);
            return MapperHelper.MapperProperties<MixGovPcDataDto, MixGovPcModels>(dtoResult);
        }
        /*
        [HttpGet]
        [Route("MixGovData/BulkUpdateElectricityFee/{passWord}")]
        public bool BulkUpdateElectricityFee(string passWord)
        {
            if (!string.IsNullOrWhiteSpace(passWord) && passWord == "癡漢昇昇發大財")
            {
                var dtoResult = GovDataFactory.MixGovPcDataRepository.GetMixGovPcDataDtos();
                ConcurrentStack<MixGovPcDataDto> result = new ConcurrentStack<MixGovPcDataDto>();
                Parallel.ForEach(dtoResult, item =>
                {
                    CalculationCost(ref item);
                    result.Push(item);
                });
                var r = GovDataFactory.MixGovPcDataRepository.UpdateElectricityFee(result);
                return r;
            }
            else
            {
                return false;
            }
        }
        */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("MixGovData/GetSelect3CDataNumber/{keyWord}")]
        public Select3CDataNumber GetSelect3CDataNumber(string keyWord)
        {
            return GovDataFactory.MixGovPcDataRepository.GetSelect3CDataNumber(keyWord);
        }

        private void CalculationCost(ref MixGovPcDataDto model)
        {
            decimal energyCost = default(decimal);
            decimal dayCost = default(decimal);
            decimal monthCost = default(decimal);
            EnergyEfficiencyModels energyEfficiencyModels = JsonConvert.DeserializeObject<EnergyEfficiencyModels>(model?.test_report_of_energy_efficiency);
            switch (model.key_word)
            {
                case "冷暖空調":
                    energyCost = Convert.ToDecimal(energyEfficiencyModels.CSPF?.Split(' ').FirstOrDefault());
                    dayCost = energyCost * 24 * 4;
                    monthCost = energyCost * 24 * 30 * 4;
                    break;
                case "冰溫熱型開飲機":
                case "冰溫熱型飲水機":
                case "溫熱型開飲機":
                case "溫熱型飲水機":
                case "電熱水瓶":
                    energyCost = Convert.ToDecimal(energyEfficiencyModels.est24?.Split(' ').FirstOrDefault());
                    dayCost = energyCost * 4;
                    monthCost = dayCost * 30;
                    break;

                case "除濕機":
                    energyCost = (Convert.ToDecimal(energyEfficiencyModels.rated_dehumidification_capacity?.Split(' ').FirstOrDefault())) / (Convert.ToDecimal(energyEfficiencyModels.energy_factor_value?.Split(' ').FirstOrDefault()));
                    dayCost = energyCost * 4;
                    monthCost = dayCost * 30;
                    break;
                case "電冰箱":
                    energyCost = (Convert.ToDecimal(energyEfficiencyModels.effective_internal_volume?.Split(' ').FirstOrDefault())) / (Convert.ToDecimal(energyEfficiencyModels.energy_factor_value?.Split(' ').FirstOrDefault())); ;
                    monthCost = energyCost * 4;
                    dayCost = monthCost / 30;
                    break;
                default:
                    break;
            }
            model.MothlyCost = monthCost;
            model.DailyCost = dayCost;
        }
    }
}