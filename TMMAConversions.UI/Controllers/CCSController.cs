using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMMAConversions.DAL.Models;
using TMMAConversions.BLL;
using TMMAConversions.BLL.Utilities;
using System.Data;
using System.Globalization;
using System.Text;
using Ionic.Zip;
using TMMAConversions.Utilities.Utilities;
using Newtonsoft.Json;

namespace TMMAConversions.UI.Controllers
{
    public class CCSController : Controller
    {

        protected Core core = new Core();
        protected static CultureInfo usCulture = new CultureInfo("en-US");
        // GET: CCS

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MaterialBOMItem()
        {
            BOMFileFilterModel filter = new BOMFileFilterModel();
            filter.ProductsTypeID = 2; // ccs products
            filter.Order = "CreatedDate";
            filter.Sort = "asc";

            BOMFileViewModel model = core.GetBOMFileView(filter);

            ViewData["BOMFileViewModel"] = model;

            return View();
        }

        public ActionResult GetBOMFileView(int pageNo, string order, string sort)
        {
            BOMFileFilterModel filter = new BOMFileFilterModel();
            filter.ProductsTypeID = 2; // ccs products
            filter.Pagination.Page = pageNo;
            filter.Order = order;
            filter.Sort = sort;

            BOMFileViewModel model = core.GetBOMFileView(filter);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // BOM File
        [HttpPost]
        public ActionResult UploadBOMFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);

