using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMMAConversions.DAL.Models
{
    public class PaginationModel
    {
        public bool IsPaging { get; set; }
        public int Page { get; set; }
        public int Skip
        {
            get
            {
                return (Page - 1) * Take;
            }
        }
        public int Take { get; set; }
        public int TotalRecord { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public double TotalPage
        {
            get
            {
                return Convert.ToInt16(Math.Ceiling((float)TotalRecord / (float)Take));
            }
        }

        public PaginationModel()
        {
            IsPaging = true;
            Page = 1;
            Take = 10;
        }

        public PaginationModel(int totalRecord, int? page, int take = 10)
        {
            var totalPage = (int)Math.Ceiling((decimal)totalRecord / (decimal)take);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPage)
            {
                endPage = totalPage;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalRecord = totalRecord;
            Page = currentPage;
            Take = take;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}