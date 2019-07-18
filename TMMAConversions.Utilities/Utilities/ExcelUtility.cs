//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.OleDb;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TMMAConversions.DAL.Models;

//namespace TMMAConversions.Utilities.Utilities
//{
//    public class ExcelUtility
//    {
//        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

//        public static DataTable ReadExcel(string fileName, string fileExt)
//        {
//            log4net.Config.XmlConfigurator.Configure();
//            string conn = string.Empty;
//            DataTable dtexcel = new DataTable();
//            if (fileExt.CompareTo(".xls") == 0)
//                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
//            else
//                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
//            using (OleDbConnection con = new OleDbConnection(conn))
//            {
//                try
//                {
//                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Grade-Level$]", con); //here we read data from Grade-Level  
//                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
//                }
//                catch (Exception ex)
//                {
//                    log.Error("========== " + ex.Message + " =========");
//                    throw ex;
//                }
//            }

//            ConvertBOMFileToBOMModel(dtexcel);

//            return dtexcel;
//        }

//        public static void ConvertBOMFileToBOMModel(DataTable dtExcel)
//        {
//            try
//            {
//                int rowCount = dtExcel.Rows.Count;
//                int colCount = dtExcel.Columns.Count;
//                List<MMBOMHeaderModel> MMBOMHeaderList = new List<MMBOMHeaderModel>();
//                List<MMBOMItemModel> MMBOMItemList = new List<MMBOMItemModel>();

//                for (int i = 4; i < rowCount; i++)
//                {
//                    for (int j = 2; j < colCount; j++)
//                    {
//                        // Create BOM Item
//                        if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[j].ToString()))
//                        {
//                            MMBOMItemModel MMBOMItem = new MMBOMItemModel();
//                            MMBOMItem.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
//                            MMBOMItem.Plant = dtExcel.Rows[1].ItemArray[j].ToString();
//                            MMBOMItem.Quantity = Convert.ToInt16(dtExcel.Rows[i].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[j].ToString());
//                            MMBOMItem.Component = dtExcel.Rows[i].ItemArray[0].ToString();
//                            MMBOMItemList.Add(MMBOMItem);
//                        }

//                        if (i == 4) // first time
//                        {
//                            // Create BOM Header
//                            MMBOMHeaderModel MMBOMHeader = new MMBOMHeaderModel();
//                            MMBOMHeader.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
//                            MMBOMHeader.Plant = dtExcel.Rows[1].ItemArray[j].ToString();
//                            MMBOMHeader.BaseQuality = Convert.ToInt16(dtExcel.Rows[2].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[2].ItemArray[j].ToString());
//                            MMBOMHeader.BOMHeaderText = dtExcel.Rows[3].ItemArray[j].ToString();
//                            MMBOMHeaderList.Add(MMBOMHeader);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static DataTable ReadExcelInterop(string fileName, string fileExt)
//        {
//            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
//            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName);
//            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
//            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
//            try
//            {
//                int rowCount = xlRange.Rows.Count;
//                int colCount = xlRange.Columns.Count;

//                //dt.Column = colCount;
//                //dataGridMonomerProducts.ColumnCount = colCount;
//                //dataGridMonomerProducts.RowCount = rowCount;

//                DataTable dtExcel = new DataTable();

//                for (int i = 1; i <= rowCount; i++)
//                {
//                    for (int j = 1; j <= colCount; j++)
//                    {
//                        //write the value to the Grid  
//                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
//                        {
//                            //dataGridMonomerProducts.Rows[i - 1].Cells[j - 1].Value = xlRange.Cells[i, j].Value2.ToString();
//                            //dataGridMonomerProducts.Rows[i - 1].Cells[j - 1].Value = xlRange.Cells[i, j].Value2.ToString();
//                            //dtExcel.Rows[i - 1].ItemArray[j - 1] = xlRange.Cells[i, j].Value2.ToString();
//                        }
//                        // Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");  

//                        //add useful things here!
//                    }
//                }

//                return dtExcel;
//            }
//            catch (Exception ex)
//            {
//                log.Error("========== " + ex.Message + " =========");
//                throw ex;
//            }
//        }
//    }
//}
