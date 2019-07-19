using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL.Utilities
{
    public class ExcelUtility
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected static CultureInfo usCulture = new CultureInfo("en-US");
        #region Monomer

        private static DataTable GetSchemaTable(string connectionString)
        {
            using (OleDbConnection connection = new
                       OleDbConnection(connectionString))
            {
                connection.Open();
                DataTable schemaTable = connection.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "TABLE" });
                return schemaTable;
            }
        }

        public static List<DataTable> ReadMMABOMExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;

            string[] sheets = {
                "MMA Grade",
                "MMA Loading"
            };

            List<DataTable> dtExcelList = new List<DataTable>();

            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    foreach (string r in sheets)
                    {
                        DataTable dtExcel = new DataTable();
                        string query = "SELECT * FROM [" + r + "$]";
                        OleDbDataAdapter data = new OleDbDataAdapter(query, con);
                        data.Fill(dtExcel);
                        dtExcelList.Add(dtExcel);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtExcelList;
        }

        public static List<DataTable> ReadMMABOMActivityExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;

            string[] sheets = {
                "MMA Grade Activity",
                "MMA Loading Activity"
            };

            List<DataTable> dtExcelList = new List<DataTable>();

            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    foreach (string r in sheets)
                    {
                        DataTable dtExcel = new DataTable();
                        string query = "SELECT * FROM [" + r + "$]";
                        OleDbDataAdapter data = new OleDbDataAdapter(query, con);
                        data.Fill(dtExcel);
                        dtExcelList.Add(dtExcel);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtExcelList;
        }

        public static List<DataTable> ReadCCSBOMExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;

            string[] sheets = {
                    "BOM Special Pack",
                    "BOM CCS Cut and Pack",
                    "BOM CCS PMMA",
                    "BOM Additive",
                    "BOM CCS Syrup",
                    "BOM CCS Initiator",
                    "BOM Packing Pattern",
                    "BOM Gasket"
                };

            List<DataTable> dtExcelList = new List<DataTable>();

            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    foreach (string r in sheets)
                    {
                        DataTable dtExcel = new DataTable();
                        string query = "SELECT * FROM [" + r + "$]";
                        OleDbDataAdapter data = new OleDbDataAdapter(query, con);
                        data.Fill(dtExcel);
                        dtExcelList.Add(dtExcel);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtExcelList;
        }

        public static List<DataTable> ReadCCSBOMActivityExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;

            string[] sheets = {
                    "BOM Special Pack Activity",
                    "BOM CCS Cut and Pack Activity",
                    "BOM CCS PMMA Activity",
                    "BOM Additive Activity",
                    "BOM CCS Syrup Activity",
                    "BOM CCS Initiator Activity",
                    "BOM Packing Pattern Activity",
                    "BOM Gasket Activity"
                };

            List<DataTable> dtExcelList = new List<DataTable>();

            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    foreach (string r in sheets)
                    {
                        DataTable dtExcel = new DataTable();
                        string query = "SELECT * FROM [" + r + "$]";
                        OleDbDataAdapter data = new OleDbDataAdapter(query, con);
                        data.Fill(dtExcel);
                        dtExcelList.Add(dtExcel);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtExcelList;
        }

        public static DataTable ReadMMBOMGradeLevelExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Grade-Level$]", con); //here we read data from Grade-Level  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtexcel;
        }

        public static DataTable ReadMMBOMPkgLevelExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Pkg-Level$]", con); //here we read data from Grade-Level  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtexcel;
        }

        public static DataTable ReadMMWorkCenterExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [WC$]", con); //here we read data from Sheet1
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtexcel;
        }

        public static DataTable ReadMMProductionVersionExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [ROC$]", con); //here we read data from ROC
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtexcel;
        }

        public static DataTable ReadMMRoutingExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [RT$]", con); //here we read data from Sheet1
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtexcel;
        }

        public static DataTable ReadMMInspectionPlanExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [RT$]", con); //here we read data from Sheet1
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtexcel;
        }

        public static DataTable ReadMMPackagingInstructionExcel(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [PI$]", con); //here we read data from Sheet1
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtexcel;
        }

        public static List<DataTable> ReadWorkCenterRoutingExcelList(string fileName, string fileExt)
        {
            log4net.Config.XmlConfigurator.Configure();
            string conn = string.Empty;

            string[] sheets = {
                        "MMA Grade",
                        "MMA Loading",
                        "CCS Syrup",
                        "CCS Initiator",
                        "CCS Additive",
                        "CCS Casting",
                        "CCS Cut and Pack",
                        "CCS Cut and Pack Cullet",
                        "CCS Gasket",
                        "CCS Roof",
                        "CCS Heat Sealing",
                        "CCS Reprocess"
                    };

            List<DataTable> dtExcelList = new List<DataTable>();

            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    foreach (string r in sheets)
                    {
                        DataTable dtExcel = new DataTable();
                        string query = "SELECT * FROM [" + r + "$]";
                        OleDbDataAdapter data = new OleDbDataAdapter(query, con);
                        data.Fill(dtExcel);
                        dtExcelList.Add(dtExcel);
                    }

                }
                catch (Exception ex)
                {
                    log.Error("========== " + ex.Message + " =========");
                    throw ex;
                }
            }

            return dtExcelList;
        }

        public static void ConvertMMBOMGradeExcelToMMBOMGradeModel(DataTable dtExcel, ref List<BOMHeaderModel> BOMHeaderList, ref List<BOMItemModel> BOMItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    BOMHeaderList = new List<BOMHeaderModel>();
                    BOMItemList = new List<BOMItemModel>();

                    for (int i = 7; i < rowCount; i++)
                    {
                        for (int j = 6; j < colCount; j++)
                        {
                            // Create BOM Item
                            if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[j].ToString()))
                            {
                                BOMItemModel BOMItem = new BOMItemModel();
                                BOMItem.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                BOMItem.Plant = dtExcel.Rows[1].ItemArray[j].ToString();
                                BOMItem.ComponentQuantity = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[j].ToString());
                                BOMItem.ComponentMaterial = dtExcel.Rows[i].ItemArray[1].ToString();
                                BOMItem.ComponentUnit = dtExcel.Rows[i].ItemArray[2].ToString();
                                BOMItem.ComponentDescription = dtExcel.Rows[i].ItemArray[3].ToString();
                                BOMItem.ComponentScrap = dtExcel.Rows[i].ItemArray[4].ToString();
                                BOMItemList.Add(BOMItem);
                            }

                            if (i == 7) // first time
                            {
                                // Create BOM Header
                                BOMHeaderModel BOMHeader = new BOMHeaderModel();
                                BOMHeader.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                BOMHeader.BOMHeaderText = dtExcel.Rows[1].ItemArray[j].ToString();
                                BOMHeader.Plant = dtExcel.Rows[2].ItemArray[j].ToString();
                                BOMHeader.BaseQuantity = Convert.ToDecimal(dtExcel.Rows[3].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[3].ItemArray[j].ToString());
                                BOMHeader.BaseUnit = dtExcel.Rows[4].ItemArray[j].ToString();
                                BOMHeader.BOMAlt = dtExcel.Rows[5].ItemArray[j].ToString();
                                BOMHeaderList.Add(BOMHeader);
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

        public static void ConvertMMABOMExcelToMMABOMModel(DataTable dtExcel, ref List<BOMHeaderModel> BOMHeaderList, ref List<BOMItemModel> BOMItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    BOMHeaderList = new List<BOMHeaderModel>();
                    BOMItemList = new List<BOMItemModel>();

                    for (int i = 9; i < rowCount; i++)
                    {
                        for (int j = 10; j < colCount; j++)
                        {
                            if (!string.IsNullOrEmpty(dtExcel.Rows[0].ItemArray[j].ToString())) // Material Header
                            {
                                // Create BOM Item get only ComponentQuantity has value or not 0
                                if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[j].ToString()) && (dtExcel.Rows[i].ItemArray[j].ToString() != "0" && dtExcel.Rows[i].ItemArray[j].ToString() != "0.000"))
                                {
                                    // get Header
                                    BOMItemModel BOMItem = new BOMItemModel();
                                    BOMItem.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                    BOMItem.Plant = dtExcel.Rows[2].ItemArray[j].ToString();
                                    BOMItem.BOMUsage = dtExcel.Rows[5].ItemArray[j].ToString();
                                    BOMItem.BOMAlt = dtExcel.Rows[6].ItemArray[j].ToString();
                                    // get ComponentQuantity
                                    BOMItem.ComponentQuantity = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[j].ToString());
                                    // get Component
                                    BOMItem.BOMItem = dtExcel.Rows[i].ItemArray[1].ToString();
                                    BOMItem.ComponentMaterial = dtExcel.Rows[i].ItemArray[2].ToString();
                                    BOMItem.ComponentUnit = dtExcel.Rows[i].ItemArray[3].ToString();
                                    BOMItem.ComponentDescription = dtExcel.Rows[i].ItemArray[4].ToString();
                                    BOMItem.FixedQty = dtExcel.Rows[i].ItemArray[5].ToString();
                                    BOMItem.CostingRelevency = dtExcel.Rows[i].ItemArray[6].ToString();
                                    BOMItem.OperationScrap = dtExcel.Rows[i].ItemArray[7].ToString();
                                    BOMItem.ComponentScrap = dtExcel.Rows[i].ItemArray[8].ToString();
                                    BOMItemList.Add(BOMItem);
                                }

                                if (i == 9) // first time
                                {
                                    // Create BOM Header
                                    BOMHeaderModel BOMHeader = new BOMHeaderModel();
                                    BOMHeader.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                    BOMHeader.BOMHeaderText = dtExcel.Rows[1].ItemArray[j].ToString();
                                    BOMHeader.Plant = dtExcel.Rows[2].ItemArray[j].ToString();
                                    BOMHeader.BaseQuantity = Convert.ToDecimal(dtExcel.Rows[3].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[3].ItemArray[j].ToString());
                                    BOMHeader.BaseUnit = dtExcel.Rows[4].ItemArray[j].ToString();
                                    BOMHeader.BOMUsage = dtExcel.Rows[5].ItemArray[j].ToString();
                                    BOMHeader.BOMAlt = dtExcel.Rows[6].ItemArray[j].ToString();
                                    BOMHeader.RoutingGroup = dtExcel.Rows[7].ItemArray[j].ToString();
                                    BOMHeaderList.Add(BOMHeader);
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

        public static void ConvertMMABOMActivityExcelToMMABOMActivityModel(DataTable dtExcel, ref List<BOMHeaderModel> BOMHeaderList, ref List<BOMItemModel> BOMItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    BOMHeaderList = new List<BOMHeaderModel>();
                    BOMItemList = new List<BOMItemModel>();

                    for (int i = 13; i < rowCount; i++) // first row
                    {
                        for (int j = 10; j < colCount; j++) // first col
                        {
                            if (!string.IsNullOrEmpty(dtExcel.Rows[0].ItemArray[j].ToString())) // Material Header
                            {
                                // Create BOM Item get only standard value has value and not 0
                                if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[j].ToString()) && (dtExcel.Rows[i].ItemArray[j].ToString() != "0" && dtExcel.Rows[i].ItemArray[j].ToString() != "0.000"))
                                {
                                    BOMItemModel BOMItem = new BOMItemModel();
                                    // get header
                                    BOMItem.RoutingGroup = dtExcel.Rows[7].ItemArray[j].ToString();
                                    BOMItem.Plant = dtExcel.Rows[2].ItemArray[j].ToString();
                                    BOMItem.BOMUsage = dtExcel.Rows[5].ItemArray[j].ToString();
                                    BOMItem.BOMAlt = dtExcel.Rows[6].ItemArray[j].ToString();
                                    // get standard value
                                    BOMItem.StandardValueKey = dtExcel.Rows[i].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[j].ToString();
                                    // get component
                                    BOMItem.ActivityNo = Convert.ToInt32(dtExcel.Rows[i].ItemArray[1].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[1].ToString());
                                    BOMItem.OperationNo = dtExcel.Rows[i].ItemArray[2].ToString();
                                    BOMItem.Activity = dtExcel.Rows[i].ItemArray[3].ToString();
                                    BOMItem.StandardValueKeyText = dtExcel.Rows[i].ItemArray[4].ToString();
                                    BOMItem.StandardValueKeyOUM = dtExcel.Rows[i].ItemArray[5].ToString();
                                    BOMItem.ComponentDescription = dtExcel.Rows[i].ItemArray[6].ToString();
                                    BOMItem.ComponentScrap = dtExcel.Rows[i].ItemArray[8].ToString();
                                    BOMItemList.Add(BOMItem);
                                }

                                if (i == 13) // first loop
                                {
                                    // Create BOM Header
                                    BOMHeaderModel BOMHeader = new BOMHeaderModel();
                                    BOMHeader.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                    BOMHeader.BOMHeaderText = dtExcel.Rows[1].ItemArray[j].ToString();
                                    BOMHeader.Plant = dtExcel.Rows[2].ItemArray[j].ToString();
                                    BOMHeader.BaseQuantity = Convert.ToDecimal(dtExcel.Rows[3].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[3].ItemArray[j].ToString());
                                    BOMHeader.BaseUnit = dtExcel.Rows[4].ItemArray[j].ToString();
                                    BOMHeader.BOMUsage = dtExcel.Rows[5].ItemArray[j].ToString();
                                    BOMHeader.BOMAlt = dtExcel.Rows[6].ItemArray[j].ToString();
                                    BOMHeader.RoutingGroup = dtExcel.Rows[7].ItemArray[j].ToString();
                                    BOMHeader.GroupCounter = dtExcel.Rows[8].ItemArray[j].ToString();
                                    BOMHeader.ProductionVersion = dtExcel.Rows[9].ItemArray[j].ToString();
                                    BOMHeader.WorkCenter = dtExcel.Rows[10].ItemArray[j].ToString();
                                    BOMHeader.StorageLocation = dtExcel.Rows[11].ItemArray[j].ToString();
                                    BOMHeaderList.Add(BOMHeader);
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

        public static void ConvertMMBOMGradeExcelAddLineItemModel(DataTable dtExcel, ref List<BOMHeaderModel> BOMHeaderList, ref List<BOMItemModel> BOMItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    BOMHeaderList = new List<BOMHeaderModel>();
                    BOMItemList = new List<BOMItemModel>();

                    for (int i = 7; i < rowCount; i++)
                    {
                        for (int j = 6; j < colCount; j++)
                        {
                            // Create BOM Item
                            if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[j].ToString()))
                            {
                                BOMItemModel BOMItem = new BOMItemModel();
                                BOMItem.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                BOMItem.Plant = dtExcel.Rows[1].ItemArray[j].ToString();
                                BOMItem.ComponentQuantity = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[j].ToString());
                                BOMItem.ComponentMaterial = dtExcel.Rows[i].ItemArray[1].ToString();
                                BOMItem.ComponentUnit = dtExcel.Rows[i].ItemArray[2].ToString();
                                BOMItem.ComponentDescription = dtExcel.Rows[i].ItemArray[3].ToString();
                                BOMItem.ComponentScrap = dtExcel.Rows[i].ItemArray[4].ToString();
                                BOMItemList.Add(BOMItem);
                            }

                            if (i == 7) // first time
                            {
                                // Create BOM Header
                                BOMHeaderModel BOMHeader = new BOMHeaderModel();
                                BOMHeader.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                BOMHeader.BOMHeaderText = dtExcel.Rows[1].ItemArray[j].ToString();
                                BOMHeader.Plant = dtExcel.Rows[2].ItemArray[j].ToString();
                                BOMHeader.BaseQuantity = Convert.ToDecimal(dtExcel.Rows[3].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[3].ItemArray[j].ToString());
                                BOMHeader.BaseUnit = dtExcel.Rows[4].ItemArray[j].ToString();
                                BOMHeader.BOMAlt = dtExcel.Rows[5].ItemArray[j].ToString();
                                BOMHeaderList.Add(BOMHeader);
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

        public static void ConvertMMBOMPkgExcelToMMBOMPkgModel(DataTable dtExcel, ref List<BOMHeaderModel> BOMHeaderList, ref List<BOMItemModel> BOMItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    BOMHeaderList = new List<BOMHeaderModel>();
                    BOMItemList = new List<BOMItemModel>();

                    for (int i = 7; i < rowCount; i++)
                    {
                        for (int j = 6; j < colCount; j++)
                        {
                            // Create BOM Item
                            if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[j].ToString()))
                            {
                                BOMItemModel BOMItem = new BOMItemModel();
                                BOMItem.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                BOMItem.Plant = dtExcel.Rows[1].ItemArray[j].ToString();
                                BOMItem.ComponentQuantity = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[j].ToString());
                                BOMItem.ComponentMaterial = dtExcel.Rows[i].ItemArray[1].ToString();
                                BOMItem.ComponentUnit = dtExcel.Rows[i].ItemArray[2].ToString();
                                BOMItem.ComponentDescription = dtExcel.Rows[i].ItemArray[3].ToString();
                                BOMItem.ComponentScrap = dtExcel.Rows[i].ItemArray[4].ToString();
                                BOMItemList.Add(BOMItem);
                            }

                            if (i == 7) // first time
                            {
                                // Create BOM Header
                                BOMHeaderModel BOMHeader = new BOMHeaderModel();
                                BOMHeader.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                BOMHeader.BOMHeaderText = dtExcel.Rows[1].ItemArray[j].ToString();
                                BOMHeader.Plant = dtExcel.Rows[2].ItemArray[j].ToString();
                                BOMHeader.BaseQuantity = Convert.ToDecimal(dtExcel.Rows[3].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[3].ItemArray[j].ToString());
                                BOMHeader.BaseUnit = dtExcel.Rows[4].ItemArray[j].ToString();
                                BOMHeader.BOMAlt = dtExcel.Rows[5].ItemArray[j].ToString();
                                BOMHeaderList.Add(BOMHeader);
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

        public static void ConvertMMWorkCenterExcelToMMWorkCenterModel(DataTable dtExcel, ref List<WorkCenterModel> WorkCenterList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    WorkCenterList = new List<WorkCenterModel>();

                    for (int i = 1; i < rowCount; i++)
                    {
                        // Create WorkCenter
                        // Column A - AD (0 - 29)
                        WorkCenterModel workCenter = new WorkCenterModel();
                        workCenter.WorkCenter = dtExcel.Rows[i].ItemArray[0].ToString(); // col A (1) (Work Center)
                        workCenter.WorkCenterName = dtExcel.Rows[i].ItemArray[1].ToString(); // col B (2) (Work Center Name)
                        workCenter.Plant = dtExcel.Rows[i].ItemArray[2].ToString(); // col C (3) (Plant)
                        workCenter.WorkCenterCategory = dtExcel.Rows[i].ItemArray[3].ToString(); // col D (4) (Work Center Category)

                        workCenter.PersonResponsible = dtExcel.Rows[i].ItemArray[4].ToString(); // col E (5) (Responsible)
                        workCenter.StandardValueKey = dtExcel.Rows[i].ItemArray[5].ToString(); // col F (6) (SVK)
                        workCenter.StandardValueKeyName = dtExcel.Rows[i].ItemArray[6].ToString(); // col G (7) (SVK Name)
                        workCenter.ControlKey = dtExcel.Rows[i].ItemArray[7].ToString(); // col H (8) (Ctrl)
                        workCenter.UnitOfMeasurement = dtExcel.Rows[i].ItemArray[8].ToString(); // col I (9) (Unit of Measurement of Standard Value)
                        workCenter.StartDate = DateTime.ParseExact(dtExcel.Rows[i].ItemArray[9].ToString(), "dd/MM/yyyy", usCulture); // col J (10) Start Date
                        workCenter.EndDate = DateTime.ParseExact(dtExcel.Rows[i].ItemArray[10].ToString(), "dd/MM/yyyy", usCulture); // col  K  (11) End Date
                        workCenter.CostCenter = dtExcel.Rows[i].ItemArray[11].ToString(); // col L (12) (Cost Center)

                        workCenter.ActivityTypeNo1 = dtExcel.Rows[i].ItemArray[12].ToString(); // col M (13) (Activity 1)
                        workCenter.ActivityDescNo1 = dtExcel.Rows[i].ItemArray[13].ToString(); // col N (14) (Activity Description 1)
                        workCenter.UnitOfActNo1 = dtExcel.Rows[i].ItemArray[14].ToString(); // col O (15) (Activity Unit 1)
                        workCenter.ActivityTypeNo2 = dtExcel.Rows[i].ItemArray[15].ToString(); // col P (16) (Activity 2)
                        workCenter.ActivityDescNo2 = dtExcel.Rows[i].ItemArray[16].ToString(); // col Q (17) (Activity Description 2)
                        workCenter.UnitOfActNo2 = dtExcel.Rows[i].ItemArray[17].ToString(); // col R (18) (Activity Unit 2)
                        workCenter.ActivityTypeNo3 = dtExcel.Rows[i].ItemArray[18].ToString(); // col S (19) (Activity 3)
                        workCenter.ActivityDescNo3 = dtExcel.Rows[i].ItemArray[19].ToString(); // col T (20) (Activity Description 3)
                        workCenter.UnitOfActNo3 = dtExcel.Rows[i].ItemArray[20].ToString(); // col U (21) (Activity Unit 3)
                        workCenter.ActivityTypeNo4 = dtExcel.Rows[i].ItemArray[21].ToString(); // col V (22) (Activity 4)
                        workCenter.ActivityDescNo4 = dtExcel.Rows[i].ItemArray[22].ToString(); // col W (23) (Activity Description 4)
                        workCenter.UnitOfActNo4 = dtExcel.Rows[i].ItemArray[23].ToString(); // col X (24) (Activity Unit 4)
                        workCenter.ActivityTypeNo5 = dtExcel.Rows[i].ItemArray[24].ToString(); // col Y (25) (Activity 5)
                        workCenter.ActivityDescNo5 = dtExcel.Rows[i].ItemArray[25].ToString(); // col Z (26) (Activity Description 5)
                        workCenter.UnitOfActNo5 = dtExcel.Rows[i].ItemArray[26].ToString(); // col AA (27) (Activity Unit 5)
                        workCenter.ActivityTypeNo6 = dtExcel.Rows[i].ItemArray[27].ToString(); // col AB (28) (Activity 6)
                        workCenter.ActivityDescNo6 = dtExcel.Rows[i].ItemArray[28].ToString(); // col AC (29) (Activity Description 6)
                        workCenter.UnitOfActNo6 = dtExcel.Rows[i].ItemArray[29].ToString(); // col AD (30) (Activity Unit 6)
                        WorkCenterList.Add(workCenter);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMProductionVersionExcelToMMProductionVersionModel(DataTable dtExcel, ref List<ProductionVersionModel> ProductionVersionList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    ProductionVersionList = new List<ProductionVersionModel>();

                    for (int i = 1; i < rowCount; i++)
                    {
                        ProductionVersionModel ProductionVersion = new ProductionVersionModel();
                        ProductionVersion.MaterialCode = dtExcel.Rows[i].ItemArray[0].ToString(); // col 1 (MaterialCode)
                        ProductionVersion.Plant = dtExcel.Rows[i].ItemArray[1].ToString(); // col 2 (Plant)
                        ProductionVersion.ProductionVersion = dtExcel.Rows[i].ItemArray[2].ToString(); // col 3 (Production Version)
                        ProductionVersion.ProductionVersionDescription = dtExcel.Rows[i].ItemArray[3].ToString(); // col 4 (ProductionVersionDescription)
                        ProductionVersion.Group = dtExcel.Rows[i].ItemArray[4].ToString(); // col 5 (Group)
                        ProductionVersion.GroupCounter = dtExcel.Rows[i].ItemArray[5].ToString(); // col 6 (GroupCounter)
                        ProductionVersion.BOMUsage = dtExcel.Rows[i].ItemArray[6].ToString(); // col 7 (BOM Usage)
                        ProductionVersion.BOMAlt = dtExcel.Rows[i].ItemArray[7].ToString(); // col 8 (BOM Alternative)
                        ProductionVersion.ProductionLine = dtExcel.Rows[i].ItemArray[8].ToString(); // col 9 (Production Line)
                        ProductionVersion.IssueStorageLocation = dtExcel.Rows[i].ItemArray[9].ToString(); // col 10 (Issue Storage Location)
                        ProductionVersion.ReceivingStorageLocation = dtExcel.Rows[i].ItemArray[10].ToString(); // col 11 (Receiving Storage Location)
                        ProductionVersionList.Add(ProductionVersion);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMRoutingExcelToMMRoutingModel(DataTable dtExcel, ref List<RoutingHeaderModel> routingHeaderList, ref List<RoutingItemModel> routingItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    routingHeaderList = new List<RoutingHeaderModel>();
                    routingItemList = new List<RoutingItemModel>();

                    for (int i = 1; i < rowCount; i++)
                    {
                        // Create Routing Header
                        // Column A - K
                        if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[0].ToString()) && !string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[0].ToString())) // Material Code not null
                        {
                            RoutingHeaderModel routingHeader = new RoutingHeaderModel();
                            routingHeader.MaterialCode = dtExcel.Rows[i].ItemArray[0].ToString(); // col A (1) (Material Code)
                            routingHeader.MaterialDescription = dtExcel.Rows[i].ItemArray[1].ToString(); // col B (2) (Material Description)
                            routingHeader.Plant = dtExcel.Rows[i].ItemArray[2].ToString(); // col C (3) (Plant)
                            routingHeader.RoutingGroup = dtExcel.Rows[i].ItemArray[3].ToString(); // col D (4) (Routing Group)
                            routingHeader.GroupCounter = dtExcel.Rows[i].ItemArray[4].ToString(); // col F (5) (Group Counter)
                            routingHeader.RoutingDescription = dtExcel.Rows[i].ItemArray[5].ToString(); // col G (6) (Routing Description)
                            routingHeader.Usage = dtExcel.Rows[i].ItemArray[6].ToString(); // col H (7) (Usage)
                            routingHeader.OverAllStatus = dtExcel.Rows[i].ItemArray[7].ToString(); // col I (8) (Overal all status)
                            routingHeader.LotSizeFrom = Convert.ToInt32(dtExcel.Rows[i].ItemArray[8].ToString()); // col J (9) (Lot size from)
                            routingHeader.LotSizeTo = Convert.ToInt32(dtExcel.Rows[i].ItemArray[9].ToString()); // col K (10) (Lot size to)
                            routingHeader.BaseUnit = dtExcel.Rows[i].ItemArray[10].ToString(); // col L (11) (Base Unit)
                            routingHeaderList.Add(routingHeader);
                        }

                        // Create Routing Item
                        // Column M - AK (11 - 35)
                        if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[11].ToString()) && !string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[35].ToString())) // Work Center not null
                        {
                            RoutingHeaderModel lastHeader = routingHeaderList.Last(); // alway get last header
                            RoutingItemModel routingItem = new RoutingItemModel();
                            routingItem.MaterialCode = lastHeader.MaterialCode;
                            routingItem.Plant = lastHeader.Plant;

                            routingItem.WorkCenter = dtExcel.Rows[i].ItemArray[11].ToString(); // col M (12) (Work Center)
                            routingItem.StandardTextKey = dtExcel.Rows[i].ItemArray[12].ToString(); // col N (13) (Standard Text Key)
                            routingItem.OperationDecription = dtExcel.Rows[i].ItemArray[13].ToString(); // col O (14) (Operation Decription)
                            routingItem.OperationBaseQuantity = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[14].ToString()); // col P (15) (Operation Base Quantity)
                            routingItem.OperationOUM = dtExcel.Rows[i].ItemArray[15].ToString(); // col Q (16) (Operation OUM)
                            routingItem.ConversionOfOUMN = dtExcel.Rows[i].ItemArray[16].ToString(); // col R (17) (Routing Description)
                            routingItem.ConversionOfOUMD = dtExcel.Rows[i].ItemArray[17].ToString(); // col S (18) (Numerator for Converting Routing and Operation UoM)
                            routingItem.ActivityType1 = dtExcel.Rows[i].ItemArray[18].ToString(); // col T (19) (Activity 1)
                            routingItem.StandardValue1 = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[19].ToString()); // col U (20) (Activity Value 1)
                            routingItem.StandardValue1OUM = dtExcel.Rows[i].ItemArray[20].ToString(); // col V (21) (Activity Unit 1)
                            routingItem.ActivityType2 = dtExcel.Rows[i].ItemArray[21].ToString(); // col W (22) (Activity 2)
                            routingItem.StandardValue2 = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[22].ToString()); // col X (23) (Ativity Value 2)
                            routingItem.StandardValue2OUM = dtExcel.Rows[i].ItemArray[23].ToString(); // col Y (24) (Activity Unit 2)
                            routingItem.ActivityType3 = dtExcel.Rows[i].ItemArray[24].ToString(); // col Z (25) (Activity 3)
                            routingItem.StandardValue3 = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[25].ToString()); // col AA (26) (Activity Value 3)
                            routingItem.StandardValue3OUM = dtExcel.Rows[i].ItemArray[26].ToString(); // col AB (27) (Activity Unit 3)
                            routingItem.ActivityType4 = dtExcel.Rows[i].ItemArray[27].ToString(); // col AC (28) (Activity 4)
                            routingItem.StandardValue4 = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[28].ToString()); // col AD (29) (Activity Value 4)
                            routingItem.StandardValue4OUM = dtExcel.Rows[i].ItemArray[29].ToString(); // col AE (30) (Activity Unit 4)
                            routingItem.ActivityType5 = dtExcel.Rows[i].ItemArray[30].ToString(); // col AF (31) (Activity 5)
                            routingItem.StandardValue5 = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[31].ToString()); // col AG (32) (Activity Value 5)
                            routingItem.StandardValue5OUM = dtExcel.Rows[i].ItemArray[32].ToString(); // col AH (33) (Activity Unit 5)
                            routingItem.ActivityType6 = dtExcel.Rows[i].ItemArray[33].ToString(); // col AI (34) (Activity 6)
                            routingItem.StandardValue6 = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[34].ToString()); // col AJ (35) (Activity Value 6)
                            routingItem.StandardValue6OUM = dtExcel.Rows[i].ItemArray[35].ToString(); // col AK (36) (Activity Unit 6)
                            routingItemList.Add(routingItem);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMInspectionPlanExcelToMMInspectionPlanModel(DataTable dtExcel, ref List<InspectionPlanHeaderModel> inspectionPlanHeaderList, ref List<InspectionPlanItemModel> inspectionPlanItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    inspectionPlanHeaderList = new List<InspectionPlanHeaderModel>();
                    inspectionPlanItemList = new List<InspectionPlanItemModel>();

                    // Create Inspection Plan Header
                    // Column A - H (0 - 7)
                    for (int i = 1; i < rowCount; i++)
                    {
                        if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[0].ToString())) // Material Code not null
                        {
                            InspectionPlanHeaderModel inspectionPlanHeader = new InspectionPlanHeaderModel();
                            inspectionPlanHeader.MaterialCode = dtExcel.Rows[i].ItemArray[0].ToString(); // col A (1) (Material Code)
                            inspectionPlanHeader.MaterialDescription = dtExcel.Rows[i].ItemArray[1].ToString(); // col B (2) (Material Description)
                            inspectionPlanHeader.Plant = dtExcel.Rows[i].ItemArray[2].ToString(); // col C (3) (Plant)
                            inspectionPlanHeader.Group = dtExcel.Rows[i].ItemArray[3].ToString(); // col D (4) (Group)
                            inspectionPlanHeader.HeaderDescription = dtExcel.Rows[i].ItemArray[4].ToString(); // col E (5) (Description)
                            inspectionPlanHeader.TaskListUsage = dtExcel.Rows[i].ItemArray[5].ToString(); // col F (6) (Usage)
                            inspectionPlanHeader.Status = dtExcel.Rows[i].ItemArray[6].ToString(); // col G (7) (Usage)
                            inspectionPlanHeader.InspectionPoint = dtExcel.Rows[i].ItemArray[7].ToString(); // col H (8) (Usage)
                            inspectionPlanHeaderList.Add(inspectionPlanHeader);
                        }
                        // Create Inspection Plan Item
                        // Column A - F (8 - 15)
                        if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[8].ToString())) // MIC Not Null
                        {
                            InspectionPlanItemModel inspectionPlanItem = new InspectionPlanItemModel();
                            inspectionPlanItem.MaterialCode = inspectionPlanHeaderList.Last().MaterialCode; // alway get last item
                            inspectionPlanItem.MIC = dtExcel.Rows[i].ItemArray[8].ToString(); // col I (9) (MIC)
                            inspectionPlanItem.ItemDescription = dtExcel.Rows[i].ItemArray[9].ToString(); // col J (10) (Item Description)
                            inspectionPlanItem.InpectionMethod = dtExcel.Rows[i].ItemArray[10].ToString(); // col K (11) (Inpection Method)
                            inspectionPlanItem.SetUpValue1 = dtExcel.Rows[i].ItemArray[11].ToString(); // col L (12) (Setup Value1)
                            inspectionPlanItem.SetUpValue2 = dtExcel.Rows[i].ItemArray[12].ToString(); // col M (13) (Setup Value2)
                            inspectionPlanItem.SetUpValue3 = dtExcel.Rows[i].ItemArray[13].ToString(); // col N (14) (Setup Value3)
                            inspectionPlanItem.SetUpValue4 = dtExcel.Rows[i].ItemArray[14].ToString(); // col O (15) (Setup Value4)
                            inspectionPlanItem.SetUpValue5 = dtExcel.Rows[i].ItemArray[15].ToString(); // col P (16) (Setup Value5)
                            inspectionPlanItem.SetUpValue6 = dtExcel.Rows[i].ItemArray[16].ToString(); // col Q (17) (Setup Value6)
                            inspectionPlanItemList.Add(inspectionPlanItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertMMPackagingInstructionExcelToMMPackagingInstructionModel(DataTable dtExcel, ref List<PackagingInstructionModel> PackagingInstructionList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    PackagingInstructionList = new List<PackagingInstructionModel>();

                    for (int i = 1; i < rowCount; i++)
                    {
                        // Create Packaging Instruction
                        // Column A - F (0 - 5)
                        PackagingInstructionModel packagingInstruction = new PackagingInstructionModel();
                        packagingInstruction.PackagingInstruction = dtExcel.Rows[i].ItemArray[0].ToString(); // col A (1) (Packaging Instruction)
                        packagingInstruction.Description = dtExcel.Rows[i].ItemArray[1].ToString(); // col B (2) (Description)
                        packagingInstruction.ComponentPackage = dtExcel.Rows[i].ItemArray[2].ToString(); // col C (3) (Component Package)
                        packagingInstruction.ComponentReferenceMaterial = dtExcel.Rows[i].ItemArray[3].ToString(); // col D (4) (Component Reference Material)
                        packagingInstruction.TargetQuantity = dtExcel.Rows[i].ItemArray[4].ToString(); // col E (5) (Target Quantity)
                        packagingInstruction.MinimumQuantity = dtExcel.Rows[i].ItemArray[5].ToString(); // col F (6) (Minimum Quantity)
                        PackagingInstructionList.Add(packagingInstruction);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertWorkCenterRoutingExcelToWorkCenterRoutingModel(DataTable dtExcel, ref List<WorkCenterRoutingModel> WorkCenterRoutingList, ref List<WorkCenterRoutingItemModel> WorkCenterRoutingItemList)
        {
            int currentIndex = 0;
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    WorkCenterRoutingList = new List<WorkCenterRoutingModel>();
                    WorkCenterRoutingItemList = new List<WorkCenterRoutingItemModel>();

                    for (int i = 1; i < rowCount; i++)
                    {
                        currentIndex = i;
                        // Create WorkCenterRoutingList
                        // Column A - AI
                        WorkCenterRoutingModel workCenterRouting = new WorkCenterRoutingModel();
                        WorkCenterRoutingItemModel workCenterRoutingItem = new WorkCenterRoutingItemModel();
                        if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[1].ToString()) || !string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[2].ToString())) // Group Flag or Header Flag not be null
                        {
                            workCenterRouting.RoutingGroupDescription = dtExcel.Rows[i].ItemArray[0].ToString().Count() > 40 ? dtExcel.Rows[i].ItemArray[0].ToString().Substring(0, 40) : dtExcel.Rows[i].ItemArray[0].ToString(); //  col A (1) (Work Center Group)
                            workCenterRouting.GroupFlag = dtExcel.Rows[i].ItemArray[1].ToString(); //  col A (2) (Group Flag)
                            workCenterRouting.HeaderFlag = dtExcel.Rows[i].ItemArray[2].ToString(); //  col B (2) (Header Flag)
                            workCenterRouting.RoutingGroup = dtExcel.Rows[i].ItemArray[3].ToString(); //  col R (18) (Routing Group)
                            workCenterRouting.OperationNo = dtExcel.Rows[i].ItemArray[4].ToString(); //  col C (3) (Operation No)
                            workCenterRouting.OperationDecription = dtExcel.Rows[i].ItemArray[5].ToString(); // col D (4) Operation description)
                            workCenterRouting.ConversionOfOUMN = dtExcel.Rows[i].ItemArray[6].ToString(); //  col E (5) (Conversion of UOM - Numerator for Converting Routing and Operation UoM)
                            workCenterRouting.ConversionOfOUMD = dtExcel.Rows[i].ItemArray[7].ToString(); //  col F (6) (Conversion of UOM - Numerator for Converting Routing and Operation UoM)
                            workCenterRouting.WorkCenter = dtExcel.Rows[i].ItemArray[8].ToString(); //  col G (7) (WorkCenter)
                            workCenterRouting.WorkCenterDescription = dtExcel.Rows[i].ItemArray[9].ToString(); //  col H (8) (WorkCenterDescription)
                            workCenterRouting.Plant = dtExcel.Rows[i].ItemArray[10].ToString(); //  col I (9) (Plan)
                            workCenterRouting.WorkCenterCategory = dtExcel.Rows[i].ItemArray[11].ToString(); //  col J (10) (WorkCenter Category)
                            workCenterRouting.CostCenter = dtExcel.Rows[i].ItemArray[12].ToString(); //  col K (11) (CostCenter)
                            workCenterRouting.CostCenterDescription = dtExcel.Rows[i].ItemArray[13].ToString(); //  col L (12) (CostCenter Description)
                            workCenterRouting.Usage = dtExcel.Rows[i].ItemArray[14].ToString(); //  col M (13) (Usage)
                            workCenterRouting.BaseUnit = dtExcel.Rows[i].ItemArray[15].ToString(); //  col N (14) (Base Unit)
                            workCenterRouting.OverAllStatus = dtExcel.Rows[i].ItemArray[16].ToString(); //  col O (15) (Over all status)
                            workCenterRouting.ControlKey = dtExcel.Rows[i].ItemArray[17].ToString(); //  col P (16) (Control Key)
                            workCenterRouting.StandardValueKeyHeader = dtExcel.Rows[i].ItemArray[18].ToString(); //  col Q (17) (Standard Value Key)
                            workCenterRouting.UnitOfMeasurement1 = dtExcel.Rows[i].ItemArray[19].ToString(); //  col S (19) (Group Counter)
                            workCenterRouting.UnitOfMeasurement2 = dtExcel.Rows[i].ItemArray[20].ToString(); //  col S (19) (Group Counter)
                            workCenterRouting.UnitOfMeasurement3 = dtExcel.Rows[i].ItemArray[21].ToString(); //  col S (19) (Group Counter)
                            workCenterRouting.UnitOfMeasurement4 = dtExcel.Rows[i].ItemArray[22].ToString(); //  col S (19) (Group Counter)
                            workCenterRouting.UnitOfMeasurement5 = dtExcel.Rows[i].ItemArray[23].ToString(); //  col S (19) (Group Counter)
                            workCenterRouting.UnitOfMeasurement6 = dtExcel.Rows[i].ItemArray[24].ToString(); //  col S (19) (Group Counter)
                            workCenterRouting.GroupCounter = dtExcel.Rows[i].ItemArray[25].ToString(); //  col S (19) (Group Counter)
                            //workCenterRouting.StartDate = DateTime.FromOADate(double.Parse(dtExcel.Rows[i].ItemArray[20].ToString()));
                            //workCenterRouting.EndDate = DateTime.FromOADate(double.Parse(dtExcel.Rows[i].ItemArray[21].ToString()));
                            //workCenterRouting.StartDate = DateTime.ParseExact(dtExcel.Rows[i].ItemArray[26].ToString(), "dd/M/yyyy", usCulture); // col T (20) Start Date
                            //workCenterRouting.EndDate = DateTime.ParseExact(dtExcel.Rows[i].ItemArray[27].ToString(), "dd/M/yyyy", usCulture); // col U (21) Start Date
                            workCenterRouting.StartDate = validateDate(dtExcel.Rows[i].ItemArray[26].ToString(), "dd/M/yyyy") ? DateTime.ParseExact(dtExcel.Rows[i].ItemArray[26].ToString(), "dd/M/yyyy", usCulture) : DateTime.FromOADate(double.Parse(dtExcel.Rows[i].ItemArray[26].ToString()));
                            workCenterRouting.EndDate = validateDate(dtExcel.Rows[i].ItemArray[27].ToString(), "dd/M/yyyy") ? DateTime.ParseExact(dtExcel.Rows[i].ItemArray[27].ToString(), "dd/M/yyyy", usCulture) : DateTime.FromOADate(double.Parse(dtExcel.Rows[i].ItemArray[27].ToString()));
                            workCenterRouting.StartTime = DateTime.ParseExact(dtExcel.Rows[i].ItemArray[28].ToString(), "HH:mm:ss", usCulture); // col T (20) Start Date
                            workCenterRouting.EndTime = DateTime.ParseExact(dtExcel.Rows[i].ItemArray[29].ToString(), "HH:mm:ss", usCulture); // col U (21) Start Date
                            workCenterRouting.CapacityUtilization = dtExcel.Rows[i].ItemArray[30].ToString(); //  col W (23) (Operation Base OUM)
                            workCenterRouting.NoIndCapacities = dtExcel.Rows[i].ItemArray[31].ToString(); //  col W (23) (Operation Base OUM)
                            workCenterRouting.FactoryCalendarID = dtExcel.Rows[i].ItemArray[32].ToString(); //  col W (23) (Operation Base OUM)
                            workCenterRouting.CapacityBaseUnit = dtExcel.Rows[i].ItemArray[33].ToString(); //  col W (23) (Operation Base OUM)
                            workCenterRouting.OperationBaseQuantity = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[34].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[34].ToString()); //  col V (22) (Operation Base Q'ty)
                            workCenterRouting.OperationOUM = dtExcel.Rows[i].ItemArray[35].ToString(); //  col W (23) (Operation Base OUM)
                            WorkCenterRoutingList.Add(workCenterRouting);

                            workCenterRoutingItem.ItemFlag = dtExcel.Rows[i].ItemArray[36].ToString(); //  col X (24) (Item Flag)
                            workCenterRoutingItem.ActivityNo = Convert.ToInt32(dtExcel.Rows[i].ItemArray[37].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[37].ToString()); //  col Y (25) (Activity No)
                            workCenterRoutingItem.ActivityDescription = dtExcel.Rows[i].ItemArray[38].ToString(); //  col Z (26) (Activity Description)
                            workCenterRoutingItem.ActivityUnit = dtExcel.Rows[i].ItemArray[39].ToString(); //  col AA (27) (Unit)
                            workCenterRoutingItem.CostDiverUnit = dtExcel.Rows[i].ItemArray[40].ToString(); //  col AB (28) (Cost Driver Unit)
                            workCenterRoutingItem.Remark = dtExcel.Rows[i].ItemArray[41].ToString(); //  col AC (29) (Remark)
                            workCenterRoutingItem.ActivityType = dtExcel.Rows[i].ItemArray[42].ToString(); //  col AD (30) (Activity Type)
                                                                                                           // Ativity Description col 31
                            workCenterRoutingItem.CostElement = dtExcel.Rows[i].ItemArray[44].ToString(); //  col AG (32) (Cost Element)
                            workCenterRoutingItem.CostElementDescription = dtExcel.Rows[i].ItemArray[45].ToString(); //  col AH (33) (Cost Element Description)
                            workCenterRoutingItem.CostingFormular = dtExcel.Rows[i].ItemArray[46].ToString(); //  col AI (34) (Costing Formular)
                            WorkCenterRoutingItemList.Add(workCenterRoutingItem);
                        }
                        else
                        {
                            if (WorkCenterRoutingList.Count > 0)
                            {
                                workCenterRoutingItem.ItemFlag = dtExcel.Rows[i].ItemArray[36].ToString(); //  col X (24) (Item Flag)
                                workCenterRoutingItem.ActivityNo = Convert.ToInt32(dtExcel.Rows[i].ItemArray[37].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[37].ToString()); //  col Y (25) (Activity No)
                                workCenterRoutingItem.ActivityDescription = dtExcel.Rows[i].ItemArray[38].ToString(); //  col Z (26) (Activity Description)
                                workCenterRoutingItem.ActivityUnit = dtExcel.Rows[i].ItemArray[39].ToString(); //  col AA (27) (Unit)
                                workCenterRoutingItem.CostDiverUnit = dtExcel.Rows[i].ItemArray[40].ToString(); //  col AB (28) (Cost Driver Unit)
                                workCenterRoutingItem.Remark = dtExcel.Rows[i].ItemArray[41].ToString(); //  col AC (29) (Remark)
                                workCenterRoutingItem.ActivityType = dtExcel.Rows[i].ItemArray[42].ToString(); //  col AD (30) (Activity Type)
                                                                                                               // Ativity Description col 31
                                workCenterRoutingItem.CostElement = dtExcel.Rows[i].ItemArray[44].ToString(); //  col AG (32) (Cost Element)
                                workCenterRoutingItem.CostElementDescription = dtExcel.Rows[i].ItemArray[45].ToString(); //  col AH (33) (Cost Element Description)
                                workCenterRoutingItem.CostingFormular = dtExcel.Rows[i].ItemArray[46].ToString(); //  col AI (34) (Costing Formular)
                                WorkCenterRoutingItemList.Add(workCenterRoutingItem);
                            }
                            else
                            {
                                throw new Exception("Count WorkCenterRoutingList < 1");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + currentIndex);
            }
        }

        public static bool validateDate(string date, string date_format)
        {
            try
            {
                DateTime.ParseExact(date, date_format, DateTimeFormatInfo.InvariantInfo);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static DataTable ReadExcelInterop(string fileName, string fileExt)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            try
            {
                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                DataTable dtExcel = new DataTable();

                //dt.Column = colCount;
                //dataGridMonomerProducts.ColumnCount = colCount;
                //dataGridMonomerProducts.RowCount = rowCount;

                for (int i = 1; i <= rowCount; i++)
                {
                    for (int j = 1; j <= colCount; j++)
                    {
                        //write the value to the Grid  
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        {
                            //dataGridMonomerProducts.Rows[i - 1].Cells[j - 1].Value = xlRange.Cells[i, j].Value2.ToString();
                            //dataGridMonomerProducts.Rows[i - 1].Cells[j - 1].Value = xlRange.Cells[i, j].Value2.ToString();
                            dtExcel.Rows[i - 1].ItemArray[j - 1] = xlRange.Cells[i, j].Value2.ToString();
                        }
                        // Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");

                        //add useful things here!
                    }
                }

                return dtExcel;
            }
            catch (Exception ex)
            {
                log.Error("========== " + ex.Message + " =========");
                throw ex;
            }
        }

        #endregion

        #region CCS

        //public static List<DataTable> ReadCCSBOMExcelList(string fileName, string fileExt)
        //{
        //    log4net.Config.XmlConfigurator.Configure();
        //    string conn = string.Empty;
        //    List<DataTable> dtexceList = new List<DataTable>();

        //    if (fileExt.CompareTo(".xls") == 0)
        //        conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
        //    else
        //        conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1';"; //for above excel 2007

        //    using (OleDbConnection con = new OleDbConnection(conn))
        //    {
        //        try
        //        {
        //            DataTable gradeLevel = new DataTable();
        //            OleDbDataAdapter oleAdpt1 = new OleDbDataAdapter("select * from [Grade-Level$]", con); //here we read data from Grade-Level  
        //            oleAdpt1.Fill(gradeLevel); //fill excel data into dataTable
        //            dtexceList.Add(gradeLevel);

        //            DataTable maskingCode = new DataTable();
        //            OleDbDataAdapter oleAdpt2 = new OleDbDataAdapter("select * from [Masking Code$]", con); //here we read data from Masking Code
        //            oleAdpt2.Fill(maskingCode); //fill excel data into dataTable
        //            dtexceList.Add(maskingCode);

        //            DataTable additive = new DataTable();
        //            OleDbDataAdapter oleAdpt3 = new OleDbDataAdapter("select * from [Additive$]", con); //here we read data from Additive
        //            oleAdpt3.Fill(additive); //fill excel data into dataTable
        //            dtexceList.Add(additive);

        //            DataTable color = new DataTable();
        //            OleDbDataAdapter oleAdpt4 = new OleDbDataAdapter("select * from [Color$]", con); //here we read data from Color
        //            oleAdpt4.Fill(color); //fill excel data into dataTable
        //            dtexceList.Add(color);

        //            DataTable syrub = new DataTable();
        //            OleDbDataAdapter oleAdpt5 = new OleDbDataAdapter("select * from [Syrup$]", con); //here we read data from Syrup
        //            oleAdpt5.Fill(syrub); //fill excel data into dataTable
        //            dtexceList.Add(syrub);

        //            DataTable initiator = new DataTable();
        //            OleDbDataAdapter oleAdpt6 = new OleDbDataAdapter("select * from [Initiator$]", con); //here we read data from Initiator
        //            oleAdpt6.Fill(initiator); //fill excel data into dataTable
        //            dtexceList.Add(initiator);

        //        }
        //        catch (Exception ex)
        //        {
        //            log.Error("========== " + ex.Message + " =========");
        //            throw ex;
        //        }
        //    }

        //    return dtexceList;
        //}

        public static void ConvertCCSBOMExcelToCCSBOMModel(DataTable dtExcel, ref List<BOMHeaderModel> BOMHeaderList, ref List<BOMItemModel> BOMItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    BOMHeaderList = new List<BOMHeaderModel>();
                    BOMItemList = new List<BOMItemModel>();

                    for (int i = 10; i < rowCount; i++)
                    {
                        for (int j = 10; j < colCount; j++)
                        {
                            if (!string.IsNullOrEmpty(dtExcel.Rows[1].ItemArray[j].ToString())) // Material Header
                            {
                                // Create BOM Item
                                if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[j].ToString()) && !String.Equals("-", dtExcel.Rows[i].ItemArray[j].ToString().Trim()))
                                {
                                    BOMItemModel BOMItem = new BOMItemModel();
                                    // get header value 
                                    BOMItem.MaterialCode = dtExcel.Rows[1].ItemArray[j].ToString();
                                    BOMItem.Plant = dtExcel.Rows[3].ItemArray[j].ToString();
                                    BOMItem.BOMUsage = dtExcel.Rows[6].ItemArray[j].ToString();
                                    BOMItem.BOMAlt = dtExcel.Rows[7].ItemArray[j].ToString();
                                    // get component quatity
                                    BOMItem.ComponentQuantity = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[j].ToString());
                                    // get component
                                    BOMItem.BOMItem = dtExcel.Rows[i].ItemArray[1].ToString();
                                    BOMItem.ComponentMaterial = dtExcel.Rows[i].ItemArray[2].ToString();
                                    BOMItem.ComponentUnit = dtExcel.Rows[i].ItemArray[3].ToString();
                                    BOMItem.ComponentDescription = dtExcel.Rows[i].ItemArray[4].ToString();
                                    BOMItem.FixedQty = dtExcel.Rows[i].ItemArray[5].ToString();
                                    BOMItem.CostingRelevency = dtExcel.Rows[i].ItemArray[6].ToString();
                                    BOMItem.OperationScrap = dtExcel.Rows[i].ItemArray[7].ToString();
                                    BOMItem.ComponentScrap = dtExcel.Rows[i].ItemArray[8].ToString();
                                    BOMItemList.Add(BOMItem);
                                }

                                if (i == 10) // only first loop
                                {
                                    // Create BOM Header
                                    BOMHeaderModel BOMHeader = new BOMHeaderModel();
                                    int pNo = 0;
                                    int.TryParse(dtExcel.Rows[0].ItemArray[j].ToString(), out pNo);
                                    BOMHeader.ProductNo = pNo;
                                    BOMHeader.MaterialCode = dtExcel.Rows[1].ItemArray[j].ToString();
                                    BOMHeader.BOMHeaderText = dtExcel.Rows[2].ItemArray[j].ToString();
                                    BOMHeader.Plant = dtExcel.Rows[3].ItemArray[j].ToString();
                                    BOMHeader.BaseQuantity = Convert.ToDecimal(dtExcel.Rows[4].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[4].ItemArray[j].ToString());
                                    BOMHeader.BaseUnit = dtExcel.Rows[5].ItemArray[j].ToString();
                                    BOMHeader.BOMUsage = dtExcel.Rows[6].ItemArray[j].ToString();
                                    BOMHeader.BOMAlt = dtExcel.Rows[7].ItemArray[j].ToString();
                                    BOMHeader.RoutingGroup = dtExcel.Rows[8].ItemArray[j].ToString();
                                    BOMHeaderList.Add(BOMHeader);
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

        public static void ConvertCCSBOMActivityExcelToCCSBOMActivityModel(DataTable dtExcel, ref List<BOMHeaderModel> BOMHeaderList, ref List<BOMItemModel> BOMItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    BOMHeaderList = new List<BOMHeaderModel>();
                    BOMItemList = new List<BOMItemModel>();

                    for (int i = 16; i < rowCount; i++)
                    {
                        for (int j = 10; j < colCount; j++)
                        {
                            if (!string.IsNullOrEmpty(dtExcel.Rows[1].ItemArray[j].ToString())) // check material
                            {
                                // Create BOM Item
                                if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[j].ToString()))
                                {
                                    BOMItemModel BOMItem = new BOMItemModel();
                                    // get header
                                    BOMItem.RoutingGroup = dtExcel.Rows[8].ItemArray[j].ToString();
                                    BOMItem.WorkCenter = dtExcel.Rows[13].ItemArray[j].ToString();
                                    BOMItem.Plant = dtExcel.Rows[3].ItemArray[j].ToString();
                                    BOMItem.BOMUsage = dtExcel.Rows[6].ItemArray[j].ToString();
                                    BOMItem.BOMAlt = dtExcel.Rows[7].ItemArray[j].ToString();
                                    // get standard value
                                    BOMItem.ComponentQuantity = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[j].ToString());
                                    // get component
                                    BOMItem.ActivityNo = Convert.ToInt32(dtExcel.Rows[i].ItemArray[1].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[1].ToString());
                                    BOMItem.OperationNo = dtExcel.Rows[i].ItemArray[2].ToString();
                                    BOMItem.Activity = dtExcel.Rows[i].ItemArray[3].ToString();
                                    BOMItem.StandardValueKeyText = dtExcel.Rows[i].ItemArray[4].ToString();
                                    BOMItem.StandardValueKeyOUM = dtExcel.Rows[i].ItemArray[5].ToString();
                                    BOMItem.ComponentDescription = dtExcel.Rows[i].ItemArray[6].ToString();
                                    BOMItem.ComponentScrap = dtExcel.Rows[i].ItemArray[8].ToString();
                                    BOMItemList.Add(BOMItem);
                                }

                                if (i == 16) // first loop
                                {
                                    // Create BOM Header
                                    BOMHeaderModel BOMHeader = new BOMHeaderModel();
                                    int pNo = 0;
                                    int.TryParse(dtExcel.Rows[0].ItemArray[j].ToString(), out pNo);
                                    BOMHeader.ProductNo = pNo;
                                    BOMHeader.MaterialCode = dtExcel.Rows[1].ItemArray[j].ToString();
                                    BOMHeader.BOMHeaderText = dtExcel.Rows[2].ItemArray[j].ToString();
                                    BOMHeader.Plant = dtExcel.Rows[3].ItemArray[j].ToString();
                                    BOMHeader.BaseQuantity = Convert.ToDecimal(dtExcel.Rows[4].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[4].ItemArray[j].ToString());
                                    BOMHeader.BaseUnit = dtExcel.Rows[5].ItemArray[j].ToString();
                                    BOMHeader.BOMUsage = dtExcel.Rows[6].ItemArray[j].ToString();
                                    BOMHeader.BOMAlt = dtExcel.Rows[7].ItemArray[j].ToString();
                                    BOMHeader.RoutingGroup = dtExcel.Rows[8].ItemArray[j].ToString();
                                    BOMHeader.GroupCounter = dtExcel.Rows[9].ItemArray[j].ToString();
                                    BOMHeader.ProductionVersion = dtExcel.Rows[10].ItemArray[j].ToString();
                                    BOMHeader.LotSizeFrom = dtExcel.Rows[11].ItemArray[j].ToString();
                                    BOMHeader.LotSizeTo = dtExcel.Rows[12].ItemArray[j].ToString();
                                    BOMHeader.WorkCenter = dtExcel.Rows[13].ItemArray[j].ToString();
                                    BOMHeader.StorageLocation = dtExcel.Rows[14].ItemArray[j].ToString();
                                    BOMHeaderList.Add(BOMHeader);
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

        public static void ConvertCCSBOMGradeExcelToCCSBOMGradeModel(DataTable dtExcel, ref List<BOMHeaderModel> BOMHeaderList, ref List<BOMItemModel> BOMItemList)
        {
            try
            {
                if (dtExcel != null)
                {
                    int rowCount = dtExcel.Rows.Count;
                    int colCount = dtExcel.Columns.Count;

                    BOMHeaderList = new List<BOMHeaderModel>();
                    BOMItemList = new List<BOMItemModel>();

                    for (int i = 8; i < rowCount; i++)
                    {
                        for (int j = 9; j < colCount; j++)
                        {
                            // Create BOM Item
                            if (!string.IsNullOrEmpty(dtExcel.Rows[i].ItemArray[j].ToString()))  // check component quantity is not null
                            {
                                BOMItemModel BOMItem = new BOMItemModel();
                                BOMItem.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                BOMItem.Plant = dtExcel.Rows[2].ItemArray[j].ToString();
                                BOMItem.BOMUsage = dtExcel.Rows[5].ItemArray[j].ToString();
                                BOMItem.BOMAlt = dtExcel.Rows[6].ItemArray[j].ToString();

                                // component
                                BOMItem.BOMItem = dtExcel.Rows[i].ItemArray[0].ToString(); // A
                                BOMItem.ComponentMaterial = dtExcel.Rows[i].ItemArray[1].ToString(); // B
                                BOMItem.ComponentUnit = dtExcel.Rows[i].ItemArray[2].ToString(); // C
                                BOMItem.ComponentDescription = dtExcel.Rows[i].ItemArray[3].ToString(); // D
                                BOMItem.FixedQty = dtExcel.Rows[i].ItemArray[4].ToString(); // E
                                BOMItem.CostingRelevency = dtExcel.Rows[i].ItemArray[5].ToString(); // F
                                BOMItem.OperationScrap = dtExcel.Rows[i].ItemArray[6].ToString(); // G
                                BOMItem.ComponentScrap = dtExcel.Rows[i].ItemArray[7].ToString(); // H

                                BOMItem.ComponentQuantity = Convert.ToDecimal(dtExcel.Rows[i].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[i].ItemArray[j].ToString());

                                BOMItemList.Add(BOMItem);
                            }

                            if (i == 8) // only get value first loop
                            {
                                // Create BOM Header
                                BOMHeaderModel BOMHeader = new BOMHeaderModel();
                                BOMHeader.MaterialCode = dtExcel.Rows[0].ItemArray[j].ToString();
                                BOMHeader.BOMHeaderText = dtExcel.Rows[1].ItemArray[j].ToString();
                                BOMHeader.Plant = dtExcel.Rows[2].ItemArray[j].ToString();
                                BOMHeader.BaseQuantity = Convert.ToDecimal(dtExcel.Rows[3].ItemArray[j].ToString() == "" ? "0" : dtExcel.Rows[3].ItemArray[j].ToString());
                                BOMHeader.BaseUnit = dtExcel.Rows[4].ItemArray[j].ToString();
                                BOMHeader.BOMUsage = dtExcel.Rows[5].ItemArray[j].ToString();
                                BOMHeader.BOMAlt = dtExcel.Rows[6].ItemArray[j].ToString();
                                BOMHeaderList.Add(BOMHeader);
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

        //public static void ReadExcelTest(string fileName, string fileExt)
        //{
        //    Application app = new Application();
        //    Workbook book = null;
        //    Range range = null;

        //    try
        //    {
        //        app.Visible = false;
        //        app.ScreenUpdating = false;
        //        app.DisplayAlerts = false;

        //        string execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

        //        book = app.Workbooks.Open(@"C:\data.xls", Missing.Value, Missing.Value, Missing.Value
        //                                          , Missing.Value, Missing.Value, Missing.Value, Missing.Value
        //                                         , Missing.Value, Missing.Value, Missing.Value, Missing.Value
        //                                        , Missing.Value, Missing.Value, Missing.Value);

        //        book = app.Workbooks.Open(fileName);

        //        foreach (Worksheet sheet in book.Worksheets)
        //        {

        //            Console.WriteLine(@"Values for Sheet " + sheet.Index);

        //            get a range to work with
        //            range = sheet.get_Range("A1", Missing.Value);
        //            get the end of values to the right (will stop at the first empty cell)
        //            range = range.get_End(XlDirection.xlToRight);
        //            get the end of values toward the bottom, looking in the last column(will stop at first empty cell)
        //            range = range.get_End(XlDirection.xlDown);

        //            get the address of the bottom, right cell
        //            string downAddress = range.get_Address(
        //                false, false, XlReferenceStyle.xlA1,
        //                Type.Missing, Type.Missing);

        //            Get the range, then values from a1
        //           range = sheet.get_Range("A1", downAddress);
        //            object[,] values = (object[,])range.Value2;

        //            View the values
        //            Console.Write("\t");
        //            Console.WriteLine();
        //            for (int i = 1; i <= values.GetLength(0); i++)
        //            {
        //                for (int j = 1; j <= values.GetLength(1); j++)
        //                {
        //                    Console.Write("{0}\t", values[i, j]);
        //                }
        //                Console.WriteLine();
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    finally
        //    {
        //        range = null;
        //        if (book != null)
        //            book.Close(false, Missing.Value, Missing.Value);
        //        book = null;
        //        if (app != null)
        //            app.Quit();
        //        app = null;
        //    }
        //}

    }
}