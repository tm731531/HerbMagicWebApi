using HerbMagic.Repository.DTO.GovData;
using HerbMagic.Repository.Factory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetGeometryInfo();


            //https://www.twse.com.tw/exchangeReport/MI_INDEX?response=json&date=20200214&type=0099P&_=1581756963783
            Stopwatch sw = new Stopwatch();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://data.nhi.gov.tw/Datasets/Download.ashx?rid=A21030000I-D50001-001&l=https://data.nhi.gov.tw/resource/mask/maskdata.csv");
            string responseStr = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = sr.ReadToEnd();
                }
            }
            List<GovMaskInfoDto> lgovMaskInfoDto = new List<GovMaskInfoDto>();
            GovMaskInfoDto govMaskInfoDto = new GovMaskInfoDto();
            GovHospitalInfoDto govHospitalInfoDto = new GovHospitalInfoDto();
            var targetData = responseStr.Split('\n');
            sw.Start();
            var total = targetData.Count();
            for (int i = 1; i < total; ++i)
            {
                var strData = targetData[i];
                govMaskInfoDto = new GovMaskInfoDto();
                //  Console.WriteLine($"{strData} 開始塞");
                if (string.IsNullOrEmpty(strData)) continue;
                var list = strData.Split(',');
                govMaskInfoDto.hospital_id = list[0];

                govHospitalInfoDto.hospital_id = list[0];
                govHospitalInfoDto.hospital_name = list[1];
                govHospitalInfoDto.hospital_address = list[2];
                govHospitalInfoDto.hospital_cellphone = list[3];
                govMaskInfoDto.audit_mask_count = Convert.ToInt32(list[4]);
                govMaskInfoDto.child_mask_count = Convert.ToInt32(list[5]);
                govMaskInfoDto.dataTime = Convert.ToDateTime(list[6]);
                // Console.WriteLine($"{strData} 物件準備好");
                lgovMaskInfoDto.Add(govMaskInfoDto);

                //try
                //{
                //    GovDataFactory.GovHospitalInfoRepository.InsertGovHospitalInfoDtos(govHospitalInfoDto);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                //Console.WriteLine($"{strData} 已完成");
            }
            Console.WriteLine($"資料準備 已完成");
            try
            {
                GovDataFactory.GovMaskInfoRepository.BulkInsertGovMaskInfoDtos(lgovMaskInfoDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sw.Stop();
            Console.WriteLine($"使用時間 {sw.ElapsedMilliseconds}  數量{targetData.Count()}");
            // Console.Read();
        }

        static void GetGeometryInfo()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://raw.githubusercontent.com/kiang/pharmacies/master/json/points.json");
            string responseStr = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = sr.ReadToEnd();
                }
            }
            List<string> ls = new List<string>() {
                "2337160015"
,"2338070010"
,"2342080015"
,"2342240011"
,"5901092014"
,"5901102444"
,"5901121725"
,"5901184106"
,"5902090827"
,"5903160088"
,"5903260743"
,"5905010045"
,"5905070078"
,"5905340262"
,"5907350319"
,"5912013234"
,"5917030100"
,"5921020112"
,"5921021851"
,"5921071244"
,"592202A330"
,"5931010342"
,"5931024524"
,"5931044866"
,"5931050211"
,"5931050364"
,"5931051950"
,"593106A511"
,"593106A959"
,"5931072280"
,"5931081332"
,"5931091856"
,"5931091909"
,"5931102229"
,"5931142303"
,"5931211063"
,"5932012939"
,"5932019581"
,"5932100916"
,"5933031741"
,"5933060162"
,"5934012357"
,"5936051441"
,"5936081243"
,"5936090037"
,"5936102970"
,"5937021830"
,"5937050135"
,"5937061503"
,"5937061594"
,"5937150032"
,"5937171166"
,"5938021745"
,"5938032328"
,"5939131400"
,"5939151135"
,"5939201283"
,"5943012972"
,"5943131534"
            };
            var datas = JsonConvert.DeserializeObject<Rootobject>(responseStr);
            foreach (var data in datas.features)
            {
                //           File.AppendAllText("update.txt",

                //               $@"INSERT INTO [dbo].[GovHospitalInfo]([hospital_id],[hospital_name],[hospital_address],[hospital_cellphone],[hospital_longitude],[hospital_latitude])
                //VALUES (N'{ data.properties.id}',N'{data.properties.name}' , N'{data.properties.address}',N'{data.properties.phone}',{ data.geometry.coordinates[0]},{data.geometry.coordinates[1]} ) GO" + Environment.NewLine);
                if (ls.Contains(data.properties.id))
                {

                    File.AppendAllText("update.txt",

                                       $@"UPDATE [dbo].[GovHospitalInfo] SET [hospital_longitude] =  { data.geometry.coordinates[0]},,[hospital_latitude] ={data.geometry.coordinates[1]}
  WHERE  [hospital_id] =N'{ data.properties.id}'  " + Environment.NewLine);
                }
            }
        }


        public class Rootobject
        {
            public string type { get; set; }
            public Feature[] features { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public Properties properties { get; set; }
            public Geometry geometry { get; set; }
        }

        public class Properties
        {
            public string id { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public int mask_adult { get; set; }
            public int mask_child { get; set; }
            public string updated { get; set; }
            public string available { get; set; }
            public object note { get; set; }
            public string custom_note { get; set; }
            public string website { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public float[] coordinates { get; set; }
        }



        public static class 註解Dic
        {
            public static Dictionary<int, int> 綠 = new Dictionary<int, int>() {
              {1,0 }, {2,200 }, {3,500 }, {4,900 }, {5,1500}
        };
            public static Dictionary<int, int> 藍 = new Dictionary<int, int>() {
              {1,0 }, {2,250 }, {3,650 }, {4,1150 }, {5,1900},
              {6,2900 }, {7,4150 }, {8,5650 }, {9,7650 }, {10,10150}
        };
            public static Dictionary<int, int> 紫 = new Dictionary<int, int>() {
              {1,0 }, {2,500 }, {3,1300 }, {4,2300 }, {5,3500},
              {6,5000 }, {7,7000 }, {8,9500 }, {9,12500 }, {10,16000},
              {11,20000 }, {12,25000 }, {13,31000 }, {14,38000 }, {15,46000}
        };
            public static Dictionary<int, int> 金 = new Dictionary<int, int>() {
              {1,0 }, {2,1000 }, {3,1600 }, {4,4600 }, {5,7000},
              {6,10000 }, {7,13600 }, {8,17600 }, {9,22000 }, {10,27000},
              {11,32600 }, {12,38600 }, {13,45600 }, {14,53600 }, {15,62600},
              {16,72600 }, {17,83600 }, {18,95600 }, {19,108600 }, {20,122600}
        };
            public static Dictionary<int, int> 紅 = new Dictionary<int, int>() {
              {1,0 }, {2,2000 }, {3,4400 }, {4,7400 }, {5,11000},
              {6,15000 }, {7,19400 }, {8,24400 }, {9,30000 }, {10,36000},
              {11,43000 }, {12,51000 }, {13,60000 }, {14,70000 }, {15,81000},
              {16,93000 }, {17,106000 }, {18,120000 }, {19,135000 }, {20,151000},
              {21,168000 }, {22,186000 }, {23,205000 }, {24,225000 }, {25,246000},
              {26,268000 }, {27,291000 }, {28,315000 }, {29,340000 }, {30,366000}
        };
        }
        }
    }
