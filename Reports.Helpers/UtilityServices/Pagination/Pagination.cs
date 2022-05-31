using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Helpers.UtilityServices.Pagination
{
    public static class Pagination
    {
        public static PaginationResult<T> ToPaged<T>(this IEnumerable<T> source, int requestedPageIndex, int itemsInPageCount)
        {
            double count = source.Count();
            var pagesCount = (int)Math.Ceiling(count / itemsInPageCount);

            if (count == 0)
                return new PaginationResult<T>()
                {
                    Succeeded = false,
                    Message = "آیتمی جهت نمایش موجود نیست!",
                    PagesCount = 0

                };

            else if (requestedPageIndex < 0 || requestedPageIndex > pagesCount)
            {
                return new PaginationResult<T>()
                {
                    Succeeded = false,
                    Message = "صفحه درخواست شده خارج از محدوده است!",

                };
            }

            var result = new PaginationResult<T>()
            {
                Succeeded = true,
                RequestedPageList = source.Skip((requestedPageIndex - 1) * itemsInPageCount).Take(itemsInPageCount).ToList(),
                PagesCount = pagesCount
            };

            if (requestedPageIndex == 1)
                result.PrevIsDiabled = true;
            if (requestedPageIndex == pagesCount)
                result.NextIsDisabled = true;

            if (requestedPageIndex <= 5)
                result.FirstPageIndexToShow = 1;
            else
                result.FirstPageIndexToShow = requestedPageIndex - 4;

            if (result.FirstPageIndexToShow + 8 <= pagesCount)
                result.LastPageIndexToShow = result.FirstPageIndexToShow + 8;
            else
                result.LastPageIndexToShow = pagesCount;

            return result;

        }
        public class PaginationResult<T>
        {
            public List<T>? RequestedPageList { get; set; } = null;
            public int PagesCount { get; set; } = 1;
            public bool Succeeded { get; set; }
            public string Message { get; set; }
            public bool PrevIsDiabled { get; set; }
            public bool NextIsDisabled { get; set; }
            public int FirstPageIndexToShow { get; set; }
            public int LastPageIndexToShow { get; set; }
            public bool isEmptyList { get; set; }
            public bool OutOfRangeRequestedPage { get; set; }
        }
    }
}
