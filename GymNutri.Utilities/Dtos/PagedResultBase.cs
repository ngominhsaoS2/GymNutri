using System;
using System.Collections.Generic;
using System.Text;

namespace GymNutri.Utilities.Dtos
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = RowCount / PageSize;
                return RowCount % PageSize > 0 ? pageCount + 1 : pageCount;
            }
            set
            {
                PageCount = value;
            }
        }

        public int FirstRowOnPage
        {
            get
            {
                return (CurrentPage - 1) * PageSize + 1;
            }
        }

        public int LastRowOnPage
        {
            get
            {
                return Math.Min(CurrentPage * PageSize, RowCount);
            }
        }
    }
}
