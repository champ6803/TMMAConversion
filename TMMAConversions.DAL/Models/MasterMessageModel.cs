using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMMAConversions.DAL.Models
{
    public class MasterMeassageViewModel : ResponseModel
    {
        public List<MasterMessageModel> List { get; set; }
        public MasterMessageFilterModel Filter { get; set; }
    }

    public class MasterMessageModel
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public string TypeOfMessage { get; set; }
        public string MessageClass { get; set; }
        public string MassageNumber { get; set; }
        public string MessageGroup { get; set; }
        public string MessageLevel { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class MasterMessageFilterModel
    {
        public string Keywords { get; set; }
        public int? MessageID { get; set; }
        public int[] MessageIDs { get; set; }
        public string MessageText { get; set; }
        public string TypeOfMessage { get; set; }
        public string MessageClass { get; set; }
        public string MassageNumber { get; set; }
        public string MessageGroup { get; set; }
        public string MessageLevel { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public PaginationModel Pagination { get; set; }
        public MasterMessageFilterModel()
        {
            Pagination = new PaginationModel();
        }
    }
}