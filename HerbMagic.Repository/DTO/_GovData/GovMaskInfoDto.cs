using System;

namespace HerbMagic.Repository.DTO.GovData
{
    public class GovMaskInfoDto
    {
        public int id { set; get; }
        public string hospital_id { set; get; }
        public int audit_mask_count { set; get; }
        public int child_mask_count { set; get; }
        public DateTime dataTime { set; get; }
    }

    public class GovMaskHospitalInfoDto: GovMaskInfoDto
    {
        public string hospital_name { set; get; }
        public string hospital_address { set; get; }
        public string hospital_cellphone { set; get; }
        public int audit_mask_count_Diff { set; get; }
        public int child_mask_count_Diff { set; get; }
        public int dataTime_Diff { set; get; }
        public string RankData { set; get; }
        public double hospital_longitude { set; get; }
        public double hospital_latitude { set; get; }
    }
}
