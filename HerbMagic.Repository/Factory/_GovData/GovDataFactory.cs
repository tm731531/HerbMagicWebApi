using HerbMagic.Repository.Common.Helper;
using HerbMagic.Repository.GovData;

namespace HerbMagic.Repository.Factory
{
    public class GovDataFactory
    {
        private static MixGovPcDataRepository _mixGovPcDataRepository => new MixGovPcDataRepository(new BookStoreDbConnectionHelper());
        private static GovDataRepository _govDataRepository => new GovDataRepository(new BookStoreDbConnectionHelper());
        private static PchomePriceRepository _pchomePriceRepository => new PchomePriceRepository(new BookStoreDbConnectionHelper());
        private static GovMaskInfoRepository _govMaskInfoRepository => new GovMaskInfoRepository(new BookStoreDbConnectionHelper());
        private static GovHospitalInfoRepository _govHospitalInfoRepository => new GovHospitalInfoRepository(new BookStoreDbConnectionHelper());
        private static GovMaskUsageLogRepository _govMaskUsageLogRepository => new GovMaskUsageLogRepository(new BookStoreDbConnectionHelper());



        public static MixGovPcDataRepository MixGovPcDataRepository = _mixGovPcDataRepository;
        public static GovDataRepository GovDataRepository = _govDataRepository;
        public static PchomePriceRepository PchomePriceRepository = _pchomePriceRepository;
        public static GovMaskInfoRepository GovMaskInfoRepository = _govMaskInfoRepository;
        public static GovHospitalInfoRepository GovHospitalInfoRepository = _govHospitalInfoRepository;
        public static GovMaskUsageLogRepository GovMaskUsageLogRepository = _govMaskUsageLogRepository;
    }
}
