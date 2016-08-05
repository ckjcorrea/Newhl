using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.Common.Utilities;

namespace Newhl.MainSite.Web.Models
{
    public class PagingModel<TListData>
    {
        public PagingModel(IPagedList<TListData> source)
        {
            this.HasNextPage = source.HasNextPage;
            this.HasPreviousPage = source.HasPreviousPage;
            this.IsFirstPage = source.IsFirstPage;
            this.IsLastPage = source.IsLastPage;
            this.PageCount = source.PageCount;
            this.PageIndex = source.PageIndex;
            this.PageNumber = source.PageNumber;
            this.PageSize = source.PageSize;
            this.TotalItemCount = source.TotalItemCount;
        }

        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public int PageIndex { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
    }
}