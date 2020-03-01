﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Common.WordTemplate
{
    public class 兵書說明
    {

        public static string 綠色兵書 = $@"
綠色兵書主要為資源加成兵書及單一兵種屬性加成兵書(如單一防/血/攻擊等等).滿級為5級,屬性加1.5(即1.5級兵種效果)

需要集:
兵道類,優先集前排加血（如不屈、騎甲、盾甲），後排就加攻（ 如戰吼、弓襲）。因較容易升級，平民應該先收集，令戰隊有小量戰力提升，有比沒有的好。但若沒用上的兵種,如槍 / 盾等等，就不需要集，直接化掉。

不需要集:
1.文德類,不解釋,全當肥料化掉.
";
        public static string 藍色兵書 = $@"
藍色兵書比綠色兵書較為多元化, 除了兵種兵書及文德兵書外,還有功能兵書.

需要集:
兵道類：藍色兵道類兵書為加強兵種各屬性(血攻防)每級各0.2，兵書10級滿級，這是說滿級加各屬性2點，合共6點，即是 = 6級兵種效果。超有用！必集！
同樣,按自己需要，如果沒槍隊或盾隊的兵書可化掉。
功能兵書(招募,治療,擴兵,戰具,扶傷)
功能兵書 = 功能城效果，必集不解釋，但要再說一下扶傷及擴兵重要性。如果50兵營配上滿級擴兵(20 %)後，士兵總量為168萬，p這士兵量出1及2隊打軍團BOSS是不會需要用上加速令回兵，可以打完後慢慢回，省回加速 / 兵符作日後打架需要，對長遠發展不錯。

看似要集其實不需要集:
所有兵道‧弓 / 槍 / 盾 / 騎「守」兵書全部都化掉。驟眼看可以加各屬性0.2是很威，但需要在戰隊駐守城池所有加成。但你是平民的話，駐守也很快被打掉，對你來說作用根本不大，不要在這些兵書浪費註解，全都化掉！
";
        public static string 紫色兵書 = $@"
紫色兵書大致分為 1)控制類 2)屬性類 3)附助類。對平民來說，不建議一開始就集紫色兵書，主要原因為你是平民，本身的註解不多，要升上紫色兵書的等級花很長時間，還不如把資源集中在藍／綠色兵書先滿再想。

必集：
紫色兵書最重要的一本一定是均兵，它是強騎／盾／槍／弓兵書的升級版，而且不限兵種，加強兵種各屬性(血攻防)每級各0.3，兵書15級滿級，這是說滿級加各屬性4.5點，合共13.5點！先丟給前排再慢慢的集。

另外控制類的兵書如懈氣、錘擊也有一定效用，能有小機率延長控制效果。但是這類兵書不是最優先，請量力而為。

屬性類一般是不建議的，滿級才加30點，但是特別推薦帷幄。你是平民的話，後排最強的炮台一定是雙暴孟子郭嘉，只要多1點智力就能內訌對方郭嘉，所以這本後期必備。能集好就先存好，不升級也給郭嘉。

其他紫兵書，平民可以化掉吧，尤其是每周打過關都能拿到的剛勁。
";
        public static string 橙色兵書 = $@"
橙色兵書大多都很好用，但是所需經驗不少。平民應該先升好藍綠兵書比較好。
必集：
利刃和蓄力就像是孖生兄弟，一起用才能發揮最大效用。丟給你火力最強的武將配以加暴擊的寶物，就算是平民也能打出巨大的傷害。

另外迅疾和閃擊也是很好用的兵書。一個加閃避，如果有的蘆應該能成避雷高手。另外一個加攻防速度，這個有經驗才升級也可以，一等已是好用，可以丟給控制型武將增加速度儲怒。

堅韌兵書前期是不太需要，但是後期人人都有利刃的時候就很重要，能減低被暴擊的機會。

金睛兵書對平民來說是沒有用的兵書。一來橙將最多只有三個兵書空位，二來迅疾是很難抽的，根本平民很難有很多本。所以這是一本對大神有用，對平民沒用的兵書。";


        public static string 紅色兵書 = $@"課下去就有了還要問？";
    }
}