using HerbMagicWebApi.Common;
using HerbMagicWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTool
{
    class Program
    {
        static void Main(string[] args)
        {
            GetRogueData();
        }

        private static Rogue GetRogueData()
        {


            var q = $@"  SELECT[SeqNo]
      ,[RogueType]
      ,[Day]
      ,[RogueDes]
      ,[Date]
        FROM[dbo].[Rogue]
        where[Date]='{DateTime.Now.ToShortDateString()}'";

            var response = DapperHelper.Search<Rogue>(
                    SamgoGameHelper.connectionString,
                    $@"
SELECT  [SeqNo]
      ,[RogueType]
      ,[Day]
      ,[RogueDes]
      ,[Date]
  FROM [dbo].[Rogue]
  where [Date]='{DateTime.Now.ToShortDateString()}'

"
                    );

            return new Rogue();
        }
    }
}
