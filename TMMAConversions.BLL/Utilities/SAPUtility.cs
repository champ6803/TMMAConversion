using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.Utilities
{
    public class SAPUtility
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected static CultureInfo usCulture = new CultureInfo("en-US");

        #region Monomer

        /// <summary>
        /// Create
        /// </summary>
        public static void ConvertToMMBOMTextFile(List<BOMHeaderModel> bomHeaderList, List<BOMItemModel> bomItemList, string fileName, string extension, string user, DateTime validDate)
        {
            //string ValidDateFrom = DateTime.Now.ToString("dd.MM.yyyy", usCulture);
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);
            string BOMUsage = "1";
            string BOMStatus = "1";

            try
            {
                if (bomHeaderList != null && bomItemList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        // generate create bom header
                        foreach (var o in bomHeaderList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_CS01	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	CS01                                                                                                                                	");
                            fs.WriteLine("SAPLCSDI                                	0100	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC29N-STLAN                                                                                                                         	{0}", o.BOMAlt); // BOM Alternative
                            fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", o.BOMUsage); // BOMUsage
                            fs.WriteLine("SAPLCSDI                                	0110	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	RC29K-ZTEXT                                                                                                                         	{0}", o.BOMHeaderText); // BOM Usage
                            fs.WriteLine("                                        	0000	 	RC29K-BMENG                                                                                                                         	{0}", o.BaseQuantity); // Base Quantity
                            fs.WriteLine("                                        	0000	 	RC29K-STLST                                                                                                                         	{0}", BOMStatus); // BOM Status
                            fs.WriteLine("SAPLCSDI                                	0111	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                        }

                        // generate delete bom item
                        foreach (var o in bomHeaderList)
                        {
                            if (String.Equals("1", o.Condition, StringComparison.OrdinalIgnoreCase))
                            {
                                fs.WriteLine("	0000	M	ZCONV_CS05	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                fs.WriteLine("                                        	0000	T	CS05                                                                                                                                	");
                                fs.WriteLine("SAPLCSDI                                	0102	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                                fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", BOMUsage); // BOMUsage
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MALL");
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCDL");
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                            }
                            else
                            {
                                fs.WriteLine("	0000	M	ZCONV_CS02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                fs.WriteLine("                                        	0000	T	CS02                                                                                                                                	");
                                fs.WriteLine("SAPLCSDI                                	0102	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                                fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", BOMUsage); // BOMUsage
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=SETP");
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=CLWI");
                                fs.WriteLine("                                        	0000	 	RC29K-SELAL                                                                                                                           	{0}", o.BOMAlt);
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=/CS");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MALL");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCDL");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                            }
                        }

                        // generate bom add new line item 
                        foreach (var o in bomHeaderList)
                        {
                            int count = 1;
                            int BOMItem = 10;
                            foreach (var a in bomItemList)
                            {
                                if (o.MaterialCode == a.MaterialCode)
                                {
                                    List<BOMItemModel> resultList = bomItemList.Where(t => t.MaterialCode == o.MaterialCode).ToList();
                                    BOMItemModel first = resultList.First();

                                    // add component
                                    if (a.Equals(first)) // if not exist
                                    {
                                        // BOM Header
                                        fs.WriteLine("	0000	M	ZCONV_CS02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                        fs.WriteLine("                                        	0000	T	CS02                                                                                                                                	");
                                        fs.WriteLine("SAPLCSDI                                	0100	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", a.MaterialCode); // Header Material
                                        fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", a.Plant); // Plant
                                        fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", a.BOMUsage); // BOMUsage
                                        fs.WriteLine("                                        	0000	 	RC29N-STLAN                                                                                                                         	{0}", a.BOMAlt); // BOM Alternative
                                        fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From

                                        // Init srceen for component
                                        fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-IDNRK(0{0})                                                                                                                     	{1}", count, a.ComponentMaterial); // Component Material
                                        fs.WriteLine("                                        	0000	 	RC29P-POSTP(0{0})                                                                                                                     	L", count); // fix

                                        // BOM Item
                                        fs.WriteLine("SAPLCSDI                                	0130	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-POSNR                                                                                                                         	{0}", a.BOMItem);
                                        fs.WriteLine("                                        	0000	 	RC29P-IDNRK                                                                                                                         	{0}", a.ComponentMaterial);
                                        fs.WriteLine("                                        	0000	 	RC29P-MENGE                                                                                                                         	{0}", a.ComponentQuantity);
                                        fs.WriteLine("                                        	0000	 	RC29P-MEINS                                                                                                                         	{0}", a.ComponentUnit);
                                        fs.WriteLine("                                        	0000	 	RC29P-FMENG                                                                                                                         	{0}", a.FixedQty);
                                        fs.WriteLine("                                        	0000	 	RC29P-AVOAU                                                                                                                         	{0}", a.OperationScrap);
                                        fs.WriteLine("                                        	0000	 	RC29P-AUSCH                                                                                                                         	{0}", a.ComponentScrap);
                                        fs.WriteLine("                                        	0000	 	RC29P-NETAU                                                                                                                         	{0}", a.NetScrap);
                                        fs.WriteLine("SAPLCSDI                                	0131	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-SANKA                                                                                                                         	X");
                                        fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                                    }
                                    else
                                    {
                                        // BOM Header
                                        fs.WriteLine("	0000	M	ZCONV_CS02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                        fs.WriteLine("                                        	0000	T	CS02                                                                                                                                	");
                                        fs.WriteLine("SAPLCSDI                                	0100	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", a.MaterialCode); // Header Material
                                        fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", a.Plant); // Plant
                                        fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", a.BOMUsage); // BOMUsage
                                        fs.WriteLine("                                        	0000	 	RC29N-STLAN                                                                                                                         	{0}", a.BOMAlt); // BOM Alternative
                                        fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                        fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");

                                        // Init srceen for component
                                        fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-IDNRK(0{0})                                                                                                                     	{1}", count, a.ComponentMaterial); // Component Material
                                        fs.WriteLine("                                        	0000	 	RC29P-POSTP(0{0})                                                                                                                     	L", count); // fix

                                        // BOM Item
                                        fs.WriteLine("SAPLCSDI                                	0130	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-POSNR                                                                                                                         	{0}", a.BOMItem);
                                        fs.WriteLine("                                        	0000	 	RC29P-IDNRK                                                                                                                         	{0}", a.ComponentMaterial);
                                        fs.WriteLine("                                        	0000	 	RC29P-MENGE                                                                                                                         	{0}", a.ComponentQuantity);
                                        fs.WriteLine("                                        	0000	 	RC29P-MEINS                                                                                                                         	{0}", a.ComponentUnit);
                                        fs.WriteLine("                                        	0000	 	RC29P-FMENG                                                                                                                         	{0}", a.FixedQty);
                                        fs.WriteLine("                                        	0000	 	RC29P-AVOAU                                                                                                                         	{0}", a.OperationScrap);
                                        fs.WriteLine("                                        	0000	 	RC29P-AUSCH                                                                                                                         	{0}", a.ComponentScrap);
                                        fs.WriteLine("                                        	0000	 	RC29P-NETAU                                                                                                                         	{0}", a.NetScrap);
                                        fs.WriteLine("SAPLCSDI                                	0131	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-SANKA                                                                                                                         	X");
                                        fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                                    }

                                    count++;
                                    BOMItem += 10;
                                }
                            }
                        }


                        //foreach (var o in bomHeaderList)
                        //{
                        //    int count = 1;
                        //    int BOMItem = 10;

                        //    fs.WriteLine("	0000	M	ZCONV_CS01	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                        //    fs.WriteLine("                                        	0000	T	CS01                                                                                                                                	");
                        //    fs.WriteLine("SAPLCSDI                                	0100	X	                                                                                                                                    	");
                        //    fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                        //    fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                        //    fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                        //    fs.WriteLine("                                        	0000	 	RC29N-STLAN                                                                                                                         	{0}", o.BOMAlt); // BOM Alternative
                        //    fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", ValidDateFrom); // Valid Date From
                        //    fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", BOMUsage); // BOMUsage
                        //    fs.WriteLine("SAPLCSDI                                	0110	X	                                                                                                                                    	");
                        //    fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                        //    fs.WriteLine("                                        	0000	 	RC29K-ZTEXT                                                                                                                         	{0}", o.BOMHeaderText); // BOM Usage
                        //    fs.WriteLine("                                        	0000	 	RC29K-BMENG                                                                                                                         	{0}", o.BaseQuantity); // Base Quantity
                        //    fs.WriteLine("                                        	0000	 	RC29K-STLST                                                                                                                         	{0}", BOMStatus); // BOM Status
                        //    fs.WriteLine("SAPLCSDI                                	0111	X	                                                                                                                                    	");
                        //    fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");

                        //    foreach (var a in bomItemList)
                        //    {
                        //        if (o.MaterialCode == a.MaterialCode)
                        //        {
                        //            List<BOMItemModel> resultList = bomItemList.Where(t => t.MaterialCode == o.MaterialCode).ToList();
                        //            BOMItemModel last = resultList.Last();

                        //            // Init srceen for component
                        //            fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                        //            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=/CS");
                        //            fs.WriteLine("                                        	0000	 	RC29P-IDNRK(0{0})                                                                                                                     	{1}", count, a.ComponentMaterial); // Component Material
                        //            fs.WriteLine("                                        	0000	 	RC29P-MENGE(0{0})                                                                                                                     	{1}", count, a.ComponentQuantity); // Component Quantity
                        //            fs.WriteLine("                                        	0000	 	RC29P-MEINS(0{0})                                                                                                                       	{1}", count, a.ComponentUnit); // Component Unit
                        //            fs.WriteLine("                                        	0000	 	RC29P-POSTP(0{0})                                                                                                                     	L", count);

                        //            // Last Item
                        //            if (a.Equals(last))
                        //            {
                        //                fs.WriteLine("SAPLCSDI                                	0130	X	                                                                                                                                    	");
                        //                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                        //                fs.WriteLine("                                        	0000	 	RC29P-POSNR                                                                                                                         	{0}", string.Format("00{0}", BOMItem));
                        //                fs.WriteLine("                                        	0000	 	RC29P-IDNRK                                                                                                                         	{0}", a.ComponentMaterial);
                        //                fs.WriteLine("                                        	0000	 	RC29P-MENGE                                                                                                                         	{0}", a.ComponentQuantity);
                        //                fs.WriteLine("                                        	0000	 	RC29P-MEINS                                                                                                                         	{0}", a.ComponentUnit);
                        //                fs.WriteLine("SAPLCSDI                                	0131	X	                                                                                                                                    	");
                        //                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                        //                fs.WriteLine("                                        	0000	 	RC29P-SANKA                                                                                                                         	X");
                        //                fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                        //                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                        //            }
                        //            else
                        //            {
                        //                // First Item
                        //                if (count == 1)
                        //                {
                        //                    fs.WriteLine("SAPLCSDI                                	0130	X	                                                                                                                                    	");
                        //                    fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                        //                    fs.WriteLine("                                        	0000	 	RC29P-POSNR                                                                                                                         	{0}", string.Format("00{0}", BOMItem));
                        //                    fs.WriteLine("                                        	0000	 	RC29P-IDNRK                                                                                                                         	{0}", a.ComponentMaterial);
                        //                    fs.WriteLine("                                        	0000	 	RC29P-MENGE                                                                                                                         	{0}", a.ComponentQuantity);
                        //                    fs.WriteLine("                                        	0000	 	RC29P-MEINS                                                                                                                         	{0}", a.ComponentUnit);
                        //                    fs.WriteLine("SAPLCSDI                                	0131	X	                                                                                                                                    	");
                        //                    fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                        //                    fs.WriteLine("                                        	0000	 	RC29P-SANKA                                                                                                                         	X");
                        //                }
                        //                else
                        //                {
                        //                    fs.WriteLine("SAPLCSDI                                	0130	X	                                                                                                                                    	");
                        //                    fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                        //                    fs.WriteLine("                                        	0000	 	RC29P-POSNR                                                                                                                         	{0}", string.Format("00{0}", BOMItem));
                        //                    fs.WriteLine("                                        	0000	 	RC29P-IDNRK                                                                                                                         	{0}", a.ComponentMaterial);
                        //                    fs.WriteLine("                                        	0000	 	RC29P-MENGE                                                                                                                         	{0}", a.ComponentQuantity);
                        //                    fs.WriteLine("                                        	0000	 	RC29P-MEINS                                                                                                                         	{0}", a.ComponentUnit);
                        //                    fs.WriteLine("SAPLCSDI                                	0131	X	                                                                                                                                    	");
                        //                    fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                        //                    fs.WriteLine("                                        	0000	 	RC29P-SANKA                                                                                                                         	X");
                        //                }
                        //            }
                        //            count++;
                        //            BOMItem += 10;
                        //        }
                        //    }
                        //}


                    } // StreamWriter


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // use
        public static void ConvertToMMABOMTextFile(List<BOMHeaderModel> bomHeaderList, List<BOMItemModel> bomItemList, List<BOMHeaderModel> routingHeaderList, List<BOMItemModel> routingItemList, string filePath, string fileName, string extension, string user, DateTime validDate)
        {
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);
            string BOMStatus = "1"; // 1 is active
            string Name = Path.GetFileName(filePath);
            string ConversionOfUOMNumerator = "1";
            string ConversionOfUOMDenominator = "1";
            string TaskListType = "N";
            string ValidDateTo = "31.12.9999";
            //string IssueStorageLocation = "P001";
            string ReceivingStorageLocation = "";

            try
            {
                if ((bomHeaderList != null && bomHeaderList.Count > 0) && (bomItemList != null && bomItemList.Count > 0))
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    // Create a new file
                    using (StreamWriter fs = new StreamWriter(filePath + extension, false, Encoding.Unicode))
                    {
                        fs.WriteLine("\t0000\tM\tZCONV_{0}\t{1}          {2}  {3}", Name, user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time

                        // 1
                        // generate create bom header
                        foreach (var o in bomHeaderList)
                        {
                            fs.WriteLine("                                        \t0000\tT\tCS01                                                                                                                                \t");
                            fs.WriteLine("SAPLCSDI                                \t0100\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                            fs.WriteLine("                                        \t0000\t \tRC29N-MATNR                                                                                                                         \t{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        \t0000\t \tRC29N-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                            fs.WriteLine("                                        \t0000\t \tRC29N-STLAN                                                                                                                         \t{0}", o.BOMUsage); // BOM Usage
                            fs.WriteLine("                                        \t0000\t \tRC29N-DATUV                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("                                        \t0000\t \tRC29N-STLAL                                                                                                                         \t{0}", o.BOMAlt); // BOM Alternative
                            fs.WriteLine("SAPLCSDI                                \t0110\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                            fs.WriteLine("                                        \t0000\t \tRC29K-ZTEXT                                                                                                                         \t{0}", o.BOMHeaderText); // BOM Text
                            fs.WriteLine("                                        \t0000\t \tRC29K-BMENG                                                                                                                         \t{0}", o.BaseQuantity); // Base Quantity
                            fs.WriteLine("                                        \t0000\t \tRC29K-STLST                                                                                                                         \t{0}", BOMStatus); // BOM Status
                            fs.WriteLine("SAPLCSDI                                \t0111\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                            fs.WriteLine("SAPLCSDI                                \t0140\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCBU");
                        }

                        // 1.1
                        // generate bom change header
                        foreach (var o in bomHeaderList)
                        {
                            // BOM Header
                            fs.WriteLine("                                        \t0000\tT\tCS02                                                                                                                                \t");
                            fs.WriteLine("SAPLCSDI                                \t0100\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=KALL");
                            fs.WriteLine("                                        \t0000\t \tRC29N-MATNR                                                                                                                         \t{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        \t0000\t \tRC29N-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                            fs.WriteLine("                                        \t0000\t \tRC29N-STLAN                                                                                                                         \t{0}", o.BOMUsage); // BOMUsage
                            fs.WriteLine("                                        \t0000\t \tRC29N-STLAL                                                                                                                         \t{0}", o.BOMAlt); // BOM Alternative
                            fs.WriteLine("                                        \t0000\t \tRC29N-DATUV                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("SAPLCSDI                                \t2110\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCBU");
                            fs.WriteLine("                                        \t0000\t \tRC29K-ZTEXT                                                                                                                         \t{0}", o.BOMHeaderText); // BOM Text
                            fs.WriteLine("                                        \t0000\t \tRC29K-BMENG                                                                                                                         \t{0}", o.BaseQuantity); // Base Quantity
                            fs.WriteLine("                                        \t0000\t \tRC29K-STLST                                                                                                                         \t{0}", BOMStatus); // BOM Status
                        }

                        // 2
                        // generate delete bom item
                        foreach (var o in bomHeaderList)
                        {
                            if (String.Equals("1", o.Condition, StringComparison.OrdinalIgnoreCase))
                            {
                                fs.WriteLine("                                        \t0000\tT\tCS05                                                                                                                                \t");
                                fs.WriteLine("SAPLCSDI                                \t0102\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                fs.WriteLine("                                        \t0000\t \tRC29N-MATNR                                                                                                                         \t{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        \t0000\t \tRC29N-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                fs.WriteLine("                                        \t0000\t \tRC29N-DATUV                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                fs.WriteLine("                                        \t0000\t \tRC29N-STLAN                                                                                                                         \t{0}", o.BOMUsage); // BOMUsage
                                fs.WriteLine("SAPLCSDI                                \t0160\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=MALL");
                                fs.WriteLine("SAPLCSDI                                \t0160\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCDL");
                                fs.WriteLine("SAPLCSDI                                \t0160\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCBU");
                            }
                            else
                            {
                                fs.WriteLine("                                        \t0000\tT\tCS02                                                                                                                                \t");
                                fs.WriteLine("SAPLCSDI                                \t0100\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                fs.WriteLine("                                        \t0000\t \tRC29N-MATNR                                                                                                                         \t{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        \t0000\t \tRC29N-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                fs.WriteLine("                                        \t0000\t \tRC29N-DATUV                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                fs.WriteLine("                                        \t0000\t \tRC29N-STLAN                                                                                                                         \t{0}", o.BOMUsage); // BOM Usage
                                fs.WriteLine("SAPLCSDI                                \t0180\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=SETP");
                                fs.WriteLine("SAPLCSDI                                \t0708\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=CLWI");
                                fs.WriteLine("                                        \t0000\t \tRC29K-SELAL                                                                                                                         \t{0}", o.BOMAlt);
                                fs.WriteLine("SAPLCSDI                                \t0180\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tRC29K-AUSKZ(01)                                                                                                                     \t{0}", "X"); // Header Material
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCPU");
                                fs.WriteLine("SAPLCSDI                                \t0150\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=MALL");
                                fs.WriteLine("SAPLCSDI                                \t0150\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCDL");
                                fs.WriteLine("SAPLCSDI                                \t0150\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCBU");
                            }
                        }

                        // 3
                        // generate bom add new line item 
                        foreach (var o in bomHeaderList)
                        {
                            int count = 1;
                            List<BOMItemModel> resultList = bomItemList.Where(t => t.MaterialCode == o.MaterialCode && t.BOMAlt == o.BOMAlt && t.BOMUsage == o.BOMUsage).ToList();
                            BOMItemModel first = resultList.Count > 0 ? resultList.First() : null;
                            foreach (var a in resultList)
                            {
                                // add component
                                if (a.Equals(first)) // if not exist
                                {
                                    // BOM Header
                                    fs.WriteLine("                                        \t0000\tT\tCS02                                                                                                                                \t");
                                    fs.WriteLine("SAPLCSDI                                \t0100\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                    fs.WriteLine("                                        \t0000\t \tRC29N-MATNR                                                                                                                         \t{0}", o.MaterialCode); // Header Material
                                    fs.WriteLine("                                        \t0000\t \tRC29N-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                    fs.WriteLine("                                        \t0000\t \tRC29N-STLAN                                                                                                                         \t{0}", o.BOMUsage); // BOMUsage
                                    fs.WriteLine("                                        \t0000\t \tRC29N-STLAL                                                                                                                         \t{0}", o.BOMAlt); // BOM Alternative
                                    fs.WriteLine("                                        \t0000\t \tRC29N-DATUV                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From

                                    // Init srceen for component
                                    fs.WriteLine("SAPLCSDI                                \t0140\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                    fs.WriteLine("                                        \t0000\t \tRC29P-IDNRK(01)                                                                                                                     \t{0}", a.ComponentMaterial); // Component Material
                                    fs.WriteLine("                                        \t0000\t \tRC29P-POSTP(01)                                                                                                                     \tL"); // fix

                                    // BOM Item
                                    fs.WriteLine("SAPLCSDI                                \t0130\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                    fs.WriteLine("                                        \t0000\t \tRC29P-POSNR                                                                                                                         \t{0}", a.BOMItem);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-IDNRK                                                                                                                         \t{0}", a.ComponentMaterial);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-MENGE                                                                                                                         \t{0}", a.ComponentQuantity);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-MEINS                                                                                                                         \t{0}", a.ComponentUnit);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-FMENG                                                                                                                         \t{0}", a.FixedQty);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-AVOAU                                                                                                                         \t{0}", a.OperationScrap);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-AUSCH                                                                                                                         \t{0}", a.ComponentScrap);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-NETAU                                                                                                                         \t{0}", a.NetScrap);
                                    fs.WriteLine("SAPLCSDI                                \t0131\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                    fs.WriteLine("                                        \t0000\t \tRC29P-SANKA                                                                                                                         \tX");
                                    fs.WriteLine("SAPLCSDI                                \t0140\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCBU");
                                }
                                else
                                {
                                    // BOM Header
                                    fs.WriteLine("                                        \t0000\tT\tCS02                                                                                                                                \t");
                                    fs.WriteLine("SAPLCSDI                                \t0100\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                    fs.WriteLine("                                        \t0000\t \tRC29N-MATNR                                                                                                                         \t{0}", o.MaterialCode); // Header Material
                                    fs.WriteLine("                                        \t0000\t \tRC29N-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                    fs.WriteLine("                                        \t0000\t \tRC29N-STLAN                                                                                                                         \t{0}", o.BOMUsage); // BOMUsage
                                    fs.WriteLine("                                        \t0000\t \tRC29N-STLAL                                                                                                                         \t{0}", o.BOMAlt); // BOM Alternative
                                    fs.WriteLine("                                        \t0000\t \tRC29N-DATUV                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                    fs.WriteLine("SAPLCSDI                                \t0150\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCNP");

                                    // Init srceen for component
                                    fs.WriteLine("SAPLCSDI                                \t0140\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                    fs.WriteLine("                                        \t0000\t \tRC29P-IDNRK(02)                                                                                                                     \t{0}", a.ComponentMaterial); // Component Material
                                    fs.WriteLine("                                        \t0000\t \tRC29P-POSTP(02)                                                                                                                     \tL"); // fix

                                    // BOM Item
                                    fs.WriteLine("SAPLCSDI                                \t0130\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                    fs.WriteLine("                                        \t0000\t \tRC29P-POSNR                                                                                                                         \t{0}", a.BOMItem);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-IDNRK                                                                                                                         \t{0}", a.ComponentMaterial);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-MENGE                                                                                                                         \t{0}", a.ComponentQuantity);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-MEINS                                                                                                                         \t{0}", a.ComponentUnit);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-FMENG                                                                                                                         \t{0}", a.FixedQty);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-AVOAU                                                                                                                         \t{0}", a.OperationScrap);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-AUSCH                                                                                                                         \t{0}", a.ComponentScrap);
                                    fs.WriteLine("                                        \t0000\t \tRC29P-NETAU                                                                                                                         \t{0}", a.NetScrap);
                                    fs.WriteLine("SAPLCSDI                                \t0131\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                    fs.WriteLine("                                        \t0000\t \tRC29P-SANKA                                                                                                                         \tX");
                                    fs.WriteLine("SAPLCSDI                                \t0140\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=FCBU");
                                }
                                count++;
                            }
                        }

                        // 3.1
                        // generate routing change header
                        foreach (var o in routingHeaderList)
                        {
                            // BOM Header
                            fs.WriteLine("                                        \t0000\tT\tCA02                                                                                                                                \t");
                            fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=ALD1");
                            fs.WriteLine("                                        \t0000\t \tRC27M-MATNR                                                                                                                         \t"); // Header Material
                            fs.WriteLine("                                        \t0000\t \tRC27M-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                            fs.WriteLine("                                        \t0000\t \tRC271-PLNNR                                                                                                                         \t{0}", o.RoutingGroup); // RoutingGroup
                            fs.WriteLine("                                        \t0000\t \tRC271-STTAG                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("                                        \t0000\t \tRC271-PLNAL                                                                                                                         \t{0}", o.GroupCounter); // GroupCounter
                            fs.WriteLine("SAPLCPDA                                \t1200\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BU");
                            fs.WriteLine("                                        \t0000\t \tPLKOD-KTEXT                                                                                                                         \t{0}", o.BOMHeaderText); // BOM Text
                            fs.WriteLine("                                        \t0000\t \tPLKOD-LOSVN                                                                                                                         \t{0}", o.LotSizeFrom); // LotSizeFrom
                            fs.WriteLine("                                        \t0000\t \tPLKOD-LOSBS                                                                                                                         \t{0}", o.LotSizeTo); // LotSizeTo
                            fs.WriteLine("                                        \t0000\t \tPLKOD-PLNME                                                                                                                         \t{0}", o.BaseUnit); // BOM Status
                        }

                        var routingGroupList = routingHeaderList.GroupBy(u => u.RoutingGroup).Select(grp => grp.ToList()).ToList();
                        // 4
                        // generate assign material to routing
                        foreach (var k in routingGroupList)
                        {
                            if (k.Count == 1)
                            {
                                foreach (var o in k)
                                {
                                    fs.WriteLine("                                        \t0000\tT\tCA02                                                                                                                                \t");
                                    fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=ALD1");
                                    fs.WriteLine("                                        \t0000\t \tRC27M-MATNR                                                                                                                         \t");
                                    fs.WriteLine("                                        \t0000\t \tRC27M-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                    fs.WriteLine("                                        \t0000\t \tRC271-PLNNR                                                                                                                         \t{0}", o.RoutingGroup); // Routing Group
                                    fs.WriteLine("                                        \t0000\t \tRC271-STTAG                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                    fs.WriteLine("SAPLCPDA                                \t1200\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=MTUE");
                                    fs.WriteLine("SAPLCZDI                                \t1010\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=P+");
                                    fs.WriteLine("SAPLCZDI                                \t1010\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BACK");
                                    fs.WriteLine("                                        \t0000\t \tMAPL-PLNAL(01)                                                                                                                      \t{0}", o.GroupCounter);
                                    fs.WriteLine("                                        \t0000\t \tMAPL-MATNR(01)                                                                                                                      \t{0}", o.MaterialCode);
                                    fs.WriteLine("                                        \t0000\t \tMAPL-WERKS(01)                                                                                                                      \t{0}", o.Plant);
                                    fs.WriteLine("SAPLCPDA                                \t1200\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BU");
                                }
                            }
                            else
                            {
                                BOMHeaderModel lastAssign = k.Last();
                                int countAssign = 1;
                                foreach (var o in k)
                                {
                                    fs.WriteLine("                                        \t0000\tT\tCA02                                                                                                                                \t");
                                    fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=ALD1");
                                    fs.WriteLine("                                        \t0000\t \tRC27M-MATNR                                                                                                                         \t");
                                    fs.WriteLine("                                        \t0000\t \tRC27M-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                    fs.WriteLine("                                        \t0000\t \tRC271-PLNNR                                                                                                                         \t{0}", o.RoutingGroup); // Routing Group
                                    fs.WriteLine("                                        \t0000\t \tRC271-STTAG                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                    fs.WriteLine("SAPLCPDA                                \t1200\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=MTUE");

                                    if (o.Equals(lastAssign))
                                    {
                                        fs.WriteLine("SAPLCZDI                                \t1010\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                        fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", countAssign);
                                        fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BACK");
                                        fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", countAssign);
                                        fs.WriteLine("                                        \t0000\t \tMAPL-PLNAL(02)                                                                                                                      \t{0}", o.GroupCounter);
                                        fs.WriteLine("                                        \t0000\t \tMAPL-MATNR(02)                                                                                                                      \t{0}", o.MaterialCode);
                                        fs.WriteLine("                                        \t0000\t \tMAPL-WERKS(02)                                                                                                                      \t{0}", o.Plant);
                                        fs.WriteLine("SAPLCZDI                                \t1010\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BACK");
                                        fs.WriteLine("SAPLCPDA                                \t1200\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BU");

                                    }
                                    else
                                    {
                                        if (countAssign == 1)
                                        {
                                            fs.WriteLine("SAPLCZDI                                \t1010\tX\t                                                                                                                                    \t");
                                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=P+");
                                            fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", countAssign);
                                            fs.WriteLine("SAPLCZDI                                \t1010\tX\t                                                                                                                                    \t");
                                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=P+");
                                            fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", countAssign);
                                            fs.WriteLine("                                        \t0000\t \tMAPL-PLNAL(02)                                                                                                                      \t{0}", o.GroupCounter);
                                            fs.WriteLine("                                        \t0000\t \tMAPL-MATNR(02)                                                                                                                      \t{0}", o.MaterialCode);
                                            fs.WriteLine("                                        \t0000\t \tMAPL-WERKS(02)                                                                                                                      \t{0}", o.Plant);
                                        }
                                        else
                                        {
                                            fs.WriteLine("SAPLCZDI                                \t1010\tX\t                                                                                                                                    \t");
                                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                            fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", countAssign);
                                            fs.WriteLine("SAPLCZDI                                \t1010\tX\t                                                                                                                                    \t");
                                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BACK");
                                            fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", countAssign);
                                            fs.WriteLine("                                        \t0000\t \tMAPL-PLNAL(02)                                                                                                                      \t{0}", o.GroupCounter);
                                            fs.WriteLine("                                        \t0000\t \tMAPL-MATNR(02)                                                                                                                      \t{0}", o.MaterialCode);
                                            fs.WriteLine("                                        \t0000\t \tMAPL-WERKS(02)                                                                                                                      \t{0}", o.Plant);
                                        }
                                    }
                                    countAssign++;
                                }
                            }
                        }

                        // check routing null
                        if ((routingHeaderList != null && routingItemList.Count > 0))
                        {
                            // 5
                            // generate change detail op routing
                            foreach (var o in routingHeaderList)
                            {
                                List<BOMItemModel> resultList = routingItemList.Where(t => t.WorkCenter == o.WorkCenter).ToList(); // Group by Routing Group
                                if (resultList.Count > 0)
                                {
                                    var operationList = resultList.GroupBy(u => u.OperationNo).Select(grp => grp.ToList()).ToList(); // Group by Operation No
                                    foreach (var eList in operationList)
                                    {
                                        fs.WriteLine("                                        \t0000\tT\tCA02                                                                                                                                \t");
                                        fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=XALU");
                                        fs.WriteLine("                                        \t0000\t \tRC27M-MATNR                                                                                                                         \t{0}", o.MaterialCode); // Header Material
                                        fs.WriteLine("                                        \t0000\t \tRC27M-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                        fs.WriteLine("                                        \t0000\t \tRC271-PLNNR                                                                                                                         \t{0}", o.RoutingGroup); // Routing Group
                                        fs.WriteLine("                                        \t0000\t \tRC271-STTAG                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                        fs.WriteLine("                                        \t0000\t \tRC271-PLNAL                                                                                                                         \t{0}", o.GroupCounter); // Group Counter

                                        // Search operation command
                                        fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=OSEA");
                                        fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t1");
                                        // Operation detail
                                        fs.WriteLine("SAPLCP02                                \t1010\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=ENT1");
                                        fs.WriteLine("                                        \t0000\t \tRC27H-VORNR                                                                                                                         \t{0}", eList[0].OperationNo);
                                        // Select operation command
                                        fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VOD1");
                                        fs.WriteLine("                                        \t0000\t \tRC27X-FLG_SEL(01)                                                                                                                   \tX");

                                        fs.WriteLine("SAPLCPDO                                \t1200\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BU");
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-VORNR                                                                                                                         \t{0}", eList[0].OperationNo); // first item
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-ARBPL                                                                                                                         \t{0}", o.WorkCenter);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-WERKS                                                                                                                         \t{0}", o.Plant);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-KTSCH                                                                                                                         \t{0}", o.StandardTextKey); // optional
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-LTXA1                                                                                                                         \t{0}", o.BOMHeaderText);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-BMSCH                                                                                                                         \t{0}", o.BaseQuantity);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-MEINH                                                                                                                         \t{0}", o.BaseUnit);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-UMREZ                                                                                                                         \t{0}", ConversionOfUOMNumerator);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-UMREN                                                                                                                         \t{0}", ConversionOfUOMDenominator);

                                        foreach (var a in eList)
                                        {
                                            // set item data
                                            if (a.ActivityNo == 1)
                                            {
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-LAR01                                                                                                                         \t{0}", a.Activity);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGW01                                                                                                                         \t{0}", a.StandardValueKey);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGE01                                                                                                                         \t{0}", a.StandardValueKeyOUM);
                                            }
                                            if (a.ActivityNo == 2)
                                            {
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-LAR02                                                                                                                         \t{0}", a.Activity);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGW02                                                                                                                         \t{0}", a.StandardValueKey);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGE02                                                                                                                         \t{0}", a.StandardValueKeyOUM);
                                            }
                                            if (a.ActivityNo == 3)
                                            {
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-LAR03                                                                                                                         \t{0}", a.Activity);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGW03                                                                                                                         \t{0}", a.StandardValueKey);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGE03                                                                                                                         \t{0}", a.StandardValueKeyOUM);
                                            }
                                            if (a.ActivityNo == 4)
                                            {
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-LAR04                                                                                                                         \t{0}", a.Activity);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGW04                                                                                                                         \t{0}", a.StandardValueKey);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGE04                                                                                                                         \t{0}", a.StandardValueKeyOUM);
                                            }
                                            if (a.ActivityNo == 5)
                                            {
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-LAR05                                                                                                                         \t{0}", a.Activity);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGW05                                                                                                                         \t{0}", a.StandardValueKey);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGE05                                                                                                                         \t{0}", a.StandardValueKeyOUM);
                                            }
                                            if (a.ActivityNo == 6)
                                            {
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-LAR06                                                                                                                         \t{0}", a.Activity);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGW06                                                                                                                         \t{0}", a.StandardValueKey);
                                                fs.WriteLine("                                        \t0000\t \tPLPOD-VGE06                                                                                                                         \t{0}", a.StandardValueKeyOUM);
                                            }
                                        }
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-AUFAK                                                                                                                         \t{0}", eList[0].OperationScrap);

                                    }

                                }
                            }
                        }

                        // 6
                        // generate delete production version
                        //var bomRoutingList = bomHeaderList.Join
                        //    (routingHeaderList,
                        //    bom => bom.RoutingGroup,
                        //    routing => routing.RoutingGroup,
                        //    (bom, routing) => routing).ToList();

                        foreach (var o in routingHeaderList)
                        {
                            fs.WriteLine("                                        \t0000\tT\tC223                                                                                                                                \t");
                            fs.WriteLine("SAPLCMFV                                \t1000\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=ESEL");
                            fs.WriteLine("                                        \t0000\t \tMKAL-WERKS                                                                                                                          \t{0}", o.Plant); // Plant
                            fs.WriteLine("SAPLCMFV                                \t1001\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=NONE");
                            fs.WriteLine("                                        \t0000\t \tRANG_MAT-LOW                                                                                                                        \t{0}", o.MaterialCode); // MaterialCode
                            fs.WriteLine("                                        \t0000\t \tRANG_VER-LOW                                                                                                                        \t{0}", o.ProductionVersion); // Production Version
                            fs.WriteLine("SAPLCMFV                                \t1001\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=CRET");
                            fs.WriteLine("SAPLCMFV                                \t1000\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=DELE");
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-MARK(01)                                                                                                                \tX"); // Fix
                            fs.WriteLine("SAPLSPO1                                \t0100\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=YES");
                            fs.WriteLine("SAPLCMFV                                \t1000\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=SAVE");
                        }

                        // 7
                        // generate production version
                        foreach (var o in routingHeaderList)
                        {
                            fs.WriteLine("                                        \t0000\tT\tC223                                                                                                                                \t");
                            fs.WriteLine("SAPLCMFV                                \t1000\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=ENTE");
                            fs.WriteLine("                                        \t0000\t \tMKAL-WERKS                                                                                                                          \t{0}", o.Plant); // Plant
                            fs.WriteLine("SAPLCMFV                                \t1000\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=CREA");
                            fs.WriteLine("SAPLCMFV                                \t2000\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=ENTE");
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-MATNR                                                                                                                   \t{0}", o.MaterialCode); // MaterialCode
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-VERID                                                                                                                   \t{0}", o.ProductionVersion); // Production Version
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-TEXT1                                                                                                                   \t{0}", o.BOMHeaderText); // Production Version Description
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-BSTMI                                                                                                                   \t{0}", o.LotSizeFrom); // LotSizeFrom
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-BSTMA                                                                                                                   \t{0}", o.LotSizeTo); // LotSizeTo
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-ADATU                                                                                                                   \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-BDATU                                                                                                                   \t{0}", ValidDateTo); // Valid Date To
                            fs.WriteLine("SAPLCMFV                                \t2000\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \tPRFG");
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-PLNTY                                                                                                                   \t{0}", TaskListType); // Fix
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-PLNNR                                                                                                                   \t{0}", o.RoutingGroup); // Group
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-ALNAL                                                                                                                   \t{0}", o.GroupCounter); // GroupCounter
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-STLAL                                                                                                                   \t{0}", o.BOMAlt); // BOM Alternative
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-STLAN                                                                                                                   \t{0}", o.BOMUsage); // BOM Usage
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-MDV01                                                                                                                   \t{0}", o.ProductionLine); // Production Line
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-ELPRO                                                                                                                   \t{0}", o.StorageLocation); // Issue Storage Location
                            fs.WriteLine("                                        \t0000\t \tMKAL_EXPAND-ALORT                                                                                                                   \t{0}", ReceivingStorageLocation); // Receiving Storage Location
                            fs.WriteLine("SAPMSSY0                                \t0120\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BACK");
                            fs.WriteLine("SAPLCMFV                                \t2000\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=CLOS");
                            fs.WriteLine("SAPLCMFV                                \t1000\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=SAVE");
                        }
                    } // StreamWriter
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMWorkCenterToTextFile(List<WorkCenterModel> workCenterList, string fileName, string extension, string user)
        {
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);
            string Usage = "001"; // Task list usage
            string Indicator = "X"; // Indicator: Control Profile is Referenced
            string UnitOfMeasurement = "TO"; // Unit of Measurement of Standard Value
            string CapacityCategory = "001"; // Capacity Category

            try
            {
                if (workCenterList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        foreach (var o in workCenterList)
                        {
                            fs.WriteLine("\t0000\tM\tZCONV_CR01\t{0}\t{1}\t{2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("\t0000\tT\tCR01\t");
                            fs.WriteLine("SAPLCRA0                                	0101	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	NEXT");
                            fs.WriteLine("                                        	0000	 	RC68A-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC68A-ARBPL                                                                                                                         	{0}", o.WorkCenter); // Work Center
                            fs.WriteLine("                                        	0000	 	RC68A-VERWE                                                                                                                         	{0}", o.WorkCenterCategory); // Work Center Category
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	P1000-STEXT                                                                                                                         	{0}", o.WorkCenterName); // Work Center Name
                            fs.WriteLine("                                        	0000		P3000-VERAN                                                                                                                          	{0}", o.PersonResponsible); // Person responsible
                            fs.WriteLine("                                        	0000	 	P3000-PLANV                                                                                                                         	{0}", Usage); // task list usage
                            fs.WriteLine("                                        	0000	 	P3000-VGWTS                                                                                                                         	{0}", o.StandardValueKey); // Standard value key
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=VORA");
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	P3003-STEUS                                                                                                                         	{0}", o.ControlKey); // Control key
                            fs.WriteLine("                                        	0000	 	P3003-STEUS                                                                                                                         	{0}", Indicator); // Indicator: Control Profile is Referenced
                            fs.WriteLine("                                        	0000	 	RC68A-VGEXX(01)                                                                                                                     	{0}", UnitOfMeasurement); // Unit of Measurement of Standard Value
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=KAUE");
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=NPOS");
                            fs.WriteLine("                                        	0000	 	RC68A-KAPART(01)                                                                                                                    	{0}", o.CapacityCategory); // Capacity category (Optional)
                            fs.WriteLine("SAPLCRK0                                	0101	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	KAKO-PLANR                                                                                                                          	{0}", o.CapacityResponsible); // Capacity Responsible Planner Group (Optional)
                            fs.WriteLine("                                        	0000	 	RC68K-BEGZT                                                                                                                         	{0}", o.StartTime.ToString("h:mm:ss")); // Start time (Optional)
                            fs.WriteLine("                                        	0000	 	RC68K-ENDZT                                                                                                                         	{0}", o.EndTime.ToString("h:mm:ss")); // End time (Optional)
                            fs.WriteLine("                                        	0000	 	KAKO-NGRAD                                                                                                                          	{0}", o.CapacityUtilization); // Capacity Utilization Ratio in Percent (Optional)
                            fs.WriteLine("                                        	0000	 	RC68K-PAUSE                                                                                                                         	{0}", o.CumulativeLength); // Cumulative length of breaks per shift (Optional)
                            fs.WriteLine("                                        	0000	 	KAKO-AZNOR                                                                                                                          	{0}", o.NoIndCapacities); // No. Ind. Capacities (Optional)
                            fs.WriteLine("                                        	0000	 	KAKO-KAPEH                                                                                                                          	{0}", o.CapacityBaseUnit); // Capacity Base Unit of Measure (Optional)
                            fs.WriteLine("                                        	0000	 	KAKO-MEINS                                                                                                                          	{0}", o.CapacityUnit); // Capacity Unit of Measure (Optional)
                            fs.WriteLine("                                        	0000	 	KAKO-UEBERLAST                                                                                                                      	{0}", o.Overload); // Overload (Optional)
                            fs.WriteLine("                                        	0000	 	RC68K-KAPLPL                                                                                                                        	{0}", o.LongtermPlanning); // Longterm planning (Optional)
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=TERM");
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	RC68A-KAPART                                                                                                                    	    {0}", CapacityCategory); // Capacity category (Optional)
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=VK11");
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	P1001-BEGDA                                                                                                                          	{0}", o.StartDate.ToString("dd.MM.yyyy", usCulture)); // Start Date
                            fs.WriteLine("                                        	0000	 	P1001-ENDDA                                                                                                                          	{0}", o.EndDate.ToString("dd.MM.yyyy", usCulture)); // End Date
                            fs.WriteLine("                                        	0000	 	CRKEYK-KOSTL                                                                                                                          	{0}", o.CostCenter); // Cost center
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=VK11");
                            fs.WriteLine("                                        	0000	 	P1001-BEGDA                                                                                                                          	{0}", o.StartDate.ToString("dd.MM.yyyy", usCulture)); // Start Date
                            fs.WriteLine("                                        	0000	 	P1001-ENDDA                                                                                                                          	{0}", o.EndDate.ToString("dd.MM.yyyy", usCulture)); // End Date
                            fs.WriteLine("                                        	0000	 	CRKEYK-KOSTL                                                                                                                          	{0}", o.CostCenter); // Cost center
                            fs.WriteLine("                                        	0000	 	RC68A-ACTXKT(01)                                                                                                                        {0}", o.ActivityDescNo1); // Activity description no.1 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-ACTXKT(02)                                                                                                                        {0}", o.ActivityDescNo2); // Activity description no.2 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-ACTXKT(03)                                                                                                                        {0}", o.ActivityDescNo3); // Activity description no.3 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-ACTXKT(04)                                                                                                                        {0}", o.ActivityDescNo4); // Activity description no.4 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-ACTXKT(05)                                                                                                                        {0}", o.ActivityDescNo5); // Activity description no.5 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-ACTXKT(06)                                                                                                                        {0}", o.ActivityDescNo6); // Activity description no.6 (Optional)
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                        ");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=VK11");
                            fs.WriteLine("                                        	0000		RC68A-LARXX(01)                                                                                                                         {0}", o.ActivityTypeNo1); // Activity type no.1 (Optional)
                            fs.WriteLine("                                        	0000		RC68A-LARXX(02)                                                                                                                         {0}", o.ActivityTypeNo2); // Activity type no.2 (Optional)
                            fs.WriteLine("                                        	0000		RC68A-LARXX(03)                                                                                                                         {0}", o.ActivityTypeNo3); // Activity type no.3 (Optional)
                            fs.WriteLine("                                        	0000		RC68A-LARXX(04)                                                                                                                         {0}", o.ActivityTypeNo4); // Activity type no.4 (Optional)
                            fs.WriteLine("                                        	0000		RC68A-LARXX(05)                                                                                                                         {0}", o.ActivityTypeNo5); // Activity type no.5 (Optional)
                            fs.WriteLine("                                        	0000		RC68A-LARXX(06)                                                                                                                         {0}", o.ActivityTypeNo6); // Activity type no.6 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-LEINH(01)                                                                                                                     	{0}", o.UnitOfActNo1); // Unit of activity no.1 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-LEINH(02)                                                                                                                     	{0}", o.UnitOfActNo2); // Unit of activity no.2 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-LEINH(03)                                                                                                                     	{0}", o.UnitOfActNo3); // Unit of activity no.3 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-LEINH(04)                                                                                                                     	{0}", o.UnitOfActNo4); // Unit of activity no.4 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-LEINH(05)                                                                                                                     	{0}", o.UnitOfActNo5); // Unit of activity no.5 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-LEINH(06)                                                                                                                     	{0}", o.UnitOfActNo6); // Unit of activity no.6 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FORXX(01)                                                                                                                     	{0}", o.FomularKeyNo1); // Fomular key no.1 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FORXX(02)                                                                                                                     	{0}", o.FomularKeyNo2); // Fomular key no.2 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FORXX(03)                                                                                                                     	{0}", o.FomularKeyNo3); // Fomular key no.3 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FORXX(04)                                                                                                                     	{0}", o.FomularKeyNo4); // Fomular key no.4 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FORXX(05)                                                                                                                     	{0}", o.FomularKeyNo5); // Fomular key no.5 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FORXX(06)                                                                                                                     	{0}", o.FomularKeyNo6); // Fomular key no.6 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FLG_REF(01)                                                                                                                     	{0}", o.RefIndicatorNo1); // Refference indicator no.1 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FLG_REF(02)                                                                                                                     	{0}", o.RefIndicatorNo2); // Refference indicator no.2 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FLG_REF(03)                                                                                                                     	{0}", o.RefIndicatorNo3); // Refference indicator no.3 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FLG_REF(04)                                                                                                                     	{0}", o.RefIndicatorNo4); // Refference indicator no.4 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FLG_REF(05)                                                                                                                     	{0}", o.RefIndicatorNo5); // Refference indicator no.5 (Optional)
                            fs.WriteLine("                                        	0000	 	RC68A-FLG_REF(06)                                                                                                                     	{0}", o.RefIndicatorNo6); // Refference indicator no.6 (Optional)
                            fs.WriteLine("SAPLCRA0                                  4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                     	        UPD");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMProductionVersionToTextFile(List<ProductionVersionModel> productionVersionList, string fileName, string extension, string user, DateTime validDate)
        {
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);
            string ValidDateTo = "31.12.9999";
            string TaskListType = "N";

            try
            {
                if (productionVersionList != null && productionVersionList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        foreach (var o in productionVersionList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_C223	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	C223                                                                                                                                	");
                            fs.WriteLine("SAPLCMFV                                	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ENTE");
                            fs.WriteLine("                                        	0000	 	MKAL-WERKS                                                                                                                          	{0}", o.Plant); // Plant
                            fs.WriteLine("SAPLCMFV                                	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=CREA");
                            fs.WriteLine("SAPLCMFV                                	2000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ENTE");
                            fs.WriteLine("                                        	0000		MKAL_EXPAND-MATNR                                                                                                                       {0}", o.MaterialCode); // MaterialCode
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-VERID                                                                                                                       {0}", o.ProductionVersion); // Production Version
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-TEXT1                                                                                                                       {0}", o.ProductionVersionDescription); // Production Version Description
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-ADATU                                                                                                                       {0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-BDATU                                                                                                                       {0}", ValidDateTo); // Valid Date To
                            fs.WriteLine("SAPLCMFV                                	2000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	PRFG");
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-PLNTY                                                                                                                       {0}", TaskListType); // Fix
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-PLNNR                                                                                                                       {0}", o.Group); // Group
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-ALNAL                                                                                                                       {0}", o.GroupCounter); // GroupCounter
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-STLAL                                                                                                                       {0}", o.BOMAlt); // BOM Alternative
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-STLAN                                                                                                                       {0}", o.BOMUsage); // BOM Usage
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-MDV01                                                                                                                       {0}", o.ProductionLine); // Production Line
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-ELPRO                                                                                                                       {0}", o.IssueStorageLocation); // Issue Storage Location
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-ALORT                                                                                                                       {0}", o.ReceivingStorageLocation); // Receiving Storage Location
                            fs.WriteLine("SAPMSSY0                                	0120	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BACK");
                            fs.WriteLine("SAPLCMFV                                	2000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=CLOS");
                            fs.WriteLine("SAPLCMFV                                	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=SAVE");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMRoutingWithoutMaterialToTextFile(List<RoutingWithoutMaterialHeaderModel> routingWithoutMaterialHeaderList, List<RoutingWithoutMaterialItemModel> routingWithoutMaterialItemList, string fileName, string extension, string user)
        {
            string ValidDateFrom = DateTime.Now.ToString("dd.MM.yyyy", usCulture);
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);

            try
            {
                if (routingWithoutMaterialHeaderList != null && routingWithoutMaterialItemList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        foreach (var o in routingWithoutMaterialHeaderList)
                        {
                            int count = 1;
                            int routingItem = 10;
                            int act = 0;

                            fs.WriteLine("	0000	M	ZCONV_CA01	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	CA01                                                                                                                                	");
                            fs.WriteLine("SAPLCPDI                                	1010	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=XALU");
                            fs.WriteLine("                                        	0000	 	RC27M-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        	0000	 	RC27M-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC271-PLNNR                                                                                                                         	{0}", o.RoutingGroup); // Routing Group
                            fs.WriteLine("                                        	0000	 	RC271-STTAG                                                                                                                         	{0}", ValidDateFrom); // Valid Date From
                            fs.WriteLine("                                        	0000	 	PLKOD-PLNAL                                                                                                                         	{0}", o.GroupCounter); // Group Counter
                            fs.WriteLine("SAPLCPDA                                	1200	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=VOUE");
                            fs.WriteLine("                                        	0000	 	PLKOD-PLNAL                                                                                                                         	{0}", o.GroupCounter); // Group Counter
                            fs.WriteLine("                                        	0000	 	PLKOD-WERKS                                                                                                                         	{0}", o.Plant); // Plant 
                            fs.WriteLine("                                        	0000	 	PLKOD-VERWE                                                                                                                         	{0}", o.Usage); // BOM Usage
                            fs.WriteLine("                                        	0000	 	PLKOD-STATU                                                                                                                         	{0}", o.OverAllStatus); // Over all status
                            fs.WriteLine("                                        	0000	 	PLKOD-LOSBS                                                                                                                         	{0}", o.LotSizeTo); // Lot size to
                            fs.WriteLine("                                        	0000	 	PLKOD-PLNME                                                                                                                         	{0}", o.BaseUnit); // Base unit
                            foreach (var a in routingWithoutMaterialItemList)
                            {
                                if (o.MaterialCode == a.MaterialCode)
                                {
                                    List<RoutingWithoutMaterialItemModel> resultList = routingWithoutMaterialItemList.Where(t => t.MaterialCode == o.MaterialCode).ToList();
                                    RoutingWithoutMaterialItemModel last = resultList.Last();

                                    // Last Item
                                    if (a.Equals(last))
                                    {
                                        // Init srceen for component
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	1"); // Fix
                                        fs.WriteLine("                                        	0000	 	PLPOD-VORNR(0{0})                                                                                                                     	{1}", count, string.Format("00{0}", routingItem)); // Operation no.
                                        fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(0{0})                                                                                                                     	{1}", count, a.WorkCenter); // WorkCenter
                                        fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(0{0})                                                                                                                       {1}", count, a.OperationDecription); // Operation Decription
                                        // Operation command
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act); // Fix
                                        // Operation detail
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=PICK");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act + 1); // Fix
                                        fs.WriteLine("SAPLCPDO                                	1200	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BACK");
                                        fs.WriteLine("                                        	0000	 	PLPOD-VORNR                                                                                                                         	{0}", string.Format("00{0}", routingItem));
                                        fs.WriteLine("                                        	0000	 	PLPOD-ARBPL                                                                                                                          	{0}", a.WorkCenter);
                                        fs.WriteLine("                                        	0000	 	PLPOD-WERKS                                                                                                                         	{0}", a.Plant);
                                        fs.WriteLine("                                       	0000	 	PLPOD-KTSCH                                                                                                                         	{0}", a.StandardTextKey); // optional
                                        fs.WriteLine("                                        	0000	 	PLPOD-LTXA1                                                                                                                         	{0}", a.OperationDecription);
                                        fs.WriteLine("                                        	0000	 	PLPOD-BMSCH                                                                                                                         	{0}", a.OperationBaseQuantity);
                                        fs.WriteLine("                                        	0000	 	PLPOD-MEINH                                                                                                                          	{0}", a.OperationOUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-UMREZ                                                                                                                         	{0}", a.ConversionOfOUMN);
                                        fs.WriteLine("                                        	0000	 	PLPOD-UMREN                                                                                                                         	{0}", a.ConversionOfOUMD);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR01                                                                                                                         	{0}", a.ActivityType1);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW01                                                                                                                         	{0}", a.StandardValue1);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE01                                                                                                                         	{0}", a.StandardValue1OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR02                                                                                                                          	{0}", a.ActivityType2);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW02                                                                                                                         	{0}", a.StandardValue2);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE02                                                                                                                         	{0}", a.StandardValue2OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR03                                                                                                                          	{0}", a.ActivityType3);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW03                                                                                                                         	{0}", a.StandardValue3);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE03                                                                                                                         	{0}", a.StandardValue3OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR04                                                                                                                          	{0}", a.ActivityType4);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW04                                                                                                                         	{0}", a.StandardValue4);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE04                                                                                                                         	{0}", a.StandardValue4OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR05                                                                                                                          	{0}", a.ActivityType5);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW05                                                                                                                         	{0}", a.StandardValue5);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE05                                                                                                                         	{0}", a.StandardValue5OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR06                                                                                                                          	{0}", a.ActivityType6);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW06                                                                                                                         	{0}", a.StandardValue6);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE06                                                                                                                         	{0}", a.StandardValue6OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-AUFAK                                                                                                                         	{0}", a.Scarp);
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BU");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                         3");

                                    }
                                    else
                                    {
                                        // First Item
                                        if (count == 1)
                                        {
                                            // Init srceen for component
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=PICK");
                                            fs.WriteLine("                                        	0000	 	PLPOD-VORNR(0{0})                                                                                                                     	{1}", count, string.Format("00{0}", routingItem)); // Operation no.
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(0{0})                                                                                                                     	{1}", count, a.WorkCenter); // WorkCenter
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(0{0})                                                                                                                       {1}", count, a.OperationDecription); // Operation Decription
                                            // Operation detail
                                            fs.WriteLine("SAPLCPDI                                	1200	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BACK");
                                            fs.WriteLine("                                        	0000	 	PLPOD-VORNR                                                                                                                         	{0}", string.Format("00{0}", routingItem));
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL                                                                                                                          	{0}", a.WorkCenter);
                                            fs.WriteLine("                                        	0000	 	PLPOD-WERKS                                                                                                                         	{0}", a.Plant);
                                            fs.WriteLine("                                       	0000	 	PLPOD-KTSCH                                                                                                                         	{0}", a.StandardTextKey); // optional
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1                                                                                                                         	{0}", a.OperationDecription);
                                            fs.WriteLine("                                        	0000	 	PLPOD-BMSCH                                                                                                                         	{0}", a.OperationBaseQuantity);
                                            fs.WriteLine("                                        	0000	 	PLPOD-MEINH                                                                                                                          	{0}", a.OperationOUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-UMREZ                                                                                                                         	{0}", a.ConversionOfOUMN);
                                            fs.WriteLine("                                        	0000	 	PLPOD-UMREN                                                                                                                         	{0}", a.ConversionOfOUMD);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR01                                                                                                                         	{0}", a.ActivityType1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW01                                                                                                                         	{0}", a.StandardValue1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE01                                                                                                                         	{0}", a.StandardValue1OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR02                                                                                                                          	{0}", a.ActivityType2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW02                                                                                                                         	{0}", a.StandardValue2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE02                                                                                                                         	{0}", a.StandardValue2OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR03                                                                                                                          	{0}", a.ActivityType3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW03                                                                                                                         	{0}", a.StandardValue3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE03                                                                                                                         	{0}", a.StandardValue3OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR04                                                                                                                          	{0}", a.ActivityType4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW04                                                                                                                         	{0}", a.StandardValue4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE04                                                                                                                         	{0}", a.StandardValue4OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR05                                                                                                                          	{0}", a.ActivityType5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW05                                                                                                                         	{0}", a.StandardValue5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE05                                                                                                                         	{0}", a.StandardValue5OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR06                                                                                                                          	{0}", a.ActivityType6);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW06                                                                                                                         	{0}", a.StandardValue6);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE06                                                                                                                         	{0}", a.StandardValue6OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-AUFAK                                                                                                                         	{0}", a.Scarp);
                                        }
                                        // Next Item
                                        else
                                        {
                                            // Init srceen for component
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	1"); // Fix
                                            fs.WriteLine("                                        	0000	 	PLPOD-VORNR(0{0})                                                                                                                     	{1}", count, string.Format("00{0}", routingItem)); // Operation no.
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(0{0})                                                                                                                     	{1}", count, a.WorkCenter); // WorkCenter
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(0{0})                                                                                                                       {1}", count, a.OperationDecription); // Operation Decription
                                            // Operation command
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act); // Fix
                                            // Operation detail
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=PICK");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act + 1); // Fix
                                            fs.WriteLine("SAPLCPDO                                	1200	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BACK");
                                            fs.WriteLine("                                        	0000	 	PLPOD-VORNR                                                                                                                         	{0}", string.Format("00{0}", routingItem));
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL                                                                                                                          	{0}", a.WorkCenter);
                                            fs.WriteLine("                                        	0000	 	PLPOD-WERKS                                                                                                                         	{0}", a.Plant);
                                            fs.WriteLine("                                       	0000	 	PLPOD-KTSCH                                                                                                                         	{0}", a.StandardTextKey); // optional
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1                                                                                                                         	{0}", a.OperationDecription);
                                            fs.WriteLine("                                        	0000	 	PLPOD-BMSCH                                                                                                                         	{0}", a.OperationBaseQuantity);
                                            fs.WriteLine("                                        	0000	 	PLPOD-MEINH                                                                                                                          	{0}", a.OperationOUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-UMREZ                                                                                                                         	{0}", a.ConversionOfOUMN);
                                            fs.WriteLine("                                        	0000	 	PLPOD-UMREN                                                                                                                         	{0}", a.ConversionOfOUMD);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR01                                                                                                                         	{0}", a.ActivityType1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW01                                                                                                                         	{0}", a.StandardValue1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE01                                                                                                                         	{0}", a.StandardValue1OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR02                                                                                                                          	{0}", a.ActivityType2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW02                                                                                                                         	{0}", a.StandardValue2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE02                                                                                                                         	{0}", a.StandardValue2OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR03                                                                                                                          	{0}", a.ActivityType3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW03                                                                                                                         	{0}", a.StandardValue3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE03                                                                                                                         	{0}", a.StandardValue3OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR04                                                                                                                          	{0}", a.ActivityType4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW04                                                                                                                         	{0}", a.StandardValue4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE04                                                                                                                         	{0}", a.StandardValue4OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR05                                                                                                                          	{0}", a.ActivityType5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW05                                                                                                                         	{0}", a.StandardValue5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE05                                                                                                                         	{0}", a.StandardValue5OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR06                                                                                                                          	{0}", a.ActivityType6);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW06                                                                                                                         	{0}", a.StandardValue6);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE06                                                                                                                         	{0}", a.StandardValue6OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-AUFAK                                                                                                                         	{0}", a.Scarp);
                                        }
                                    }
                                    count++;
                                    routingItem += 10;
                                    act++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMRoutingToTextFile(List<RoutingHeaderModel> routingHeaderList, List<RoutingItemModel> routingItemList, string fileName, string extension, string user, DateTime validDate)
        {
            string ValidDateFrom = DateTime.Now.ToString("dd.MM.yyyy", usCulture);
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);

            try
            {
                if (routingHeaderList != null && routingItemList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        // generate create routing header
                        foreach (var o in routingHeaderList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_CA01	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	CA01                                                                                                                                	");
                            fs.WriteLine("SAPLCPDI                                	1010	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=XALU");
                            fs.WriteLine("                                        	0000	 	RC27M-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        	0000	 	RC27M-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC271-PLNNR                                                                                                                         	{0}", o.RoutingGroup); // BOM Alternative
                            fs.WriteLine("                                        	0000	 	RC271-STTAG                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("SAPLCPDA                                	1200	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BU");
                            fs.WriteLine("                                        	0000	 	PLKOD-PLNAL                                                                                                                         	{0}", o.GroupCounter); // Group Counter
                            fs.WriteLine("                                        	0000	 	PLKOD-KTEXT                                                                                                                         	{0}", o.RoutingDescription); // Routing Description
                            fs.WriteLine("                                        	0000	 	PLKOD-WERKS                                                                                                                           	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	PLKOD-VERWE                                                                                                                           	{0}", o.Usage);
                            fs.WriteLine("                                        	0000	 	PLKOD-STATU                                                                                                                           	{0}", o.OverAllStatus);
                            fs.WriteLine("                                        	0000	 	PLKOD-LOSVN                                                                                                                           	{0}", o.LotSizeFrom);
                            fs.WriteLine("                                        	0000	 	PLKOD-LOSBS                                                                                                                           	{0}", o.LotSizeTo);
                            fs.WriteLine("                                        	0000	 	PLKOD-PLNME                                                                                                                           	{0}", o.BaseUnit);
                        }

                        RoutingHeaderModel lastAssign = routingHeaderList.Last();
                        int countAssign = 1;
                        // generate assign material to routing
                        foreach (var o in routingHeaderList)
                        {
                            string countString = countAssign > 9 ? countAssign + "" : "0" + countAssign;

                            fs.WriteLine("	0000	M	ZCONV_CA02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	CA02                                                                                                                                	");
                            fs.WriteLine("SAPLCPDI                                	1010	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ALD1");
                            fs.WriteLine("                                        	0000	 	RC27M-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC271-PLNNR                                                                                                                         	{0}", o.RoutingGroup); // Routing Group
                            fs.WriteLine("                                        	0000	 	RC271-STTAG                                                                                                                          	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("SAPLCPDA                                	1200	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MTUE");
                            fs.WriteLine("SAPLCZDI                                	1010	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");

                            if (o.Equals(lastAssign))
                            {
                                fs.WriteLine("SAPLCPDI                                	1010	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");
                                fs.WriteLine("                                        	0000	 	MAPL-PLNAL({0})                                                                                                                         {1}", countString, o.GroupCounter);
                                fs.WriteLine("                                        	0000	 	MAPL-MATNR({0})                                                                                                                         {1}", countString, o.MaterialCode);
                                fs.WriteLine("                                        	0000	 	MAPL-WERKS({0})                                                                                                                         {1}", countString, o.Plant);
                                fs.WriteLine("SAPLCPDI                                	1010	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BACK");
                                fs.WriteLine("SAPLCPDI                                	1200	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BU");

                            }
                            else
                            {
                                if (countAssign == 1)
                                {
                                    fs.WriteLine("SAPLCPDI                                	1010	X	                                                                                                                                    	");
                                    fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P");
                                    fs.WriteLine("                                        	0000	 	MAPL-PLNAL({0})                                                                                                                         {1}", countString, o.GroupCounter);
                                    fs.WriteLine("                                        	0000	 	MAPL-MATNR({0})                                                                                                                         {1}", countString, o.MaterialCode);
                                    fs.WriteLine("                                        	0000	 	MAPL-WERKS({0})                                                                                                                         {1}", countString, o.Plant);
                                }
                                else
                                {
                                    fs.WriteLine("SAPLCPDI                                	1010	X	                                                                                                                                    	");
                                    fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");
                                    fs.WriteLine("                                        	0000	 	MAPL-PLNAL({0})                                                                                                                         {1}", countString, o.GroupCounter);
                                    fs.WriteLine("                                        	0000	 	MAPL-MATNR({0})                                                                                                                         {1}", countString, o.MaterialCode);
                                    fs.WriteLine("                                        	0000	 	MAPL-WERKS({0})                                                                                                                         {1}", countString, o.Plant);
                                }
                            }
                        }

                        // generate delete operation routing
                        foreach (var o in routingHeaderList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_CA02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	CA02                                                                                                                                	");
                            fs.WriteLine("SAPLCPDI                                	1010	X	                                                                                                                                    	");
                            fs.WriteLine("   s                                     	0000	 	BDC_OKCODE                                                                                                                          	=XALU");
                            fs.WriteLine("                                        	0000	 	RC27M-MATNR                                                                                                                          	{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        	0000	 	RC27M-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC271-PLNNR                                                                                                                         	{0}", o.RoutingGroup); // Routing Group
                            fs.WriteLine("                                        	0000	 	RC271-STTAG                                                                                                                         	{0}", o.ValidDate.ToString("dd.MM.yyyy", usCulture)); // ValidDate
                            fs.WriteLine("                                        	0000	 	RC271-PLNAL                                                                                                                          	{0}", o.RoutingCounter); // Routing Counter
                            fs.WriteLine("SAPLCPDI                                 	1400	X	                                                                                                                                    	"); // Select All
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MAAL");
                            fs.WriteLine("SAPLCPDI                                 	1400	X	                                                                                                                                    	"); // Delete
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=LOE");
                            fs.WriteLine("SAPLSPO1                                	0100	X	                                                                                                                                    	"); // Confirm
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=YES");
                            fs.WriteLine("SAPLCPDI                                 	1400	X	                                                                                                                                    	"); // Save
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BU");
                        }

                        // generate add operation routing
                        foreach (var o in routingHeaderList)
                        {
                            int count = 1;
                            int routingItem = 10;
                            int act = 0;

                            fs.WriteLine("	0000	M	ZCONV_CA02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	CA02                                                                                                                                	");
                            fs.WriteLine("SAPLCPDI                                	1010	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=XALU");
                            fs.WriteLine("                                        	0000	 	RC27M-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        	0000	 	RC27M-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC271-PLNNR                                                                                                                         	{0}", o.RoutingGroup); // Routing Group
                            fs.WriteLine("                                        	0000	 	RC271-STTAG                                                                                                                          	{0}", o.ValidDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("                                        	0000	 	RC271-PLNAL                                                                                                                         	{0}", o.GroupCounter); // Group Counter

                            foreach (var a in routingItemList)
                            {
                                if (o.MaterialCode == a.MaterialCode)
                                {
                                    List<RoutingItemModel> resultList = routingItemList.Where(t => t.MaterialCode == o.MaterialCode).ToList();
                                    RoutingItemModel last = resultList.Last();

                                    // Last Item
                                    if (a.Equals(last))
                                    {
                                        // Init srceen for component
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act); // running number
                                        fs.WriteLine("                                        	0000	 	PLPOD-VORNR(0{0})                                                                                                                     	{1}", count, string.Format("00{0}", routingItem)); // Operation no.
                                        fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(0{0})                                                                                                                     	{1}", count, a.WorkCenter); // WorkCenter
                                        fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(0{0})                                                                                                                       {1}", count, a.OperationDecription); // Operation Decription
                                        // Operation command
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act); // Fix
                                        // Operation detail
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=PICK");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act + 1); // Fix
                                        fs.WriteLine("SAPLCPDO                                	1200	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BACK");
                                        fs.WriteLine("                                        	0000	 	PLPOD-VORNR                                                                                                                         	{0}", string.Format("00{0}", routingItem));
                                        fs.WriteLine("                                        	0000	 	PLPOD-ARBPL                                                                                                                          	{0}", a.WorkCenter);
                                        fs.WriteLine("                                        	0000	 	PLPOD-WERKS                                                                                                                         	{0}", a.Plant);
                                        fs.WriteLine("                                       	0000	 	PLPOD-KTSCH                                                                                                                         	{0}", a.StandardTextKey); // optional
                                        fs.WriteLine("                                        	0000	 	PLPOD-LTXA1                                                                                                                         	{0}", a.OperationDecription);
                                        fs.WriteLine("                                        	0000	 	PLPOD-BMSCH                                                                                                                         	{0}", a.OperationBaseQuantity);
                                        fs.WriteLine("                                        	0000	 	PLPOD-MEINH                                                                                                                          	{0}", a.OperationOUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-UMREZ                                                                                                                         	{0}", a.ConversionOfOUMN);
                                        fs.WriteLine("                                        	0000	 	PLPOD-UMREN                                                                                                                         	{0}", a.ConversionOfOUMD);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR01                                                                                                                         	{0}", a.ActivityType1);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW01                                                                                                                         	{0}", a.StandardValue1);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE01                                                                                                                         	{0}", a.StandardValue1OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR02                                                                                                                          	{0}", a.ActivityType2);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW02                                                                                                                         	{0}", a.StandardValue2);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE02                                                                                                                         	{0}", a.StandardValue2OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR03                                                                                                                          	{0}", a.ActivityType3);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW03                                                                                                                         	{0}", a.StandardValue3);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE03                                                                                                                         	{0}", a.StandardValue3OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR04                                                                                                                          	{0}", a.ActivityType4);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW04                                                                                                                         	{0}", a.StandardValue4);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE04                                                                                                                         	{0}", a.StandardValue4OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR05                                                                                                                          	{0}", a.ActivityType5);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW05                                                                                                                         	{0}", a.StandardValue5);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE05                                                                                                                         	{0}", a.StandardValue5OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LAR06                                                                                                                          	{0}", a.ActivityType6);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW06                                                                                                                         	{0}", a.StandardValue6);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGE06                                                                                                                         	{0}", a.StandardValue6OUM);
                                        fs.WriteLine("                                        	0000	 	PLPOD-AUFAK                                                                                                                         	{0}", a.Scarp);
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BU");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                         {0}", act + 1); // Fix

                                    }
                                    else
                                    {
                                        // First Item
                                        if (count == 1)
                                        {
                                            // Init srceen for component
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=PICK");
                                            fs.WriteLine("                                        	0000	 	PLPOD-VORNR(0{0})                                                                                                                     	{1}", count, string.Format("00{0}", routingItem)); // Operation no.
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(0{0})                                                                                                                     	{1}", count, a.WorkCenter); // WorkCenter
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(0{0})                                                                                                                       {1}", count, a.OperationDecription); // Operation Decription
                                            // Operation detail
                                            fs.WriteLine("SAPLCPDI                                	1200	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BACK");
                                            fs.WriteLine("                                        	0000	 	PLPOD-VORNR                                                                                                                         	{0}", string.Format("00{0}", routingItem));
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL                                                                                                                          	{0}", a.WorkCenter);
                                            fs.WriteLine("                                        	0000	 	PLPOD-WERKS                                                                                                                         	{0}", a.Plant);
                                            fs.WriteLine("                                       	0000	 	PLPOD-KTSCH                                                                                                                         	{0}", a.StandardTextKey); // optional
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1                                                                                                                         	{0}", a.OperationDecription);
                                            fs.WriteLine("                                        	0000	 	PLPOD-BMSCH                                                                                                                         	{0}", a.OperationBaseQuantity);
                                            fs.WriteLine("                                        	0000	 	PLPOD-MEINH                                                                                                                          	{0}", a.OperationOUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-UMREZ                                                                                                                         	{0}", a.ConversionOfOUMN);
                                            fs.WriteLine("                                        	0000	 	PLPOD-UMREN                                                                                                                         	{0}", a.ConversionOfOUMD);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR01                                                                                                                         	{0}", a.ActivityType1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW01                                                                                                                         	{0}", a.StandardValue1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE01                                                                                                                         	{0}", a.StandardValue1OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR02                                                                                                                          	{0}", a.ActivityType2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW02                                                                                                                         	{0}", a.StandardValue2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE02                                                                                                                         	{0}", a.StandardValue2OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR03                                                                                                                          	{0}", a.ActivityType3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW03                                                                                                                         	{0}", a.StandardValue3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE03                                                                                                                         	{0}", a.StandardValue3OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR04                                                                                                                          	{0}", a.ActivityType4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW04                                                                                                                         	{0}", a.StandardValue4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE04                                                                                                                         	{0}", a.StandardValue4OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR05                                                                                                                          	{0}", a.ActivityType5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW05                                                                                                                         	{0}", a.StandardValue5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE05                                                                                                                         	{0}", a.StandardValue5OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR06                                                                                                                          	{0}", a.ActivityType6);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW06                                                                                                                         	{0}", a.StandardValue6);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE06                                                                                                                         	{0}", a.StandardValue6OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-AUFAK                                                                                                                         	{0}", a.Scarp);
                                        }
                                        // Next Item
                                        else
                                        {
                                            // Init srceen for component
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act); // Fix
                                            fs.WriteLine("                                        	0000	 	PLPOD-VORNR(0{0})                                                                                                                     	{1}", count, string.Format("00{0}", routingItem)); // Operation no.
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(0{0})                                                                                                                     	{1}", count, a.WorkCenter); // WorkCenter
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(0{0})                                                                                                                       {1}", count, a.OperationDecription); // Operation Decription
                                            // Operation command
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act); // Fix
                                            fs.WriteLine("                                        	0000	 	RC27X-FLG_SEL(0{0})                                                                                                                     ", act);
                                            // Operation detail
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=PICK");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", act + 1); // Fix
                                            fs.WriteLine("SAPLCPDO                                	1200	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BACK");
                                            fs.WriteLine("                                        	0000	 	PLPOD-VORNR                                                                                                                         	{0}", string.Format("00{0}", routingItem));
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL                                                                                                                          	{0}", a.WorkCenter);
                                            fs.WriteLine("                                        	0000	 	PLPOD-WERKS                                                                                                                         	{0}", a.Plant);
                                            fs.WriteLine("                                       	0000	 	PLPOD-KTSCH                                                                                                                         	{0}", a.StandardTextKey); // optional
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1                                                                                                                         	{0}", a.OperationDecription);
                                            fs.WriteLine("                                        	0000	 	PLPOD-BMSCH                                                                                                                         	{0}", a.OperationBaseQuantity);
                                            fs.WriteLine("                                        	0000	 	PLPOD-MEINH                                                                                                                          	{0}", a.OperationOUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-UMREZ                                                                                                                         	{0}", a.ConversionOfOUMN);
                                            fs.WriteLine("                                        	0000	 	PLPOD-UMREN                                                                                                                         	{0}", a.ConversionOfOUMD);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR01                                                                                                                         	{0}", a.ActivityType1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW01                                                                                                                         	{0}", a.StandardValue1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE01                                                                                                                         	{0}", a.StandardValue1OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR02                                                                                                                          	{0}", a.ActivityType2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW02                                                                                                                         	{0}", a.StandardValue2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE02                                                                                                                         	{0}", a.StandardValue2OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR03                                                                                                                          	{0}", a.ActivityType3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW03                                                                                                                         	{0}", a.StandardValue3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE03                                                                                                                         	{0}", a.StandardValue3OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR04                                                                                                                          	{0}", a.ActivityType4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW04                                                                                                                         	{0}", a.StandardValue4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE04                                                                                                                         	{0}", a.StandardValue4OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR05                                                                                                                          	{0}", a.ActivityType5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW05                                                                                                                         	{0}", a.StandardValue5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE05                                                                                                                         	{0}", a.StandardValue5OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LAR06                                                                                                                          	{0}", a.ActivityType6);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW06                                                                                                                         	{0}", a.StandardValue6);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGE06                                                                                                                         	{0}", a.StandardValue6OUM);
                                            fs.WriteLine("                                        	0000	 	PLPOD-AUFAK                                                                                                                         	{0}", a.Scarp);
                                        }
                                    }
                                    count++;
                                    routingItem += 10;
                                    act++;
                                }
                            }
                        }
                    } //End StreamWriter
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMPackagingInstructionToTextFile(List<PackagingInstructionModel> packagingInstructionList, string fileName, string extension, string user)
        {
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);

            try
            {
                if (packagingInstructionList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = new StreamWriter(fileName + extension, false, Encoding.Unicode))
                    {
                        foreach (var o in packagingInstructionList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_POP1	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	POP1                                                                                                                                	");
                            fs.WriteLine("SAPLVHUPO                                	0101	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	PIKP-PIID                                                                                                                         	    {0}", o.PackagingInstruction); // Packaging Instruction

                            fs.WriteLine("SAPLVHUPO                                	0610	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	PIKP-CONTENT                                                                                                                         	{0}", o.Description); // Description
                            fs.WriteLine("                                        	0000	 	PIPO_ROW-DETAIL_ITEMTYPE(02)                                                                                                            {0}", "R"); // Fix Reference Material
                            fs.WriteLine("                                        	0000	 	PIPO_ROW-COMPONENT(01)                                                                                                                  {0}", o.ComponentPackage); // Component Package
                            fs.WriteLine("                                        	0000	 	PIPO_ROW-COMPONENT(02)                                                                                                                  {0}", o.ComponentReferenceMaterial); // Component Reference Material
                            fs.WriteLine("                                        	0000	 	PIPO_ROW-TRGQTY(02)                                                                                                                     {0}", o.TargetQuantity); // Target Quantity
                            fs.WriteLine("                                        	0000	 	PIPO_ROW-MINQTY(02)                                                                                                                     {0}", o.MinimumQuantity); // Minimum Quantity

                            fs.WriteLine("SAPLVHUPO                                	0610	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=PICK");

                            fs.WriteLine("SAPLVHUPO                                	0610	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=TAB4");

                            fs.WriteLine("SAPLVHUPO                                	0610	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=TAB1");
                            fs.WriteLine("                                        	0000	 	PIKP-CHECKPROF                                                                                                                          {0}", "01"); // Fix

                            fs.WriteLine("SAPLVHUPO                                	0610	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=TAB5");

                            fs.WriteLine("SAPLVHUPO                                	0610	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=SAVE");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMInspectionPlanToTextFile(List<InspectionPlanHeaderModel> inspectionPlanHeaderList, List<InspectionPlanItemModel> inspectionPlanItemList, string fileName, string extension, string user, DateTime validDate)
        {
            string ValidDateFrom = DateTime.Now.ToString("dd.MM.yyyy", usCulture);
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);

            try
            {
                if (inspectionPlanHeaderList != null && inspectionPlanItemList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = new StreamWriter(fileName + extension, false, Encoding.Unicode))
                    {
                        // generate create inspection plan header
                        foreach (var o in inspectionPlanHeaderList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_{0}	{1}          {2}  {3}", fileName, user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	QP01                                                                                                                                	");
                            fs.WriteLine("SAPLCPDI                                	8010	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=XALU");
                            fs.WriteLine("                                        	0000	 	RC27M-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        	0000	 	RC27M-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC271-PLNNR                                                                                                                         	{0}", o.Group); // Group
                            fs.WriteLine("                                        	0000	 	RC271-STTAG                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("SAPLCPDA                                	1200	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BU");
                            fs.WriteLine("                                        	0000	 	PLKOD-PLNAL                                                                                                                         	{0}", o.GroupCounter); // Group Counter
                            fs.WriteLine("                                        	0000	 	PLKOD-KTEXT                                                                                                                         	{0}", o.HeaderDescription); // Routing Description
                            fs.WriteLine("                                        	0000	 	PLKOD-WERKS                                                                                                                           	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	PLKOD-VERWE                                                                                                                           	{0}", o.TaskListUsage); // Task List Usage
                            fs.WriteLine("                                        	0000	 	PLKOD-STATU                                                                                                                           	{0}", o.Status); // Status
                            fs.WriteLine("                                        	0000	 	PLKOD-LOSVN                                                                                                                           	{0}", o.LotSizeFrom); // Lot Size To
                            fs.WriteLine("                                        	0000	 	PLKOD-LOSBS                                                                                                                           	{0}", o.LotSizeTo); // Lot Size From
                            fs.WriteLine("                                        	0000	 	PLKOD-PLNME                                                                                                                           	{0}", o.BaseUnit); // Base Unit
                            fs.WriteLine("                                        	0000	 	PLKOD-SLWBEZ                                                                                                                           	{0}", o.InspectionPoint); // Inspection Point
                        }

                        InspectionPlanHeaderModel lastAssign = inspectionPlanHeaderList.Last();
                        int countAssign = 1;
                        int actAssign = 0;
                        // generate assign material to routing
                        foreach (var o in inspectionPlanHeaderList)
                        {
                            string countString = countAssign > 9 ? countAssign + "" : "0" + countAssign;

                            fs.WriteLine("	0000	M	ZCONV_QP02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	QP02                                                                                                                                	");
                            fs.WriteLine("SAPLCPDI                                	8010	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=XALU");
                            fs.WriteLine("                                        	0000	 	RC27M-MATNR                                                                                                                         	{0}", o.MaterialCode); // Material Code
                            fs.WriteLine("                                        	0000	 	RC27M-WERKS                                                                                                                          	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC271-PLNNR                                                                                                                         	{0}", o.TaskListUsage); // Routing Group
                            fs.WriteLine("                                        	0000	 	RC271-STTAG                                                                                                                          	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("                                        	0000	 	RC271-PLNAL                                                                                                                         	{0}", o.TaskListUsage); // Group Counter
                            fs.WriteLine("SAPLCPDA                                	1400	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=QMUE");
                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                         {0}", "1"); // Fix
                            fs.WriteLine("                                        	0000	 	RC27X-FLG_SEL(01)                                                                                                                       {0}", "X"); // Fix

                            foreach (var a in inspectionPlanItemList)
                            {
                                if (o.MaterialCode == a.MaterialCode)
                                {
                                    List<InspectionPlanItemModel> resultList = inspectionPlanItemList.Where(t => t.MaterialCode == o.MaterialCode).ToList();

                                    if (countAssign == 1) // first operation
                                    {
                                        fs.WriteLine("SAPLQPAA                                	0150	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	PLMKB-VERWMERKM(01)                                                                                                                     {0}", a.MIC); // MIC
                                        fs.WriteLine("                                        	0000	 	PLMKB-MKVERSION(01)                                                                                                                     {0}", "1"); // (Fix) MIC Version
                                        fs.WriteLine("                                        	0000	 	PLMKB-KURZTEXT(01)                                                                                                                      {0}", a.ShortText); // Short Text
                                        fs.WriteLine("                                        	0000	 	PLMKB-PMETHODE(01)                                                                                                                      {0}", a.InpectionMethod); // Inspection Method
                                        fs.WriteLine("                                        	0000	 	PLMKB-PMTVERSION(01)                                                                                                                    {0}", "1"); // (Fix) Inspection Mothod Version
                                        fs.WriteLine("SAPLQPAA                                 	1501	X	                                                                                                                                    	"); // Select All
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ENT1");
                                        fs.WriteLine("SAPLQPAA                                 	1502	X	                                                                                                                                    	"); // Delete
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ENT1");
                                        fs.WriteLine("SAPLQPAA                                	1502	X	                                                                                                                                    	"); // Confirm
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ENT1");
                                        fs.WriteLine("SAPLQPAA                                 	0150	X	                                                                                                                                    	"); // Save
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=QMEF");
                                        fs.WriteLine("                                        	0000	 	RQPAS-ENTRY_ACT                                                                                                                         {0}", "1"); // Fix
                                    }
                                    else
                                    {
                                        fs.WriteLine("SAPLQPAA                                	0150	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RQPAS-ENTRY_ACT                                                                                                                         {0}", actAssign); // running number
                                        fs.WriteLine("                                        	0000	 	PLMKB-VERWMERKM(01)                                                                                                                     {0}", a.MIC); // MIC
                                        fs.WriteLine("                                        	0000	 	PLMKB-MKVERSION(01)                                                                                                                     {0}", "1"); // (Fix) MIC Version
                                        fs.WriteLine("                                        	0000	 	PLMKB-KURZTEXT(01)                                                                                                                      {0}", a.ShortText); // Short Text
                                        fs.WriteLine("                                        	0000	 	PLMKB-PMETHODE(01)                                                                                                                      {0}", a.InpectionMethod); // Inspection Method
                                        fs.WriteLine("                                        	0000	 	PLMKB-PMTVERSION(01)                                                                                                                    {0}", "1"); // (Fix) Inspection Mothod Version
                                        fs.WriteLine("SAPLQPAA                                 	1501	X	                                                                                                                                    	"); // Select All
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ENT1");
                                        fs.WriteLine("SAPLQPAA                                 	1502	X	                                                                                                                                    	"); // Delete
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ENT1");
                                        fs.WriteLine("SAPLQPAA                                	1502	X	                                                                                                                                    	"); // Confirm
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ENT1");
                                        fs.WriteLine("SAPLQPAA                                 	0150	X	                                                                                                                                    	"); // Save
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=QMEF");
                                        fs.WriteLine("                                        	0000	 	RQPAS-ENTRY_ACT                                                                                                                         {0}", "1"); // Fix
                                    }

                                    actAssign++;
                                    countAssign++;
                                }
                            }
                        }

                        // generate delete operation inspection plan
                        foreach (var o in inspectionPlanHeaderList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_QP02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	QP02                                                                                                                                	");
                            fs.WriteLine("SAPLCPDI                                	8010	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=XALU");
                            fs.WriteLine("                                        	0000	 	RC27M-MATNR                                                                                                                          	{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        	0000	 	RC27M-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC271-STTAG                                                                                                                         	{0}", o.ValidDate.ToString("dd.MM.yyyy", usCulture)); // ValidDate
                            fs.WriteLine("                                        	0000	 	RC271-PLNNR                                                                                                                         	{0}", o.Group); // Inspection Plan Group
                            fs.WriteLine("SAPLCPDI                                 	1400	X	                                                                                                                                    	"); // Select All
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MAAL");
                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                         {0}", "1"); //  Fix
                            fs.WriteLine("SAPLCPDI                                 	1400	X	                                                                                                                                    	"); // Delete
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=LOE");
                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                         {0}", "1"); //  Fix
                            fs.WriteLine("SAPLSPO1                                	0100	X	                                                                                                                                    	"); // Confirm
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=YES");
                            fs.WriteLine("SAPLCPDI                                 	1400	X	                                                                                                                                    	"); // Save
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BU");
                        }

                        // generate add operation Inspection plan
                        foreach (var o in inspectionPlanHeaderList)
                        {
                            int countItem = 1;
                            int routingItem = 10;
                            int actItem = 1;

                            fs.WriteLine("	0000	M	ZCONV_QP02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	QP02                                                                                                                                	");
                            fs.WriteLine("SAPLCPDI                                	8010	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=XALU");
                            fs.WriteLine("                                        	0000    	RC27M-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        	0000	 	RC27M-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC271-PLNNR                                                                                                                         	{0}", o.Group); // Routing Group
                            fs.WriteLine("                                        	0000	 	RC271-STTAG                                                                                                                          	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From

                            foreach (var a in inspectionPlanItemList)
                            {
                                if (o.MaterialCode == a.MaterialCode)
                                {
                                    List<InspectionPlanItemModel> resultList = inspectionPlanItemList.Where(t => t.MaterialCode == o.MaterialCode).ToList();
                                    InspectionPlanItemModel last = resultList.Last();

                                    if (resultList.Equals(last))
                                    {
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", "1"); // Fix
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", actItem); // Fix
                                        fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(02)                                                                                                                         {0}", a.MIC);
                                        fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(02)                                                                                                                         {0}", a.ItemDescription);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW01(02)                                                                                                                         {0}", a.SetUpValue1);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW02(02)                                                                                                                         {0}", a.SetUpValue2);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW03(02)                                                                                                                         {0}", a.SetUpValue3);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW04(02)                                                                                                                         {0}", a.SetUpValue4);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW05(02)                                                                                                                         {0}", a.SetUpValue5);
                                        fs.WriteLine("                                        	0000	 	PLPOD-VGW06(02)                                                                                                                         {0}", a.SetUpValue6);
                                        fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	"); // Save
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=BU");
                                        fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", actItem); // Fix
                                    }
                                    else
                                    {
                                        // First Item
                                        if (countItem == 1)
                                        {
                                            // Operation detail
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(01)                                                                                                                         {0}", a.MIC);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(01)                                                                                                                         {0}", a.ItemDescription);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW01(01)                                                                                                                         {0}", a.SetUpValue1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW02(01)                                                                                                                         {0}", a.SetUpValue2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW03(01)                                                                                                                         {0}", a.SetUpValue3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW04(01)                                                                                                                         {0}", a.SetUpValue4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW05(01)                                                                                                                         {0}", a.SetUpValue5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW06(01)                                                                                                                         {0}", a.SetUpValue6);
                                        }
                                        // Second Item
                                        else if (countItem == 2)
                                        {
                                            // Operation detail
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", "1"); // Fix
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(02)                                                                                                                         {0}", a.MIC);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(02)                                                                                                                         {0}", a.ItemDescription);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW01(02)                                                                                                                         {0}", a.SetUpValue1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW02(02)                                                                                                                         {0}", a.SetUpValue2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW03(02)                                                                                                                         {0}", a.SetUpValue3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW04(02)                                                                                                                         {0}", a.SetUpValue4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW05(02)                                                                                                                         {0}", a.SetUpValue5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW06(02)                                                                                                                         {0}", a.SetUpValue6);
                                        }
                                        // Next Item
                                        else
                                        {
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", "1"); // Fix
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", "2"); // Fix
                                            fs.WriteLine("                                        	0000	 	PLPOD-ARBPL(02)                                                                                                                         {0}", a.MIC);
                                            fs.WriteLine("                                        	0000	 	PLPOD-LTXA1(02)                                                                                                                         {0}", a.ItemDescription);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW01(02)                                                                                                                         {0}", a.SetUpValue1);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW02(02)                                                                                                                         {0}", a.SetUpValue2);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW03(02)                                                                                                                         {0}", a.SetUpValue3);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW04(02)                                                                                                                         {0}", a.SetUpValue4);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW05(02)                                                                                                                         {0}", a.SetUpValue5);
                                            fs.WriteLine("                                        	0000	 	PLPOD-VGW06(02)                                                                                                                         {0}", a.SetUpValue6);
                                            fs.WriteLine("SAPLCPDI                                	1400	X	                                                                                                                                    	");
                                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=P+");
                                            fs.WriteLine("                                        	0000	 	RC27X-ENTRY_ACT                                                                                                                       	{0}", actItem); // Fix
                                        }
                                    }

                                    countItem++;
                                    routingItem += 10;
                                    actItem++;
                                }
                            }
                        }
                    } //End StreamWriter
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertWorkCenterRoutingToTextFile(List<WorkCenterRoutingModel> workCenterRoutingList, List<WorkCenterRoutingItemModel> workCenterRoutingItemList, string filePath, string fileName, string extension, string user, DateTime validDate)
        {
            #region Variable

            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);
            string TaskListUsage = "009"; // Task list usage
            string Name = Path.GetFileName(filePath);
            string OperationBaseQuantity = "1";
            string ConversionOfOUMN = "1";
            string ConversionOfOUMD = "1";

            string ActivityTypeNo1 = string.Empty;
            string ActivityTypeNo2 = string.Empty;
            string ActivityTypeNo3 = string.Empty;
            string ActivityTypeNo4 = string.Empty;
            string ActivityTypeNo5 = string.Empty;
            string ActivityTypeNo6 = string.Empty;

            string ActivityDesNo1 = string.Empty;
            string ActivityDesNo2 = string.Empty;
            string ActivityDesNo3 = string.Empty;
            string ActivityDesNo4 = string.Empty;
            string ActivityDesNo5 = string.Empty;
            string ActivityDesNo6 = string.Empty;

            string FomularKeyNo1 = string.Empty;
            string FomularKeyNo2 = string.Empty;
            string FomularKeyNo3 = string.Empty;
            string FomularKeyNo4 = string.Empty;
            string FomularKeyNo5 = string.Empty;
            string FomularKeyNo6 = string.Empty;

            string RefIndicatorNo1 = string.Empty;
            string RefIndicatorNo2 = string.Empty;
            string RefIndicatorNo3 = string.Empty;
            string RefIndicatorNo4 = string.Empty;
            string RefIndicatorNo5 = string.Empty;
            string RefIndicatorNo6 = string.Empty;

            string UnitOfActNo1 = string.Empty;
            string UnitOfActNo2 = string.Empty;
            string UnitOfActNo3 = string.Empty;
            string UnitOfActNo4 = string.Empty;
            string UnitOfActNo5 = string.Empty;
            string UnitOfActNo6 = string.Empty;

            #endregion

            try
            {
                if (workCenterRoutingList != null && workCenterRoutingList.Count > 0)
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    // Create a new file
                    using (StreamWriter fs = new StreamWriter(filePath + extension, false, Encoding.Unicode))
                    {
                        fs.WriteLine("\t0000\tM\tZCONV_{0}\t{1}          {2}  {3}", Name, user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                        // generate delete operation routing
                        foreach (var o in workCenterRoutingList)
                        {
                            fs.WriteLine("                                        \t0000\tT\tCA02                                                                                                                                \t");
                            fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=XALU");
                            fs.WriteLine("                                        \t0000\t \tRC27M-MATNR                                                                                                                         \t{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        \t0000\t \tRC27M-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                            fs.WriteLine("                                        \t0000\t \tRC271-PLNNR                                                                                                                         \t{0}", o.RoutingGroup); // Routing Group
                            fs.WriteLine("                                        \t0000\t \tRC271-STTAG                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // ValidDate
                            fs.WriteLine("                                        \t0000\t \tRC271-PLNAL                                                                                                                         \t{0}", o.RoutingCounter); // Routing Counter
                            fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t"); // Select All
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=MAAL");
                            fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t"); // Delete
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=LOE");
                            fs.WriteLine("SAPLSPO1                                \t0100\tX\t                                                                                                                                    \t"); // Confirm
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=YES");
                            fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t"); // Save
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BU");
                        }

                        // generate delete work center
                        foreach (var o in workCenterRoutingList)
                        {
                            fs.WriteLine("                                        \t0000\tT\tCR02                                                                                                                                \t");
                            fs.WriteLine("SAPLCRA0                                \t0100\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                            fs.WriteLine("                                        \t0000\t \tRC68A-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                            fs.WriteLine("                                        \t0000\t \tRC68A-ARBPL                                                                                                                         \t{0}", o.WorkCenter); // Work Center
                            fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t"); // Delete
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=DEL");
                        }

                        // generate create work center
                        foreach (var o in workCenterRoutingList)
                        {
                            // set item data
                            foreach (var a in workCenterRoutingItemList)
                            {
                                if (a.ItemFlag == o.HeaderFlag)
                                {
                                    if (a.ActivityNo == 1)
                                    {
                                        ActivityTypeNo1 = a.ActivityType;
                                        ActivityDesNo1 = a.ActivityDescription;
                                        FomularKeyNo1 = a.CostingFormular;
                                        RefIndicatorNo1 = a.RefIndicator;
                                        UnitOfActNo1 = a.ActivityUnit;
                                    }
                                    if (a.ActivityNo == 2)
                                    {
                                        ActivityTypeNo2 = a.ActivityType;
                                        ActivityDesNo2 = a.ActivityDescription;
                                        FomularKeyNo2 = a.CostingFormular;
                                        RefIndicatorNo2 = a.RefIndicator;
                                        UnitOfActNo2 = a.ActivityUnit;
                                    }
                                    if (a.ActivityNo == 3)
                                    {
                                        ActivityTypeNo3 = a.ActivityType;
                                        ActivityDesNo3 = a.ActivityDescription;
                                        FomularKeyNo3 = a.CostingFormular;
                                        RefIndicatorNo3 = a.RefIndicator;
                                        UnitOfActNo3 = a.ActivityUnit;
                                    }
                                    if (a.ActivityNo == 4)
                                    {
                                        ActivityTypeNo4 = a.ActivityType;
                                        ActivityDesNo4 = a.ActivityDescription;
                                        FomularKeyNo4 = a.CostingFormular;
                                        RefIndicatorNo4 = a.RefIndicator;
                                        UnitOfActNo4 = a.ActivityUnit;
                                    }
                                    if (a.ActivityNo == 5)
                                    {
                                        ActivityTypeNo5 = a.ActivityType;
                                        ActivityDesNo5 = a.ActivityDescription;
                                        FomularKeyNo5 = a.CostingFormular;
                                        RefIndicatorNo5 = a.RefIndicator;
                                        UnitOfActNo5 = a.ActivityUnit;
                                    }
                                    if (a.ActivityNo == 6)
                                    {
                                        ActivityTypeNo6 = a.ActivityType;
                                        ActivityDesNo6 = a.ActivityDescription;
                                        FomularKeyNo6 = a.CostingFormular;
                                        RefIndicatorNo6 = a.RefIndicator;
                                        UnitOfActNo6 = a.ActivityUnit;
                                    }
                                }

                            }

                            if (String.Equals(o.WorkCenterCategory, "PP1", StringComparison.OrdinalIgnoreCase)) // default value PP01
                            {
                                fs.WriteLine("                                        \t0000\tT\tCR01                                                                                                                                \t");
                                fs.WriteLine("SAPLCRA0                                \t0101\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \tNEXT");
                                fs.WriteLine("                                        \t0000\t \tRC68A-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                fs.WriteLine("                                        \t0000\t \tRC68A-ARBPL                                                                                                                         \t{0}", o.WorkCenter); // Work Center
                                fs.WriteLine("                                        \t0000\t \tRC68A-VERWE                                                                                                                         \t{0}", o.WorkCenterCategory); // Work Center Category
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                fs.WriteLine("                                        \t0000\t \tP1000-STEXT                                                                                                                         \t{0}", o.WorkCenterDescription); // Work Center Name
                                fs.WriteLine("                                        \t0000\t \tP3000-PLANV                                                                                                                         \t{0}", TaskListUsage); // Task List Usage
                                fs.WriteLine("                                        \t0000\t \tP3000-VGWTS                                                                                                                         \t{0}", o.StandardValueKeyHeader); // Standard value key Header
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VORA");
                                fs.WriteLine("                                        \t0000\t \tP3000-RGEKZ                                                                                                                         \t{0}", o.Backflushing); // Indicator: Backflushing
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                fs.WriteLine("                                        \t0000\t \tP3003-STEUS                                                                                                                         \t{0}", o.ControlKey); // Control Key
                                fs.WriteLine("                                        \t0000\t \tP3003-STEUS_REF                                                                                                                     \tX"); // Fix Indicator: Control Profile is Referenced
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(01)                                                                                                                     \t{0}", o.UnitOfMeasurement1); // Unit of Measurement of Standard Value 1
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(02)                                                                                                                     \t{0}", o.UnitOfMeasurement2); // Unit of Measurement of Standard Value 2
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(03)                                                                                                                     \t{0}", o.UnitOfMeasurement3); // Unit of Measurement of Standard Value 3
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(04)                                                                                                                     \t{0}", o.UnitOfMeasurement4); // Unit of Measurement of Standard Value 4
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(05)                                                                                                                     \t{0}", o.UnitOfMeasurement5); // Unit of Measurement of Standard Value 5
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(06)                                                                                                                     \t{0}", o.UnitOfMeasurement6); // Unit of Measurement of Standard Value 6
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=KAUE");
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                fs.WriteLine("                                        \t0000\t \tRC68A-KAPART(01)                                                                                                                    \t{0}", o.CapacityCategory); // Capacity category (Optional)
                                fs.WriteLine("                                        \t0000\t \tP3006-FORK1(01)                                                                                                                     \t{0}", o.FormulaForSetupCapacityRequirements); // Formula for Setup Capacity Requirements (Optional)
                                fs.WriteLine("SAPLCRK0                                \t0101\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                fs.WriteLine("                                        \t0000\t \tRC68K-BEGZT                                                                                                                         \t{0}", ""); // Start time (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68K-ENDZT                                                                                                                         \t{0}", ""); // End time (Optional)
                                fs.WriteLine("                                        \t0000\t \tKAKO-NGRAD                                                                                                                          \t{0}", o.CapacityUtilization); // Capacity Utilization Ratio in Percent (Optional)
                                fs.WriteLine("                                        \t0000\t \tKAKO-AZNOR                                                                                                                          \t{0}", o.NoIndCapacities); // No. Ind. Capacities (Optional)
                                fs.WriteLine("                                        \t0000\t \tKAKO-KALID                                                                                                                          \t{0}", o.FactoryCalendarID); // Factory Calendar ID (Optional)
                                fs.WriteLine("                                        \t0000\t \tKAKO-MEINS                                                                                                                          \t{0}", o.CapacityBaseUnit); // Capacity Base Unit of Measure (Optional)
                                fs.WriteLine("                                        \t0000\t \tKAKO-UEBERLAST                                                                                                                      \t{0}", o.CapacityUtilization); // Capacity Utilization Ratio in Percent (Optional)
                                fs.WriteLine("                                        \t0000\t \tKAKO-KAPTER                                                                                                                         \t{0}", o.CapacityRelevantToFiniteScheduling); // CapacityRelevantToFiniteScheduling (Optional)
                                fs.WriteLine("                                        \t0000\t \tKAKO-KAPAVO                                                                                                                         \t{0}", o.SeveralOperationsCanUseCapacity); // SeveralOperationsCanUseCapacity (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68K-KAPLPL                                                                                                                        \t{0}", o.LongtermPlanning); // Longterm planning (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=PARA");
                                fs.WriteLine("SAPLCRA0                                \t0104\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=WEIT");
                                fs.WriteLine("                                        \t0000\t \tP3000-PAR01                                                                                                                         \t{0}", o.FirstWorkCenterParameter); // FirstWorkCenterParameter (Optional)
                                fs.WriteLine("                                        \t0000\t \tP3000-PARV1                                                                                                                         \t{0}", o.ParameterValue); // ParameterValue (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=TERM");
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VK11");
                                fs.WriteLine("                                        \t0000\t \tRC68A-KAPART                                                                                                                        \t{0}", o.CapacityCategory); // Capacity Category (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VK11");
                                fs.WriteLine("                                        \t0000\t \tP1001-BEGDA                                                                                                                         \t{0}", o.StartDate.ToString("dd.MM.yyyy", usCulture)); // Start Date
                                fs.WriteLine("                                        \t0000\t \tP1001-ENDDA                                                                                                                         \t{0}", o.EndDate.ToString("dd.MM.yyyy", usCulture)); // End Date
                                fs.WriteLine("                                        \t0000\t \tCRKEYK-KOSTL                                                                                                                        \t{0}", o.CostCenter); // Cost center
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(01)                                                                                                                    \t{0}", "%%%2"); // Activity description no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(02)                                                                                                                    \t{0}", "%%%2"); // Activity description no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(03)                                                                                                                    \t{0}", "%%%2"); // Activity description no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(04)                                                                                                                    \t{0}", "%%%2"); // Activity description no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(05)                                                                                                                    \t{0}", "%%%2"); // Activity description no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(06)                                                                                                                    \t{0}", "%%%2"); // Activity description no.6 (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VK11");
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(01)                                                                                                                     \t{0}", ActivityTypeNo1); // Activity type no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(02)                                                                                                                     \t{0}", ActivityTypeNo2); // Activity type no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(03)                                                                                                                     \t{0}", ActivityTypeNo3); // Activity type no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(04)                                                                                                                     \t{0}", ActivityTypeNo4); // Activity type no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(05)                                                                                                                     \t{0}", ActivityTypeNo5); // Activity type no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(06)                                                                                                                     \t{0}", ActivityTypeNo6); // Activity type no.6 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(01)                                                                                                                     \t{0}", UnitOfActNo1); // Unit of activity no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(02)                                                                                                                     \t{0}", UnitOfActNo2); // Unit of activity no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(03)                                                                                                                     \t{0}", UnitOfActNo3); // Unit of activity no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(04)                                                                                                                     \t{0}", UnitOfActNo4); // Unit of activity no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(05)                                                                                                                     \t{0}", UnitOfActNo5); // Unit of activity no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(06)                                                                                                                     \t{0}", UnitOfActNo6); // Unit of activity no.6 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(01)                                                                                                                     \t{0}", FomularKeyNo1); // Fomular key no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(02)                                                                                                                     \t{0}", FomularKeyNo2); // Fomular key no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(03)                                                                                                                     \t{0}", FomularKeyNo3); // Fomular key no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(04)                                                                                                                     \t{0}", FomularKeyNo4); // Fomular key no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(05)                                                                                                                     \t{0}", FomularKeyNo5); // Fomular key no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(06)                                                                                                                     \t{0}", FomularKeyNo6); // Fomular key no.6 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(01)                                                                                                                   \t{0}", RefIndicatorNo1); // Refference indicator no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(02)                                                                                                                   \t{0}", RefIndicatorNo2); // Refference indicator no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(03)                                                                                                                   \t{0}", RefIndicatorNo3); // Refference indicator no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(04)                                                                                                                   \t{0}", RefIndicatorNo4); // Refference indicator no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(05)                                                                                                                   \t{0}", RefIndicatorNo5); // Refference indicator no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(06)                                                                                                                   \t{0}", RefIndicatorNo6); // Refference indicator no.6 (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \tUPD");
                            }
                            else if (String.Equals(o.WorkCenterCategory, "PP2", StringComparison.OrdinalIgnoreCase)) // default value PP02
                            {
                                fs.WriteLine("                                        \t0000\tT\tCR01                                                                                                                                \t");
                                fs.WriteLine("SAPLCRA0                                \t0101\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \tNEXT");
                                fs.WriteLine("                                        \t0000\t \tRC68A-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                fs.WriteLine("                                        \t0000\t \tRC68A-ARBPL                                                                                                                         \t{0}", o.WorkCenter); // Work Center
                                fs.WriteLine("                                        \t0000\t \tRC68A-VERWE                                                                                                                         \t{0}", o.WorkCenterCategory); // Work Center Category
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VORA");
                                fs.WriteLine("                                        \t0000\t \tP1000-STEXT                                                                                                                         \t{0}", o.WorkCenterDescription); // Work Center Name
                                fs.WriteLine("                                        \t0000\t \tP3000-PLANV                                                                                                                         \t{0}", TaskListUsage); // Task List Usage
                                fs.WriteLine("                                        \t0000\t \tP3000-RGEKZ                                                                                                                         \t{0}", o.Backflushing); // Indicator: Backflushing
                                fs.WriteLine("                                        \t0000\t \tP3000-VGWTS                                                                                                                         \t{0}", o.StandardValueKeyHeader); // Standard value key Header
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=KAUE");
                                fs.WriteLine("                                        \t0000\t \tP3003-STEUS                                                                                                                         \t{0}", o.ControlKey); // Control Key
                                fs.WriteLine("                                        \t0000\t \tP3003-STEUS_REF                                                                                                                     \tX"); // Fix Indicator: Control Profile is Referenced
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(01)                                                                                                                     \t{0}", o.UnitOfMeasurement1); // Unit of Measurement of Standard Value 1
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(02)                                                                                                                     \t{0}", o.UnitOfMeasurement2); // Unit of Measurement of Standard Value 2
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(03)                                                                                                                     \t{0}", o.UnitOfMeasurement3); // Unit of Measurement of Standard Value 3
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(04)                                                                                                                     \t{0}", o.UnitOfMeasurement4); // Unit of Measurement of Standard Value 4
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(05)                                                                                                                     \t{0}", o.UnitOfMeasurement5); // Unit of Measurement of Standard Value 5
                                fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(06)                                                                                                                     \t{0}", o.UnitOfMeasurement6); // Unit of Measurement of Standard Value 6
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                fs.WriteLine("                                        \t0000\t \tRC68A-KAPART(01)                                                                                                                    \t{0}", o.CapacityCategory); // Capacity category (Optional)
                                fs.WriteLine("                                        \t0000\t \tP3006-FORK1(01)                                                                                                                     \t{0}", o.FormulaForSetupCapacityRequirements); // Formula for Setup Capacity Requirements (Optional)
                                fs.WriteLine("SAPLCRK0                                \t0101\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                fs.WriteLine("                                        \t0000\t \tRC68K-BEGZT                                                                                                                         \t{0}", o.StartTime); // Start time
                                fs.WriteLine("                                        \t0000\t \tRC68K-ENDZT                                                                                                                         \t{0}", o.EndTime); // End time
                                fs.WriteLine("                                        \t0000\t \tKAKO-NGRAD                                                                                                                          \t{0}", o.CapacityUtilization); // Capacity Utilization Ratio in Percent
                                fs.WriteLine("                                        \t0000\t \tKAKO-AZNOR                                                                                                                          \t{0}", o.NoIndCapacities); // No. Ind. Capacities
                                fs.WriteLine("                                        \t0000\t \tKAKO-KALID                                                                                                                          \t{0}", o.FactoryCalendarID); // Factory Calendar ID
                                fs.WriteLine("                                        \t0000\t \tKAKO-MEINS                                                                                                                          \t{0}", o.CapacityBaseUnit); // Capacity Base Unit of Measure
                                fs.WriteLine("                                        \t0000\t \tKAKO-UEBERLAST                                                                                                                      \t{0}", o.CapacityUtilization); // Capacity Utilization Ratio in Percent
                                fs.WriteLine("                                        \t0000\t \tKAKO-KAPTER                                                                                                                         \t{0}", o.CapacityRelevantToFiniteScheduling); // CapacityRelevantToFiniteScheduling (Optional)
                                fs.WriteLine("                                        \t0000\t \tKAKO-KAPAVO                                                                                                                         \t{0}", o.SeveralOperationsCanUseCapacity); // SeveralOperationsCanUseCapacity (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68K-KAPLPL                                                                                                                        \t{0}", o.LongtermPlanning); // Longterm planning (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=PARA");
                                fs.WriteLine("SAPLCRA0                                \t0104\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=WEIT");
                                fs.WriteLine("                                        \t0000\t \tP3000-PAR01                                                                                                                         \t{0}", o.FirstWorkCenterParameter); // FirstWorkCenterParameter (Optional)
                                fs.WriteLine("                                        \t0000\t \tP3000-PARV1                                                                                                                         \t{0}", o.ParameterValue); // ParameterValue (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=TERM");
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VK11");
                                fs.WriteLine("                                        \t0000\t \tRC68A-KAPART                                                                                                                        \t{0}", o.CapacityCategory); // Capacity Category (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VK11");
                                fs.WriteLine("                                        \t0000\t \tP1001-BEGDA                                                                                                                         \t{0}", o.StartDate.ToString("dd.MM.yyyy", usCulture)); // Start Date
                                fs.WriteLine("                                        \t0000\t \tP1001-ENDDA                                                                                                                         \t{0}", o.EndDate.ToString("dd.MM.yyyy", usCulture)); // End Date
                                fs.WriteLine("                                        \t0000\t \tCRKEYK-KOSTL                                                                                                                        \t{0}", o.CostCenter); // Cost center
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(01)                                                                                                                    \t{0}", "%%%2"); // Activity description no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(02)                                                                                                                    \t{0}", "%%%2"); // Activity description no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(03)                                                                                                                    \t{0}", "%%%2"); // Activity description no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(04)                                                                                                                    \t{0}", "%%%2"); // Activity description no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(05)                                                                                                                    \t{0}", "%%%2"); // Activity description no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(06)                                                                                                                    \t{0}", "%%%2"); // Activity description no.6 (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VK11");
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(01)                                                                                                                     \t{0}", ActivityTypeNo1); // Activity type no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(02)                                                                                                                     \t{0}", ActivityTypeNo2); // Activity type no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(03)                                                                                                                     \t{0}", ActivityTypeNo3); // Activity type no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(04)                                                                                                                     \t{0}", ActivityTypeNo4); // Activity type no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(05)                                                                                                                     \t{0}", ActivityTypeNo5); // Activity type no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(06)                                                                                                                     \t{0}", ActivityTypeNo6); // Activity type no.6 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(01)                                                                                                                     \t{0}", UnitOfActNo1); // Unit of activity no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(02)                                                                                                                     \t{0}", UnitOfActNo2); // Unit of activity no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(03)                                                                                                                     \t{0}", UnitOfActNo3); // Unit of activity no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(04)                                                                                                                     \t{0}", UnitOfActNo4); // Unit of activity no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(05)                                                                                                                     \t{0}", UnitOfActNo5); // Unit of activity no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(06)                                                                                                                     \t{0}", UnitOfActNo6); // Unit of activity no.6 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(01)                                                                                                                     \t{0}", FomularKeyNo1); // Fomular key no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(02)                                                                                                                     \t{0}", FomularKeyNo2); // Fomular key no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(03)                                                                                                                     \t{0}", FomularKeyNo3); // Fomular key no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(04)                                                                                                                     \t{0}", FomularKeyNo4); // Fomular key no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(05)                                                                                                                     \t{0}", FomularKeyNo5); // Fomular key no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(06)                                                                                                                     \t{0}", FomularKeyNo6); // Fomular key no.6 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(01)                                                                                                                   \t{0}", RefIndicatorNo1); // Refference indicator no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(02)                                                                                                                   \t{0}", RefIndicatorNo2); // Refference indicator no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(03)                                                                                                                   \t{0}", RefIndicatorNo3); // Refference indicator no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(04)                                                                                                                   \t{0}", RefIndicatorNo4); // Refference indicator no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(05)                                                                                                                   \t{0}", RefIndicatorNo5); // Refference indicator no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(06)                                                                                                                   \t{0}", RefIndicatorNo6); // Refference indicator no.6 (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \tUPD");
                            }
                            // PP03
                            else
                            {
                                fs.WriteLine("                                        \t0000\tT\tCR01                                                                                                                                \t");
                                fs.WriteLine("SAPLCRA0                                \t0101\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \tNEXT");
                                fs.WriteLine("                                        \t0000\t \tRC68A-WERKS                                                                                                                         \t{0}", o.Plant); // Plant
                                fs.WriteLine("                                        \t0000\t \tRC68A-ARBPL                                                                                                                         \t{0}", o.WorkCenter); // Work Center
                                fs.WriteLine("                                        \t0000\t \tRC68A-VERWE                                                                                                                         \t{0}", o.WorkCenterCategory); // Work Center Category
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VORA");
                                fs.WriteLine("                                        \t0000\t \tP1000-STEXT                                                                                                                         \t{0}", o.WorkCenterDescription); // Work Center Name
                                fs.WriteLine("                                        \t0000\t \tP3000-PLANV                                                                                                                         \t{0}", TaskListUsage); // Task List Usage
                                //fs.WriteLine("                                        \t0000\t \tP3000-RGEKZ                                                                                                                         \t{0}", o.Backflushing); // Indicator: Backflushing
                                fs.WriteLine("                                        \t0000\t \tP3000-VGWTS                                                                                                                         \t{0}", o.StandardValueKeyHeader); // Standard value key Header
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VK11");
                                fs.WriteLine("                                        \t0000\t \tP3003-STEUS                                                                                                                         \t{0}", o.ControlKey); // Control Key
                                fs.WriteLine("                                        \t0000\t \tP3003-STEUS_REF                                                                                                                     \tX"); // Fix Indicator: Control Profile is Referenced
                                if (!string.IsNullOrEmpty(o.UnitOfMeasurement1))
                                {
                                    fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(01)                                                                                                                     \t{0}", o.UnitOfMeasurement1); // Unit of Measurement of Standard Value 1
                                }
                                if (!string.IsNullOrEmpty(o.UnitOfMeasurement2))
                                {
                                    fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(02)                                                                                                                     \t{0}", o.UnitOfMeasurement2); // Unit of Measurement of Standard Value 2
                                }
                                if (!string.IsNullOrEmpty(o.UnitOfMeasurement3))
                                {
                                    fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(03)                                                                                                                     \t{0}", o.UnitOfMeasurement3); // Unit of Measurement of Standard Value 3
                                }
                                if (!string.IsNullOrEmpty(o.UnitOfMeasurement4))
                                {
                                    fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(04)                                                                                                                     \t{0}", o.UnitOfMeasurement4); // Unit of Measurement of Standard Value 4
                                }
                                if (!string.IsNullOrEmpty(o.UnitOfMeasurement5))
                                {
                                    fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(05)                                                                                                                     \t{0}", o.UnitOfMeasurement5); // Unit of Measurement of Standard Value 5
                                }
                                if (!string.IsNullOrEmpty(o.UnitOfMeasurement6))
                                {
                                    fs.WriteLine("                                        \t0000\t \tRC68A-VGEXX(06)                                                                                                                     \t{0}", o.UnitOfMeasurement6); // Unit of Measurement of Standard Value 6
                                }

                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VK11");
                                fs.WriteLine("                                        \t0000\t \tP1001-BEGDA                                                                                                                         \t{0}", o.StartDate.ToString("dd.MM.yyyy", usCulture)); // Start Date
                                fs.WriteLine("                                        \t0000\t \tP1001-ENDDA                                                                                                                         \t{0}", o.EndDate.ToString("dd.MM.yyyy", usCulture)); // End Date
                                fs.WriteLine("                                        \t0000\t \tCRKEYK-KOSTL                                                                                                                        \t{0}", o.CostCenter); // Cost center
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(01)                                                                                                                    \t{0}", "%%%2"); // Activity description no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(02)                                                                                                                    \t{0}", "%%%2"); // Activity description no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(03)                                                                                                                    \t{0}", "%%%2"); // Activity description no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(04)                                                                                                                    \t{0}", "%%%2"); // Activity description no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(05)                                                                                                                    \t{0}", "%%%2"); // Activity description no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-ACTXKT(06)                                                                                                                    \t{0}", "%%%2"); // Activity description no.6 (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=VK11");
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(01)                                                                                                                     \t{0}", ActivityTypeNo1); // Activity type no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(02)                                                                                                                     \t{0}", ActivityTypeNo2); // Activity type no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(03)                                                                                                                     \t{0}", ActivityTypeNo3); // Activity type no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(04)                                                                                                                     \t{0}", ActivityTypeNo4); // Activity type no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(05)                                                                                                                     \t{0}", ActivityTypeNo5); // Activity type no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LARXX(06)                                                                                                                     \t{0}", ActivityTypeNo6); // Activity type no.6 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(01)                                                                                                                     \t{0}", UnitOfActNo1); // Unit of activity no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(02)                                                                                                                     \t{0}", UnitOfActNo2); // Unit of activity no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(03)                                                                                                                     \t{0}", UnitOfActNo3); // Unit of activity no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(04)                                                                                                                     \t{0}", UnitOfActNo4); // Unit of activity no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(05)                                                                                                                     \t{0}", UnitOfActNo5); // Unit of activity no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-LEINH(06)                                                                                                                     \t{0}", UnitOfActNo6); // Unit of activity no.6 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(01)                                                                                                                     \t{0}", FomularKeyNo1); // Fomular key no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(02)                                                                                                                     \t{0}", FomularKeyNo2); // Fomular key no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(03)                                                                                                                     \t{0}", FomularKeyNo3); // Fomular key no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(04)                                                                                                                     \t{0}", FomularKeyNo4); // Fomular key no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(05)                                                                                                                     \t{0}", FomularKeyNo5); // Fomular key no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FORXX(06)                                                                                                                     \t{0}", FomularKeyNo6); // Fomular key no.6 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(01)                                                                                                                   \t{0}", RefIndicatorNo1); // Refference indicator no.1 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(02)                                                                                                                   \t{0}", RefIndicatorNo2); // Refference indicator no.2 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(03)                                                                                                                   \t{0}", RefIndicatorNo3); // Refference indicator no.3 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(04)                                                                                                                   \t{0}", RefIndicatorNo4); // Refference indicator no.4 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(05)                                                                                                                   \t{0}", RefIndicatorNo5); // Refference indicator no.5 (Optional)
                                fs.WriteLine("                                        \t0000\t \tRC68A-FLG_REF(06)                                                                                                                   \t{0}", RefIndicatorNo6); // Refference indicator no.6 (Optional)
                                fs.WriteLine("SAPLCRA0                                \t4000\tX\t                                                                                                                                    \t");
                                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \tUPD");
                            }
                        }

                        var routingGroupList = workCenterRoutingList.GroupBy(u => u.RoutingGroup).Select(grp => grp.ToList()).ToList();
                        // generate create routing header
                        foreach (var aList in routingGroupList)
                        {
                            fs.WriteLine("                                        \t0000\tT\tCA01                                                                                                                                \t");
                            fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=XALU");
                            fs.WriteLine("                                        \t0000\t \tRC27M-MATNR                                                                                                                         \t{0}", aList[0].MaterialCode); // Header Material
                            fs.WriteLine("                                        \t0000\t \tRC27M-WERKS                                                                                                                         \t{0}", aList[0].Plant); // Plant
                            fs.WriteLine("                                        \t0000\t \tRC271-PLNNR                                                                                                                         \t{0}", aList[0].RoutingGroup); // BOM Alternative
                            fs.WriteLine("                                        \t0000\t \tRC271-STTAG                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("SAPLCPDA                                \t1200\tX\t                                                                                                                                    \t");
                            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BU");
                            fs.WriteLine("                                        \t0000\t \tPLKOD-PLNAL                                                                                                                         \t{0}", aList[0].GroupCounter); // Group Counter
                            fs.WriteLine("                                        \t0000\t \tPLKOD-KTEXT                                                                                                                         \t{0}", aList[0].RoutingGroupDescription); // Routing Description
                            fs.WriteLine("                                        \t0000\t \tPLKOD-WERKS                                                                                                                         \t{0}", aList[0].Plant); // Plant
                            fs.WriteLine("                                        \t0000\t \tPLKOD-VERWE                                                                                                                         \t{0}", aList[0].Usage);
                            fs.WriteLine("                                        \t0000\t \tPLKOD-STATU                                                                                                                         \t{0}", aList[0].OverAllStatus);
                            fs.WriteLine("                                        \t0000\t \tPLKOD-LOSVN                                                                                                                         \t{0}", aList[0].LotSizeFrom == 0 ? "" : aList[0].LotSizeFrom.ToString());
                            fs.WriteLine("                                        \t0000\t \tPLKOD-LOSBS                                                                                                                         \t{0}", aList[0].LotSizeTo == 0 ? "" : aList[0].LotSizeTo.ToString());
                            fs.WriteLine("                                        \t0000\t \tPLKOD-PLNME                                                                                                                         \t{0}", aList[0].BaseUnit);
                        }

                        // generate assign material to routing
                        //foreach (var aList in routingGroupList)
                        //{
                        //    int countGroup = 1;
                        //    int countAct = 1;
                        //    foreach (var p in aList)
                        //    {
                        //        var last = aList.Last(); // find last
                        //        if (countGroup == 1)
                        //        {
                        //            fs.WriteLine("                                        \t0000\tT\tCA02                                                                                                                                \t");
                        //            fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                        //            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=ALD1");
                        //            fs.WriteLine("                                        \t0000\t \tRC27M-WERKS                                                                                                                         \t{0}", p.Plant); // Plant
                        //            fs.WriteLine("                                        \t0000\t \tRC271-PLNNR                                                                                                                         \t{0}", p.RoutingGroup); // Routing Group
                        //            fs.WriteLine("                                        \t0000\t \tRC271-STTAG                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                        //            fs.WriteLine("SAPLCPDA                                \t1200\tX\t                                                                                                                                    \t");
                        //            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=MTUE");
                        //            fs.WriteLine("SAPLCZDI                                \t1010\tX\t                                                                                                                                    \t");
                        //            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=P+");
                        //        }

                        //        if (p.Equals(last))
                        //        {
                        //            fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                        //            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=P+");
                        //            fs.WriteLine("                                        \t0000\t \tMAPL-PLNAL(02)                                                                                                                      \t{0}", p.GroupCounter);
                        //            fs.WriteLine("                                        \t0000\t \tMAPL-MATNR(02)                                                                                                                      \t{0}", p.MaterialCode);
                        //            fs.WriteLine("                                        \t0000\t \tMAPL-WERKS(02)                                                                                                                      \t{0}", p.Plant);
                        //            fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                        //            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BACK");
                        //            fs.WriteLine("SAPLCPDI                                \t1200\tX\t                                                                                                                                    \t");
                        //            fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BU");
                        //        }
                        //        else
                        //        {
                        //            if (countGroup == 1)
                        //            {
                        //                fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                        //                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=P");
                        //                fs.WriteLine("                                        \t0000\t \tMAPL-PLNAL(01)                                                                                                                      \t{0}", p.GroupCounter);
                        //                fs.WriteLine("                                        \t0000\t \tMAPL-MATNR(01)                                                                                                                      \t{0}", p.MaterialCode);
                        //                fs.WriteLine("                                        \t0000\t \tMAPL-WERKS(01)                                                                                                                      \t{0}", p.Plant);
                        //            }
                        //            else
                        //            {
                        //                fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                        //                fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=P+");
                        //                fs.WriteLine("                                        \t0000\t \tMAPL-PLNAL(02)                                                                                                                      \t{0}", p.GroupCounter);
                        //                fs.WriteLine("                                        \t0000\t \tMAPL-MATNR(02)                                                                                                                      \t{0}", p.MaterialCode);
                        //                fs.WriteLine("                                        \t0000\t \tMAPL-WERKS(02)                                                                                                                      \t{0}", p.Plant);
                        //            }
                        //        }
                        //        countGroup++;
                        //        countAct++;
                        //    }
                        //}

                        // generate add operation routing
                        foreach (var aList in routingGroupList)
                        {
                            int countGroup = 1;
                            int countAct = 1;
                            foreach (var p in aList)
                            {
                                var last = aList.Last(); // find last
                                // First loop
                                if (countGroup == 1)
                                {
                                    fs.WriteLine("                                        \t0000\tT\tCA02                                                                                                                                \t");
                                    fs.WriteLine("SAPLCPDI                                \t1010\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=XALU");
                                    fs.WriteLine("                                        \t0000\t \tRC27M-MATNR                                                                                                                         \t{0}", p.MaterialCode); // Header Material
                                    fs.WriteLine("                                        \t0000\t \tRC27M-WERKS                                                                                                                         \t{0}", p.Plant); // Plant
                                    fs.WriteLine("                                        \t0000\t \tRC271-PLNNR                                                                                                                         \t{0}", p.RoutingGroup); // Routing Group
                                    fs.WriteLine("                                        \t0000\t \tRC271-STTAG                                                                                                                         \t{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                    fs.WriteLine("                                        \t0000\t \tRC271-PLNAL                                                                                                                         \t{0}", p.GroupCounter); // Group Counter
                                }

                                // Last Item
                                if (p.Equals(last))
                                {
                                    // Init srceen for component
                                    fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                    fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", 2); // Fix
                                    //fs.WriteLine("                                        \t0000\t \tPLPOD-VORNR(02)                                                                                                                     \t{0}", p.OperationNo); // Operation no.
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-ARBPL(02)                                                                                                                     \t{0}", p.WorkCenter); // WorkCenter
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-LTXA1(02)                                                                                                                     \t{0}", p.OperationDecription); // Operation Decription
                                                                                                                                                                                                                                                                         // Operation command
                                    fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=P+");
                                    fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", 1); // Fix
                                                                                                                                                                                                                                                     // Operation detail
                                    fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=PICK");
                                    fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", 3); // Fix
                                    fs.WriteLine("SAPLCPDO                                \t1200\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BACK");
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-VORNR                                                                                                                         \t{0}", p.OperationNo);
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-ARBPL                                                                                                                         \t{0}", p.WorkCenter);
                                    //fs.WriteLine("                                        \t0000\t \tPLPOD-WERKS                                                                                                                         \t{0}", p.Plant);
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-KTSCH                                                                                                                         \t{0}", p.StandardTextKey); // optional
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-LTXA1                                                                                                                         \t{0}", p.OperationDecription);
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-BMSCH                                                                                                                         \t{0}", OperationBaseQuantity);
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-MEINH                                                                                                                         \t{0}", p.OperationOUM);
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-UMREZ                                                                                                                         \t{0}", ConversionOfOUMN);
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-UMREN                                                                                                                         \t{0}", ConversionOfOUMD);
                                    fs.WriteLine("                                        \t0000\t \tPLPOD-AUFAK                                                                                                                         \t{0}", p.Scarp);
                                    fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                    fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BU");
                                    fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", 3); // Fix
                                }
                                else
                                {
                                    // First Item
                                    if (countGroup == 1)
                                    {
                                        // Init srceen for component
                                        fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=PICK");
                                        //fs.WriteLine("                                        \t0000\t \tPLPOD-VORNR                                                                                                                         \t{0}", p.OperationNo); // Operation no.
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-ARBPL(01)                                                                                                                     \t{0}", p.WorkCenter); // WorkCenter
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-LTXA1(01)                                                                                                                     \t{0}", p.OperationDecription); // Operation Decription
                                                                                                                                                                                                                                                                             // Operation detail
                                        fs.WriteLine("SAPLCPDO                                \t1200\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BACK");
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-VORNR                                                                                                                         \t{0}", p.OperationNo);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-ARBPL                                                                                                                         \t{0}", p.WorkCenter);
                                        //fs.WriteLine("                                        \t0000\t \tPLPOD-WERKS                                                                                                                         \t{0}", p.Plant);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-KTSCH                                                                                                                         \t{0}", p.StandardTextKey); // optional
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-LTXA1                                                                                                                         \t{0}", p.OperationDecription);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-BMSCH                                                                                                                         \t{0}", OperationBaseQuantity);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-MEINH                                                                                                                         \t{0}", p.OperationOUM);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-UMREZ                                                                                                                         \t{0}", ConversionOfOUMN);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-UMREN                                                                                                                         \t{0}", ConversionOfOUMD);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-AUFAK                                                                                                                         \t{0}", p.Scarp);
                                    }
                                    // Next Item
                                    else
                                    {
                                        // Init srceen for component
                                        fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t/00");
                                        fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", 1); // Fix
                                        //fs.WriteLine("                                        \t0000\t \tPLPOD-VORNR                                                                                                                         \t{0}", p.OperationNo); // Operation no.
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-ARBPL(02)                                                                                                                     \t{0}", p.WorkCenter); // WorkCenter
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-LTXA1(02)                                                                                                                     \t{0}", p.OperationDecription); // Operation Decription
                                                                                                                                                                                                                                                                             // Operation command
                                        fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=P+");
                                        fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", 1); // Fix
                                        fs.WriteLine("                                        \t0000\t \tRC27X-FLG_SEL(01)                                                                                                                   \t");
                                        // Operation detail
                                        fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=PICK");
                                        fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", 2); // Fix
                                        fs.WriteLine("SAPLCPDO                                \t1200\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=BACK");
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-VORNR                                                                                                                         \t{0}", p.OperationNo);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-ARBPL                                                                                                                         \t{0}", p.WorkCenter);
                                        //fs.WriteLine("                                        \t0000\t \tPLPOD-WERKS                                                                                                                         \t{0}", p.Plant);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-KTSCH                                                                                                                         \t{0}", p.StandardTextKey); // optional
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-LTXA1                                                                                                                         \t{0}", p.OperationDecription);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-BMSCH                                                                                                                         \t{0}", OperationBaseQuantity);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-MEINH                                                                                                                         \t{0}", p.OperationOUM);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-UMREZ                                                                                                                         \t{0}", ConversionOfOUMN);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-UMREN                                                                                                                         \t{0}", ConversionOfOUMD);
                                        fs.WriteLine("                                        \t0000\t \tPLPOD-AUFAK                                                                                                                         \t{0}", p.Scarp);
                                        fs.WriteLine("SAPLCPDI                                \t1400\tX\t                                                                                                                                    \t");
                                        fs.WriteLine("                                        \t0000\t \tBDC_OKCODE                                                                                                                          \t=MALO");
                                        fs.WriteLine("                                        \t0000\t \tRC27X-ENTRY_ACT                                                                                                                     \t{0}", countAct); // Fix
                                    }
                                }
                                countGroup++;
                                countAct++;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Change Delete
        /// </summary>

        public static void ConvertMMBOMToDeleteTextFile(List<BOMHeaderModel> bomHeaderList, List<BOMItemModel> bomItemList, string fileName, string extension, string user)
        {
            string ValidDateFrom = DateTime.Now.ToString("dd.MM.yyyy", usCulture);
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);
            string BOMUsage = "1";

            try
            {
                if (bomHeaderList != null && bomItemList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        foreach (var o in bomHeaderList)
                        {
                            if (String.Equals("1", o.BOMAlt, StringComparison.OrdinalIgnoreCase))
                            {
                                fs.WriteLine("	0000	M	ZCONV_CS05	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                fs.WriteLine("                                        	0000	T	CS05                                                                                                                                	");
                                fs.WriteLine("SAPLCSDI                                	0102	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                                fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", ValidDateFrom); // Valid Date From
                                fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", BOMUsage); // BOMUsage
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MALL");
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCDL");
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                            }
                            else
                            {
                                fs.WriteLine("	0000	M	ZCONV_CS02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                fs.WriteLine("                                        	0000	T	CS02                                                                                                                                	");
                                fs.WriteLine("SAPLCSDI                                	0102	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                                fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", ValidDateFrom); // Valid Date From
                                fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", BOMUsage); // BOMUsage
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=SETP");
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=CLWI");
                                fs.WriteLine("                                        	0000	 	RC29K-SELAL                                                                                                                           	{0}", o.BOMAlt);
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=/CS");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MALL");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCDL");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMWorkCenterToDeleteTextFile(List<WorkCenterModel> workCenterList, string fileName, string extension, string user)
        {
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);

            try
            {
                if (workCenterList != null && workCenterList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        foreach (var o in workCenterList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_CR02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	CR02                                                                                                                                	");
                            fs.WriteLine("SAPLCRA0                                	0100	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	RC68A-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC68A-ARBPL                                                                                                                         	{0}", o.WorkCenter); // Work Center
                            fs.WriteLine("SAPLCRA0                                	4000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=DEL");

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMProductionVersionToChangeTextFile(List<ProductionVersionModel> productionVersionList, string fileName, string extension, string user, DateTime validDate)
        {
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);
            string ValidDateFrom = DateTime.Now.ToString("dd.MM.yyyy", usCulture);
            string ValidDateTo = "31.12.9999";
            string TaskListType = "N";

            try
            {
                if (productionVersionList != null && productionVersionList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        foreach (var o in productionVersionList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_C223	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	C223                                                                                                                                	");
                            fs.WriteLine("SAPLCMFV                                	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ESEL");
                            fs.WriteLine("                                        	0000	 	MKAL-WERKS                                                                                                                          	{0}", o.Plant); // Plant
                            fs.WriteLine("SAPLCMFV                                	1001	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=CRET");
                            fs.WriteLine("                                        	0000		RANG_MAT-LOW                                                                                                                            {0}", o.MaterialCode); // MaterialCode
                            fs.WriteLine("                                        	0000	 	RANG_VER-LOW                                                                                                                            {0}", o.ProductionVersion); // Production Version
                            fs.WriteLine("SAPLCMFV                                	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=DETA");
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-MARK(01)                                                                                                                    X"); // Fix
                            fs.WriteLine("SAPLCMFV                                	2000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                              =ENTE");
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-TEXT1                                                                                                                       {0}", o.ProductionVersionDescription); // Production Version Description
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-ADATU                                                                                                                       {0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From 
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-BDATU                                                                                                                       {0}", ValidDateTo); // Valid Date To
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-PLNTY                                                                                                                       {0}", TaskListType); // TaskList Type
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-PLNNR                                                                                                                       {0}", o.Group); // Group
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-ALNAL                                                                                                                       {0}", o.GroupCounter); // GroupCounter
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-STLAL                                                                                                                       {0}", o.BOMAlt); // BOM Alternative
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-STLAN                                                                                                                       {0}", o.BOMUsage); // BOM Usage
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-MDV01                                                                                                                       {0}", o.ProductionLine); // Production Line
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-ELPRO                                                                                                                       {0}", o.IssueStorageLocation); // Issue Storage Location
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-ALORT                                                                                                                       {0}", o.ReceivingStorageLocation); // Receiving Storage Location
                            fs.WriteLine("SAPLCMFV                                 	2000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=CLOS");
                            fs.WriteLine("SAPLCMFV                                 	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=PRFG");
                            fs.WriteLine("SAPMSSY0                                 	0120	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=RW");
                            fs.WriteLine("SAPLCMFV                                	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=SAVE");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMProductionVersionToDeleteTextFile(List<ProductionVersionModel> productionVersionList, string fileName, string extension, string user, DateTime validDate)
        {
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);

            try
            {
                if (productionVersionList != null && productionVersionList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        foreach (var o in productionVersionList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_C223	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	C223                                                                                                                                	");
                            fs.WriteLine("SAPLCMFV                                	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=ESEL");
                            fs.WriteLine("                                        	0000	 	MKAL-WERKS                                                                                                                          	{0}", o.Plant); // Plant
                            fs.WriteLine("SAPLCMFV                                	1001	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=NONE");
                            fs.WriteLine("                                        	0000		RANG_MAT-LOW                                                                                                                            {0}", o.MaterialCode); // MaterialCode
                            fs.WriteLine("                                        	0000	 	RANG_VER-LOW                                                                                                                            {0}", o.ProductionVersion); // Production Version
                            fs.WriteLine("SAPLCMFV                                	1001	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=CRET");
                            fs.WriteLine("SAPLCMFV                                	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                              =DELE");
                            fs.WriteLine("                                        	0000	 	MKAL_EXPAND-MARK(01)                                                                                                                    X"); // Fix
                            fs.WriteLine("SAPLSPO1                                 	0100	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=YES");
                            fs.WriteLine("SAPLCMFV                                 	1000	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=SAVE");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CCS

        public static void ConvertToCCSBOMTextFile(List<BOMHeaderModel> bomHeaderList, List<BOMItemModel> bomItemList, string fileName, string extension, string user, DateTime validDate)
        {
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);
            string BOMUsage = "1";
            string BOMStatus = "1";

            try
            {
                if (bomHeaderList != null && bomItemList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        // generate create bom header
                        foreach (var o in bomHeaderList)
                        {
                            fs.WriteLine("	0000	M	ZCONV_CS01	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            fs.WriteLine("                                        	0000	T	CS01                                                                                                                                	");
                            fs.WriteLine("SAPLCSDI                                	0100	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                            fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                            fs.WriteLine("                                        	0000	 	RC29N-STLAN                                                                                                                         	{0}", o.BOMAlt); // BOM Alternative
                            fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                            fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", o.BOMUsage); // BOMUsage
                            fs.WriteLine("SAPLCSDI                                	0110	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("                                        	0000	 	RC29K-ZTEXT                                                                                                                         	{0}", o.BOMHeaderText); // BOM Usage
                            fs.WriteLine("                                        	0000	 	RC29K-BMENG                                                                                                                         	{0}", o.BaseQuantity); // Base Quantity
                            fs.WriteLine("                                        	0000	 	RC29K-STLST                                                                                                                         	{0}", BOMStatus); // BOM Status
                            fs.WriteLine("SAPLCSDI                                	0111	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                            fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                            fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                        }

                        // generate delete bom item
                        foreach (var o in bomHeaderList)
                        {
                            if (String.Equals("1", o.Condition, StringComparison.OrdinalIgnoreCase))
                            {
                                fs.WriteLine("	0000	M	ZCONV_CS05	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                fs.WriteLine("                                        	0000	T	CS05                                                                                                                                	");
                                fs.WriteLine("SAPLCSDI                                	0102	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                                fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", BOMUsage); // BOMUsage
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MALL");
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCDL");
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                            }
                            else
                            {
                                fs.WriteLine("	0000	M	ZCONV_CS02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                fs.WriteLine("                                        	0000	T	CS02                                                                                                                                	");
                                fs.WriteLine("SAPLCSDI                                	0102	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                                fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", BOMUsage); // BOMUsage
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=SETP");
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=CLWI");
                                fs.WriteLine("                                        	0000	 	RC29K-SELAL                                                                                                                           	{0}", o.BOMAlt);
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=/CS");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MALL");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCDL");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                            }
                        }

                        // generate bom add new line item 
                        foreach (var o in bomHeaderList)
                        {
                            int count = 1;
                            int BOMItem = 10;
                            foreach (var a in bomItemList)
                            {
                                if (o.MaterialCode == a.MaterialCode)
                                {
                                    List<BOMItemModel> resultList = bomItemList.Where(t => t.MaterialCode == o.MaterialCode).ToList();
                                    BOMItemModel first = resultList.First();

                                    // add component
                                    if (a.Equals(first)) // if not exist
                                    {
                                        // BOM Header
                                        fs.WriteLine("	0000	M	ZCONV_CS02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                        fs.WriteLine("                                        	0000	T	CS02                                                                                                                                	");
                                        fs.WriteLine("SAPLCSDI                                	0100	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", a.MaterialCode); // Header Material
                                        fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", a.Plant); // Plant
                                        fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", a.BOMUsage); // BOMUsage
                                        fs.WriteLine("                                        	0000	 	RC29N-STLAN                                                                                                                         	{0}", a.BOMAlt); // BOM Alternative
                                        fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From

                                        // Init srceen for component
                                        fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-IDNRK(0{0})                                                                                                                     	{1}", count, a.ComponentMaterial); // Component Material
                                        fs.WriteLine("                                        	0000	 	RC29P-POSTP(0{0})                                                                                                                     	L", count); // fix

                                        // BOM Item
                                        fs.WriteLine("SAPLCSDI                                	0130	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-POSNR                                                                                                                         	{0}", a.BOMItem);
                                        fs.WriteLine("                                        	0000	 	RC29P-IDNRK                                                                                                                         	{0}", a.ComponentMaterial);
                                        fs.WriteLine("                                        	0000	 	RC29P-MENGE                                                                                                                         	{0}", a.ComponentQuantity);
                                        fs.WriteLine("                                        	0000	 	RC29P-MEINS                                                                                                                         	{0}", a.ComponentUnit);
                                        fs.WriteLine("                                        	0000	 	RC29P-FMENG                                                                                                                         	{0}", a.FixedQty);
                                        fs.WriteLine("                                        	0000	 	RC29P-AVOAU                                                                                                                         	{0}", a.OperationScrap);
                                        fs.WriteLine("                                        	0000	 	RC29P-AUSCH                                                                                                                         	{0}", a.ComponentScrap);
                                        fs.WriteLine("                                        	0000	 	RC29P-NETAU                                                                                                                         	{0}", a.NetScrap);
                                        fs.WriteLine("SAPLCSDI                                	0131	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-SANKA                                                                                                                         	X");
                                        fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                                    }
                                    else
                                    {
                                        // BOM Header
                                        fs.WriteLine("	0000	M	ZCONV_CS02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                        fs.WriteLine("                                        	0000	T	CS02                                                                                                                                	");
                                        fs.WriteLine("SAPLCSDI                                	0100	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", a.MaterialCode); // Header Material
                                        fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", a.Plant); // Plant
                                        fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", a.BOMUsage); // BOMUsage
                                        fs.WriteLine("                                        	0000	 	RC29N-STLAN                                                                                                                         	{0}", a.BOMAlt); // BOM Alternative
                                        fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", validDate.ToString("dd.MM.yyyy", usCulture)); // Valid Date From
                                        fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");

                                        // Init srceen for component
                                        fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-IDNRK(0{0})                                                                                                                     	{1}", count, a.ComponentMaterial); // Component Material
                                        fs.WriteLine("                                        	0000	 	RC29P-POSTP(0{0})                                                                                                                     	L", count); // fix

                                        // BOM Item
                                        fs.WriteLine("SAPLCSDI                                	0130	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-POSNR                                                                                                                         	{0}", a.BOMItem);
                                        fs.WriteLine("                                        	0000	 	RC29P-IDNRK                                                                                                                         	{0}", a.ComponentMaterial);
                                        fs.WriteLine("                                        	0000	 	RC29P-MENGE                                                                                                                         	{0}", a.ComponentQuantity);
                                        fs.WriteLine("                                        	0000	 	RC29P-MEINS                                                                                                                         	{0}", a.ComponentUnit);
                                        fs.WriteLine("                                        	0000	 	RC29P-FMENG                                                                                                                         	{0}", a.FixedQty);
                                        fs.WriteLine("                                        	0000	 	RC29P-AVOAU                                                                                                                         	{0}", a.OperationScrap);
                                        fs.WriteLine("                                        	0000	 	RC29P-AUSCH                                                                                                                         	{0}", a.ComponentScrap);
                                        fs.WriteLine("                                        	0000	 	RC29P-NETAU                                                                                                                         	{0}", a.NetScrap);
                                        fs.WriteLine("SAPLCSDI                                	0131	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                        fs.WriteLine("                                        	0000	 	RC29P-SANKA                                                                                                                         	X");
                                        fs.WriteLine("SAPLCSDI                                	0140	X	                                                                                                                                    	");
                                        fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                                    }

                                    count++;
                                    BOMItem += 10;
                                }
                            }
                        }
                    } // StreamWriter
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertCCSBOMToDeleteTextFile(List<BOMHeaderModel> bomHeaderList, List<BOMItemModel> bomItemList, string fileName, string extension, string user)
        {
            string ValidDateFrom = DateTime.Now.ToString("dd.MM.yyyy", usCulture);
            string HeaderDate = DateTime.Now.ToString("ddMMyyyy", usCulture);
            string HeaderTime = DateTime.Now.ToString("HHmmss", usCulture);
            string BOMUsage = "1";

            try
            {
                if (bomHeaderList != null && bomItemList != null)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file
                    using (StreamWriter fs = File.CreateText(fileName + extension))
                    {
                        foreach (var o in bomHeaderList)
                        {
                            if (String.Equals("1", o.BOMAlt, StringComparison.OrdinalIgnoreCase))
                            {
                                fs.WriteLine("	0000	M	ZCONV_CS05	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                fs.WriteLine("                                        	0000	T	CS05                                                                                                                                	");
                                fs.WriteLine("SAPLCSDI                                	0102	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                                fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", ValidDateFrom); // Valid Date From
                                fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", BOMUsage); // BOMUsage
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MALL");
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCDL");
                                fs.WriteLine("SAPLCSDI                                	0160	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                            }
                            else
                            {
                                fs.WriteLine("	0000	M	ZCONV_CS02	{0}          {1}  {2}", user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                                fs.WriteLine("                                        	0000	T	CS02                                                                                                                                	");
                                fs.WriteLine("SAPLCSDI                                	0102	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	/00");
                                fs.WriteLine("                                        	0000	 	RC29N-MATNR                                                                                                                         	{0}", o.MaterialCode); // Header Material
                                fs.WriteLine("                                        	0000	 	RC29N-WERKS                                                                                                                         	{0}", o.Plant); // Plant
                                fs.WriteLine("                                        	0000	 	RC29N-DATUV                                                                                                                         	{0}", ValidDateFrom); // Valid Date From
                                fs.WriteLine("                                        	0000	 	RC29N-STLAL                                                                                                                         	{0}", BOMUsage); // BOMUsage
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=SETP");
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=CLWI");
                                fs.WriteLine("                                        	0000	 	RC29K-SELAL                                                                                                                           	{0}", o.BOMAlt);
                                fs.WriteLine("SAPLCSDI                                	0180	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=/CS");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=MALL");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCDL");
                                fs.WriteLine("SAPLCSDI                                	0150	X	                                                                                                                                    	");
                                fs.WriteLine("                                        	0000	 	BDC_OKCODE                                                                                                                          	=FCBU");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}