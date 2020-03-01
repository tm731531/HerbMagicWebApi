using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using System.IO;
namespace CrabStockDataTool
{
    class Program
    {
        static Dictionary<int, string> dic = new Dictionary<int, string>();
        static void Main(string[] args)
        {
            string path = @"D:\temp";
            //Test("Wait()", () =>
            //{
            //    RaiseErrorAsync().Wait();
            //});
            //Test("Result", () =>
            //{
            //    var s = RaiseErrorAsync().Result;
            //});
            //Test("GetAwaiter().GetResult()", () =>
            //{
            //    var s = RaiseErrorAsync().GetAwaiter().GetResult();
            //});
            //Test("Fire and Forget", async () =>
            //{
            //    var s = await RaiseErrorAsync();
            //});
            //Console.WriteLine("Done!");
            InitType();
            foreach (var value in dic.Values)
            {
                string responseBody = GetData(value).GetAwaiter().GetResult();
                string strpath = Path.Combine(path, value + ".txt");
                using (StreamWriter sw = File.AppendText(strpath))
                {
                    sw.WriteLine(responseBody);
                }
                Console.WriteLine(responseBody);
            };
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            Manager a1 = new Manager("阿福"); // 經理
            Director a2 = new Director("技安"); // 協理
            GeneralManager a3 = new GeneralManager("宜靜"); // 總經理
            a1.SetUpManager(a2); // 設定經理的上級為協理
            a2.SetUpManager(a3); // 設定協理的上級為總經理

            // 假單初始化
            LeaveRequest leaveRequest = new LeaveRequest(); // 假單
            leaveRequest.Name = "大雄"; // 員工姓名

            leaveRequest.DayNum = 1; // 請假天數
            a1.RequestPersonalLeave(leaveRequest);// 送出1天的假單

            leaveRequest.DayNum = 3; // 請假天數
            a1.RequestPersonalLeave(leaveRequest);// 送出3天的假單

            leaveRequest.DayNum = 7; // 請假天數
            a1.RequestPersonalLeave(leaveRequest);// 送出7天的假單

            leaveRequest.DayNum = 10; // 請假天數
            a1.RequestPersonalLeave(leaveRequest);// 送出10天的假單

            Console.Read();
            // https://www.twse.com.tw/exchangeReport/MI_INDEX?response=json&date=20200214&type=0099P&_=1581756963783

        }

        private static void InitType()
        {
            dic.Add(0, "MS");//大盤統計資訊
            dic.Add(1, "IND");//收盤指數資訊
            dic.Add(2, "MS2");//委託及成交統計資訊
            dic.Add(3, "ALL");//全部
            dic.Add(4, "ALLBUT0999");//全部(不含權證、牛熊證、可展延牛熊證)
            dic.Add(5, "0049");//封閉式基金
            dic.Add(6, "0099P");//ETF
            dic.Add(7, "029999");//ETN
            dic.Add(8, "019919T");//受益證券
            dic.Add(9, "0999");//認購權證(不含牛證)
            dic.Add(10, "0999P");//認售權證(不含熊證)
            dic.Add(11, "0999C");//牛證(不含可展延牛證)
            dic.Add(12, "0999B");//熊證(不含可展延熊證)
            dic.Add(13, "0999X");//可展延牛證
            dic.Add(14, "0999Y");//可展延熊證
            dic.Add(15, "0999GA");//附認股權特別股
            dic.Add(16, "0999GD");//附認股權公司債
            dic.Add(17, "0999G9");//認股權憑證
            dic.Add(18, "CB");//可轉換公司債
            dic.Add(19, "01");//水泥工業
            dic.Add(20, "02");//食品工業
            dic.Add(21, "03");//塑膠工業
            dic.Add(22, "04");//紡織纖維
            dic.Add(23, "05");//電機機械
            dic.Add(24, "06");//電器電纜
            dic.Add(25, "07");//化學生技醫療
            dic.Add(26, "21");//化學工業
            dic.Add(27, "22");//生技醫療業
            dic.Add(28, "08");//玻璃陶瓷
            dic.Add(29, "09");//造紙工業
            dic.Add(30, "10");//鋼鐵工業
            dic.Add(31, "11");//橡膠工業
            dic.Add(32, "12");//汽車工業
            dic.Add(33, "13");//電子工業
            dic.Add(34, "24");//半導體業
            dic.Add(35, "25");//電腦及週邊設備業
            dic.Add(36, "26");//光電業
            dic.Add(37, "27");//通信網路業
            dic.Add(38, "28");//電子零組件業
            dic.Add(39, "29");//電子通路業
            dic.Add(40, "30");//資訊服務業
            dic.Add(41, "31");//其他電子業
            dic.Add(42, "14");//建材營造
            dic.Add(43, "15");//航運業
            dic.Add(44, "16");//觀光事業
            dic.Add(45, "17");//金融保險
            dic.Add(46, "18");//貿易百貨
            dic.Add(47, "9299");//存託憑證
            dic.Add(48, "23");//油電燃氣業
            dic.Add(49, "19");//綜合
            dic.Add(50, "20");//其他
        }