                    if (String.Equals("CCSBOM", fileName, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (string file in Request.Files)
                        {
                            var fileContent = Request.Files[file];
                            var user = Request.Form["User"];
                            var recObjectName = Request.Form["RecObjectName"];
                            DateTime validDate = DateTime.ParseExact(Request.Form["ValidDate"], "dd/MM/yyyy", usCulture);

                            if (fileContent != null && fileContent.ContentLength > 0)
                            {
                                string extension = Path.GetExtension(fileContent.FileName).ToLower();

                                string[] validFileTypes = { ".xls", ".xlsx", ".xlsm" }; //init file type

                                var path = Path.Combine(Server.MapPath("~/Files/CCS/Excels/BOM"), fileContent.FileName);

                                if (validFileTypes.Contains(extension))
                                {
                                    if (System.IO.File.Exists(path))
                                    {
                                        //System.IO.File.Delete(path);
                                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/CCS/Excels/BOM"));
                                        foreach (string filePath in filePaths)
                                            System.IO.File.Delete(filePath);
                                    }

                                    fileContent.SaveAs(path);

                                    int version = 0;
                                    SaveBOMFileVersion(path, recObjectName, user, validDate, ref version);

                                    BOMFileFilterModel filter = new BOMFileFilterModel();
                                    filter.ProductsTypeID = 2; // CCS
                                    BOMFileViewModel model = core.GetBOMFileView(filter);

                                    return Json(model, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new ResponseModel()
                                    {
                                        Message = "Please Upload Files in .xls or .xlsx format.",
                                        Status = false
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    else
                    {
                        return Json(new ResponseModel()
                        {
                            Message = "Excel File incorrect.",
                            Status = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new ResponseModel()
                {
                    Message = "Please Insert File.",
                    Status = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseModel()
                {
                    Message = ex.Message,
                    Status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public void SaveBOMFileVersion(string path, string recObjectName, string userSAP, DateTime validDate, ref int version)
        {
            try
            {
                var enUser = Environment.UserName;
                int ProductsTypeID = 2; // CCS
                var last = core.GetBOMFileLastVersion(ProductsTypeID);
                version = (int)last.BOMFileVersion + 1;

                // Add BOMFile to Database
                BOMFileModel bomFile = new BOMFileModel();
                bomFile.ProductsTypeID = ProductsTypeID;
                bomFile.RecObjectName = recObjectName;
                bomFile.UserSAP = userSAP;
                bomFile.BOMFilePath = path;
                bomFile.BOMFileStatus = 1; // create new
                bomFile.BOMFileVersion = (decimal)(version);
                bomFile.ValidDate = validDate;
                bomFile.IsActive = true;
                bomFile.CreatedBy = !string.IsNullOrEmpty(enUser) ? enUser : Session["Username"].ToString();
                bomFile.CreatedDate = DateTime.Now;

                int _newID = 0;
                ResponseModel res = core.AddBOMFile(bomFile, ref _newID);

                if (!res.Status)
                {
                    throw new Exception(res.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost]
        //public ActionResult GenerateCreateBOMTextFile(int bomFileID, string fileName, string userSAP, string validDateText, int pageNo)
        //{
        //    try
        //    {
        //        DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture);
        //        var path = Path.Combine(Server.MapPath("~/Files/CCS/Excels/BOM"), "BOM.xlsx"); // Get BOM File
        //        string extension = Path.GetExtension("BOM.xlsx").ToLower();

        //        List<DataTable> dtList = ExcelUtility.ReadCCSBOMExcelList(path, extension);
        //        /***************************************************************/
        //        List<BOMHeaderModel> bomGradeLevelHeaderList = null;
        //        List<BOMItemModel> bomGradeLevelItemList = null;
        //        // dtList[0] is Grade-Level
        //        ExcelUtility.ConvertCCSBOMGradeExcelToCCSBOMGradeModel(dtList[0], ref bomGradeLevelHeaderList, ref bomGradeLevelItemList);

        //        string textBOMGradeLevelName = fileName + "_BOMGradeLevel";
        //        string textBOMGradeLevelExtension = ".txt";
        //        string textBOMGradeLevelPath = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), textBOMGradeLevelName);

        //        List<BOMHeaderModel> newBomGradeLevelHeaderList = CheckBOMAlt(bomGradeLevelHeaderList);

        //        SAPUtility.ConvertToCCSBOMTextFile(newBomGradeLevelHeaderList, bomGradeLevelItemList, textBOMGradeLevelPath, textBOMGradeLevelExtension, userSAP, validDate);
        //        /***************************************************************/
        //        List<BOMHeaderModel> bomMaskingCodeHeaderList = null;
        //        List<BOMItemModel> bomMaskingCodeItemList = null;
        //        // dtList[1] is MaskingCode
        //        ExcelUtility.ConvertCCSBOMGradeExcelToCCSBOMGradeModel(dtList[1], ref bomMaskingCodeHeaderList, ref bomMaskingCodeItemList);

        //        string textBOMMaskingCodeName = fileName + "_BOMMaskingCode";
        //        string textBOMMaskingCodeExtension = ".txt";
        //        string textBOMMaskingCodePath = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), textBOMGradeLevelName);

        //        List<BOMHeaderModel> newBomMaskingCodeHeaderList = CheckBOMAlt(bomMaskingCodeHeaderList);

        //        SAPUtility.ConvertToCCSBOMTextFile(newBomMaskingCodeHeaderList, bomMaskingCodeItemList, textBOMMaskingCodePath, textBOMMaskingCodeExtension, userSAP, validDate);
        //        /***************************************************************/
        //        List<BOMHeaderModel> bomAdditiveHeaderList = null;
        //        List<BOMItemModel> bomAdditiveItemList = null;
        //        // dtList[2] is Additive
        //        ExcelUtility.ConvertCCSBOMGradeExcelToCCSBOMGradeModel(dtList[2], ref bomAdditiveHeaderList, ref bomAdditiveItemList);

        //        string textBOMAdditiveName = fileName + "_BOMAdditive";
        //        string textBOMAdditiveExtension = ".txt";
        //        string textBOMAdditivePath = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), textBOMAdditiveName);

        //        List<BOMHeaderModel> newBomAdditiveHeaderList = CheckBOMAlt(bomAdditiveHeaderList);

        //        SAPUtility.ConvertToCCSBOMTextFile(newBomAdditiveHeaderList, bomAdditiveItemList, textBOMAdditivePath, textBOMAdditiveExtension, userSAP, validDate);
        //        /***************************************************************/
        //        List<BOMHeaderModel> bomColorHeaderList = null;
        //        List<BOMItemModel> bomColorItemList = null;
        //        // dtList[3] is Color
        //        ExcelUtility.ConvertCCSBOMGradeExcelToCCSBOMGradeModel(dtList[3], ref bomColorHeaderList, ref bomColorItemList);

        //        string textBOMColorName = fileName + "_BOMColor";
        //        string textBOMColorExtension = ".txt";
        //        string textBOMColorPath = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), textBOMColorName);

        //        List<BOMHeaderModel> newBomColorHeaderList = CheckBOMAlt(bomColorHeaderList);

        //        SAPUtility.ConvertToCCSBOMTextFile(newBomColorHeaderList, bomColorItemList, textBOMColorPath, textBOMColorExtension, userSAP, validDate);
        //        /***************************************************************/
        //        List<BOMHeaderModel> bomSyrupHeaderList = null;
        //        List<BOMItemModel> bomSyrupItemList = null;
        //        // dtList[4] is Syrup
        //        ExcelUtility.ConvertCCSBOMGradeExcelToCCSBOMGradeModel(dtList[4], ref bomSyrupHeaderList, ref bomSyrupItemList);

        //        string textBOMSyrupName = fileName + "_BOMSyrup";
        //        string textBOMSyrupExtension = ".txt";
        //        string textBOMSyrupPath = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), textBOMSyrupName);

        //        List<BOMHeaderModel> newBomSyrupHeaderList = CheckBOMAlt(bomSyrupHeaderList);

        //        SAPUtility.ConvertToCCSBOMTextFile(newBomSyrupHeaderList, bomSyrupItemList, textBOMSyrupPath, textBOMSyrupExtension, userSAP, validDate);
        //        /***************************************************************/
        //        List<BOMHeaderModel> bomInitiatorHeaderList = null;
        //        List<BOMItemModel> bomInitiatorItemList = null;
        //        // dtList[5] is Initiator
        //        ExcelUtility.ConvertCCSBOMGradeExcelToCCSBOMGradeModel(dtList[5], ref bomInitiatorHeaderList, ref bomInitiatorItemList);

        //        string textBOMInitiatorName = fileName + "_BOMInitiator";
        //        string textBOMInitiatorExtension = ".txt";
        //        string textBOMInitiatorPath = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), textBOMInitiatorName);

        //        List<BOMHeaderModel> newBomInitiatorHeaderList = CheckBOMAlt(bomInitiatorHeaderList);

        //        SAPUtility.ConvertToCCSBOMTextFile(newBomInitiatorHeaderList, bomInitiatorItemList, textBOMInitiatorPath, textBOMInitiatorExtension, userSAP, validDate);
        //        /***************************************************************/

        //        int BOMFileStatus = 3; // Create

        //        ResponseModel res = core.UpdateStatusBOMFile(bomFileID, BOMFileStatus);

        //        if (res.Status)
        //        {
        //            BOMFileFilterModel filter = new BOMFileFilterModel();
        //            filter.ProductsTypeID = 2; // CCS
        //            filter.Pagination.Page = pageNo;
        //            var model = core.GetBOMFileView(filter);
        //            return Json(model, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(res, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message
        //        });
        //    }
        //}

        [HttpPost]
        public ActionResult GenerateCreateBOMTextFile(int bomFileID, string fileName, string userSAP, string validDateText, string pathText, int pageNo, List<string> options, List<string> sheets)
        {
            try
            {
                DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture);
                string path = pathText;
                string extension = Path.GetExtension(pathText).ToLower();

                //var pathRangeExcelJson = Path.Combine(Server.MapPath("~/"), "rangeExcel.json");
                //List<RangeExcelModel> items = new List<RangeExcelModel>();
                //using (StreamReader r = new StreamReader(pathRangeExcelJson))
                //{
                //    string json = r.ReadToEnd();
                //    items = JsonConvert.DeserializeObject<List<RangeExcelModel>>(json);
                //}

                List<SheetsModel> objSheetsList = new List<SheetsModel>();
                List<SheetsModel> objSheetsActivityList = new List<SheetsModel>();

                if (sheets.Contains("Special Pack"))
                {
                    objSheetsList.Add(new SheetsModel() { Name = "Special Pack", Count = 1 });
                    objSheetsActivityList.Add(new SheetsModel() { Name = "Special Pack Activity", Count = 1 });
                }
                if (sheets.Contains("CCS Cut and Pack"))
                {
                    objSheetsList.Add(new SheetsModel() { Name = "CCS Cut and Pack", Count = 1 });
                    objSheetsActivityList.Add(new SheetsModel() { Name = "CCS Cut and Pack Activity", Count = 1 });
                }

                if (sheets.Contains("CCS PMMA"))
                {
                    objSheetsList.Add(new SheetsModel() { Name = "CCS PMMA", Count = 1 });
                    objSheetsActivityList.Add(new SheetsModel() { Name = "CCS PMMA Activity", Count = 1 });
                }

                if (sheets.Contains("Additive"))
                {
                    objSheetsList.Add(new SheetsModel() { Name = "Additive", Count = 1 });
                    objSheetsActivityList.Add(new SheetsModel() { Name = "Additive Activity", Count = 1 });
                }

                if (sheets.Contains("CCS Syrup"))
                {
                    objSheetsList.Add(new SheetsModel() { Name = "CCS Syrup", Count = 1 });
                    objSheetsActivityList.Add(new SheetsModel() { Name = "CCS Syrup Activity", Count = 1 });
                }

                if (sheets.Contains("CCS Initiator"))
                {
                    objSheetsList.Add(new SheetsModel() { Name = "CCS Initiator", Count = 1 });
                    objSheetsActivityList.Add(new SheetsModel() { Name = "CCS Initiator Activity", Count = 1 });
                }

                if (sheets.Contains("Packing Pattern"))
                {
                    objSheetsList.Add(new SheetsModel() { Name = "Packing Pattern", Count = 1 });
                    objSheetsActivityList.Add(new SheetsModel() { Name = "Packing Pattern Activity", Count = 1 });
                }

                if (sheets.Contains("Gasket"))
                {
                    objSheetsList.Add(new SheetsModel() { Name = "Gasket", Count = 1 });
                    objSheetsActivityList.Add(new SheetsModel() { Name = "Gasket Activity", Count = 1 });
                }

                List<List<DataTable>> dtList = ExcelUtility.ReadCCSBOMExcel(path, extension, objSheetsList);
                List<List<DataTable>> dtActivityList = ExcelUtility.ReadCCSBOMActivityExcel(path, extension, objSheetsActivityList);

                // delete all files before generate new files
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/CCS/SAP/BOM"));
                foreach (string filePath in filePaths)
                    System.IO.File.Delete(filePath);

                int i = 0;
                foreach (var o in dtList) // map 1 by 1 bom and activity
                {
                    List<BOMHeaderModel> list1 = null;
                    List<BOMItemModel> list2 = null;
                    ExcelUtility.ConvertCCSBOMExcelToCCSBOMModel(o, ref list1, ref list2);

                    List<BOMHeaderModel> acList1 = null;
                    List<BOMItemModel> acList2 = null;
                    ExcelUtility.ConvertCCSBOMActivityExcelToCCSBOMActivityModel(dtActivityList[i], ref acList1, ref acList2);

                    //int limit = 100; // 100 items limit by header
                    //if (list1.Count() > limit)
                    //    int j = 1;
                    //{
                    //    int ht = 0;
                    //    int hc = 100; // number of hlist
                    //    int countHList = 100;
                    //    while (ht < list1.Count())
                    //    {
                    //        List<BOMHeaderModel> listHcut = new List<BOMHeaderModel>();
                    //        List<BOMHeaderModel> listHactcut = new List<BOMHeaderModel>();
                    //        int hlast = list1.Count();

                    //        int hCount = countHList > hlast ? hlast - ht : hc; // bom & act same

                    //        listHcut = list1.GetRange(ht, hCount);
                    //        listHactcut = acList1.GetRange(ht, hCount);

                    //        List<BOMHeaderModel> newList1 = BOMUtility.CheckBOMAlt(listHcut);

                    //        string textName = fileName + sheets[i].Replace(" ", "") + j;
                    //        string textExtension = ".txt";
                    //        string textPath = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), textName);

                    //        SAPUtility.ConvertToMMABOMTextFile(newList1, list2, listHactcut, acList2, textPath, fileName, textExtension, userSAP, validDate, options);
                    //        ht += hc;
                    //        countHList += hc;
                    //        j++;
                    //    }
                    //}
                    //else
                    //{

                    List<BOMHeaderModel> newList1 = BOMUtility.CheckBOMAlt(list1);

                    string textName = fileName + sheets[i].Replace(" ", "");
                    string textExtension = ".txt";
                    string textPath = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), textName);

                    SAPUtility.ConvertToMMABOMTextFile(newList1, list2, acList1, acList2, textPath, fileName, textExtension, userSAP, validDate, options);
                    //}
                    i++;
                }

                int BOMFileStatus = 3; // Create

                ResponseModel res = core.UpdateStatusBOMFile(bomFileID, BOMFileStatus);

                if (res.Status) // update status success
                {
                    BOMFileFilterModel filter = new BOMFileFilterModel();
                    filter.ProductsTypeID = 2; // CCS
                    filter.Pagination.Page = pageNo;
                    BOMFileViewModel model = core.GetBOMFileView(filter);

                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public void DownloadCreateBOMTextFile(string fileName)
        {
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

            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("BOM");

                DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/Files/CCS/SAP/BOM"));
                FileInfo[] Files = d.GetFiles("*.txt");

                foreach (var a in Files)
                {
                    var path = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), a.Name);
                    if (System.IO.File.Exists(path))
                    {
                        zip.AddFile(path, "BOM");
                    }
                }

                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("CCSBOMFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }

        //[HttpPost]
        //public ActionResult GenerateDeleteBOMTextFile(int bomFileID, string fileName, string userSAP, int pageNo)
        //{
        //    try
        //    {
        //        var path = Path.Combine(Server.MapPath("~/Files/CCS/Excels/BOM"), "BOM.xlsx"); // Get BOM File
        //        string extension = Path.GetExtension("BOM.xlsx").ToLower();

        //        DataTable dt = ExcelUtility.ReadCCSBOMExcel(path, extension);

        //        List<BOMHeaderModel> bomGradeLevelHeaderList = null;
        //        List<BOMItemModel> bomGradeLevelItemList = null;
        //        ExcelUtility.ConvertCCSBOMGradeExcelToCCSBOMGradeModel(dt, ref bomGradeLevelHeaderList, ref bomGradeLevelItemList);

        //        string textBOMGradeLevelName = fileName + "_CCSBOMGradeLevelDelete";
        //        string textBOMGradeLevelExtension = ".txt";
        //        string textBOMGradeLevelPath = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), textBOMGradeLevelName);

        //        List<BOMHeaderModel> newBomGradeLevelHeaderList = CheckBOMAlt(bomGradeLevelHeaderList);

        //        SAPUtility.ConvertCCSBOMToDeleteTextFile(newBomGradeLevelHeaderList, bomGradeLevelItemList, textBOMGradeLevelPath, textBOMGradeLevelExtension, userSAP);

        //        int BOMFileStatus = 4; // Delete

        //        ResponseModel res = core.UpdateStatusBOMFile(bomFileID, BOMFileStatus);

        //        if (res.Status)
        //        {
        //            BOMFileFilterModel filter = new BOMFileFilterModel();
        //            filter.ProductsTypeID = 2; // CCS
        //            filter.Pagination.Page = pageNo;
        //            var model = core.GetBOMFileView(filter);
        //            return Json(model, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(res, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message
        //        });
        //    }
        //}

        [HttpGet]
        public void DownloadDeleteBOMTextFile(string fileName)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("BOM");

                var pathGrade = Path.Combine(Server.MapPath("~/Files/CCS/SAP/BOM"), fileName + "_CCSBOMGradeLevelDelete" + ".txt");

                zip.AddFile(pathGrade, "BOM");

                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("CCSBOMDeleteFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }

    }
}