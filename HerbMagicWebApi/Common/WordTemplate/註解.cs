using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Common.WordTemplate
{
    public class 註解Dic
    {
        public static Dictionary<int, int> 綠 = new Dictionary<int, int>() {
              {0,0 },   {1,0 }, {2,200 }, {3,500 }, {4,900 }, {5,1500}
        };
        public static Dictionary<int, int> 藍 = new Dictionary<int, int>() {
               {0,0 },  {1,0 }, {2,250 }, {3,650 }, {4,1150 }, {5,1900},
              {6,2900 }, {7,4150 }, {8,5650 }, {9,7650 }, {10,10150}
        };
        public static Dictionary<int, int> 紫 = new Dictionary<int, int>() {
                {0,0 }, {1,0 }, {2,500 }, {3,1300 }, {4,2300 }, {5,3500},
              {6,5000 }, {7,7000 }, {8,9500 }, {9,12500 }, {10,16000},
              {11,20000 }, {12,25000 }, {13,31000 }, {14,38000 }, {15,46000}
        };
        public static Dictionary<int, int> 金 = new Dictionary<int, int>() {
               {0,0 },  {1,0 }, {2,1000 }, {3,1600 }, {4,4600 }, {5,7000},
              {6,10000 }, {7,13600 }, {8,17600 }, {9,22000 }, {10,27000},
              {11,32600 }, {12,38600 }, {13,45600 }, {14,53600 }, {15,62600},
              {16,72600 }, {17,83600 }, {18,95600 }, {19,108600 }, {20,122600}
        };
        public static Dictionary<int, int> 紅 = new Dictionary<int, int>() {
              {0,0 },  {1,0 }, {2,2000 }, {3,4400 }, {4,7400 }, {5,11000},
              {6,15000 }, {7,19400 }, {8,24400 }, {9,30000 }, {10,36000},
              {11,43000 }, {12,51000 }, {13,60000 }, {14,70000 }, {15,81000},
              {16,93000 }, {17,106000 }, {18,120000 }, {19,135000 }, {20,151000},
              {21,168000 }, {22,186000 }, {23,205000 }, {24,225000 }, {25,246000},
              {26,268000 }, {27,291000 }, {28,315000 }, {29,340000 }, {30,366000}
        };
        public static int 初級 = 20;
        public static int 中級 = 50;
        public static int 高級 = 100;
        public static int 特級 = 500;

        public static string 範本 = $@"請輸入您目前的兵書等級：
（範例）
紅:21/30
橙:20/20/20
紫:15/15
藍:10/10
綠:5/5
初級:1
中級:2
高級:3
特級:4
";

    }
}