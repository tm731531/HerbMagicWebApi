using System;

namespace HerbMagic.Repository.DTO.GovData
{
    public class GovDataDto
    {
        public string product_model { set; get; }
        public string brand_name { set; get; }
        public string labeling_company { set; get; }
        public DateTime from_date_of_expiration { set; get; }
        public DateTime to_date_of_expiration { set; get; }
        public string efficiency_benchmark { set; get; }
        public string test_report_of_energy_efficiency { set; get; }
        public decimal annual_power_consumption_degrees_dive_year { set; get; }
        public string Id { set; get; }
        public string login_number { set; get; }
        public string detailUri { set; get; }
        public int efficiency_rating { set; get; }
        public string key_word { set; get; }
    }
}
