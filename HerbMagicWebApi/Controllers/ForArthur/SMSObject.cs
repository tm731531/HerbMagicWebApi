namespace HerbMagicWebApi.Controllers.ForArthur
{
    public class SMSObject
    {

        /// <summary>
        /// 簡訊主體
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 手機號碼 EX  +886911578345 是我的手機
        /// </summary>
        public string phone { get; set; }
    }
}