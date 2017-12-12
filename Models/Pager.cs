using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShBazmool.Models
{
    public class Pager
    {
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }

        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if(startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if(endPage > totalItems)
            {
                endPage = totalPages;
                if(endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            TotalPages = totalPages;
            EndPage = endPage;
            StartPage = startPage;
            PageSize = pageSize;
            CurrentPage = currentPage;

        }
    }
}
