using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HerbMagicWebApi.Models;
using static HerbMagicWebApi.Models.ViewModels;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using Newtonsoft.Json;
using System.Configuration;
using HerbMagicWebApi.Common.WordTemplate;
using System.Text;
using HerbMagicWebApi.Common.Emoji;
using HerbMagicWebApi.Common.LineTemplate;
using HerbMagic.Repository.Factory;

namespace HerbMagicWebApi.Common
{
    public class LineHelper
    {
        public static string connectionString =
             ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;
        private static string comma = 寶物.comma;

        static Dictionary<string, string> 聖聖名單 = 腳色.建立聖聖名單();
        static Dictionary<string, string> 龍龍名單 = 腳色.建立龍龍名單();
        static Dictionary<string, string> 三八名單 = 腳色.建立三八名單();
        static Dictionary<string, TempCache> tempCaches = new Dictionary<string, TempCache>();

        private static string target = "更新代儲資訊:";
        public static ReplyModel SamgoReply(Event msg)
        {

            try
            {

                // Ua9659b8e6723edd7f2135641754abde2
                //U179044b2c930e61e05385d00472ab2d8
                //if (msg.source.userId == "U179044b2c930e61e05385d00472ab2d8")
                //{
                //    return null;
                //}
                //else
                //{
                if (msg.message.text.Trim() == ("測試"))
                {
                    return test(msg.replyToken, JsonConvert.SerializeObject(msg));
                }
                else
                {

                    return SamgoReply(msg.replyToken, msg.message.text, msg);
                }
                // }
            }
            catch (Exception ex)
            {
                return ReturnSingleSting(msg.replyToken, JsonConvert.SerializeObject(ex.Message));

            }


        }
        public static ReplyModel NanaSamgoReply(Event msg)
        {

            if (msg.message.text.Trim() == ("測試"))
            {

                return ReturnSingleSting(msg.replyToken, JsonConvert.SerializeObject(msg));
            }
            else
            {
                // Ua9659b8e6723edd7f2135641754abde2
                if (msg.source.userId == "U29b8d63011a8204778c73b44bedf112f")
                {
                    if (msg.message.text.Trim().StartsWith("更新代儲資訊:"))
                    {
                        return UpdateRe(msg.replyToken, msg.message.text);
                    }
                    else
                    {
                        return NaNaSamgoReply(msg.replyToken, msg.message.text, msg);
                    }
                }
                // return null;
                return NaNaSamgoReply(msg.replyToken, msg.message.text , msg);
            }
        }
        public static ReplyModel SamgoReply(string token, string data, Event msg = null)
        {


            string returnString = "";
            var returnModel = new ReplyModel();

            data = data.Trim();
            data = data.ToLower();
            data = ConvertToChinese(data, "Big5");
            if (data == "愛奶奶" || data == "幫手" || data == "機器人")
                returnModel = Return幣別範本Msg(token);
            if (data == "幣別")
                returnModel = ReturnSingleSting(token, "ZAR CAD USD CHF THB CNY GBP AUD EUR NZD SGD SEK JPY HKD");
            if (data.StartsWith("匯率資料"))
                returnModel = Return匯率Sting(token, data);
            if (data.StartsWith("area"))
            {
                returnModel = AddressMask(token, data, msg);

            }
            if (data.EndsWith("分院") || data.EndsWith("醫院") ||
                data.EndsWith("衛生所") || data.EndsWith("藥局") ||
                data.EndsWith("店") || data.EndsWith("診所"))
            {
                returnModel = 藥局(token, data);

            }
            if (data.StartsWith("紅:") || data.StartsWith("紅："))
            {
                returnModel = 註解(token, data, msg);

            }
            if (data.StartsWith("註冊速算器"))
            {
                returnModel = 註冊功能(token, data, msg);

            }

            ;
            return returnModel;
        }

        private static ReplyModel 註冊功能(string token, string data, Event msg)
        {
            NanaLine nanaLine = new NanaLine();
            nanaLine.timestampInt = msg.timestamp;
            nanaLine.timeStamp = DateTime.UtcNow;
            nanaLine.groupId = msg.source.groupId;
            nanaLine.type = msg.source.type;
            nanaLine.userId = msg.source.userId;
            nanaLine.roomId = msg.source.roomId;
            nanaLine.messageText = msg.message.text;
            DapperHelper.InsertSQL<NanaLine>(connectionString, "NanaLine", nanaLine);
            return ReturnSingleSting(msg.replyToken, "申請提交中");

        }

