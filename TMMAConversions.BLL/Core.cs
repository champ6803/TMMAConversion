using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMMAConversions.BLL.BLL;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.BLL
{
    public class Core
    {
        /// <summary>
        /// Monomer BOM
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ProductsViewModel GetProductsView(ProductsFilterModel filter)
        {
            return ProductsBLL.GetProductsView(filter);
        }

        public BOMFileViewModel GetBOMFileView(BOMFileFilterModel filter)
        {
            return BOMFileBLL.GetBOMFileView(filter);
        }

        public BOMFileModel GetBOMFile(int bomFileID)
        {
            return BOMFileBLL.GetBOMFile(bomFileID);
        }

        public ResponseModel AddBOMFile(BOMFileModel m, ref int _newID)
        {
            return BOMFileBLL.AddBOMFile(m, ref _newID);
        }

        public ResponseModel UpdateStatusBOMFile(int BOMFileID, int BOMFileStatus)
        {
            return BOMFileBLL.UpdateStatusBOMFile(BOMFileID, BOMFileStatus);
        }

        public BOMFileModel GetBOMFileLastVersion(int ProductsTypeID)
        {
            return BOMFileBLL.GetBOMFileLastVersion(ProductsTypeID);
        }

        /// <summary>
        /// Monomer Routing
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public RoutingFileViewModel GetRoutingFileView(RoutingFileFilterModel filter)
        {
            return RoutingFileBLL.GetRoutingFileView(filter);
        }

        public RoutingFileModel GetRoutingFile(int routingFileID)
        {
            return RoutingFileBLL.GetRoutingFile(routingFileID);
        }

        public ResponseModel AddRoutingFile(RoutingFileModel m, ref int _newID)
        {
            return RoutingFileBLL.AddRoutingFile(m, ref _newID);
        }

        public ResponseModel UpdateStatusRoutingFile(int RoutingFileID, int RoutingFileStatus)
        {
            return RoutingFileBLL.UpdateStatusRoutingFile(RoutingFileID, RoutingFileStatus);
        }

        public RoutingFileModel GetRoutingFileLastVersion(int ProductsTypeID)
        {
            return RoutingFileBLL.GetRoutingFileLastVersion(ProductsTypeID);
        }


        /// <summary>
        ///  Monomer Work Center
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public WorkCenterFileViewModel GetWorkCenterFileView(WorkCenterFileFilterModel filter)
        {
            return WorkCenterFileBLL.GetWorkCenterFileView(filter);
        }

        public ResponseModel AddWorkCenterFile(WorkCenterFileModel m, ref int _newID)
        {
            return WorkCenterFileBLL.AddWorkCenterFile(m, ref _newID);
        }

        public ResponseModel UpdateStatusWorkCenterFile(int WorkCenterFileID, int WorkCenterFileStatus)
        {
            return WorkCenterFileBLL.UpdateStatusWorkCenterFile(WorkCenterFileID, WorkCenterFileStatus);
        }

        public WorkCenterFileModel GetWorkCenterFileLastVersion()
        {
            return WorkCenterFileBLL.GetWorkCenterFileLastVersion();
        }


        /// <summary>
        /// Monomer Production Version
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ProductionVersionFileViewModel GetProductionVersionFileView(ProductionVersionFileFilterModel filter)
        {
            return ProductionVersionFileBLL.GetProductionVersionFileView(filter);
        }

        public ResponseModel AddProductionVersionFile(ProductionVersionFileModel m, ref int _newID)
        {
            return ProductionVersionFileBLL.AddProductionVersionFile(m, ref _newID);
        }

        public ProductionVersionFileModel GetProductionVersionFileLastVersion()
        {
            return ProductionVersionFileBLL.GetProductionVersionFileLastVersion();
        }

        public ResponseModel UpdateStatusProductionVersionFile(int ProductionVersionFileID, int ProductionVersionFileStatus)
        {
            return ProductionVersionFileBLL.UpdateStatusProductionVersionFile(ProductionVersionFileID, ProductionVersionFileStatus);
        }


        /// <summary>
        /// RoutingWithoutMaterialFile
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public RoutingWithoutMaterialFileViewModel GetRoutingWithoutMaterialFileView(RoutingWithoutMaterialFileFilterModel filter)
        {
            return RoutingWithoutMaterialFileBLL.GetRoutingWithoutMaterialFileView(filter);
        }

        public RoutingWithoutMaterialFileModel GetRoutingWithoutMaterialFile(int routingWithoutMaterialFileID)
        {
            return RoutingWithoutMaterialFileBLL.GetRoutingWithoutMaterialFile(routingWithoutMaterialFileID);
        }

        public ResponseModel AddRoutingWithoutMaterialFile(RoutingWithoutMaterialFileModel m, ref int _newID)
        {
            return RoutingWithoutMaterialFileBLL.AddRoutingWithoutMaterialFile(m, ref _newID);
        }

        public ResponseModel UpdateStatusRoutingWithoutMaterialFile(int routingWithoutMaterialFileID, int routingWithoutMaterialFileStatus)
        {
            return RoutingWithoutMaterialFileBLL.UpdateStatusRoutingWithoutMaterialFile(routingWithoutMaterialFileID, routingWithoutMaterialFileStatus);
        }

        public RoutingWithoutMaterialFileModel GetRoutingWithoutMaterialFileLastVersion(int ProductsTypeID)
        {
            return RoutingWithoutMaterialFileBLL.GetRoutingWithoutMaterialFileLastVersion(ProductsTypeID);
        }

        /// <summary>
        /// RoutingWithMaterialFile
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public RoutingWithMaterialFileViewModel GetRoutingWithMaterialFileView(RoutingWithMaterialFileFilterModel filter)
        {
            return RoutingWithMaterialFileBLL.GetRoutingWithMaterialFileView(filter);
        }

        public RoutingWithMaterialFileModel GetRoutingWithMaterialFile(int routingWithMaterialFileID)
        {
            return RoutingWithMaterialFileBLL.GetRoutingWithMaterialFile(routingWithMaterialFileID);
        }

        public ResponseModel AddRoutingWithMaterialFile(RoutingWithMaterialFileModel m, ref int _newID)
        {
            return RoutingWithMaterialFileBLL.AddRoutingWithMaterialFile(m, ref _newID);
        }

        public ResponseModel UpdateStatusRoutingWithMaterialFile(int routingWithMaterialFileID, int routingWithMaterialFileStatus)
        {
            return RoutingWithMaterialFileBLL.UpdateStatusRoutingWithMaterialFile(routingWithMaterialFileID, routingWithMaterialFileStatus);
        }

        public RoutingWithMaterialFileModel GetRoutingWithMaterialFileLastVersion(int ProductsTypeID)
        {
            return RoutingWithMaterialFileBLL.GetRoutingWithMaterialFileLastVersion(ProductsTypeID);
        }


        /// <summary>
        /// AssignMaterialToRoutingFile
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public AssignMaterialToRoutingFileViewModel GetAssignMaterialToRoutingFileView(AssignMaterialToRoutingFileFilterModel filter)
        {
            return AssignMaterialToRoutingFileBLL.GetAssignMaterialToRoutingFileView(filter);
        }

        public AssignMaterialToRoutingFileModel GetAssignMaterialToRoutingFile(int assignMaterialToRoutingFileID)
        {
            return AssignMaterialToRoutingFileBLL.GetAssignMaterialToRoutingFile(assignMaterialToRoutingFileID);
        }

        public ResponseModel AddAssignMaterialToRoutingFile(AssignMaterialToRoutingFileModel m, ref int _newID)
        {
            return AssignMaterialToRoutingFileBLL.AddAssignMaterialToRoutingFile(m, ref _newID);
        }

        public ResponseModel UpdateStatusAssignMaterialToRoutingFile(int assignMaterialToRoutingFileID, int assignMaterialToRoutingFileStatus)
        {
            return AssignMaterialToRoutingFileBLL.UpdateStatusAssignMaterialToRoutingFile(assignMaterialToRoutingFileID, assignMaterialToRoutingFileStatus);
        }

        public AssignMaterialToRoutingFileModel GetAssignMaterialToRoutingFileLastVersion(int ProductsTypeID)
        {
            return AssignMaterialToRoutingFileBLL.GetAssignMaterialToRoutingFileLastVersion(ProductsTypeID);
        }

        /// <summary>
        /// Monomer Inspection Plan
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        public InspectionPlanFileViewModel GetInspectionPlanFileView(InspectionPlanFileFilterModel filter)
        {
            return InspectionPlanFileBLL.GetInspectionPlanFileView(filter);
        }

        public InspectionPlanFileModel GetInspectionPlanFile(int inspectionPlanFileID)
        {
            return InspectionPlanFileBLL.GetInspectionPlanFile(inspectionPlanFileID);
        }

        public ResponseModel AddInspectionPlanFile(InspectionPlanFileModel m, ref int _newID)
        {
            return InspectionPlanFileBLL.AddInspectionPlanFile(m, ref _newID);
        }

        public ResponseModel UpdateStatusInspectionPlanFile(int InspectionPlanFileID, int InspectionPlanFileStatus)
        {
            return InspectionPlanFileBLL.UpdateStatusInspectionPlanFile(InspectionPlanFileID, InspectionPlanFileStatus);
        }

        public InspectionPlanFileModel GetInspectionPlanFileLastVersion(int ProductsTypeID)
        {
            return InspectionPlanFileBLL.GetInspectionPlanFileLastVersion(ProductsTypeID);
        }

        /// <summary>
        /// Monomer Packaging Instruction
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        public PackagingInstructionFileViewModel GetPackagingInstructionFileView(PackagingInstructionFileFilterModel filter)
        {
            return PackagingInstructionFileBLL.GetPackagingInstructionFileView(filter);
        }

        public PackagingInstructionFileModel GetPackagingInstructionFile(int packagingInstructionFileID)
        {
            return PackagingInstructionFileBLL.GetPackagingInstructionFile(packagingInstructionFileID);
        }

        public ResponseModel AddPackagingInstructionFile(PackagingInstructionFileModel m, ref int _newID)
        {
            return PackagingInstructionFileBLL.AddPackagingInstructionFile(m, ref _newID);
        }

        public ResponseModel UpdateStatusPackagingInstructionFile(int PackagingInstructionFileID, int PackagingInstructionFileStatus)
        {
            return PackagingInstructionFileBLL.UpdateStatusPackagingInstructionFile(PackagingInstructionFileID, PackagingInstructionFileStatus);
        }

        public PackagingInstructionFileModel GetPackagingInstructionFileLastVersion(int ProductsTypeID)
        {
            return PackagingInstructionFileBLL.GetPackagingInstructionFileLastVersion(ProductsTypeID);
        }

        /// <summary>
        /// User
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        public UserModel GetUserByUsernamePassword(string username, string password)
        {
            return UserBLL.GetUserByUsernamePassword(username, password);
        }

        public UserModel GetUserByUsername(string username)
        {
            return UserBLL.GetUserByUsername(username);
        }

        /// <summary>
        ///  Monomer WorkCenter Routing
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public WorkCenterRoutingFileViewModel GetWorkCenterRoutingFileView(WorkCenterRoutingFileFilterModel filter)
        {
            return WorkCenterRoutingFileBLL.GetWorkCenterRoutingFileView(filter);
        }

        public ResponseModel AddWorkCenterRoutingFile(WorkCenterRoutingFileModel m, ref int _newID)
        {
            return WorkCenterRoutingFileBLL.AddWorkCenterRoutingFile(m, ref _newID);
        }

        public ResponseModel UpdateStatusWorkCenterRoutingFile(int WorkCenterRoutingFileID, int WorkCenterRoutingFileStatus)
        {
            return WorkCenterRoutingFileBLL.UpdateStatusWorkCenterRoutingFile(WorkCenterRoutingFileID, WorkCenterRoutingFileStatus);
        }

        public WorkCenterRoutingFileModel GetWorkCenterRoutingFileLastVersion()
        {
            return WorkCenterRoutingFileBLL.GetWorkCenterRoutingFileLastVersion();
        }
    }
}