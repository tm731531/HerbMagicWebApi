using System;

namespace HerbMagic.Repository.DTO.GovData
{
    public class MixGovPcDataDto
    {
        public string product_model { set; get; }
        public string brand_name { set; get; }
        public string labeling_company { set; get; }
        public DateTime from_date_of_expiration { set; get; }
        public DateTime to_date_of_expiration { set; get; }
        public string efficiency_benchmark { set; get; }
        public string test_report_of_energy_efficiency { set; get; }
        public string annual_power_consumption_degrees_dive_year { set; get; }
        public string Id { set; get; }
        public string name { set; get; }
        public int originprice { set; get; }
        public string pics { set; get; }
        public string picb { set; get; }
        public string login_number { set; get; }
        public string detailUri { set; get; }
        public int efficiency_rating { set; get; }
        public string key_word { get; set; }
        public string Pchome_Id { get; set; }
        public string data_from { get; set; }
        public decimal MothlyCost { get; set; }
        public decimal DailyCost { get; set; }
    }
}
