using System;

namespace HerbMagic.Repository.DTO.GovData
{
    public class GovMaskUsageLogDto
    {
        public int id { set; get; }
        public string line_id { set; get; }
        public string group_id { set; get; }
        public string room_id { set; get; }
        public double user_longitude { set; get; }
        public double user_latitude { set; get; }
        public DateTime create_time { set; get; }
    }

}
