using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MixGovPcModels
    {
        public string product_model { get; set; }
        public string brand_name { get; set; }
        public string labeling_company { get; set; }
        public DateTime from_date_of_expiration { get; set; }
        public DateTime to_date_of_expiration { get; set; }
        public string efficiency_benchmark { get; set; }
        public EnergyEfficiencyModels test_report_of_energy_efficiency { get; set; }
        public decimal annual_power_consumption_degrees_dive_year { get; set; }
        public string Id { get; set; }
        public string name { get; set; }
        public int origin_price { get; set; }
        public string pics { get; set; }
        public string picb { get; set; }
        public string login_number { get; set; }
        public string detail_uri { get; set; }
        public int efficiency_rating { get; set; }
        public string key_word { get; set; }
        public string Pchome_Id { get; set; }
        public string data_from { get; set; }
        public decimal MothlyCost { get; set; }
        public decimal DailyCost { get; set; }
    }
}