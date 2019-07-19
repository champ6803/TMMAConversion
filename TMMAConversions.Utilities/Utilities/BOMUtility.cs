using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.Utilities.Utilities
{
    public class BOMUtility
    {
        public static List<BOMHeaderModel> CheckBOMAlt(List<BOMHeaderModel> bomGradeLevelHeaderList)
        {
            List<string> bomAltList = new List<string>();
            List<string> materialCodeList = new List<string>();
            List<BOMHeaderModel> newBOMHeader = new List<BOMHeaderModel>();

            for (int i = 0; i < bomGradeLevelHeaderList.Count(); i++)
            {
                if (!materialCodeList.Contains(bomGradeLevelHeaderList[i].MaterialCode))
                {
                    var list = bomGradeLevelHeaderList.Where(o => o.MaterialCode == bomGradeLevelHeaderList[i].MaterialCode).ToList();
                    string condition = "1"; // 1 is no bom alt 
                    foreach (var o in list)
                    {
                        if (!bomAltList.Contains(o.BOMAlt) && bomAltList.Count() > 0)
                        {
                            condition = "2"; // 2 is count bom alt > 2
                            break;
                        }
                        bomAltList.Add(o.BOMAlt);
                    }

                    var newList = SetCondition(list, condition);
                    newBOMHeader.AddRange(newList);
                }

                materialCodeList.Add(bomGradeLevelHeaderList[i].MaterialCode);
            }

            return newBOMHeader;
        }

        private static List<BOMHeaderModel> SetCondition(List<BOMHeaderModel> list, string condition)
        {
            foreach (var o in list)
            {
                o.Condition = condition;
            }

            return list;
        }
    }
}
