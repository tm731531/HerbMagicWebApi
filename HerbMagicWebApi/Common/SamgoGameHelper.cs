using HerbMagicWebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using static HerbMagicWebApi.Models.SamgoModels;
using static HerbMagicWebApi.Models.ViewModels;

namespace HerbMagicWebApi.Common
{
    public class SamgoGameHelper
    {

        public static string connectionString =
          ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;

        public static string Susses = "資料輸入成功";
        public static string DataInputError = "資料有誤 輸入長度錯誤";

        public static string DataConvertError = "資料有誤 類別錯誤";

        public static int AddUserData(All userData)
        {
            UserBuild ub = new UserBuild();
            userData.Time = Function.GetUTCTime();
            SamgoModels.UserInfo ui = new SamgoModels.UserInfo();
            UserValue uLord = new UserValue();
            UserValue uProsperity = new UserValue();
            UserPower up = new UserPower();
            UserTechnique ut = new UserTechnique();
            UserOrdnance uo = new UserOrdnance();

            var userInfo = new SamgoModels.UserInfo();
            userData.SeqNo = DapperHelper.Search<int>(
               connectionString, " exec [samgo_add_user_and_group] N'" +
               userData.Role + "' , N'" + userData.Legion + "'").FirstOrDefault();
            ConvertDataToModels(ref ub, ref ui, ref uLord, ref uProsperity, ref up, userData);

            DapperHelper.InsertSQLNormal<UserBuild>(connectionString, "Build", ub);
            //DapperHelper.InsertSQL<SamgoModels.UserInfo>(connectionString, "SamgoGame", ui);
            DapperHelper.InsertSQLNormal<UserValue>(connectionString, "Lord", uLord);
            DapperHelper.InsertSQLNormal<UserValue>(connectionString, "Prosperity", uProsperity);
            DapperHelper.InsertSQLNormal<UserPower>(connectionString, "SamgoGamePower", up);
            ConvertDataToModels(ref ut, ref uo, userData);

            DapperHelper.InsertSQLNormal<UserTechnique>(connectionString, "Technique", ut);
            var response = DapperHelper.InsertSQLNormal<UserOrdnance>(connectionString, "Ordnance", uo);

            return userData.SeqNo;
        }


        private static void ConvertDataToModels(ref UserBuild ub,
          ref SamgoModels.UserInfo ui,
          ref UserValue uLord,
          ref UserValue uProsperity,
          ref UserPower up,
          All userData)
        {

            //SamgoGame(主紀錄+編號)
            ui.Legion = userData.Legion;
            ui.Official = userData.Official;
            ui.Role = userData.Role;
            ui.SeqNo = userData.SeqNo;

            //Lord(主公)
            uLord.SeqNo = userData.SeqNo;
            uLord.Time = userData.Time;
            uLord.Value = userData.Lord;

            //Prosperity(繁榮)
            uProsperity.SeqNo = userData.SeqNo;
            uProsperity.Time = userData.Time;
            uProsperity.Value = userData.Prosperity;

            //Build(大殿)
            ub.SeqNo = userData.SeqNo;
            ub.Time = userData.Time;
            ub.Basilica = userData.Basilica;
            ub.Technique = userData.Technique;
            ub.Market = userData.Market;
            ub.Horse = userData.Horse;
            ub.School = userData.School;
            ub.House = userData.House;
            ub.Wood = userData.Wood;
            ub.Stone = userData.Stone;
            ub.Iron = userData.Iron;
            ub.Farmland = userData.Farmland;
            ub.Barracks = userData.Barracks;
            ub.Soldier = userData.Soldier;
            ub.Coin = userData.Coin;
            ub.Treasure = userData.Treasure;
            ub.Ordnance = userData.Ordnance;
            ub.Wall = userData.Wall;
            //戰力
            up.Power = userData.Power;
            up.SeqNo = userData.SeqNo;
            up.Time = userData.Time;
        }

        private static void ConvertDataToModels(ref UserTechnique ut,
           ref UserOrdnance uo, All userData)
        {
            //Technique(技法)
            ut.SeqNo = userData.SeqNo;
            ut.Time = userData.Time;
            ut.FoodUp = userData.FoodUp;
            ut.StoneUp = userData.StoneUp;
            ut.WoodUp = userData.WoodUp;
            ut.IronUp = userData.IronUp;
            ut.FrontAttack = userData.FrontAttack;
            ut.BackAttack = userData.BackAttack;
            ut.FrontDefense = userData.FrontDefense;
            ut.BackDefense = userData.BackDefense;
            ut.EnhancedProtection = userData.EnhancedProtection;
            ut.EnhancedPlunder = userData.EnhancedPlunder;
            ut.MartialArts = userData.MartialArts;
            ut.ThickBlack = userData.ThickBlack;
            ut.WarArt = userData.WarArt;
            ut.ArrowBuild = userData.ArrowBuild;
            ut.RefuseHorse = userData.RefuseHorse;
            ut.WallUp = userData.WallUp;
            ut.Reconstruction = userData.Reconstruction;
            ut.Judge = userData.Judge;
            ut.heroic = userData.heroic;
            ut.Ramp = userData.Ramp;
            ut.Perseverance = userData.Perseverance;
            ut.Insight = userData.Insight;
            ut.CriticalStrike = userData.CriticalStrike;
            ut.Toughness = userData.Toughness;

            //Ordnance(軍械)
            uo.SeqNo = userData.SeqNo;
            uo.Time = userData.Time;
            uo.Catapult = userData.Catapult;
            uo.ShieldCar = userData.ShieldCar;
            uo.Well = userData.Well;
            uo.Drum = userData.Drum;
            uo.WoodenCow = userData.WoodenCow;



        }