        private static ReplyModel 註解(string token, string data, Event msg)
        {
            //if (msg.source.groupId == null) msg.source.groupId = "123";
            ////return ReturnSingleSting(token, JsonConvert.SerializeObject(msg));
            //return ReturnMutiSting(token, new List<string>() { msg.source.userId
            //    , msg.source.groupId
            //    //,msg.source.roomId
            //} );
            string targetUserId = msg.source.userId == null ? "" : msg.source.userId;
            string targetGroupId = msg.source.groupId == null? "": msg.source.groupId;
            string targetRoomId = msg.source.roomId == null ? "" : msg.source.roomId;
            try
            {
                if (GetNanaCalculatorUsableData(msg.source.userId, msg.source.groupId, msg.source.roomId) == null)
                    return ReturnSingleSting(token, $"請先註冊功能 Ex 註冊速算器 "); ;
                //return ReturnSingleSting(token, GetNanaCalculatorUsableData(msg.source.userId, msg.source.groupId, msg.source.roomId).roomId); ;
            }
            catch (Exception ex) {
                return ReturnSingleSting(token, ex.StackTrace+"   "+ex.Message); ;
            }
            int Count = 0;

            try
            {
                var strList = data.Split('\n');
                foreach (var str in strList)
                {
                    if (str.StartsWith("紅"))
                    {
                        foreach (var id in str.Replace("紅", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += 取得金額(id, 註解Dic.紅);
                        }
                    }
                    if (str.StartsWith("橙"))
                    {
                        foreach (var id in str.Replace("橙", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += 取得金額(id, 註解Dic.金);
                        }
                    }
                    if (str.StartsWith("紫"))
                    {
                        foreach (var id in str.Replace("紫", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {

                            Count += 取得金額(id, 註解Dic.紫);
                        }
                    }
                    if (str.StartsWith("藍"))
                    {
                        foreach (var id in str.Replace("藍", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {

                            Count += 取得金額(id, 註解Dic.藍);
                        }
                    }
                    if (str.StartsWith("綠"))
                    {
                        foreach (var id in str.Replace("綠", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += 取得金額(id, 註解Dic.綠);
                        }
                    }
                    if (str.StartsWith("初級"))
                    {
                        foreach (var id in str.Replace("初級", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += Convert.ToInt32(id) * 註解Dic.初級;
                        }
                    }
                    if (str.StartsWith("中級"))
                    {
                        foreach (var id in str.Replace("中級", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += Convert.ToInt32(id) * 註解Dic.中級;
                        }
                    }
                    if (str.StartsWith("高級"))
                    {
                        foreach (var id in str.Replace("高級", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += Convert.ToInt32(id) * 註解Dic.高級;
                        }
                    }
                    if (str.StartsWith("特級"))
                    {
                        foreach (var id in str.Replace("特級", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += Convert.ToInt32(id) * 註解Dic.特級;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // return ReturnSingleSting(token, ex.Message+ "  "+ex.StackTrace);
                return ReturnSingleSting(token, 註解Dic.範本);

            }
            return ReturnSingleSting(token, $"總註解量 {Count}");
        }
        private static ReplyModel 註解測試(string token, string data, Event msg)
        {
            
            int Count = 0;

            try
            {
                var strList = data.Split('\n');
                foreach (var str in strList)
                {
                    if (str.StartsWith("紅"))
                    {
                        foreach (var id in str.Replace("紅", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += 取得金額(id, 註解Dic.紅);
                        }
                    }
                    if (str.StartsWith("橙"))
                    {
                        foreach (var id in str.Replace("橙", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += 取得金額(id, 註解Dic.金);
                        }
                    }
                    if (str.StartsWith("紫"))
                    {
                        foreach (var id in str.Replace("紫", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {

                            Count += 取得金額(id, 註解Dic.紫);
                        }
                    }
                    if (str.StartsWith("藍"))
                    {
                        foreach (var id in str.Replace("藍", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {

                            Count += 取得金額(id, 註解Dic.藍);
                        }
                    }
                    if (str.StartsWith("綠"))
                    {
                        foreach (var id in str.Replace("綠", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += 取得金額(id, 註解Dic.綠);
                        }
                    }
                    if (str.StartsWith("初級"))
                    {
                        foreach (var id in str.Replace("初級", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += Convert.ToInt32(id) * 註解Dic.初級;
                        }
                    }
                    if (str.StartsWith("中級"))
                    {
                        foreach (var id in str.Replace("中級", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += Convert.ToInt32(id) * 註解Dic.中級;
                        }
                    }
                    if (str.StartsWith("高級"))
                    {
                        foreach (var id in str.Replace("高級", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += Convert.ToInt32(id) * 註解Dic.高級;
                        }
                    }
                    if (str.StartsWith("特級"))
                    {
                        foreach (var id in str.Replace("特級", "").Replace("：", "").Replace(":", "").Trim().Split('/'))
                        {
                            Count += Convert.ToInt32(id) * 註解Dic.特級;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // return ReturnSingleSting(token, ex.Message+ "  "+ex.StackTrace);
                return ReturnSingleSting(token, 註解Dic.範本);

            }
            return ReturnSingleSting(token, $"總註解量 {Count}");
        }

        private static int 取得金額( string id,Dictionary<int,int> pair)
        {
            int count = 0;
            if (id.Contains("*"))
            {
                var target = id.Split('*');
                count= pair[Convert.ToInt32(target[0])] * Convert.ToInt32(target[1]);
            }
            else
                count= pair[Convert.ToInt32(id)];
            return count;
        }

        private static ReplyModel AddressMask(string token, string data, Event msg = null)
        {
            ReplyModel returnModel = new ReplyModel();
            data = data.Replace("area ", "");
            if (msg != null)
            {
                try
                {
                    GovDataFactory.GovMaskUsageLogRepository.InsertMaskUsageLogDtos(new HerbMagic.Repository.DTO.GovData.GovMaskUsageLogDto()
                    {
                        line_id = msg.source.userId,
                        group_id = msg.source.groupId,
                        room_id = msg.source.roomId,
                        user_latitude = msg.message.latitude,
                        user_longitude = msg.message.longitude,
                        create_time = DateTime.Now
                    });
                }
                catch (Exception) { }
            }
            var datas = GovDataFactory.GovMaskInfoRepository.GetGovMaskInfoDtosbySP(Convert.ToDouble(data.Split(',')[1]), Convert.ToDouble(data.Split(',')[0]));
            List<string> ls = new List<string>();
            var auditMask = new List<Models.Action>();
            var childMask = new List<Models.Action>();
            var distinctMask = new List<Models.Action>();
            try
            {
                foreach (var dats in datas.OrderByDescending(x => x.audit_mask_count))
                {
                    auditMask.Add(new Models.Action()
                    {
                        type = "uri",

                        uri = $@"https://www.google.com.tw/maps/dir/{data}/{dats.hospital_latitude},{dats.hospital_longitude}/",
                        label = $"{dats.hospital_name}  / {dats.audit_mask_count} /{dats.audit_mask_count_Diff}"
                    });

                    ls.Add($@"藥局名稱:{dats.hospital_name}
藥局地址:{dats.hospital_address}
藥局電話:{dats.hospital_cellphone}
成人口罩數:{dats.audit_mask_count} 變化量 :{ dats.audit_mask_count_Diff}
小孩口罩數:{dats.child_mask_count} 變化量 :{ dats.child_mask_count_Diff}
上一次資料時間差異:{dats.dataTime_Diff} min
資料時間:{dats.dataTime}");
                }
                foreach (var dats in datas.OrderBy(x => x.RankData))
                {
                    distinctMask.Add(new Models.Action()
                    {
                        type = "uri",

                        uri = $@"https://www.google.com.tw/maps/dir/{data}/{dats.hospital_latitude},{dats.hospital_longitude}/",
                        label = $"{dats.hospital_name} "

                    });
                }
                foreach (var dats in datas.OrderByDescending(x => x.child_mask_count))
                {
                    childMask.Add(new Models.Action()
                    {
                        type = "uri",
                        uri = $@"https://www.google.com.tw/maps/dir/{data}/{dats.hospital_latitude},{dats.hospital_longitude}/",
                        label = $"{dats.hospital_name} / {dats.child_mask_count} /{dats.child_mask_count_Diff}"
                    });
                }



                var lrm = new List<ReplyMessage>();
                var lc = new List<Column>() {

                     new Column() {
                          text =$"數量排序 ,越上面成人的口罩越多 店名/現存數/跟上一次比較的差異數",
                           actions= auditMask.Take(3).ToList(),
                     },
                     new Column() {
                          text=$"距離排序 ,越上面越進 店名",
                           actions=distinctMask.Take(3).ToList()
                     },
                      new Column() {
                          text =$"數量排序 ,越上面兒童的口罩越多 店名/現存數/跟上一次比較的差異數",

                           actions= childMask.Take(3).ToList(),
                     },
            };

                var lcc = new List<List<Column>>();
                lcc.Add(lc);
                ReturnTemplate rt = new ReturnTemplate();
                lrm.AddRange(rt.text(ls.Take(4).Reverse().ToList()));
                lrm.AddRange(rt.carousel("藥局資訊", lcc));

                returnModel = new ReplyModel()
                {
                    replyToken = token,
                    messages = lrm

                };
            }
            catch (Exception ex)
            {
                returnModel = ReturnSingleSting(token, ex.Message);
            }
            return returnModel;
        }

        public static ReplyModel NaNaSamgoReply(string token, string data, Event msg = null)
        {


            string returnString = "";
            var returnModel = new ReplyModel();

            data = data.Trim();
            data = data.ToLower();
            data = ConvertToChinese(data, "Big5");
            if (data == "娜娜奇" || data == "幫手" || data == "機器人")
                returnModel = Return範本Msg(token);
            if (data.StartsWith("紅裝") && data.Length < 5)
                returnModel = ReturnSingleImage(token, "https://i.imgur.com/z3FtBf2");
            else if (data == "兵書升級" || data == "兵書經驗")
                returnModel = ReturnSingleImage(token, "https://i.imgur.com/HDiApPL");
            else if (data.StartsWith("橙馬") && data.Length < 5)
                returnModel = ReturnSingleImage(token, "https://i.imgur.com/sR5qPTc");
            else if (data == "城池技能")
                returnModel = ReturnSingleImage(token, "https://i.imgur.com/ZKhYYnA");
            else if (data == "升階材料")
                returnModel = ReturnSingleImage(token, "https://i.imgur.com/8ZBPOC1");
            else if (data == "大殿等級")
                returnModel = ReturnSingleSting(token, 一般.大殿等級);
            else if (data == "打寇攻略" || data == "打匪攻略")
                returnModel = 打寇攻略(token);
            else if (data == "陣法加成")
                returnModel = 陣法加成(token);
            else if (data == "兵書簡介")
                returnModel = ReturnSingleSting(token, 一般.兵書簡介1);
            else if (data == "寶物簡介")
                returnModel = ReturnMutiSting(token, new List<string>() { 一般.寶物簡介1, 一般.寶物簡介2 });
            else if (data == "寶物次序")
                returnModel = ReturnSingleSting(token, 一般.寶物次序);
            else if (data == "軍械改造" || data == "軍械升級")
                returnModel = 軍械改造(token);
            else if (data == "官職建議")
                returnModel = ReturnMutiSting(token, new List<string>() { 一般.官職建議1, 一般.官職建議2, 一般.官職建議3, 一般.官職建議4 });
            else if (data == "怒氣資料")
                returnModel = ReturnMutiImage(token, new List<string>() { "https://i.imgur.com/lSOmKXm", "https://i.imgur.com/zMin4iL" });
            else if (data == "尋訪建議")
                returnModel = ReturnSingleSting(token, 一般.尋訪建議);
            else if (data == "攻城排位")
                returnModel = ReturnSingleImage(token, "https://i.imgur.com/bMRo2o9");
            else if (data == "武將經驗" || data == "武將升級")
                returnModel = ReturnSingleSting(token, 一般.武將經驗);
            else if (data == "寶物經驗" || data == "寶物升級")
                returnModel = 寶物經驗(token);

            //大殿
            else if ((data.StartsWith("大殿") || data.StartsWith("dd")) && data.Length < 5)
            {
                returnModel = 大殿等級(token, data);
            }
            else if (data == "組隊打馬")
                returnModel = ReturnSingleSting(token, 一般.組隊打馬);
            else if (data == "軍團戰")
                returnModel = ReturnSingleSting(token, 一般.軍團戰);
            else if (data == "集結" || data == "英雄集結")
                returnModel = ReturnSingleSting(token, 一般.英雄集結);
            else if (聖聖名單.Keys.Contains(data.ToLower()))
                returnModel = ReturnSingleSting(token, 聖聖名單[data.ToLower()]);
            else if (data == "封官列表")
                returnModel = ReturnSingleSting(token, 一般.封官列表);
            else if (data == "代儲活動")
                returnModel = 代儲活動(token);
            else if (data == "兵書解說")
                returnModel = 兵書解說(token);
            #region 匪寇
            //Detail
            else if (data == "烏丸")
                returnModel = 烏丸(token);
            else if (data == "羌胡")
                returnModel = 羌胡(token);
            else if (data == "匈奴")
                returnModel = 匈奴(token);
            else if (data == "山越")
                returnModel = 山越(token);
            else if (data == "丁滿的特殊陣容")
                returnModel = 特殊陣容(token);
            else if (data == "收集打匪訊息")
                returnModel = 收集打匪訊息(token);
            else if (data == "南蠻訊息")
                returnModel = 南蠻訊息(token);

            #endregion
            #region 寶物

            //寶物

            else if (data == "紅色寶物")
                returnModel = ReturnSingleSting(token, 寶物.紅色寶物);
            else if (data == "橙色寶物")
                returnModel = ReturnSingleSting(token, 寶物.橙色寶物);
            else if (data == "紫色寶物")
                returnModel = 紫色寶物(token);
            else if (data == "藍色寶物")
                returnModel = ReturnSingleSting(token, 寶物.藍色寶物);
            else if (data == "綠色寶物")
                returnModel = ReturnSingleSting(token, 寶物.綠色寶物);

            //橙寶
            else if (data == "山海經" || data == "山海")
                returnModel = ReturnSingleSting(token, 寶物.山海經);
            else if (data == "莊子")
                returnModel = ReturnSingleSting(token, 寶物.莊子);
            else if (data == "孟子")
                returnModel = ReturnSingleSting(token, 寶物.孟子);
            else if (data == "詩經")
                returnModel = ReturnSingleSting(token, 寶物.詩經);
            else if (data == "玲瓏獅蠻帶" || data == "蠻帶" || data == "腰帶" || data == "玲瓏")
                returnModel = ReturnSingleSting(token, 寶物.玲瓏獅蠻帶);
            else if (data == "獸面吞頭鎧" || data == "獸面")
                returnModel = ReturnSingleSting(token, 寶物.獸面吞頭鎧);
            else if (data == "呂氏春秋" || data == "呂氏")
                returnModel = ReturnSingleSting(token, 寶物.呂氏春秋);
            else if (data == "吳越春秋" || data == "吳越")
                returnModel = ReturnSingleSting(token, 寶物.吳越春秋);
            else if (data == "湛盧劍" || data == "湛盧")
                returnModel = ReturnSingleSting(token, 寶物.湛盧劍);
            else if (data == "史記")
                returnModel = ReturnSingleSting(token, 寶物.史記);
            else if (data == "吳鉤劍" || data == "吳鉤")
                returnModel = ReturnSingleSting(token, 寶物.吳鉤劍);
            else if (data == "周禮")
                returnModel = ReturnSingleSting(token, 寶物.周禮);
            else if (data == "論語")
                returnModel = ReturnSingleSting(token, 寶物.論語);
            else if (data == "老子")
                returnModel = ReturnSingleSting(token, 寶物.老子);
            else if (data == "禮記")
                returnModel = ReturnSingleSting(token, 寶物.禮記);
            else if (data == "龍泉劍" || data == "龍泉")
                returnModel = ReturnSingleSting(token, 寶物.龍泉劍);

            else if (data == "三略")
                returnModel = ReturnSingleSting(token, 寶物.三略);
            else if (data == "爾雅")
                returnModel = ReturnSingleSting(token, 寶物.爾雅);
            else if (data == "六韜")
                returnModel = ReturnSingleSting(token, 寶物.六韜);
            else if (data == "書經")
                returnModel = ReturnSingleSting(token, 寶物.書經);
            else if (data == "左傳")
                returnModel = ReturnSingleSting(token, 寶物.左傳);
            else if (data == "流星錘")
                returnModel = ReturnSingleSting(token, 寶物.流星錘);
            else if (data == "戰國策" || data == "戰國")
                returnModel = ReturnSingleSting(token, 寶物.戰國策);
            else if (data == "東胡飛弓" || data == "東吳飛弓" || data == "飛弓")
                returnModel = ReturnSingleSting(token, 寶物.東吳飛弓);
            //紫寶
            else if (data == "兩當鎧" || data == "兩當")
                returnModel = ReturnSingleSting(token, 寶物.兩當鎧);
            else if (data == "百里劍" || data == "百里")
                returnModel = ReturnSingleSting(token, 寶物.百里劍);
            else if (data == "雙刃斧" || data == "雙刃")
                returnModel = ReturnSingleSting(token, 寶物.雙刃斧);
            else if (data == "列女傳" || data == "列女")
                returnModel = ReturnSingleSting(token, 寶物.列女傳);
            else if (data == "漢書")
                returnModel = ReturnSingleSting(token, 寶物.漢書);
            else if (data == "傷寒雜病論" || data == "傷寒")
                returnModel = ReturnSingleSting(token, 寶物.傷寒雜病論);
            else if (data == "明光鎧" || data == "明光")
                returnModel = ReturnSingleSting(token, 寶物.明光鎧);

            else if (data == "黑光鎧" || data == "黑光"
                || data == "松紋鑲劍" || data == "松紋"
                 || data == "紫電劍" || data == "紫電"
                  || data == "日月刀" || data == "日月"
                  || data == "鐵蒺藜骨朵" || data == "鐵蒺藜")
                returnModel = ReturnSingleSting(token, 寶物.垃圾紫寶);
            #endregion
            #region 兵書
            //兵書
            else if (data == "綠色兵書")
                returnModel = ReturnSingleSting(token, 兵書說明.綠色兵書);
            else if (data == "藍色兵書")
                returnModel = ReturnSingleSting(token, 兵書說明.藍色兵書);
            else if (data == "紫色兵書")
                returnModel = ReturnSingleSting(token, 兵書說明.紫色兵書);
            else if (data == "橙色兵書")
                returnModel = ReturnSingleSting(token, 兵書說明.橙色兵書);
            else if (data == "紅色兵書")
                returnModel = ReturnSingleSting(token, 兵書說明.紅色兵書);
            #endregion
            #region 馬匹
            else if (data == "馬匹")
                returnModel = ReturnSingleSting(token, 馬匹.說明);

            else if (data == "絕影")
                returnModel = ReturnSingleSting(token, 馬匹.絕影);
            else if (data == "大宛馬" || data == "大宛")
                returnModel = ReturnSingleSting(token, 馬匹.大宛馬);
            else if (data == "汗血馬" || data == "汗血")
                returnModel = ReturnSingleSting(token, 馬匹.汗血馬);
            else if (data == "爪黄飛電" || data == "爪黄")
                returnModel = ReturnSingleSting(token, 馬匹.爪黄飛電);
            else if (data == "白鵠" || data == "白馬")
                returnModel = ReturnSingleSting(token, 馬匹.白鵠);
            else if (data == "的蘆" || data == "的盧")
                returnModel = ReturnSingleSting(token, 馬匹.的蘆);
            else if (data == "紫騂馬" || data == "紫馬" || data == "智馬")
                returnModel = ReturnSingleSting(token, 馬匹.紫騂馬);
            else if (data == "花鬃馬" || data == "花馬")
                returnModel = ReturnSingleSting(token, 馬匹.花鬃馬);
            else if (data == "赤兔")
                returnModel = ReturnSingleSting(token, 馬匹.赤兔);
            else if (data == "青馬")
                returnModel = ReturnSingleSting(token, 馬匹.青馬);
            else if (data == "黃驃馬" || data == "統帥馬" || data == "統馬")
                returnModel = ReturnSingleSting(token, 馬匹.黃標馬);

            #endregion
            if (data.StartsWith("area"))
            {
                returnModel = AddressMask(token, data, msg);

            }
            if (data == "尋找藥局口罩數使用教學" || data == "口罩")
            {
                returnModel = 口罩(token);

            }
            if (data.EndsWith("分院") || data.EndsWith("醫院") ||
                data.EndsWith("衛生所") || data.EndsWith("藥局") ||
                data.EndsWith("店") || data.EndsWith("診所"))
            {
                returnModel = 藥局(token, data);

            }
            if (data.StartsWith("紅:")|| data.StartsWith("紅："))
            {
                returnModel = 註解(token, data, msg);

            }
            if (data.StartsWith("註冊速算器"))
            {
                returnModel = 註冊功能(token, data, msg);

            }
            return returnModel;
        }
        private static ReplyModel 藥局(string token, string data)
        {
            var datas = GovDataFactory.GovMaskInfoRepository.GetGovMaskInfoDtosbySPWithName(data);
            var hoslist = datas.Select(x => x.hospital_name).Distinct().Take(5);
            var returnModel = new ReplyModel();
            List<string> ls = new List<string>();
            var auditMask = new List<Models.Action>();
            var childMask = new List<Models.Action>();
            var distinctMask = new List<Models.Action>();
            try
            {
                foreach (var dats in hoslist)
                {
                    var hosdata = datas.Where(x => x.hospital_name == dats).FirstOrDefault();
                    var ds = String.Join("\r\n", datas.Where(x => x.hospital_name == dats).OrderByDescending(x => x.dataTime).Take(10)
                                            .Select(x => "數量:" + x.audit_mask_count + "  時間:" + x.dataTime.ToString()));
                    ls.Add($@"藥局名稱:{hosdata.hospital_name}
藥局地址:{hosdata.hospital_address}
藥局電話:{hosdata.hospital_cellphone}
歷史數量 :{ds}");

                }

                returnModel = ReturnMutiSting(token, ls);
            }
            catch (Exception ex)
            {
                returnModel = ReturnSingleSting(token, ex.Message);
            }

            return returnModel;
        }
        private static ReplyModel 口罩(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    //new ReplyMessage(){type = "text",text ="在群組內無法使用此功能，請到一對一上使用 目前只有台灣地區有資料"},
                       new ReplyMessage(){type = "image"
                       ,previewImageUrl = "https://i.imgur.com/eHlldDbt.jpg"
                       ,originalContentUrl="https://i.imgur.com/eHlldDb.jpg"},
                       new ReplyMessage(){type = "image"
                       ,previewImageUrl = "https://i.imgur.com/FcbI0QIt.jpg"
                       ,originalContentUrl="https://i.imgur.com/FcbI0QI.jpg"},
                       new ReplyMessage(){type = "image"
                       ,previewImageUrl = "https://i.imgur.com/54UdLvNt.jpg"
                       ,originalContentUrl="https://i.imgur.com/54UdLvN.jpg"},
                       new ReplyMessage(){type = "image"
                       ,previewImageUrl = "https://i.imgur.com/rfC2E5it.jpg"
                       ,originalContentUrl="https://i.imgur.com/rfC2E5i.jpg"},
                }
            };
            return returnModel;
        }
        private static ReplyModel 兵書解說(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {

                    new ReplyMessage()
                    {
                        type = "template",
                        altText="兵書解說",
                        template= new Template() {
                          type="buttons",
                          thumbnailImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQhZENvO27Uxqo7KTqAIIk6wIHeVIIbsOxyJKOdWTFFJr87gAFD",
                          imageSize= "cover",
                          imageBackgroundColor= "#FFFFFF",
                          title= "兵書解說",
                          text="請問想要知道哪個兵書的資料",
                          actions= new List<Models.Action>() {
                              new Models.Action() {
                                   type="message",
                                   label= "綠色兵書",
                                   text="綠色兵書"
                              },
                              new Models.Action() {
                                   type="message",
                                   label= "藍色兵書",
                                   text="藍色兵書"
                              },
                              new Models.Action() {
                                   type="message",
                                   label="紫色兵書",
                                   text="紫色兵書"},
                              new Models.Action() {
                                   type="message",
                                   label= "橙色兵書",
                                   text="橙色兵書"},
                            }
                        }
                    }
                }
            };
            return returnModel;
        }

        private static ReplyModel 代儲流程介紹(string token)
        {
            return null;
        }

        private static ReplyModel 代儲ID(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/vkWnUAbt.jpg",originalContentUrl="https://i.imgur.com/vkWnUAb.jpg"},
                }
            };
            return null;
        }

        private static ReplyModel 代儲活動(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    new ReplyMessage(){type = "text",text=GetReserveData().ReserveDes},
                    new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/vkWnUAbt.jpg"
                    ,originalContentUrl="https://i.imgur.com/vkWnUAb.jpg"}
                }
            };
            return returnModel;
        }

        private static ReplyModel 寶物經驗(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/Msj0lzPt.png",originalContentUrl="https://i.imgur.com/Msj0lzP.png"},
                    new ReplyMessage(){type = "text",text=一般.寶物經驗}
                }
            };
            return returnModel;
        }


        #region 寶物

        private static ReplyModel 紫色寶物(string token)
        {
            var data1 = "不要以為是紫色寶物後期就沒用，其實也很有用－－－當然要識揀";
            var data2 = "四大紫寶\r\n列女傳 － 戰鬥開始十秒後加怒氣搶先手，一般是給貂嬋用，或是給賈詡\r\n漢書 － 普攻有機會加怒氣，給賈詡或奶媽用一流\r\n兩當鎧 － 有機會減傷，可給馬超或夏侯惇延長生命\r\n百里劍 － 普攻後有機率造成額外武力傷害，無孟子或青釭劍的趙雲必備，給打副本的馬超也不錯";
            var data3 = "其他有用紫寶\r\n傷寒雜病論 － 戰鬥開始十秒後回兵，幾乎是公孫瓚專用，但也有人給趙雲或馬超\r\n明光鎧 － 戰鬥開始十秒後減傷\r\n日月刀 － 普攻時有機會加傷害 （孫尚香／夏侯淵過渡用）";
            var data4 = "其他紫寶？\r\n\r\n化啦！";
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    new ReplyMessage()
                    {
                        type = "text",
                        text=data1
                    },new ReplyMessage()
                    {
                        type = "text",
                        text=data2
                    },new ReplyMessage()
                    {
                        type = "text",
                        text=data3
                    },new ReplyMessage()
                    {
                        type = "text",
                        text=data4
                    }                }

            };
            return returnModel;
        }
        #endregion
        #region 打匪 


        private static ReplyModel 特殊陣容(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    //new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/zK2X6SMt.jpg",originalContentUrl="https://i.imgur.com/zK2X6SM.jpg"},
                    new ReplyMessage(){type = "text",text=匪寇.特殊陣容山越},
                    new ReplyMessage(){type = "text",text=匪寇.特殊陣容烏丸},
                    new ReplyMessage(){type = "text",text=匪寇.特殊陣容羌胡},
                    new ReplyMessage(){type = "text",text=匪寇.特殊陣容匈奴}
                }

            };
            return returnModel;
        }
        private static ReplyModel 收集打匪訊息(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    //new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/zK2X6SMt.jpg",originalContentUrl="https://i.imgur.com/zK2X6SM.jpg"},
                    new ReplyMessage(){type = "text",text=匪寇.網路收集},
                    new ReplyMessage(){type = "text",text=匪寇.網路收集1},
                    new ReplyMessage(){type = "text",text=匪寇.網路收集2},
                }

            };
            return returnModel;
        }

        private static ReplyModel 南蠻訊息(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    new ReplyMessage(){type = "text",text=匪寇.南蠻},
                    new ReplyMessage(){type = "text",text=匪寇.南蠻1},
                }

            };
            return returnModel;
        }

        private static ReplyModel 山越(string token)
        {

            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {

                    // new ReplyMessage(){type = "text",text="更新中"},
                    //new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/zK2X6SMt.jpg",originalContentUrl="https://i.imgur.com/zK2X6SM.jpg"},
                    new ReplyMessage(){type = "text",text=匪寇.山越6},
                    new ReplyMessage(){type = "text",text=匪寇.山越Note}
                }

            };
            return returnModel;
        }

        private static ReplyModel 匈奴(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    // new ReplyMessage(){type = "text",text="更新中"},
                    new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/1HlXUqRt.jpg",originalContentUrl="https://i.imgur.com/1HlXUqR.jpg"},
                    new ReplyMessage(){type = "text",text=匪寇.匈奴6},
                    new ReplyMessage(){type = "text",text=匪寇.匈奴7},
                    new ReplyMessage(){type = "text",text=匪寇.匈奴Note}

                }

            };
            return returnModel;
        }

        private static ReplyModel 羌胡(string token)
        {

            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                     //new ReplyMessage(){type = "text",text="更新中"},
                    //new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/zK2X6SMt.jpg",originalContentUrl="https://i.imgur.com/zK2X6SM.jpg"},
                    new ReplyMessage(){type = "text",text=匪寇.羌胡6},
                    //new ReplyMessage(){type = "text",text=匪寇.羌胡7},
                    new ReplyMessage(){type = "text",text=匪寇.羌胡Note}
                }
            };
            return returnModel;
        }

        private static ReplyModel 烏丸(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {

                     //new ReplyMessage(){type = "text",text="更新中"},
                    //new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/zK2X6SMt.jpg",originalContentUrl="https://i.imgur.com/zK2X6SM.jpg"},
                    new ReplyMessage(){type = "text",text=匪寇.烏丸6},
                    // new ReplyMessage(){type = "text",text=匪寇.烏丸7},
                    new ReplyMessage(){type = "text",text=匪寇.烏丸Note}
                }

            };
            return returnModel;
        }
        #endregion
        private static ReplyModel 大殿等級(string token, string data)
        {
            int level = 0;
            data = data.Replace("大殿", "").Replace("dd", "");
            int.TryParse(data, out level);

            string msg = string.Empty;
            if (level <= 50 && level >= 46) msg = "大殿46－50級的要求如下：\r\n46級：45級鑄幣所\r\n47級：46級民居\r\n48級：47級農場、47級兵營\r\n49級：48級採石場、48級城牆\r\n50級：49級軍械所";
            else if (level <= 45 && level >= 41) msg = "大殿41－45級的要求如下：\r\n41級：40級民居\r\n42級：41級農場、41級兵營\r\n43級：42級採石場、42級城牆\r\n44級：43級軍械所\r\n45級：44級農場、44級兵營";
            else if (level <= 40 && level >= 36) msg = "大殿36－40級的要求如下：\r\n36級：35級計略府、35級冶煉廠\r\n37級：36級城牆、36級採石場\r\n38級：37級軍械所、37級技法所、37級伐木場\r\n39級：38級兵營、38級農田\r\n40級：39級技法所、39級伐木場";
            else if (level <= 35 && level >= 31) msg = "大殿31－35級的要求如下：\r\n31級：30級城牆、30級採石場\r\n32級：31級軍械所、31級技法所、31級伐木場\r\n33級：32級兵營、32級農田\r\n34級：33級技法所、33級伐木場\r\n35級：34級民居";
            else if (level <= 30 && level >= 26) msg = "大殿26－30級的要求如下：\r\n26級：25級城牆、25級採石場\r\n27級：26級兵營、26級農田\r\n28級：27級技法所、27級伐木場\r\n29級：28級民居\r\n30級：29級計略府、29級冶煉廠";
            else if (level <= 25 && level >= 21) msg = "大殿21－25級的要求如下：\r\n21級：20級軍械所、20級技法所、20級伐木場\r\n22級：21級技法所、21級伐木所\r\n23級：22級民居\r\n24級：23級計略府、23級冶煉廠\r\n25級：24級鑄幣所、24級民居";
            else if (level <= 20 && level >= 16) msg = "加油 好嗎";
            else if (level <= 15 && level >= 11) msg = "加油 好嗎";
            else if (level <= 10 && level >= 6) msg = "加油 好嗎";
            else if (level <= 5 && level >= 1) msg = "加油 好嗎";
            else msg = "你生病嗎?生病要看醫生喔!";
            return ReturnSingleSting(token, msg);

        }


        private static ReplyModel 軍械改造(string token)
        {
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                   new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/M6c1Z8Bt.jpg",originalContentUrl= "https://i.imgur.com/M6c1Z8B.jpg"},
                   new ReplyMessage(){type = "text",text = 一般.軍械改造,}
                }
            };
            return returnModel;
        }


        private static ReplyModel 陣法加成(string token)
        {

            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = new List<ReplyMessage>()
                {
                    new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/rivaEbyt.jpg",originalContentUrl="https://i.imgur.com/rivaEby.jpg"},
                    new ReplyMessage(){type = "image",previewImageUrl= "https://i.imgur.com/imKakdMt.jpg",originalContentUrl="https://i.imgur.com/imKakdM.jpg"},
                    new ReplyMessage(){type = "text",text = 一般.陣法加成,}
                }
            };
            return returnModel;
        }

        private static ReplyModel 打寇攻略(string token)
        {
            var response = 匪寇.打匪說明文件;

            var moreInfo = "本日沒有打匪活動";
            var rogue = GetRogueData();
            if (rogue != null)
            {
                moreInfo = $"今日打匪活動為 {rogue.RogueDes} ";
            }
            var lrm = new List<ReplyMessage>();
            var la =
                new List<Models.Action>() {
                              new Models.Action() {
                                   type="message",
                                   label= "山越(錦帆)",
                                   text="山越"
                              },
                              new Models.Action() {
                                   type="message",
                                   label= "烏丸(虎衛)",
                                   text="烏丸"
                              },
                              new Models.Action() {
                                   type="message",
                                   label="匈奴(西涼 並州)",
                                   text="匈奴"},
                              new Models.Action() {
                                   type="message",
                                   label="羌胡(無當飛軍)",
                                   text="羌胡"},
                            };


            var lla = new List<List<Models.Action>>();
            lla.Add(la);
            ReturnTemplate rt = new ReturnTemplate();
            // lrm.AddRange(rt.text(new List<string>() { moreInfo }));
            lrm.AddRange(rt.text(new List<string>() { response }));
            lrm.AddRange(rt.image(new List<string>() { "https://i.imgur.com/MERFtev" }));
            lrm.AddRange(rt.button("打寇攻略", "打寇攻略", "請問想要知道哪個寇匪的資料", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSLu5WFPnUBksOzhOQ4bNzdV6B_o47FPuBU3M57tpggzvqeFpiMKg", lla));

            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = lrm

            };
            return returnModel;
        }



        public static ReplyModel ReturnSingleSting(string token, string response)
        {

            var lrm = new List<ReplyMessage>();
            ReturnTemplate rt = new ReturnTemplate();
            lrm.AddRange(rt.text(new List<string>() { response }));

            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = lrm

            };
            return returnModel;
        }
        private static ReplyModel ReturnMutiSting(string token, List<string> data)
        {
            List<ReplyMessage> lr = new List<ReplyMessage>();
            foreach (var str in data)
            {
                lr.Add(new ReplyMessage() { type = "text", text = str });
            }
            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = lr

            };
            return returnModel;
        }
        public static ReplyModel ReturnSingleImage(string token, string previewImageUrl)
        {
            var lrm = new List<ReplyMessage>();
            ReturnTemplate rt = new ReturnTemplate();
            lrm.AddRange(rt.image(new List<string>() { previewImageUrl }));

            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = lrm

            };
            return returnModel;
        }

