using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newhl.Common.Utilities
{
    public static class Pagination
    {
        public static PagedList<T> ToPagedList<T>(this IList<T> source)
        {
            return new PagedList<T>(source, 0, Newhl.Common.Utilities.Constants.PageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source)
        {
            return new PagedList<T>(source, 0, Newhl.Common.Utilities.Constants.PageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }
    }
}
