using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMMAConversions.DAL.Models
{
    public class ResponseModel
    {
        public string Source { get; set; }
        public string Action { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime ResponseTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        public ResponseModel()
        {

        }
    }
}
