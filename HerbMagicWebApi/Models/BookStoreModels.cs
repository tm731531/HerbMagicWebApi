using System;
using System.Web;

namespace HerbMagicWebApi.Models
{
    public class BookStoreModels : IHttpHandler
    {
        /// <summary>
        /// 您將需要在您 Web 的 Web.config 檔中設定此處理常式，
        /// 並且向 IIS 註冊該處理程式，才能使用它。如需詳細資訊，
        /// 請參閱下列連結: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // 如果您的 Managed 處理常式無法重新使用於其他要求，則傳回 false。
            // 如果您有針對要求保留的一些狀態資訊，通常是 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //在這裡寫下您的處理常式實作。
        }

        #endregion
        public class MainBook
        {

            public int SeqNo { get; set; }
            public string Author { get; set; }
            public string Translator { get; set; }
            public string Publisher { get; set; }
            public DateTime PublicationDate { get; set; }
            public string Language { get; set; }
            public int Pricing { get; set; }
            public string Currency { get; set; }
            public string Location { get; set; }
            public bool Status { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Owner { get; set; }
            public string BookName { get; set; }

        }

        public class MainHistory
        {
            public int SeqNo { get; set; }
            public string Description { get; set; }
            public string ID { get; set; }
            public DateTime Date { get; set; }
            public string Version { get; set; }

        }

        public class MainExchangeRate
        {
            public int SeqNo { get; set; }
            public string Currency { get; set; }
            public double CashExchangeRateBuys { get; set; }
            public double CashExchangeRateSells { get; set; }
            public double SpotExchangeRateBuys { get; set; }
            public double SpotExchangeRateSells { get; set; }
            public string Bank { get; set; }
            public string BankCode { get; set; }
            public DateTime Date { get; set; }
        }
        public class MainBoardGame
        {
            public int SeqNo { get; set; }
            public string GameName { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public double Price { get; set; }
            public string Currency { get; set; }
            public DateTime Date { get; set; }
            public string Evaluation { get; set; }
            public string People { get; set; }
            public string TotalTime { get; set; }
            public int PeopleMin { get; set; }
            public int PeopleMax { get; set; }
            public int TotalTimeMin { get; set; }
            public int TotalTimeMax { get; set; }
        }



    }
    public class SamgoModels
    {
        public class UserInfo
        {
            public int SeqNo { get; set; }
            public string Role { get; set; }
            public string Official { get; set; }
            public string Legion { get; set; }
        }
        public class UserPower
        {
            public int SeqNo { get; set; }
            public DateTime Time { get; set; }
            public int Power { get; set; }
        }
        public class UserValue
        {
            public int SeqNo { get; set; }
            public DateTime Time { get; set; }
            public int Value { get; set; }
        }
        public class UserBuild
        {
            public int SeqNo { get; set; }
            public DateTime Time { get; set; }
            public int Basilica { get; set; }
            public int Technique { get; set; }
            public int Market { get; set; }
            public int Horse { get; set; }
            public int School { get; set; }
            public int House { get; set; }
            public int Wood { get; set; }
            public int Stone { get; set; }
            public int Iron { get; set; }
            public int Farmland { get; set; }
            public int Barracks { get; set; }
            public int Soldier { get; set; }
            public int Coin { get; set; }
            public int Treasure { get; set; }
            public int Ordnance { get; set; }
            public int Wall { get; set; }
        }
        public class UserTechnique
        {
            public int SeqNo { get; set; }
            public DateTime Time { get; set; }
            public int FoodUp { get; set; }
            public int StoneUp { get; set; }
            public int WoodUp { get; set; }
            public int IronUp { get; set; }
            public int FrontAttack { get; set; }
            public int BackAttack { get; set; }
            public int FrontDefense { get; set; }
            public int BackDefense { get; set; }
            public int EnhancedProtection { get; set; }
            public int EnhancedPlunder { get; set; }
            public int MartialArts { get; set; }
            public int ThickBlack { get; set; }
            public int WarArt { get; set; }
            public int ArrowBuild { get; set; }
            public int RefuseHorse { get; set; }
            public int WallUp { get; set; }
            public int Reconstruction { get; set; }
            public int Judge { get; set; }
            public int heroic { get; set; }
            public int Ramp { get; set; }
            public int Perseverance { get; set; }
            public int Insight { get; set; }
            public int CriticalStrike { get; set; }
            public int Toughness { get; set; }
        }
        public class UserOrdnance
        {
            public int SeqNo { get; set; }
            public DateTime Time { get; set; }
            public int Catapult { get; set; }
            public int ShieldCar { get; set; }
            public int Well { get; set; }
            public int Drum { get; set; }
            public int WoodenCow { get; set; }


        }

        

    }
    public class ViewModels
    {
        public class UserInfo : SamgoModels.UserInfo
        {
            public DateTime Time { get; set; }
            public int Power { get; set; }
        }
        public class SPget_samgo_power_list_diff
        {
            public string Role;
            public object SecondPower;
            public object FirstPower;
            public object Diff;
            public string Legion;
        }
        public class UserData : SamgoModels.UserBuild
        {
            public string Role { get; set; }
            public string Official { get; set; }
            public string Legion { get; set; }
            public int Power { get; set; } = 0;
            public int Prosperity { get; set; } = 0;
            public int Lord { get; set; } = 0;


