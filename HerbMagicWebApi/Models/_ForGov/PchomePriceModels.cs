using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Models
{
    /// <summary>
    /// Pchome data
    /// </summary>
    public class PchomePriceModels
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public int Price { set; get; }
        public string PictrueA { set; get; }
        public string PictrueB { set; get; }
        public string data_from { get; set; }
    }
}