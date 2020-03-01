using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Models
{
    public class Rogue
    {
        public int SeqNo { get; set; }
        public int RogueType { get; set; }
        public int Day { get; set; }
        public string RogueDes { get; set; }
        public DateTime Date { get; set; }

    }

    public class Reserve
    {
        public int SeqNo { get; set; }
        public int ReserveType { get; set; }
        public string ReserveDes { get; set; }
        public DateTime Date { get; set; }
    }

    public class Stock
    {
        public string 證券代號 { get; set; }
        public string 證券名稱 { get; set; }
        public string 成交股數 { get; set; }
        public string 成交金額 { get; set; }
        public string 開盤價 { get; set; }
        public string 最高價 { get; set; }
        public string 最低價 { get; set; }
        public string 收盤價 { get; set; }
        public string 漲跌價差 { get; set; }
        public string 成交筆數 { get; set; }
        public DateTime CreateDate { get; set; }
    }
}