        public static ReplyModel ReturnMutiImage(string token, List<string> data)
        {
            var lrm = new List<ReplyMessage>();
            ReturnTemplate rt = new ReturnTemplate();

            lrm.AddRange(rt.image(data));

            var returnModel = new ReplyModel()
            {
                replyToken = token,
                messages = lrm

            };
            return returnModel;
        }

        //繁簡轉換Funtion,參數 Language 為 Big5 則轉繁體、GB2312 則轉簡體，其他狀況則輸出原字串 
        private static string ConvertToChinese(string SourceString, string Language)
        {
            string newString = string.Empty;
            switch (Language)
            {
                case "Big5":
                    newString = ChineseConverter.Convert(SourceString, ChineseConversionDirection.SimplifiedToTraditional);
                    break;
                case "GB2312":
                    newString = ChineseConverter.Convert(SourceString, ChineseConversionDirection.TraditionalToSimplified);
                    break;
                default:
                    newString = SourceString;
                    break;
            }
            return newString;
        }
        private static ReplyModel test(string replyToken, string v)
        {

            var lrm = new List<ReplyMessage>();
            var lc =
                new List<Models.Column>() {
                    new Column() {
                          imageUrl="https://i.imgur.com/wEl3Uvz.png",
                          action = new Models.Action() {
 type="message",
   label="Yes",
   text="N"
                          }

                     }
                              //new Models.Action() {
                              //     type="message",
                              //     label= "山越",
                              //     text="山越"
                              //},
                              //new Models.Action() {
                              //     type="message",
                              //     label= "烏丸",
                              //     text="烏丸"
                              //},
                              //new Models.Action() {
                              //     type="message",
                              //     label= "匈奴",
                              //     text="匈奴"},
                              //new Models.Action() {
                              //     type="message",
                              //     label= "羌胡",
                              //     data="羌胡",
                              //     text="羌胡"},
                            };


            var lcc = new List<List<Models.Column>>();
            lcc.Add(lc);

            ReturnTemplate rt = new ReturnTemplate();
            // lrm.AddRange(rt.text(new List<string>() { GetReserveData().ReserveDes }));
            //lrm.AddRange(rt.button("莊子", "莊子標題", "莊子資料", "https://i.imgur.com/wEl3Uvz.png", lcc));
            // lrm.AddRange(rt.imageCarousel("a", lcc));
            lrm.AddRange(rt.bubble());
            var returnModel = new ReplyModel()
            {
                replyToken = replyToken,
                messages = lrm

            };
            return returnModel;
            //return null;


        }
        private static ReplyModel UpdateRe(string replyToken, string v)
        {

            Reserve re = new Reserve();
            re.ReserveDes = $@"{v.Replace(target, "")}";
            re.ReserveType = 1;
            re.Date = DateTime.Now;
            InsertReserveData(re);

            var lrm = new List<ReplyMessage>();

            ReturnTemplate rt = new ReturnTemplate();
            lrm.AddRange(rt.text(new List<string>() { GetReserveData().ReserveDes }));
            var returnModel = new ReplyModel()
            {
                replyToken = replyToken,
                messages = lrm

            };
            return returnModel;



        }
        private static ReplyModel Return範本Msg(string replyToken, string v = null)
        {

            var lrm = new List<ReplyMessage>();
            var lc = new List<Column>() {

                                new Column() {
                                     text=$"團戰{CommonEmoji.Clock9}{CommonEmoji.Clock10}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="打寇攻略",type="message",text="打寇攻略"},
                                      new Models.Action() {label="南蠻訊息",type="message",text="南蠻訊息"},
                                        new Models.Action() {label="軍團戰",type="message",text="軍團戰"},
                                     },
                                },
                               new Column() {
                                     text =$"寶物{CommonEmoji.Gift}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="寶物次序",type="message",text="寶物次序"},
                                       new Models.Action() {label="寶物簡介",type="message",text="寶物簡介"},
                                       new Models.Action() {label="寶物簡介",type="message",text="寶物簡介"},
                                      },
                                },
                                 new Column() {
                                     text =$"城池{CommonEmoji.Mount_Fuji}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="攻城排位",type="message",text="攻城排位"},
                                       new Models.Action() {label="城池技能",type="message",text="城池技能"},
                                       new Models.Action() {label="英雄集結",type="message",text="英雄集結"},
                                      },
                                },
                               new Column() {
                                     text =$"兵書{CommonEmoji.Notebook}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="兵書簡介",type="message",text="兵書簡介"},
                                       new Models.Action() {label="兵書升級",type="message",text="兵書升級"},
                                       new Models.Action() {label="兵書解說",type="message",text="兵書解說"},
                                      },
                                },
                               new Column() {
                                     text =$"升級{CommonEmoji.Muscle}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="紅裝升級",type="message",text="紅裝升級"},
                                       new Models.Action() {label="橙馬升級",type="message",text="橙馬升級"},
                                       new Models.Action() {label="寶物升級",type="message",text="寶物升級"},
                                      },
                                },

                               new Column() {
                                     text =$"建議{CommonEmoji._100}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="大殿等級",type="message",text="大殿等級"},
                                       new Models.Action() {label="尋訪建議",type="message",text="尋訪建議"},
                                       new Models.Action() {label="官職建議",type="message",text="官職建議"},
                                      },
                                },
                               new Column() {
                                     text =$"材料{CommonEmoji.Ring}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="升階材料",type="message",text="升階材料"},
                                       new Models.Action() {label="組隊打馬",type="message",text="組隊打馬"},
                                       new Models.Action() {label="軍械改造",type="message",text="軍械改造"},
                                      },
                                },
                               new Column() {
                                     text =$"加成{CommonEmoji.Books}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="陣法加成",type="message",text="陣法加成"},
                                       new Models.Action() {label="怒氣資料",type="message",text="怒氣資料"},
                                       new Models.Action() {label="武將經驗",type="message",text="武將經驗"},
                                      },
                                },
                               new Column() {
                                     text =$"砸錢區{CommonEmoji.Atm}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="代儲活動",type="message",text="代儲活動"},
                                       new Models.Action() {label="代儲好友line ID",type="message",text="代儲好友"},
                                       new Models.Action() {label="代儲流程介紹",type="message",text="代儲流程介紹"},
                                      },
                                }
            };

            var lcc = new List<List<Column>>();
            lcc.Add(lc);
            ReturnTemplate rt = new ReturnTemplate();
            lrm.AddRange(rt.text(new List<string>() { $"你好！我是38服的娜娜奇。你可以輸入關鍵字或是在下面的菜單選擇你想要的資訊。" }));
            lrm.AddRange(rt.carousel("娜娜奇列表", lcc));

            var returnModel = new ReplyModel()
            {
                replyToken = replyToken,
                messages = lrm

            };
            return returnModel;
        }
        private static ReplyModel Return幣別範本Msg(string replyToken)
        {

            var lrm = new List<ReplyMessage>();
            var lc = new List<Column>() {

                                new Column() {
                                     text=$"比較熱門的幣別{CommonEmoji.Clock9}{CommonEmoji.Clock10}",
                                      actions= new List<Models.Action>() {
                                       new Models.Action() {label="USD",type="message",text="匯率資料USD"},
                                      new Models.Action() {label="CNY",type="message",text="匯率資料CNY"},
                                        new Models.Action() {label="AUD",type="message",text="匯率資料AUD"},
                                      //  new Models.Action() {label="SGD",type="message",text="匯率資料SGD"},
                                     },
                                },
                               new Column() {
                                     text =$"加 紐 日 港{CommonEmoji.Gift}",
                                      actions= new List<Models.Action>() {
                                      // new Models.Action() {label="CAD",type="message",text="匯率資料CAD"},
                                       new Models.Action() {label="NZD",type="message",text="匯率資料NZD"},
                                       new Models.Action() {label="JPY",type="message",text="匯率資料JPY"},
                                       new Models.Action() {label="HKD",type="message",text="匯率資料HKD"},
                                      },
                                }
            };

            var lcc = new List<List<Column>>();
            lcc.Add(lc);
            ReturnTemplate rt = new ReturnTemplate();
            //  lrm.AddRange(rt.text(new List<string>() { $"你好！我是38服的娜娜奇。你可以輸入關鍵字或是在下面的菜單選擇你想要的資訊。" }));
            lrm.AddRange(rt.carousel("功能快速列表", lcc));

            var returnModel = new ReplyModel()
            {
                replyToken = replyToken,
                messages = lrm

            };
            return returnModel;
        }

        private static ReplyModel ReturnButtonMsg(string replyToken, string v = null)
        {
            var lrm = new List<ReplyMessage>();


            ReturnTemplate rt = new ReturnTemplate();
            lrm.AddRange(rt.sticker(new List<string>()
            { "325708" }));

            var returnModel = new ReplyModel()
            {
                replyToken = replyToken,
                messages = lrm

            };
            return returnModel;
        }
        private static Rogue GetRogueData()
        {
            var response = DapperHelper.Search<Rogue>(
                    SamgoGameHelper.connectionString,
                    $@"
SELECT  [SeqNo]
      ,[RogueType]
      ,[Day]
      ,[RogueDes]
      ,[Date]
  FROM [dbo].[Rogue]
  where [Date]='{DateTime.Now.AddHours(8).ToString("yyyy/MM/dd")}'

"
                    );

            return response.FirstOrDefault();
        }

        private static Reserve GetReserveData()
        {
            var response = DapperHelper.Search<Reserve>(
                    SamgoGameHelper.connectionString,
                    $@"
SELECT top 1 *
  FROM [dbo].[Reserve] order by SeqNo desc

"); return response.FirstOrDefault();
        }

        private static IEnumerable<BackInfoExchangeRate> GetBackInfoExchangeRateData(string currency)
        {
            var response = DapperHelper.Search<BackInfoExchangeRate>(
                    SamgoGameHelper.connectionString,
                    $@"
SELECT top 50 *
  FROM [dbo].[BackInfoExchangeRate] 
where currency = '{currency}'  and Convert(nvarchar,InfoDay) like '%-01'
order by InfoDay desc

"); return response;
        }


        private static ReplyModel Return匯率Sting(string token, string data)
        {
            int level = 0;
            data = data.Replace("匯率資料", "").Trim();
            // int.TryParse(data, out level);
            GetBackInfoExchangeRateData(data);
            StringBuilder sb = new StringBuilder();
            foreach (var da in GetBackInfoExchangeRateData(data))
            {
                sb.AppendLine($"買入: {da.buy}賣出: {da.sell}日期: {da.InfoDay.ToShortDateString()}");
            }

            return ReturnSingleSting(token, sb.ToString());

        }
        private static Reserve GetStockData()
        {
            var response = DapperHelper.Search<Reserve>(
                    SamgoGameHelper.connectionString,
                    $@"
SELECT top 1 *
  FROM [dbo].[Stock] order by CreateDate desc

");
            return response.FirstOrDefault();
        }

        private static NanaCalculatorUsable GetNanaCalculatorUsableData(string userId, string groupId, string roomId)
        {
            string strWhere = " isWorkable = 1 ";
            if (!string.IsNullOrEmpty(userId)) strWhere += $" and userId='{userId}' ";
            if (!string.IsNullOrEmpty(groupId)) strWhere += $" and groupId='{groupId}' ";
            if (!string.IsNullOrEmpty(roomId)) strWhere += $" and roomId='{roomId}' ";
            var response = DapperHelper.Search<NanaCalculatorUsable>(
                    SamgoGameHelper.connectionString,
                    $@"
SELECT top 1 *
  FROM [dbo].[NanaCalculatorUsable]  where {strWhere} order by timeStamp desc 


");
            return response.FirstOrDefault();
        }
        private static object InsertReserveData(Reserve data)
        {
            var response = DapperHelper.InsertSQL(
                    SamgoGameHelper.connectionString, data.GetType().Name, data);

            return response;
        }
        private static object InsertStockData(Stock data)
        {
            var response = DapperHelper.InsertSQL(
                    SamgoGameHelper.connectionString, data.GetType().Name, data);

            return response;
        }


    }

    public class NanaLine
    {
        public int SeqNo { get; set; }
        public DateTime timeStamp { get; set; }
        public string type { get; set; }
        public string userId { get; set; }
        public string groupId { get; set; }
        public string roomId { get; set; }
        public string messageText { get; set; }
        public long timestampInt { get; set; }
    }
    public class NanaCalculatorUsable: NanaLine
    {
        public bool isWorkable { get; set; }
    }
}