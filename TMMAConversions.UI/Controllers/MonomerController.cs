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

namespace TMMAConversions.UI.Controllers
{
    public class MonomerController : Controller
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected Core core = new Core();
        protected static CultureInfo usCulture = new CultureInfo("en-US");

        // GET: Monomer
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        // View BOM
        [CustomAuthorize]
        public ActionResult MaterialBOMItem()
        {
            BOMFileFilterModel filter = new BOMFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products
            filter.Order = "CreatedDate";
            filter.Sort = "asc";
            BOMFileViewModel model = core.GetBOMFileView(filter);

            ViewData["BOMFileViewModel"] = model;

            return View();
        }

        // View WorkCenter
        [CustomAuthorize]
        public ActionResult WorkCenter()
        {
            WorkCenterFileFilterModel filter = new WorkCenterFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products
            filter.Order = "CreatedDate";
            filter.Sort = "asc";
            WorkCenterFileViewModel model = core.GetWorkCenterFileView(filter);

            ViewData["WorkCenterFileViewModel"] = model;

            return View();
        }

        // View WorkCenter and Routing
        [CustomAuthorize]
        public ActionResult WorkCenterRouting()
        {
            WorkCenterRoutingFileFilterModel filter = new WorkCenterRoutingFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products
            filter.Order = "CreatedDate";
            filter.Sort = "asc";
            WorkCenterRoutingFileViewModel model = core.GetWorkCenterRoutingFileView(filter);

            string[] sheets =
            {
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

            string[] options =
            {
                "Delete Operation Routing",
                "Delete Work Center",
                "Create Work Center",
                "Create Routing header",
                "Add Operation Routing (w/o Standard value key)"
            };

            ViewData["WorkCenterRoutingFileViewModel"] = model;
            ViewData["SheetsList"] = sheets;
            ViewData["OptionsList"] = options;

            return View();
        }

        // View ProductionVersion
        [CustomAuthorize]
        public ActionResult ProductionVersion()
        {
            ProductionVersionFileFilterModel filter = new ProductionVersionFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products

            ProductionVersionFileViewModel model = core.GetProductionVersionFileView(filter);

            ViewData["ProductionVersionFileViewModel"] = model;

            return View();
        }

        // View RoutingWithoutMaterial
        [CustomAuthorize]
        public ActionResult RoutingWithoutMaterial()
        {
            RoutingWithoutMaterialFileFilterModel filter = new RoutingWithoutMaterialFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products

            RoutingWithoutMaterialFileViewModel model = core.GetRoutingWithoutMaterialFileView(filter);

            ViewData["RoutingWithoutMaterialFileViewModel"] = model;

            return View();
        }

        // View RoutingWithMaterial
        [CustomAuthorize]
        public ActionResult RoutingWithMaterial()
        {
            RoutingWithoutMaterialFileFilterModel filter = new RoutingWithoutMaterialFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products

            RoutingWithoutMaterialFileViewModel model = core.GetRoutingWithoutMaterialFileView(filter);

            ViewData["RoutingWithoutMaterialFileViewModel"] = model;

            return View();
        }

        // View Assign Material To Routing
        [CustomAuthorize]
        public ActionResult AssignMaterialToRouting()
        {
            AssignMaterialToRoutingFileFilterModel filter = new AssignMaterialToRoutingFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products

            AssignMaterialToRoutingFileViewModel model = core.GetAssignMaterialToRoutingFileView(filter);

            ViewData["AssignMaterialToRoutingFileViewModel"] = model;

            return View();
        }

        // View Routing
        [CustomAuthorize]
        public ActionResult Routing()
        {
            RoutingFileFilterModel filter = new RoutingFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products

            RoutingFileViewModel model = core.GetRoutingFileView(filter);

            ViewData["RoutingFileViewModel"] = model;

            return View();
        }

        // View Inspection Plan
        [CustomAuthorize]
        public ActionResult InspectionPlan()
        {
            InspectionPlanFileFilterModel filter = new InspectionPlanFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products

            InspectionPlanFileViewModel model = core.GetInspectionPlanFileView(filter);

            ViewData["InspectionPlanFileViewModel"] = model;

            return View();
        }

        // View Packaging Instruction
        [CustomAuthorize]
        public ActionResult PackagingInstruction()
        {
            PackagingInstructionFileFilterModel filter = new PackagingInstructionFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products

            PackagingInstructionFileViewModel model = core.GetPackagingInstructionFileView(filter);

            ViewData["PackagingInstructionFileViewModel"] = model;

            return View();
        }

        // Ajax call BOM
        [HttpPost]
        public ActionResult GetBOMFileView(int pageNo, string order, string sort)
        {
            BOMFileFilterModel filter = new BOMFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products
            filter.Pagination.Page = pageNo;
            filter.Order = order;
            filter.Sort = sort;

            BOMFileViewModel model = core.GetBOMFileView(filter);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Ajax call WorkCenter
        [HttpPost]
        public ActionResult GetWorkCenterFileView(int pageNo, string order, string sort)
        {
            WorkCenterFileFilterModel filter = new WorkCenterFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products
            filter.Pagination.Page = pageNo;
            filter.Order = order;
            filter.Sort = sort;
            WorkCenterFileViewModel model = core.GetWorkCenterFileView(filter);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Ajax call WorkCenter Routing
        [HttpPost]
        public ActionResult GetWorkCenterRoutingFileView(int pageNo, string order, string sort)
        {
            WorkCenterRoutingFileFilterModel filter = new WorkCenterRoutingFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products
            filter.Pagination.Page = pageNo;
            filter.Order = order;
            filter.Sort = sort;
            WorkCenterRoutingFileViewModel model = core.GetWorkCenterRoutingFileView(filter);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Ajax call Production Version
        [HttpPost]
        public ActionResult GetProductionVersionFileView(int pageNo, string order, string sort)
        {
            ProductionVersionFileFilterModel filter = new ProductionVersionFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products
            filter.Pagination.Page = pageNo;
            filter.Order = order;
            filter.Sort = sort;
            ProductionVersionFileViewModel model = core.GetProductionVersionFileView(filter);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Ajax call Routing
        [HttpPost]
        public ActionResult GetRoutingFileView(int pageNo, string order, string sort)
        {
            RoutingFileFilterModel filter = new RoutingFileFilterModel();
            filter.ProductsTypeID = 1; // monomer products
            filter.Pagination.Page = pageNo;
            filter.Order = order;
            filter.Sort = sort;

            RoutingFileViewModel model = core.GetRoutingFileView(filter);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // Ajax call Inpection Plan
        [HttpPost]
        public ActionResult GetInspectionPlanFileView(int pageNo, string order, string sort)
        {
            InspectionPlanFileFilterModel filter = new InspectionPlanFileFilterModel();
            filter.ProductsTypeID = 1; // monomer inspection plan
            filter.Pagination.Page = pageNo;
            filter.Order = order;
            filter.Sort = sort;

            InspectionPlanFileViewModel model = core.GetInspectionPlanFileView(filter);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //Ajax call Packaging Instruction
        [HttpPost]
        public ActionResult GetPackagingInstructionFileView(int pageNO, string order, string sort)
        {
            PackagingInstructionFileFilterModel filter = new PackagingInstructionFileFilterModel();
            filter.ProductsTypeID = 1; // monomer
            filter.Pagination.Page = pageNO;
            filter.Order = order;
            filter.Sort = sort;

            PackagingInstructionFileViewModel model = core.GetPackagingInstructionFileView(filter);

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

                    if (String.Equals("MMABOM", fileName, StringComparison.OrdinalIgnoreCase))
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

                                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/BOM"), fileContent.FileName);

                                if (validFileTypes.Contains(extension))
                                {
                                    if (System.IO.File.Exists(path))
                                    {
                                        //System.IO.File.Delete(path);
                                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/Monomer/Excels/BOM"));
                                        foreach (string filePath in filePaths)
                                            System.IO.File.Delete(filePath);
                                    }

                                    fileContent.SaveAs(path);

                                    log.Info("========== Save File Success =========");

                                    int version = 0;
                                    SaveBOMFileVersion(recObjectName, user, validDate, path, ref version);

                                    BOMFileFilterModel filter = new BOMFileFilterModel();
                                    filter.ProductsTypeID = 1; // Monomer
                                    BOMFileViewModel model = core.GetBOMFileView(filter);

                                    return Json(model, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    log.Error("========== Please Upload Files in .xls or .xlsx format. =========");

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
                        log.Error("========== File Name incorrect. =========");

                        return Json(new ResponseModel()
                        {
                            Message = "File Name incorrect.",
                            Status = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                log.Error("========== Please Insert File. =========");

                return Json(new ResponseModel()
                {
                    Message = "Please Insert File.",
                    Status = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("========== " + ex.Message + " =========");

                return Json(new ResponseModel()
                {
                    Message = ex.Message,
                    Status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // WorkCenter File
        [HttpPost]
        public ActionResult UploadWorkCenterFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        var user = Request.Form["User"];
                        var recObjectName = Request.Form["RecObjectName"];

                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            string extension = Path.GetExtension(fileContent.FileName).ToLower();

                            string[] validFileTypes = { ".xls", ".xlsx", ".xlsm" }; //init file type

                            var fileName = Path.GetFileName(fileContent.FileName);

                            var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/WorkCenter"), fileContent.FileName);

                            if (validFileTypes.Contains(extension))
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    //System.IO.File.Delete(path);
                                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/Monomer/Excels/WorkCenter"));
                                    foreach (string filePath in filePaths)
                                        System.IO.File.Delete(filePath);
                                }

                                fileContent.SaveAs(path);

                                int version = 0;
                                SaveWorkCenterFileVersion(recObjectName, ref version, user);

                                WorkCenterFileFilterModel filter = new WorkCenterFileFilterModel();
                                filter.ProductsTypeID = 1; // Monomer
                                WorkCenterFileViewModel model = core.GetWorkCenterFileView(filter);

                                return Json(model, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new ResponseModel()
                                {
                                    Message = "Please Upload Files in .xls or .xlsx or .xlsm format.",
                                    Status = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
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

        // WorkCenter Routing File
        [HttpPost]
        public ActionResult UploadWorkCenterRoutingFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);

                    if (String.Equals("WorkCenterRouting", fileName, StringComparison.OrdinalIgnoreCase))
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

                                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/WorkCenterRouting"), fileContent.FileName);

                                if (validFileTypes.Contains(extension))
                                {
                                    if (System.IO.File.Exists(path))
                                    {
                                        //System.IO.File.Delete(path);
                                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/Monomer/Excels/WorkCenterRouting"));
                                        foreach (string filePath in filePaths)
                                            System.IO.File.Delete(filePath);
                                    }

                                    fileContent.SaveAs(path);

                                    log.Info("========== Save File Success. =========");

                                    int version = 0;
                                    SaveWorkCenterRoutingFileVersion(recObjectName, user, validDate, path, ref version);

                                    WorkCenterRoutingFileFilterModel filter = new WorkCenterRoutingFileFilterModel();
                                    filter.ProductsTypeID = 1; // Monomer
                                    WorkCenterRoutingFileViewModel model = core.GetWorkCenterRoutingFileView(filter);

                                    return Json(model, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    log.Error("========== Please Upload Files in .xls or .xlsx format. =========");

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
                        log.Error("========== File Name incorrect. =========");

                        return Json(new ResponseModel()
                        {
                            Message = "File Name incorrect.",
                            Status = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                log.Error("========== Please Insert File. =========");

                return Json(new ResponseModel()
                {
                    Message = "Please Insert File.",
                    Status = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("========== " + ex.Message + " =========");

                return Json(new ResponseModel()
                {
                    Message = ex.Message,
                    Status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // Production Version File
        [HttpPost]
        public ActionResult UploadProductionVersionFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
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

                            string[] validFileTypes = { ".xls", ".xlsx" }; //init file type

                            var fileName = Path.GetFileName(fileContent.FileName);

                            var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/ProductionVersion"), "ProductionVersion.xlsx");

                            if (validFileTypes.Contains(extension))
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }

                                fileContent.SaveAs(path);

                                int version = 0;
                                SaveProductionVersionFileVersion(recObjectName, ref version, user, validDate);

                                ProductionVersionFileFilterModel filter = new ProductionVersionFileFilterModel();
                                filter.ProductsTypeID = 1; // Monomer
                                ProductionVersionFileViewModel model = core.GetProductionVersionFileView(filter);

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

        // Routing Without Material File
        [HttpPost]
        public ActionResult UploadRoutingWithoutMaterialFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        var user = Request.Form["User"];
                        var recObjectName = Request.Form["RecObjectName"];

                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            string extension = Path.GetExtension(fileContent.FileName).ToLower();

                            string[] validFileTypes = { ".xls", ".xlsx" }; //init file type

                            var fileName = Path.GetFileName(fileContent.FileName);

                            var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/RoutingWithoutMaterial"), "RoutingWithoutMaterial.xlsx");

                            if (validFileTypes.Contains(extension))
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }

                                fileContent.SaveAs(path);

                                int version = 0;
                                SaveRoutingWithoutMaterialFileVersion(recObjectName, ref version, user);

                                RoutingWithoutMaterialFileFilterModel filter = new RoutingWithoutMaterialFileFilterModel();
                                filter.ProductsTypeID = 1; // Monomer
                                RoutingWithoutMaterialFileViewModel model = core.GetRoutingWithoutMaterialFileView(filter);

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

        // Routing With Material File
        [HttpPost]
        public ActionResult UploadRoutingWithMaterialFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        var user = Request.Form["User"];
                        var recObjectName = Request.Form["RecObjectName"];

                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            string extension = Path.GetExtension(fileContent.FileName).ToLower();

                            string[] validFileTypes = { ".xls", ".xlsx" }; //init file type

                            var fileName = Path.GetFileName(fileContent.FileName);

                            var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/RoutingWithMaterial"), "RoutingWithMaterial.xlsx");

                            if (validFileTypes.Contains(extension))
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }

                                fileContent.SaveAs(path);

                                int version = 0;
                                SaveRoutingWithMaterialFileVersion(recObjectName, ref version, user);

                                RoutingWithoutMaterialFileFilterModel filter = new RoutingWithoutMaterialFileFilterModel();
                                filter.ProductsTypeID = 1; // Monomer
                                RoutingWithoutMaterialFileViewModel model = core.GetRoutingWithoutMaterialFileView(filter);

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

        // Routing
        [HttpPost]
        public ActionResult UploadRoutingFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
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

                            string[] validFileTypes = { ".xls", ".xlsx" }; //init file type

                            var fileName = Path.GetFileName(fileContent.FileName);

                            var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/Routing"), recObjectName + "_Routing.xlsx");

                            if (validFileTypes.Contains(extension))
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }

                                fileContent.SaveAs(path);

                                int version = 0;
                                SaveRoutingFileVersion(recObjectName, ref version, user, validDate);

                                RoutingFileFilterModel filter = new RoutingFileFilterModel();
                                filter.ProductsTypeID = 1; // Monomer
                                RoutingFileViewModel model = core.GetRoutingFileView(filter);

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

        // Inspection Plan File
        [HttpPost]
        public ActionResult UploadInspectionPlanFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);

                    if (String.Equals("InspectionPlan", fileName, StringComparison.OrdinalIgnoreCase))
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

                                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/InspectionPlan"), fileContent.FileName);

                                if (validFileTypes.Contains(extension))
                                {
                                    if (System.IO.File.Exists(path))
                                    {
                                        //System.IO.File.Delete(path);
                                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/Monomer/Excels/InspectionPlan"));
                                        foreach (string filePath in filePaths)
                                            System.IO.File.Delete(filePath);
                                    }

                                    fileContent.SaveAs(path);

                                    log.Info("========== Save File Success. =========");

                                    int version = 0;
                                    SaveInspectionPlanFileVersion(recObjectName, user, validDate, path, ref version);

                                    InspectionPlanFileFilterModel filter = new InspectionPlanFileFilterModel();
                                    filter.ProductsTypeID = 1; // Monomer
                                    InspectionPlanFileViewModel model = core.GetInspectionPlanFileView(filter);

                                    return Json(model, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    log.Error("========== Please Upload Files in .xls or .xlsx format. =========");

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
                        log.Error("========== File Name incorrect. =========");

                        return Json(new ResponseModel()
                        {
                            Message = "File Name incorrect.",
                            Status = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                log.Error("========== Please Insert File. =========");

                return Json(new ResponseModel()
                {
                    Message = "Please Insert File.",
                    Status = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error("========== " + ex.Message + " =========");

                return Json(new ResponseModel()
                {
                    Message = ex.Message,
                    Status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // Packaging Instruction File
        [HttpPost]
        public ActionResult UploadPackagingInstructionFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);

                    if (String.Equals("PackagingInstruction", fileName, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (string file in Request.Files)
                        {
                            var fileContent = Request.Files[file];
                            var user = Request.Form["User"];
                            var recObjectName = Request.Form["RecObjectName"];

                            if (fileContent != null && fileContent.ContentLength > 0)
                            {
                                string extension = Path.GetExtension(fileContent.FileName).ToLower();

                                string[] validFileTypes = { ".xls", ".xlsx" }; //init file type

                                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/PackagingInstruction"), "PackagingInstruction.xlsx");

                                if (validFileTypes.Contains(extension))
                                {
                                    if (System.IO.File.Exists(path))
                                    {
                                        System.IO.File.Delete(path);
                                    }

                                    fileContent.SaveAs(path);

                                    int version = 0;
                                    SavePackagingInstructionFileVersion(recObjectName, ref version, user);

                                    PackagingInstructionFileFilterModel filter = new PackagingInstructionFileFilterModel();
                                    filter.ProductsTypeID = 1; // Monomer
                                    PackagingInstructionFileViewModel model = core.GetPackagingInstructionFileView(filter);

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

        // Validate BOM File
        [HttpPost]
        public ActionResult UploadValidateBOMFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        var bomFileID = Request.Form["BOMFileID"];

                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            string extension = Path.GetExtension(fileContent.FileName).ToLower();

                            string[] validFileTypes = { ".txt" }; //init file type

                            var fileName = Path.GetFileName(fileContent.FileName);

                            var path = Path.Combine(Server.MapPath("~/Files/Monomer/Validate"), fileContent.FileName);

                            var excelPath = Path.Combine(Server.MapPath("~/Files/Monomer/Excels"), "BOM.xlsx");

                            if (validFileTypes.Contains(extension))
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }

                                fileContent.SaveAs(path);

                                string excelName = "BOMFile";
                                string excelExtension = ".xlsx";
                                string validatePath = Path.Combine(Server.MapPath("~/Files/Monomer/Validate"), excelName);

                                TextUtility.ReadValidateErrorBOMFile(path, excelExtension, excelPath, validatePath);

                                return Json(new ResponseModel()
                                {
                                    Message = "Success",
                                    Status = true
                                }, JsonRequestBehavior.AllowGet);
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

        // Validate WorkCenter File
        [HttpPost]
        public ActionResult UploadValidateWorkCenterFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        var workCenterFileID = Request.Form["WorkCenterFileID"];

                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            string extension = Path.GetExtension(fileContent.FileName).ToLower();

                            string[] validFileTypes = { ".txt" }; //init file type

                            var fileName = Path.GetFileName(fileContent.FileName);

                            var path = Path.Combine(Server.MapPath("~/Files/Monomer/Validate/WorkCenter"), fileContent.FileName);

                            var excelPath = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/WorkCenter"), "WorkCenter.xlsx");

                            if (validFileTypes.Contains(extension))
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }

                                fileContent.SaveAs(path);

                                string excelName = "WorkCenter.xlsx";
                                string excelExtension = ".xlsx";
                                string validatePath = Path.Combine(Server.MapPath("~/Files/Monomer/Validate/WorkCenter"), excelName);

                                TextUtility.ReadValidateErrorWorkCenterFile(path, excelExtension, excelPath, validatePath);

                                return Json(new ResponseModel()
                                {
                                    Message = "Success",
                                    Status = true
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new ResponseModel()
                                {
                                    Message = "Please Upload Files in .txt format.",
                                    Status = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
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

        // Validate Production Version File
        [HttpPost]
        public ActionResult UploadValidateProductionVersionFile()
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Form.Count > 0)
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        var workCenterFileID = Request.Form["ProductionVersionFileID"];

                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            string extension = Path.GetExtension(fileContent.FileName).ToLower();

                            string[] validFileTypes = { ".txt" }; //init file type

                            var fileName = Path.GetFileName(fileContent.FileName);

                            var path = Path.Combine(Server.MapPath("~/Files/Monomer/Validate/ProductionVersion"), fileContent.FileName);

                            var excelPath = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/ProductionVersion"), "ProductionVersion.xlsx");

                            if (validFileTypes.Contains(extension))
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }

                                fileContent.SaveAs(path);

                                string excelName = "ProductionVersion.xlsx";
                                string excelExtension = ".xlsx";
                                string validatePath = Path.Combine(Server.MapPath("~/Files/Monomer/Validate/ProductionVersion"), excelName);

                                TextUtility.ReadValidateErrorProductionVersionFile(path, excelExtension, excelPath, validatePath);

                                return Json(new ResponseModel()
                                {
                                    Message = "Success",
                                    Status = true
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new ResponseModel()
                                {
                                    Message = "Please Upload Files in .txt format.",
                                    Status = false
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
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

        public void SaveBOMFileVersion(string recObjectName, string user, DateTime validDate, string path, ref int version)
        {
            try
            {
                var enUser = Environment.UserName;
                int ProductsTypeID = 1;
                var last = core.GetBOMFileLastVersion(ProductsTypeID);
                version = (int)last.BOMFileVersion + 1;

                // Add BOMFile to Database
                BOMFileModel bomFile = new BOMFileModel();
                bomFile.ProductsTypeID = 1;
                bomFile.RecObjectName = recObjectName;
                bomFile.UserSAP = user;
                bomFile.BOMFilePath = path;
                bomFile.BOMFileStatus = 1; // create new
                bomFile.BOMFileVersion = (decimal)(version);
                bomFile.ValidDate = validDate;
                bomFile.IsActive = true;
                bomFile.CreatedBy = !string.IsNullOrEmpty(enUser) ? enUser : Session["Username"].ToString();
                bomFile.CreatedDate = DateTime.Now;

                int _newID = 0;
                ResponseModel res = core.AddBOMFile(bomFile, ref _newID);

                log.Info("========== Save BOM Success =========");

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

        public void SaveInspectionPlanFileVersion(string recObjectName, string user, DateTime validDate, string path, ref int version)
        {
            try
            {
                int ProductsTypeID = 1;
                var last = core.GetInspectionPlanFileLastVersion(ProductsTypeID);
                version = (int)last.InspectionPlanFileVersion + 1;

                // Add InspectionPlanFile to Database
                InspectionPlanFileModel inspectionPlanFile = new InspectionPlanFileModel();
                inspectionPlanFile.ProductsTypeID = 1;
                inspectionPlanFile.RecObjectName = recObjectName;
                inspectionPlanFile.UserSAP = user;
                inspectionPlanFile.InspectionPlanFilePath = path;
                inspectionPlanFile.InspectionPlanFileStatus = 1; // create new
                inspectionPlanFile.InspectionPlanFileVersion = (decimal)(version);
                inspectionPlanFile.ValidDate = validDate;
                inspectionPlanFile.IsActive = true;
                inspectionPlanFile.CreatedBy = "conversion";
                inspectionPlanFile.CreatedDate = DateTime.Now;

                int _newID = 0;
                ResponseModel res = core.AddInspectionPlanFile(inspectionPlanFile, ref _newID);

                if (!res.Status)
                {
                    throw new Exception(res.Message);
                }
                else
                {
                    log.Info("========== Save to Database Success. =========");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveWorkCenterFileVersion(string recObjectName, ref int version, string user)
        {
            try
            {
                var last = core.GetWorkCenterFileLastVersion();
                version = (int)last.WorkCenterFileVersion + 1;

                // Add WorkCenter File to Database
                WorkCenterFileModel workCenterFile = new WorkCenterFileModel();
                workCenterFile.ProductsTypeID = 1;
                workCenterFile.RecObjectName = recObjectName;
                workCenterFile.UserSAP = user;
                workCenterFile.WorkCenterFileStatus = 1; // create new
                workCenterFile.WorkCenterFileVersion = (decimal)(version);
                workCenterFile.WorkCenterFilePath = Server.MapPath("~/Files/Monomer/Excels/WorkCenter");
                workCenterFile.IsActive = true;
                workCenterFile.CreatedBy = "conversions";
                workCenterFile.CreatedDate = DateTime.Now;

                int _newID = 0;
                ResponseModel res = core.AddWorkCenterFile(workCenterFile, ref _newID);

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

        public void SaveWorkCenterRoutingFileVersion(string recObjectName, string user, DateTime validDate, string path, ref int version)
        {
            try
            {
                var enUser = Environment.UserName;
                var last = core.GetWorkCenterRoutingFileLastVersion();
                version = last != null ? (int)last.WorkCenterRoutingFileVersion + 1 : 1;

                // Add WorkCenterRouting File to Database
                WorkCenterRoutingFileModel workCenterRoutingFile = new WorkCenterRoutingFileModel();
                workCenterRoutingFile.ProductsTypeID = 1;
                workCenterRoutingFile.RecObjectName = recObjectName;
                workCenterRoutingFile.UserSAP = user;
                workCenterRoutingFile.WorkCenterRoutingFileStatus = 1; // create new
                workCenterRoutingFile.WorkCenterRoutingFileVersion = (decimal)(version);
                workCenterRoutingFile.WorkCenterRoutingFilePath = path;
                workCenterRoutingFile.IsActive = true;
                workCenterRoutingFile.CreatedBy = !string.IsNullOrEmpty(enUser) ? enUser : Session["Username"].ToString();
                workCenterRoutingFile.CreatedDate = DateTime.Now;
                workCenterRoutingFile.ValidDate = validDate;

                int _newID = 0;
                ResponseModel res = core.AddWorkCenterRoutingFile(workCenterRoutingFile, ref _newID);

                if (!res.Status)
                {
                    throw new Exception(res.Message);
                }
                else
                {
                    log.Info("========== Save to Database Success. =========");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveProductionVersionFileVersion(string recObjectName, ref int version, string user, DateTime validDate)
        {
            try
            {
                var last = core.GetProductionVersionFileLastVersion();
                version = (int)last.ProductionVersionFileVersion + 1;

                // Add WorkCenter File to Database

                ProductionVersionFileModel productionVersionFile = new ProductionVersionFileModel();
                productionVersionFile.ProductsTypeID = 1;
                productionVersionFile.RecObjectName = recObjectName;
                productionVersionFile.UserSAP = user;
                productionVersionFile.ProductionVersionFileStatus = 1; // create new
                productionVersionFile.ProductionVersionFileVersion = (decimal)(version);
                productionVersionFile.ProductionVersionFilePath = Server.MapPath("~/Files/Monomer/Excels/ProductionVersion");
                productionVersionFile.ValidDate = validDate;
                productionVersionFile.IsActive = true;
                productionVersionFile.CreatedBy = "conversions";
                productionVersionFile.CreatedDate = DateTime.Now;

                int _newID = 0;
                ResponseModel res = core.AddProductionVersionFile(productionVersionFile, ref _newID);

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

        public void SaveRoutingWithoutMaterialFileVersion(string recObjectName, ref int version, string user)
        {
            try
            {
                int productionsTypeID = 1;
                var last = core.GetRoutingWithoutMaterialFileLastVersion(productionsTypeID);
                version = (int)last.RoutingWithoutMaterialFileVersion + 1;

                // Save Routing Without Material File to Database

                RoutingWithoutMaterialFileModel routingWithoutMaterialFile = new RoutingWithoutMaterialFileModel();
                routingWithoutMaterialFile.ProductsTypeID = 1;
                routingWithoutMaterialFile.RecObjectName = recObjectName;
                routingWithoutMaterialFile.UserSAP = user;
                routingWithoutMaterialFile.RoutingWithoutMaterialFileStatus = 1; // create new
                routingWithoutMaterialFile.RoutingWithoutMaterialFileVersion = (decimal)(version);
                routingWithoutMaterialFile.RoutingWithoutMaterialFilePath = Path.Combine(Server.MapPath("~/Files/Monomer/Excels"), string.Format("{0}.xlsx", recObjectName));
                routingWithoutMaterialFile.IsActive = true;
                routingWithoutMaterialFile.CreatedBy = "conversions";
                routingWithoutMaterialFile.CreatedDate = DateTime.Now;

                int _newID = 0;
                ResponseModel res = core.AddRoutingWithoutMaterialFile(routingWithoutMaterialFile, ref _newID);

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

        public void SaveRoutingWithMaterialFileVersion(string recObjectName, ref int version, string user)
        {
            try
            {
                int productionsTypeID = 1;
                var last = core.GetRoutingWithMaterialFileLastVersion(productionsTypeID);
                version = (int)last.RoutingWithMaterialFileVersion + 1;

                // Save Routing Without Material File to Database

                RoutingWithMaterialFileModel routingWithMaterialFile = new RoutingWithMaterialFileModel();
                routingWithMaterialFile.ProductsTypeID = 1;
                routingWithMaterialFile.RecObjectName = recObjectName;
                routingWithMaterialFile.UserSAP = user;
                routingWithMaterialFile.RoutingWithMaterialFileStatus = 1; // create new
                routingWithMaterialFile.RoutingWithMaterialFileVersion = (decimal)(version);
                routingWithMaterialFile.RoutingWithMaterialFilePath = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/RoutingWithMaterial"), string.Format("{0}.xlsx", recObjectName));
                routingWithMaterialFile.IsActive = true;
                routingWithMaterialFile.CreatedBy = "conversions";
                routingWithMaterialFile.CreatedDate = DateTime.Now;

                int _newID = 0;
                ResponseModel res = core.AddRoutingWithMaterialFile(routingWithMaterialFile, ref _newID);

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

        public void SaveRoutingFileVersion(string recObjectName, ref int version, string user, DateTime validDate)
        {
            try
            {
                int productionsTypeID = 1;
                var last = core.GetRoutingFileLastVersion(productionsTypeID);
                version = (int)last.RoutingFileVersion + 1;

                // Save Routing File to Database
                RoutingFileModel routingFile = new RoutingFileModel();
                routingFile.ProductsTypeID = 1;
                routingFile.RecObjectName = recObjectName;
                routingFile.UserSAP = user;
                routingFile.RoutingFileStatus = 1; // create new
                routingFile.RoutingFileVersion = (decimal)(version);
                routingFile.RoutingFilePath = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/Routing"), string.Format("{0}.xlsx", recObjectName));
                routingFile.ValidDate = validDate;
                routingFile.IsActive = true;
                routingFile.CreatedBy = "conversions";
                routingFile.CreatedDate = DateTime.Now;

                int _newID = 0;
                ResponseModel res = core.AddRoutingFile(routingFile, ref _newID);

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

        public void SavePackagingInstructionFileVersion(string recObjectName, ref int version, string user)
        {
            try
            {
                int productionsTypeID = 1;
                var last = core.GetPackagingInstructionFileLastVersion(productionsTypeID);
                version = (int)last.PackagingInstructionFileVersion + 1;

                // Save Routing File to Database
                PackagingInstructionFileModel packagingInstructionFile = new PackagingInstructionFileModel();
                packagingInstructionFile.ProductsTypeID = 1;
                packagingInstructionFile.RecObjectName = recObjectName;
                packagingInstructionFile.UserSAP = user;
                packagingInstructionFile.PackagingInstructionFileStatus = 1; // create new
                packagingInstructionFile.PackagingInstructionFileVersion = (decimal)(version);
                packagingInstructionFile.PackagingInstructionFilePath = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/PackagingInstruction"), string.Format("{0}.xlsx", recObjectName + "_PackagingInstruction"));
                packagingInstructionFile.IsActive = true;
                packagingInstructionFile.CreatedBy = "conversions";
                packagingInstructionFile.CreatedDate = DateTime.Now;

                int _newID = 0;
                ResponseModel res = core.AddPackagingInstructionFile(packagingInstructionFile, ref _newID);

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

        [HttpPost]
        public ActionResult GenerateBOMTextFile(int bomFileID, string fileName, string userSAP, string validDateText, int pageNo)
        {
            try
            {
                DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture);
                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/BOM"), "MMABOM.xlsx"); // Get BOM File
                string extension = ".xlsx";

                // Grade Level
                DataTable dtGradeLevel = ExcelUtility.ReadMMBOMGradeLevelExcel(path, extension);

                List<BOMHeaderModel> bomGradeLevelHeaderList = null;
                List<BOMItemModel> bomGradeLevelItemList = null;
                ExcelUtility.ConvertMMBOMGradeExcelToMMBOMGradeModel(dtGradeLevel, ref bomGradeLevelHeaderList, ref bomGradeLevelItemList);

                string textBOMGradeLevelName = fileName + "_BOMGradeLevel";
                string textBOMGradeLevelExtension = ".txt";
                string textBOMGradeLevelPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), textBOMGradeLevelName);

                List<BOMHeaderModel> newBomGradeLevelHeaderList = BOMUtility.CheckBOMAlt(bomGradeLevelHeaderList);

                SAPUtility.ConvertToMMBOMTextFile(newBomGradeLevelHeaderList, bomGradeLevelItemList, textBOMGradeLevelPath, textBOMGradeLevelExtension, userSAP, validDate);

                // Pkg Level
                DataTable dtPkgLevel = ExcelUtility.ReadMMBOMPkgLevelExcel(path, extension);

                List<BOMHeaderModel> bomPkgLevelHeaderList = null;
                List<BOMItemModel> bomPkgLevelItemList = null;
                ExcelUtility.ConvertMMBOMPkgExcelToMMBOMPkgModel(dtGradeLevel, ref bomPkgLevelHeaderList, ref bomPkgLevelItemList);

                string textBOMPkgLevelName = fileName + "_BOMPkgLevel";
                string textBOMPkgLevelExtension = ".txt";
                string textBOMPkgLevelPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), textBOMPkgLevelName);

                List<BOMHeaderModel> newBomPkgLevelHeaderList = BOMUtility.CheckBOMAlt(bomPkgLevelHeaderList);

                SAPUtility.ConvertToMMBOMTextFile(bomPkgLevelHeaderList, bomPkgLevelItemList, textBOMPkgLevelPath, textBOMPkgLevelExtension, userSAP, validDate);

                int BOMFileStatus = 3; // Create

                ResponseModel res = core.UpdateStatusBOMFile(bomFileID, BOMFileStatus);

                if (res.Status) // update status success
                {
                    BOMFileFilterModel filter = new BOMFileFilterModel();
                    filter.ProductsTypeID = 1; // Monomer
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
        public void DownloadBOMTextFile(string fileName)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("BOM");

                var pathGrade = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), fileName + "_BOMGradeLevel" + ".txt");
                var pathPkg = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), fileName + "_BOMPkgLevel" + ".txt");

                zip.AddFile(pathGrade, "BOM");
                zip.AddFile(pathPkg, "BOM");

                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("BOMFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }

        [HttpPost]
        public ActionResult GenerateCreateBOMTextFile(int bomFileID, string fileName, string userSAP, string validDateText, string pathText, int pageNo, List<string> options, List<string> sheets)
        {
            try
            {
                var enUser = Environment.UserName;
                var userName = !string.IsNullOrEmpty(enUser) ? enUser : Session["Username"].ToString();
                log.Info("========== Generate By " + userName + " =========");

                DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture);
                //var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/BOM"), "MMABOM.xlsx"); // Get MMABOM File
                var path = pathText;
                string extension = Path.GetExtension(pathText).ToLower();

                List<DataTable> dtList = ExcelUtility.ReadMMABOMExcel(path, extension, sheets);
                List<DataTable> dtActivityList = ExcelUtility.ReadMMABOMActivityExcel(path, extension, sheets);

                // delete all files before generate new files
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/Monomer/SAP/BOM"));
                foreach (string filePath in filePaths)
                    System.IO.File.Delete(filePath);

                int i = 0;
                foreach (var o in dtList) // map 1 by 1 bom , activity
                {
                    List<BOMHeaderModel> list1 = null;
                    List<BOMItemModel> list2 = null;
                    ExcelUtility.ConvertMMABOMExcelToMMABOMModel(o, ref list1, ref list2);

                    List<BOMHeaderModel> acList1 = null;
                    List<BOMItemModel> acList2 = null;
                    ExcelUtility.ConvertMMABOMActivityExcelToMMABOMActivityModel(dtActivityList[i], ref acList1, ref acList2);

                    //int limit = 100; // 100 items limit by header
                    //if (list1.Count() > limit)
                    //{
                    //    int j = 1;
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
                    //        string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), textName);

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
                    log.Info("========== " + textName + " Start =========");
                    string textExtension = ".txt";
                    string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), textName);

                    SAPUtility.ConvertToMMABOMTextFile(newList1, list2, acList1, acList2, textPath, fileName, textExtension, userSAP, validDate, options);
                    //}

                    i++;
                }

                int BOMFileStatus = 3; // Create

                ResponseModel res = core.UpdateStatusBOMFile(bomFileID, BOMFileStatus);

                if (res.Status) // update status success
                {
                    log.Info("========== Update Status Success =========");

                    BOMFileFilterModel filter = new BOMFileFilterModel();
                    filter.ProductsTypeID = 1; // Monomer
                    filter.Pagination.Page = pageNo;
                    BOMFileViewModel model = core.GetBOMFileView(filter);

                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    log.Error("========== " + res.Message + " =========");

                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                log.Error("========== " + ex.Message + " =========");

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
                "MMA Grade",
                "MMA Loading"
            };

            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("BOM");

                DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/Files/Monomer/SAP/BOM"));
                FileInfo[] Files = d.GetFiles("*.txt");

                foreach (var a in Files)
                {
                    var path = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), a.Name);
                    if (System.IO.File.Exists(path))
                    {
                        zip.AddFile(path, "BOM");
                    }
                }

                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("MMABOMFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);

                log.Info("========== "  + fileName + " : Downloaded =========");

                Response.End();
            }
        }

        [HttpPost]
        public ActionResult GenerateDeleteBOMTextFile(int bomFileID, string fileName, string userSAP, int pageNo)
        {
            try
            {
                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/BOM"), "BOM.xlsx");
                string extension = Path.GetExtension("BOM.xlsx").ToLower();

                DataTable dtGradeLevel = ExcelUtility.ReadMMBOMGradeLevelExcel(path, extension);

                List<BOMHeaderModel> bomGradeLevelHeaderList = null;
                List<BOMItemModel> bomGradeLevelItemList = null;
                ExcelUtility.ConvertMMBOMGradeExcelToMMBOMGradeModel(dtGradeLevel, ref bomGradeLevelHeaderList, ref bomGradeLevelItemList);

                string textName = fileName + "_BOM_GRADE_DELETE";
                string textExtension = ".txt";
                string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), textName);
                string User = userSAP;

                List<BOMHeaderModel> gradeList = BOMUtility.CheckBOMAlt(bomGradeLevelHeaderList);

                SAPUtility.ConvertMMBOMToDeleteTextFile(gradeList, bomGradeLevelItemList, textPath, textExtension, User);

                DataTable dtPkgLevel = ExcelUtility.ReadMMBOMPkgLevelExcel(path, extension);

                List<BOMHeaderModel> bomPkgLevelHeaderList = null;
                List<BOMItemModel> bomPkgLevelItemList = null;
                ExcelUtility.ConvertMMBOMPkgExcelToMMBOMPkgModel(dtPkgLevel, ref bomPkgLevelHeaderList, ref bomPkgLevelItemList);

                string textPkgName = fileName + "_BOM_PKG_DELETE";
                string textPkgExtension = ".txt";
                string textPkgPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), textPkgName);
                string UserPkg = userSAP;

                List<BOMHeaderModel> pkgList = BOMUtility.CheckBOMAlt(bomPkgLevelHeaderList);

                SAPUtility.ConvertMMBOMToDeleteTextFile(pkgList, bomPkgLevelItemList, textPkgPath, textPkgExtension, UserPkg);

                // 4 = status delete
                int BOMFileStatus = 4; // Delete
                ResponseModel res = core.UpdateStatusBOMFile(bomFileID, BOMFileStatus);

                if (res.Status)
                {
                    BOMFileFilterModel filter = new BOMFileFilterModel();
                    filter.Pagination.Page = pageNo;
                    filter.ProductsTypeID = 1; // Monomer
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
        public void DownloadDeleteBOMTextFile(string fileName)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("BOM_DELETE");

                var pathGrade = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), fileName + "_BOM_GRADE_DELETE" + ".txt");
                var pathPkg = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/BOM"), fileName + "_BOM_PKG_DELETE" + ".txt");

                zip.AddFile(pathGrade, "BOM_DELETE");
                zip.AddFile(pathPkg, "BOM_DELETE");

                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("BOMDeleteFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }

        [HttpPost]
        public ActionResult GenerateCreateWorkCenterTextFile(int workCenterFileID, string fileName, string userSAP, int pageNo)
        {
            try
            {
                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/WorkCenter"), "WorkCenter.xlsx"); // Get WorkCenter File
                string extension = Path.GetExtension("WorkCenter.xlsx").ToLower();

                DataTable dt = ExcelUtility.ReadMMWorkCenterExcel(path, extension);
                List<WorkCenterModel> workCenterList = null;
                ExcelUtility.ConvertMMWorkCenterExcelToMMWorkCenterModel(dt, ref workCenterList);

                int WorkCenterFileStatus = 3; // Create

                ResponseModel res = core.UpdateStatusWorkCenterFile(workCenterFileID, WorkCenterFileStatus);

                if (res.Status)
                {
                    WorkCenterFileFilterModel filter = new WorkCenterFileFilterModel();
                    filter.ProductsTypeID = 1; // monomer 
                    filter.Pagination.Page = pageNo;
                    var model = core.GetWorkCenterFileView(filter);
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
        public ActionResult DownloadCreateWorkCenterTextFile(string fileName)
        {
            // download
            var downloadPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/WorkCenter"), fileName + "_WorkCenter.txt");
            return File(downloadPath, "text/plain", fileName + "_WorkCenter.txt");
        }

        [HttpPost]
        public ActionResult GenerateWorkCenterRoutingTextFile(int workCenterRoutingFileID, string fileName, string userSAP, string validDateText, string pathText, int pageNo, List<string> options, List<string> sheets)
        {
            try
            {
                var enUser = Environment.UserName;
                var userName = !string.IsNullOrEmpty(enUser) ? enUser : Session["Username"].ToString();
                log.Info("========== Generate By " + userName + " =========");
                DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture);
                //var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/WorkCenterRouting"), "WorkCenterRouting.xlsx"); // Get WorkCenter Routing File
                var path = pathText;
                string extension = Path.GetExtension(pathText);

                List<DataTable> dtList = ExcelUtility.ReadWorkCenterRoutingExcelList(path, extension, sheets);

                // delete all files before generate new files
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/Monomer/SAP/WorkCenterRouting"));
                foreach (string filePath in filePaths)
                    System.IO.File.Delete(filePath);
                
                int i = 0;
                foreach (var o in dtList)
                {
                    List<WorkCenterRoutingModel> list1 = null;
                    List<WorkCenterRoutingItemModel> list2 = null;
                    ExcelUtility.ConvertWorkCenterRoutingExcelToWorkCenterRoutingModel(o, ref list1, ref list2);

                    int limit = 15372; // limit by item
                    if (list2.Count() > limit)
                    {
                        int j = 1;
                        int ct = 1;
                        int ht = 1;
                        int ec = 15372; // number of list
                        int hc = 2562; // number of hlist
                        int countIList = 15372;
                        int countHList = 2562;
                        while (ct < list2.Count())
                        {
                            List<WorkCenterRoutingModel> listHcut = new List<WorkCenterRoutingModel>();
                            List<WorkCenterRoutingItemModel> listIcut = new List<WorkCenterRoutingItemModel>();
                            int ilast = list2.Count();
                            int hlast = list1.Count();

                            int nCount = countIList > ilast ? ilast - ct : ec;
                            int hCount = countHList > hlast ? hlast - ht : hc;

                            listHcut = list1.GetRange(ht - 1, hCount);
                            listIcut = list2.GetRange(ct - 1, nCount);

                            string textName = fileName + sheets[i].Replace(" ", "") + j;
                            log.Info("========== " + textName + " Start =========");
                            string textExtension = ".txt";
                            string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/WorkCenterRouting"), textName);
                            SAPUtility.ConvertWorkCenterRoutingToTextFile(listHcut, listIcut, textPath, fileName, textExtension, userSAP, validDate, options);
                            ct += ec;
                            ht += hc;
                            countIList += ec;
                            countHList += hc;
                            j++;
                        }
                    }
                    else
                    {
                        

                        string textName = fileName + sheets[i].Replace(" ", "");
                        log.Info("========== " + textName + " Start =========");
                        string textExtension = ".txt";
                        string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/WorkCenterRouting"), textName);
                        SAPUtility.ConvertWorkCenterRoutingToTextFile(list1, list2, textPath, fileName, textExtension, userSAP, validDate, options);
                    }
                    i++;
                }

                int WorkCenterRoutingFileStatus = 3; // Create

                ResponseModel res = core.UpdateStatusWorkCenterRoutingFile(workCenterRoutingFileID, WorkCenterRoutingFileStatus);

                if (res.Status)
                {
                    log.Info("========== Update Status Success =========");

                    WorkCenterRoutingFileFilterModel filter = new WorkCenterRoutingFileFilterModel();
                    filter.ProductsTypeID = 1; // monomer 
                    filter.Pagination.Page = pageNo;
                    var model = core.GetWorkCenterRoutingFileView(filter);
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    log.Info("========== " + res.Message + " =========");

                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                log.Info("========== " + ex.Message + " =========");

                return Json(new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public void DownloadWorkCenterRoutingTextFile(string fileName)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("WorkCenterRouting");

                DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/Files/Monomer/SAP/WorkCenterRouting"));
                FileInfo[] Files = d.GetFiles("*.txt");

                foreach (var a in Files)
                {
                    var path = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/WorkCenterRouting"), a.Name);
                    if (System.IO.File.Exists(path))
                    {
                        zip.AddFile(path, "WorkCenterRouting");
                    }
                }

                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("WorkCenterRoutingFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);

                log.Info("========== " + fileName + " : Downloaded =========");

                Response.End();
            }
        }

        [HttpPost]
        public ActionResult GenerateDeleteWorkCenterTextFile(int workCenterFileID, string fileName, string userSAP, int pageNo)
        {
            try
            {
                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/WorkCenter"), "WorkCenter.xlsx"); // Get BOM File
                string extension = Path.GetExtension("WorkCenter.xlsx").ToLower();

                DataTable dt = ExcelUtility.ReadMMWorkCenterExcel(path, extension);

                List<WorkCenterModel> workCenterList = null;

                ExcelUtility.ConvertMMWorkCenterExcelToMMWorkCenterModel(dt, ref workCenterList);

                string textName = fileName + "_DeleteWorkCenter";
                string textExtension = ".txt";
                string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/WorkCenter"), textName);

                SAPUtility.ConvertMMWorkCenterToDeleteTextFile(workCenterList, textPath, textExtension, userSAP);

                int WorkCenterFileStatus = 4; // Delete

                ResponseModel res = core.UpdateStatusWorkCenterFile(workCenterFileID, WorkCenterFileStatus);

                if (res.Status)
                {
                    WorkCenterFileFilterModel filter = new WorkCenterFileFilterModel();
                    filter.ProductsTypeID = 1; // monomer
                    filter.Pagination.Page = pageNo;
                    var model = core.GetWorkCenterFileView(filter);
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
        public ActionResult DownloadDeleteWorkCenterTextFile(string fileName)
        {
            // Download
            var downloadPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/WorkCenter"), fileName + "_DeleteWorkCenter.txt");
            return File(downloadPath, "text/plain", fileName + "_DeleteWorkCenter.txt");
        }

        [HttpPost]
        public ActionResult GenerateCreateProductionVersionTextFile(int productionVersionFileID, string fileName, string userSAP, string validDateText, int pageNo)
        {
            try
            {
                DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture);
                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/ProductionVersion"), "ProductionVersion.xlsx"); // Get ProductionVersion File
                string extension = Path.GetExtension("ProductionVersion.xlsx").ToLower();

                DataTable dt = ExcelUtility.ReadMMProductionVersionExcel(path, extension);

                List<ProductionVersionModel> productionVersionList = null;
                ExcelUtility.ConvertMMProductionVersionExcelToMMProductionVersionModel(dt, ref productionVersionList);

                string textName = fileName + "_ProductionVersion";
                string textExtension = ".txt";
                string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/ProductionVersion"), textName);

                SAPUtility.ConvertMMProductionVersionToTextFile(productionVersionList, textPath, textExtension, userSAP, validDate);

                int ProductionVersionFileStatus = 3; // Text

                ResponseModel res = core.UpdateStatusProductionVersionFile(productionVersionFileID, ProductionVersionFileStatus);

                if (res.Status)
                {
                    ProductionVersionFileFilterModel filter = new ProductionVersionFileFilterModel();
                    filter.ProductsTypeID = 1; // monomer
                    filter.Pagination.Page = pageNo;
                    var model = core.GetProductionVersionFileView(filter);
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
                });
            }
        }

        [HttpGet]
        public ActionResult DownloadCreateProductionVersionTextFile(string fileName)
        {
            // Download
            var downloadPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/ProductionVersion"), fileName + "_ProductionVersion.txt");
            return File(downloadPath, "text/plain", fileName + "_ProductionVersion.txt");
        }

        [HttpPost]
        public ActionResult GenerateChangeProductionVersionTextFile(int productionVersionFileID, string fileName, string userSAP, string validDateText, int pageNo)
        {
            try
            {
                DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture);
                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/ProductionVersion"), "ProductionVersion.xlsx"); // Get ProductionVersion File
                string extension = Path.GetExtension("ProductionVersion.xlsx").ToLower();

                DataTable dt = ExcelUtility.ReadMMProductionVersionExcel(path, extension);

                List<ProductionVersionModel> productionVersionList = null;
                ExcelUtility.ConvertMMProductionVersionExcelToMMProductionVersionModel(dt, ref productionVersionList);

                string textName = fileName + "_ChangeProductionVersion";
                string textExtension = ".txt";
                string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/ProductionVersion"), textName);

                SAPUtility.ConvertMMProductionVersionToChangeTextFile(productionVersionList, textPath, textExtension, userSAP, validDate);

                int ProductionVersionFileStatus = 5; // Change

                ResponseModel res = core.UpdateStatusProductionVersionFile(productionVersionFileID, ProductionVersionFileStatus);

                if (res.Status)
                {
                    ProductionVersionFileFilterModel filter = new ProductionVersionFileFilterModel();
                    filter.ProductsTypeID = 1;
                    filter.Pagination.Page = pageNo;
                    var model = core.GetProductionVersionFileView(filter);
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
        public ActionResult DownloadChangeProductionVersionTextFile(string fileName)
        {
            // Download
            var downloadPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/ProductionVersion"), fileName + "_ProductionVersion.txt");
            return File(downloadPath, "text/plain", fileName + "_ProductionVersion.txt");
        }

        [HttpPost]
        public ActionResult GenerateDeleteProductionVersionTextFile(int productionVersionFileID, string fileName, string userSAP, string validDateText, int pageNo)
        {
            try
            {
                DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture);
                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/ProductionVersion"), "ProductionVersion.xlsx"); // Get ProductionVersion File
                string extension = Path.GetExtension("ProductionVersion.xlsx").ToLower();

                DataTable dt = ExcelUtility.ReadMMProductionVersionExcel(path, extension);

                List<ProductionVersionModel> productionVersionList = null;
                ExcelUtility.ConvertMMProductionVersionExcelToMMProductionVersionModel(dt, ref productionVersionList);

                string textName = fileName + "_DeleteProductionVersion";
                string textExtension = ".txt";
                string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/ProductionVersion"), textName);

                SAPUtility.ConvertMMProductionVersionToDeleteTextFile(productionVersionList, textPath, textExtension, userSAP, validDate);

                int ProductionVersionFileStatus = 4; // Delete

                ResponseModel res = core.UpdateStatusProductionVersionFile(productionVersionFileID, ProductionVersionFileStatus);

                if (res.Status)
                {
                    ProductionVersionFileFilterModel filter = new ProductionVersionFileFilterModel();
                    filter.ProductsTypeID = 1; //  monomer
                    filter.Pagination.Page = pageNo;
                    var model = core.GetProductionVersionFileView(filter);

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
        public ActionResult DownloadDeleteProductionVersionTextFile(string fileName)
        {
            // Download
            var downloadPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/ProductionVersion"), fileName + "_DeleteProductionVersion.txt");
            return File(downloadPath, "text/plain", fileName + "_DeleteProductionVersion.txt");
        }

        [HttpPost]
        public ActionResult GenerateCreateRoutingTextFile(int routingFileID, string fileName, string userSAP, string validDateText, int pageNo)
        {
            try
            {
                DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture);
                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/Routing"), "Routing.xlsx"); // Get Routing File
                string extension = Path.GetExtension("Routing.xlsx").ToLower();

                // Routing Excel
                DataTable dt = ExcelUtility.ReadMMRoutingExcel(path, extension);

                List<RoutingHeaderModel> routingHeaderList = null;
                List<RoutingItemModel> routingItemList = null;
                ExcelUtility.ConvertMMRoutingExcelToMMRoutingModel(dt, ref routingHeaderList, ref routingItemList);

                string textName = fileName + "_Routing";
                string textExtension = ".txt";
                string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/Routing"), textName);

                SAPUtility.ConvertMMRoutingToTextFile(routingHeaderList, routingItemList, textPath, textExtension, userSAP, validDate);

                int RoutingFileStatus = 3; // Create

                ResponseModel res = core.UpdateStatusRoutingFile(routingFileID, RoutingFileStatus);

                if (res.Status)
                {
                    RoutingFileFilterModel filter = new RoutingFileFilterModel();
                    filter.Pagination.Page = pageNo;
                    filter.ProductsTypeID = 1; // monomer
                    var model = core.GetRoutingFileView(filter);

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
                });
            }
        }

        [HttpGet]
        public ActionResult DownloadCreateRoutingTextFile(string fileName)
        {
            // Download
            var downloadPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/Routing"), fileName + "_Routing.txt");
            return File(downloadPath, "text/plain", fileName + "_Routing.txt");
        }

        [HttpPost]
        public ActionResult GenerateCreatePackagingInstructionTextFile(int packagingInstructionFileID, string fileName, string userSAP, int pageNo)
        {
            try
            {
                var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/PackagingInstruction"), "PackagingInstruction.xlsx"); // Get PackagingInstruction File
                string extension = ".xlsx";

                // PackagingInstruction Excel
                DataTable dt = ExcelUtility.ReadMMPackagingInstructionExcel(path, extension);

                List<PackagingInstructionModel> packagingInstructionList = null;
                ExcelUtility.ConvertMMPackagingInstructionExcelToMMPackagingInstructionModel(dt, ref packagingInstructionList);

                // delete all files before generate new files
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/Monomer/SAP/PackagingInstruction"));
                foreach (string filePath in filePaths)
                    System.IO.File.Delete(filePath);

                string textName = fileName + "_PackagingInstruction";
                string textExtension = ".txt";
                string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/PackagingInstruction"), textName);

                SAPUtility.ConvertMMPackagingInstructionToTextFile(packagingInstructionList, textPath, textExtension, userSAP);

                int PackagingInstructionFileStatus = 3; // Create

                ResponseModel res = core.UpdateStatusPackagingInstructionFile(packagingInstructionFileID, PackagingInstructionFileStatus);

                if (res.Status)
                {
                    PackagingInstructionFileFilterModel filter = new PackagingInstructionFileFilterModel();
                    filter.ProductsTypeID = 1; //monomer
                    filter.Pagination.Page = pageNo;
                    var model = core.GetPackagingInstructionFileView(filter);

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
                });
            }
        }

        [HttpGet]
        public ActionResult DownloadCreatePackagingInstructionTextFile(string fileName)
        {
            // Download
            var downloadPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/PackagingInstruction"), fileName + "_PackagingInstruction.txt");
            return File(downloadPath, "text/plain", fileName + "_PackagingInstruction.txt");
        }

        [HttpPost]
        public ActionResult GenerateCreateInspectionPlanTextFile(int inspectionPlanFileID, string fileName, string userSAP, string validDateText, string pathText, int pageNo)
        {
            try
            {
                DateTime validDate = DateTime.ParseExact(validDateText, "dd/MM/yyyy", usCulture); // convert date
                //var path = Path.Combine(Server.MapPath("~/Files/Monomer/Excels/InspectionPlan"), "InspectionPlan.xlsx"); // Get Inspection Plan File
                var path = pathText;
                string extension = Path.GetExtension(pathText);

                // Inspection Plan Excel
                DataTable dt = ExcelUtility.ReadMMInspectionPlanExcel(path, extension);

                List<InspectionPlanHeaderModel> inspectionPlanHeaderList = null;
                List<InspectionPlanItemModel> inspectionPlanItemList = null;
                ExcelUtility.ConvertMMInspectionPlanExcelToMMInspectionPlanModel(dt, ref inspectionPlanHeaderList, ref inspectionPlanItemList);

                // delete all files before generate new files
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/Monomer/SAP/InspectionPlan"));
                foreach (string filePath in filePaths)
                    System.IO.File.Delete(filePath);

                string textName = fileName + "_InspectionPlan";
                string textExtension = ".txt";
                string textPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/InspectionPlan"), textName);

                SAPUtility.ConvertMMInspectionPlanToTextFile(inspectionPlanHeaderList, inspectionPlanItemList, textPath, textExtension, userSAP, validDate);

                int InspectionPlanFileStatus = 3; // Create

                ResponseModel res = core.UpdateStatusInspectionPlanFile(inspectionPlanFileID, InspectionPlanFileStatus);

                if (res.Status)
                {
                    InspectionPlanFileFilterModel filter = new InspectionPlanFileFilterModel();
                    filter.ProductsTypeID = 1; // monomer
                    filter.Pagination.Page = pageNo;
                    var model = core.GetInspectionPlanFileView(filter);

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
        public ActionResult DownloadCreateInspectionPlanTextFile(string fileName)
        {
            // Download
            var downloadPath = Path.Combine(Server.MapPath("~/Files/Monomer/SAP/InspectionPlan"), fileName + "_InspectionPlan.txt");
            return File(downloadPath, "text/plain", fileName + "InspectionPlan.txt");
        }

        [HttpGet]
        public ActionResult DownloadRoutingTextFile(int version)
        {
            var path = Path.Combine(Server.MapPath("~/Files/Monomer/SAP"), "TMMA_ROUTING_TEXT_SAP_PP" + version + ".txt");
            return File(path, "text/plain", "TMMA_ROUTING_TEXT_SAP_PP" + version + ".txt");
        }
    }
}