namespace HerbMagicWebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EnergyEfficiencyModels
    {
        public string keyWord { get; set; }
        public int originprice { get; set; }
        public string EER { get; set; }
        public string CSPF { get; set; }
        public string rated_dehumidification_capacity { get; set; }
        public string energy_factor_value { get; set; }
        public string effective_internal_volume { get; set; }
        public string est24 { get; set; }
        public string hot_water_system_storage_tank_capacity_indication_value { get; set; }
        public string warm_water_system_storage_tank_capacity_indication_value { get; set; }
        public string ice_water_system_storage_tank_capacity_indication_value { get; set; }
        public string detailUri { get; set; }
        public int efficiency_rating { get; set; }
        public string efficiency_benchmark { get; set; }
        public decimal dayCost { get; set; }
        public decimal monthCost { get; set; }
    }
}