using AutoMapper;
using HerbMagic.Repository.DTO.GovData;
using HerbMagic.Repository.Factory;
using HerbMagicWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HerbMagicWebApi.Controllers.ForGovData
{
    /// <summary>
    /// 
    /// </summary>
    public class PchomePriceController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IEnumerable<PchomePriceModels> GetPchomePriceByKeyword(string keyWord)
        {
            var dtoResult = GovDataFactory.PchomePriceRepository.GetMixGovPcDataDtosByKeyWord(keyWord);
            IEnumerable<PchomePriceModels> outPut = default(IEnumerable<PchomePriceModels>);
            if (dtoResult != null && dtoResult.Count() > 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PchomePriceDto, PchomePriceModels>(MemberList.None)
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.originprice))
                    .ForMember(dest => dest.PictrueA, opt => opt.MapFrom(src => src.pics))
                    .ForMember(dest => dest.PictrueB, opt => opt.MapFrom(src => src.picb));
                });
                config.AssertConfigurationIsValid();//←證驗應對
                var mapper = config.CreateMapper();
                outPut = mapper.Map<IEnumerable<PchomePriceModels>>(dtoResult);
            }
            return outPut;
        } 
    }
}