            public int SeqNo { get; set; }
            public DateTime Time { get; set; } = DateTime.UtcNow;
            public int FoodUp { get; set; } = 0;
            public int StoneUp { get; set; } = 0;
            public int WoodUp { get; set; } = 0;
            public int IronUp { get; set; } = 0;
            public int FrontAttack { get; set; } = 0;
            public int BackAttack { get; set; } = 0;
            public int FrontDefense { get; set; } = 0;
            public int BackDefense { get; set; } = 0;
            public int EnhancedProtection { get; set; } = 0;
            public int EnhancedPlunder { get; set; } = 0;
            public int MartialArts { get; set; } = 0;
            public int ThickBlack { get; set; } = 0;
            public int WarArt { get; set; } = 0;
            public int ArrowBuild { get; set; } = 0;
            public int RefuseHorse { get; set; } = 0;
            public int Wall { get; set; } = 0;
            public int Reconstruction { get; set; } = 0;
            public int Judge { get; set; } = 0;
            public int heroic { get; set; } = 0;
            public int Ramp { get; set; } = 0;
            public int Perseverance { get; set; } = 0;
            public int Insight { get; set; } = 0;
            public int CriticalStrike { get; set; } = 0;
            public int Toughness { get; set; } = 0;

            public int Catapult { get; set; } = 0;
            public int ShieldCar { get; set; } = 0;
            public int Well { get; set; } = 0;
            public int Drum { get; set; } = 0;
            public int WoodenCow { get; set; } = 0;

        }
        public class UserDetail : SamgoModels.UserTechnique
        {
            public int Catapult { get; set; } = 0;
            public int ShieldCar { get; set; } = 0;
            public int Well { get; set; } = 0;
            public int Drum { get; set; } = 0;
            public int WoodenCow { get; set; } = 0;

        }

        public class Ob
        {

            public string SeqNo { get; set; }
            public DateTime Time { get; set; }
            public string Basilica { get; set; }
            public string Technique { get; set; }
            public string Market { get; set; }
            public string Horse { get; set; }
            public string School { get; set; }
            public string House { get; set; }
            public string Wood { get; set; }
            public string Stone { get; set; }
            public string Iron { get; set; }
            public string Farmland { get; set; }
            public string Barracks { get; set; }
            public string Soldier { get; set; }
            public string Coin { get; set; }
            public string Treasure { get; set; }
            public string Ordnance { get; set; }
            public string Wall { get; set; }
            public string Role { get; set; }
            public string Official { get; set; }
            public string Legion { get; set; }
            public string Power { get; set; }
            public string Prosperity { get; set; }
            public string Lord { get; set; }
            public UserDetailString Tech;
        }
        public class UserDetailString {
            public string SeqNo { get; set; }
            public DateTime Time { get; set; }
            public string FoodUp { get; set; }
            public string StoneUp { get; set; }
            public string WoodUp { get; set; }
            public string IronUp { get; set; }
            public string FrontAttack { get; set; }
            public string BackAttack { get; set; }
            public string FrontDefense { get; set; }
            public string BackDefense { get; set; }
            public string EnhancedProtection { get; set; }
            public string EnhancedPlunder { get; set; }
            public string MartialArts { get; set; }
            public string ThickBlack { get; set; }
            public string WarArt { get; set; }
            public string ArrowBuild { get; set; }
            public string RefuseHorse { get; set; }
            public string WallUp { get; set; }
            public string Reconstruction { get; set; }
            public string Judge { get; set; }
            public string heroic { get; set; }
            public string Ramp { get; set; }
            public string Perseverance { get; set; }
            public string Insight { get; set; }
            public string CriticalStrike { get; set; }
            public string Toughness { get; set; }
            public string Catapult { get; set; }
            public string ShieldCar { get; set; }
            public string Well { get; set; }
            public string Drum { get; set; }
            public string WoodenCow { get; set; }
        }