        static void Test(string testName, Action callback)
        {
            Console.WriteLine("=================================");
            Console.WriteLine($"Test {testName}");
            Console.WriteLine($"Start {DateTime.Now:mm:ss}");
            try
            {
                callback();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Stop {DateTime.Now:mm:ss}");
                Console.WriteLine("Error: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner Error: " + ex.InnerException.Message);
            }
        }

        static async Task<string> RaiseErrorAsync()
        {
            Thread.Sleep(5000);
            if (DateTime.Now.CompareTo(new DateTime(2012, 12, 21)) > 0)
                throw new ApplicationException("刻意產生錯誤");
            return DateTime.Now.ToString();
        }
        private static async System.Threading.Tasks.Task<string> GetData(string type)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($@"https://www.twse.com.tw/exchangeReport/MI_INDEX?response=json&date=20200227&type={type}&_=1581756963783");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            await Task.Delay(5000);
            return responseBody;
        }
    }
    class todolist
    {
        //// 1.股票id表
        ////https://www.tej.com.tw/webtej/doc/uid.htm
        ///   2.證卷交易所(月)
        ///   https://www.twse.com.tw/zh/page/trading/exchange/FMSRFK.html
        ///   3. yahoo 股市
        ///   https://tw.stock.yahoo.com/h/getclass.php
        ///   4. ETF id表
        ///   https://www.twse.com.tw/zh/page/ETF/list.html
        ///   5. 建立相關的table跟關聯表
        ///   6. 目標 根據時間 可以找出相對應的股的相對高點跟低點
        ///   7.後續嘗試找出事件跟動態的關係


    }
    abstract class ManagerHandler
    {
        protected string name;
        protected ManagerHandler upManager; // 上一級的管理者

        public ManagerHandler(string name)
        {
            this.name = name;
        }

        // 設定上一級的管理者
        public void SetUpManager(ManagerHandler upManager)
        {
            this.upManager = upManager;
        }

        // 事假處理
        abstract public void RequestPersonalLeave(LeaveRequest leaveRequest);
    }

    // 經理
    class Manager : ManagerHandler
    {
        public Manager(string name) : base(name) { }

        public override void RequestPersonalLeave(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DayNum <= 2)
            {
                // 2天以內，經理可以批准
                Console.WriteLine("經理 {0} 已批准 {1}{2}天的事假", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
            else
            {
                // 超過2天，轉呈上級
                if (null != upManager)
                {
                    upManager.RequestPersonalLeave(leaveRequest);
                }
            }
        }
    }

    // 協理
    class Director : ManagerHandler
    {
        public Director(string name) : base(name) { }

        public override void RequestPersonalLeave(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DayNum <= 5)
            {
                // 5天以內，經理可以批准
                Console.WriteLine("協理 {0} 已批准 {1}{2}天的事假", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
            else
            {
                // 超過5天，轉呈上級
                if (null != upManager)
                {
                    upManager.RequestPersonalLeave(leaveRequest);
                }
            }
        }
    }

    // 總經理
    class GeneralManager : ManagerHandler
    {
        public GeneralManager(string name) : base(name) { }

        public override void RequestPersonalLeave(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DayNum <= 7)
            {
                // 7天以內，總經理批准
                Console.WriteLine("總經理 {0} 已批准 {1}{2}天的事假", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
            else
            {
                // 超過7天以上，再深入了解原因
                Console.WriteLine("總經理 {0} 要再了解 {1}{2}天的事假原因", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
        }
    }


    // 假單
    class LeaveRequest
    {
        // 姓名
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // 天數
        private int dayNum;
        public int DayNum
        {
            get { return dayNum; }
            set { dayNum = value; }
        }
    }

    public class Rootobject
    {
        public Groups1[] groups1 { get; set; }
        public string[] notes1 { get; set; }
        public Params _params { get; set; }
        public string stat { get; set; }
        public string[] fields1 { get; set; }
        public string subtitle1 { get; set; }
        public string[][] data1 { get; set; }
        public string date { get; set; }
        public object[][] alignsStyle1 { get; set; }
    }

    public class Params
    {
        public string response { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string _ { get; set; }
        public string controller { get; set; }
        public object format { get; set; }
        public string action { get; set; }
        public string lang { get; set; }
    }

    public class Groups1
    {
        public int start { get; set; }
        public int span { get; set; }
        public string title { get; set; }
    }


}