        public static void ConvertDataToModels(ref AllString ob,
            UserData ud, UserDetail userData)
        {
            try
            {
                ob.Horse = ud.Horse.ToString();
                ob.House = ud.House.ToString();
                ob.Role = ud.Role;
                ob.Official = ud.Official;
                ob.Legion = ud.Legion;
                ob.Power = ud.Power.ToString();
                ob.Prosperity = ud.Prosperity.ToString();
                ob.Lord = ud.Lord.ToString();
                ob.SeqNo = ud.SeqNo.ToString();
                ob.Time = ud.Time.ToString();
                ob.Basilica = ud.Basilica.ToString();
                ob.Technique = ud.Technique.ToString();
                ob.Market = ud.Market.ToString();
                ob.School = ud.School.ToString();
                ob.Wood = ud.Wood.ToString();
                ob.Stone = ud.Stone.ToString();
                ob.Iron = ud.Iron.ToString();
                ob.Farmland = ud.Farmland.ToString();
                ob.Barracks = ud.Barracks.ToString();
                ob.Soldier = ud.Soldier.ToString();
                ob.Coin = ud.Coin.ToString();
                ob.Treasure = ud.Treasure.ToString();
                ob.Ordnance = ud.Ordnance.ToString();
                ob.Wall = ud.Wall.ToString();
                //Technique(技法)
                ob.SeqNo = userData.SeqNo.ToString();
                ob.Time = userData.Time.ToString();
                ob.FoodUp = userData.FoodUp.ToString();
                ob.StoneUp = userData.StoneUp.ToString();
                ob.WoodUp = userData.WoodUp.ToString();
                ob.IronUp = userData.IronUp.ToString();
                ob.FrontAttack = userData.FrontAttack.ToString();
                ob.BackAttack = userData.BackAttack.ToString();
                ob.FrontDefense = userData.FrontDefense.ToString();
                ob.BackDefense = userData.BackDefense.ToString();
                ob.EnhancedProtection = userData.EnhancedProtection.ToString();
                ob.EnhancedPlunder = userData.EnhancedPlunder.ToString();
                ob.MartialArts = userData.MartialArts.ToString();
                ob.ThickBlack = userData.ThickBlack.ToString();
                ob.WarArt = userData.WarArt.ToString();
                ob.ArrowBuild = userData.ArrowBuild.ToString();
                ob.RefuseHorse = userData.RefuseHorse.ToString();
                ob.WallUp = userData.WallUp.ToString();
                ob.Reconstruction = userData.Reconstruction.ToString();
                ob.Judge = userData.Judge.ToString();
                ob.heroic = userData.heroic.ToString();
                ob.Ramp = userData.Ramp.ToString();
                ob.Perseverance = userData.Perseverance.ToString();
                ob.Insight = userData.Insight.ToString();
                ob.CriticalStrike = userData.CriticalStrike.ToString();
                ob.Toughness = userData.Toughness.ToString();

                //Ordnance(軍械)
                ob.SeqNo = userData.SeqNo.ToString();
                ob.Time = userData.Time.ToString();
                ob.Catapult = userData.Catapult.ToString();
                ob.ShieldCar = userData.ShieldCar.ToString();
                ob.Well = userData.Well.ToString();
                ob.Drum = userData.Drum.ToString();
                ob.WoodenCow = userData.WoodenCow.ToString();

            }
            catch (Exception)
            {
            }
        }
        public static List<AllString> FillDataForView(IEnumerable<AllString> allData, int count)
        {
            List<AllString> la = new List<AllString>();


            foreach (var role in allData.Select(x => x.Role).Distinct())
            {

                AllString aas = new AllString();
                aas.Role = role;
                aas.Power = string.Join(" , ", allData.Where(x => x.Role == role).Select(x => x.Power).Distinct().Take(count));
                aas.Basilica = string.Join(" , ", allData.Where(x => x.Role == role).Select(x => x.Basilica).Distinct().Take(count));
                aas.Time = string.Join(" , ", allData.Where(x => x.Role == role).Select(x => Convert.ToDateTime(x.Time).ToShortDateString()).Distinct().Take(count));
                aas.Lord = string.Join(" , ", allData.Where(x => x.Role == role).Select(x => x.Lord).Distinct().Take(count));
                aas.Well = string.Join(" , ", allData.Where(x => x.Role == role).Select(x => x.Well).Distinct().Take(count));
                la.Add(aas);
            }
            return la;
        }
    }
}