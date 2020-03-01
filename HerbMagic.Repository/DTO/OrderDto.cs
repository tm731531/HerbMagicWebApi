namespace HerbMagic.Repository.DTO
{
    public class OrderDto
    {
        public string doctor { get; set; }
        public string order_status { get; set; }
        public string wx_appid { get; set; }
        public int order_create_time { get; set; }
        public int completion_time { get; set; }
        public int order_paid_time { get; set; }
        public string wx_mch_id { get; set; }
        public string order_amount { get; set; }
        public int doc_accept_time { get; set; }
        public int order_id { get; set; }
        public string doc_sign { get; set; }
    }
}
