using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using HerbMagic.Repository.DTO.GovData;
using HerbMagic.Repository.Factory;
using HerbMagicWebApi.Models;
using Newtonsoft.Json;

namespace HerbMagicWebApi.Controllers.ForGovData
{
    /// <summary>
    /// ISelect 3C
    /// </summary>
    public class ISelect3CController : Controller
    {
        /// <summary>
        /// ISelect3C Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var keyWords = GovDataFactory.MixGovPcDataRepository.GetKeyWord();
            List < SelectListItem >  selectListItems= new List<SelectListItem>();
            foreach (var item in keyWords)
            {
                selectListItems.Add(new SelectListItem() {
                    Value = item,
                    Text = item,
                    Selected = item.Equals("冷暖空調")
                });
            }
            ViewBag.Item = selectListItems;
            return View();
        }

        /// <summary>
        /// MixDataGrid PartialView
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public PartialViewResult MixDataGrid(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                keyWord = "冷暖空調";
            }
            var dtoResult = GovDataFactory.MixGovPcDataRepository.GetMixGovPcDataDtosByKeyWord(keyWord);
            var viewModel = default(IEnumerable<MixGovPcModels>);
            string pchomeUrl = "https://24h.pchome.com.tw/prod/";
            string momoUrl = "https://www.momoshop.com.tw/goods/GoodsDetail.jsp?i_code=";
            if (dtoResult != null && dtoResult.Count() > 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MixGovPcDataDto, MixGovPcModels>(MemberList.None)
                    .ForMember(dest => dest.pics, opt => opt.MapFrom(src => (src.data_from == "Pchome") ? $"<a href='{pchomeUrl}{src.Pchome_Id}'target='_blank'><img class='photoSize' src = '{src.pics}'/></a>" : $"<a href='{momoUrl}{src.Pchome_Id}' target='_blank'><img class='photoSize' src = '{src.pics}'/></a>"));
                });
                config.AssertConfigurationIsValid();//←證驗應對
                var mapper = config.CreateMapper();
                viewModel = mapper.Map<IEnumerable<MixGovPcModels>>(dtoResult);
            }
            return PartialView("_MixDataGrid", viewModel);
        }

        /// <summary>
        ///  MallDataGrid PartialView
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public PartialViewResult MallDataGrid(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                keyWord = "冷暖空調";
            }
            var dtoResult = GovDataFactory.PchomePriceRepository.GetMixGovPcDataDtosByKeyWord(keyWord);
            
            IEnumerable<PchomePriceModels> viewModel = default(IEnumerable<PchomePriceModels>);
            string pchomeUrl = "https://24h.pchome.com.tw/prod/";
            string momoUrl = "https://www.momoshop.com.tw/goods/GoodsDetail.jsp?i_code=";
            if (dtoResult != null && dtoResult.Count() > 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PchomePriceDto, PchomePriceModels>(MemberList.None)
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.originprice))
                    .ForMember(dest => dest.PictrueA, opt => opt.MapFrom(src =>  (src.data_from == "Pchome")? $"<a href='{pchomeUrl}{src.Id}'target='_blank'><img class='photoSize' src = '{src.pics}'/></a>": $"<a href='{momoUrl}{src.Id}' target='_blank'><img class='photoSize' src = '{src.pics}'/></a>"))
                    .ForMember(dest => dest.PictrueB, opt => opt.MapFrom(src => src.picb));
                });
                config.AssertConfigurationIsValid();//←證驗應對
                var mapper = config.CreateMapper();
                viewModel = mapper.Map<IEnumerable<PchomePriceModels>>(dtoResult);
            }
            return PartialView("_MallDataGrid", viewModel);
        }

        /// <summary>
        ///  GovDataGrid PartialView
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public PartialViewResult GovDataGrid(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                keyWord = "冷暖空調";
            }
            var dtoResult = GovDataFactory.GovDataRepository.GetGovDataDtosByKeyWord(keyWord);
            IEnumerable<GovDataModels> viewModel = default(IEnumerable<GovDataModels>);
            if (dtoResult != null && dtoResult.Count() > 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<GovDataDto, GovDataModels>(MemberList.None)
                     .ForMember(dest => dest.test_report_of_energy_efficiency, opt => opt.MapFrom(src => EnergyEfficiencyHandler(src.test_report_of_energy_efficiency, keyWord)));
                });
                config.AssertConfigurationIsValid();//←證驗應對
                var mapper = config.CreateMapper();
                viewModel = mapper.Map<IEnumerable<GovDataModels>>(dtoResult);
            }
            return PartialView("_GovDataGrid", viewModel);
        }



        /// <summary>
        /// ISelcet 3C Energy Calculator Page
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="productModel"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult GetDetail(string keyWord, string productModel)
        {
            var dtoResults = GovDataFactory.MixGovPcDataRepository.GetMixGovPcDataDtosByKeyWord(keyWord);
            var dtoResult = dtoResults.Where(w => w.product_model == productModel && w.data_from?.Trim().ToUpper() == "PCHOME").FirstOrDefault();
            if (dtoResult == null)
                dtoResult = dtoResults.Where(w => w.product_model == productModel && w.data_from?.Trim().ToUpper() == "MOMO").FirstOrDefault();

            EnergyEfficiencyModels energyEfficiencyModels = null;
            if (dtoResult != null)
            {
                energyEfficiencyModels = JsonConvert.DeserializeObject<EnergyEfficiencyModels>(dtoResult?.test_report_of_energy_efficiency);

                energyEfficiencyModels.detailUri = dtoResult?.detailUri;
                energyEfficiencyModels.efficiency_rating = dtoResult.efficiency_rating;
                energyEfficiencyModels.originprice = dtoResult.originprice;
                energyEfficiencyModels.keyWord = keyWord;
                energyEfficiencyModels.efficiency_benchmark = dtoResult?.efficiency_benchmark;
                energyEfficiencyModels.dayCost = dtoResult.DailyCost;
                energyEfficiencyModels.monthCost = dtoResult.MothlyCost;
            }
            else
            {
                energyEfficiencyModels = new EnergyEfficiencyModels();
            }
            return PartialView("_GetDetail", energyEfficiencyModels);
        }

        /// <summary>
        /// Energy Efficiency Handler
        /// </summary>
        /// <param name="strEnergyEfficiency"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private string EnergyEfficiencyHandler(string strEnergyEfficiency, string keyWord)
        {
            string result = string.Empty;
            string htmlWrap = "<br/>";
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(strEnergyEfficiency))
            {
                EnergyEfficiencyModels energyEfficiencyModels = JsonConvert.DeserializeObject<EnergyEfficiencyModels>(strEnergyEfficiency);
                switch (keyWord)
                {
                    case "冷暖空調":
                        stringBuilder.Append("能源效率值EER:");
                        stringBuilder.Append(energyEfficiencyModels.EER);
                        stringBuilder.Append(htmlWrap);
                        stringBuilder.Append("冷氣季節性能因數CSPF:");
                        stringBuilder.Append(energyEfficiencyModels.CSPF);
                        break;
                    case "冰溫熱型開飲機":
                    case "冰溫熱型飲水機":
                    case "溫熱型開飲機":
                    case "溫熱型飲水機":
                    case "電熱水瓶":
                        stringBuilder.Append("est24:");
                        stringBuilder.Append(energyEfficiencyModels.est24);
                        break;
                    case "除濕機":
                        stringBuilder.Append("額定除濕能力:");
                        stringBuilder.Append(energyEfficiencyModels.rated_dehumidification_capacity);
                        stringBuilder.Append(htmlWrap);
                        stringBuilder.Append("能源因數值:");
                        stringBuilder.Append(energyEfficiencyModels.energy_factor_value);
                        break;
                    case "電冰箱":
                        stringBuilder.Append("能源因數值:");
                        stringBuilder.Append(energyEfficiencyModels.energy_factor_value);
                        break;
                    default:
                        break;
                }
            }
            result = stringBuilder.ToString();
            return result;
        }
    }
}