        public class AllString
        {

            public string SeqNo { get; set; }
            public string Time { get; set; }
            public string Basilica { get; set; }
            public string Technique { get; set; }
            public string Market { get; set; }
            public string Horse { get; set; }
            public string School { get; set; }
            public string House { get; set; }
            public string Wood { get; set; }
            public string Stone { get; set; }
            public string Iron { get; set; }
            public string Farmland { get; set; }
            public string Barracks { get; set; }
            public string Soldier { get; set; }
            public string Coin { get; set; }
            public string Treasure { get; set; }
            public string Ordnance { get; set; }
            public string Wall { get; set; }
            public string Role { get; set; }
            public string Official { get; set; }
            public string Legion { get; set; }
            public string Power { get; set; }
            public string Prosperity { get; set; }
            public string Lord { get; set; }
            public string FoodUp { get; set; }
            public string StoneUp { get; set; }
            public string WoodUp { get; set; }
            public string IronUp { get; set; }
            public string FrontAttack { get; set; }
            public string BackAttack { get; set; }
            public string FrontDefense { get; set; }
            public string BackDefense { get; set; }
            public string EnhancedProtection { get; set; }
            public string EnhancedPlunder { get; set; }
            public string MartialArts { get; set; }
            public string ThickBlack { get; set; }
            public string WarArt { get; set; }
            public string ArrowBuild { get; set; }
            public string RefuseHorse { get; set; }
            public string WallUp { get; set; }
            public string Reconstruction { get; set; }
            public string Judge { get; set; }
            public string heroic { get; set; }
            public string Ramp { get; set; }
            public string Perseverance { get; set; }
            public string Insight { get; set; }
            public string CriticalStrike { get; set; }
            public string Toughness { get; set; }
            public string Catapult { get; set; }
            public string ShieldCar { get; set; }
            public string Well { get; set; }
            public string Drum { get; set; }
            public string WoodenCow { get; set; }
        }

        public class All
        {

            public int SeqNo { get; set; }
            public DateTime Time { get; set; }
            public int Basilica { get; set; }
            public int Technique { get; set; }
            public int Market { get; set; }
            public int Horse { get; set; }
            public int School { get; set; }
            public int House { get; set; }
            public int Wood { get; set; }
            public int Stone { get; set; }
            public int Iron { get; set; }
            public int Farmland { get; set; }
            public int Barracks { get; set; }
            public int Soldier { get; set; }
            public int Coin { get; set; }
            public int Treasure { get; set; }
            public int Ordnance { get; set; }
            public int Wall { get; set; }
            public string Role { get; set; }
            public string Official { get; set; }
            public string Legion { get; set; }
            public int Power { get; set; }
            public int Prosperity { get; set; }
            public int Lord { get; set; }
            public int FoodUp { get; set; }
            public int StoneUp { get; set; }
            public int WoodUp { get; set; }
            public int IronUp { get; set; }
            public int FrontAttack { get; set; }
            public int BackAttack { get; set; }
            public int FrontDefense { get; set; }
            public int BackDefense { get; set; }
            public int EnhancedProtection { get; set; }
            public int EnhancedPlunder { get; set; }
            public int MartialArts { get; set; }
            public int ThickBlack { get; set; }
            public int WarArt { get; set; }
            public int ArrowBuild { get; set; }
            public int RefuseHorse { get; set; }
            public int WallUp { get; set; }
            public int Reconstruction { get; set; }
            public int Judge { get; set; }
            public int heroic { get; set; }
            public int Ramp { get; set; }
            public int Perseverance { get; set; }
            public int Insight { get; set; }
            public int CriticalStrike { get; set; }
            public int Toughness { get; set; }
            public int Catapult { get; set; }
            public int ShieldCar { get; set; }
            public int Well { get; set; }
            public int Drum { get; set; }
            public int WoodenCow { get; set; }
        }
    }
}
