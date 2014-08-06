using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridViewPagination
{
    public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);

    public class PageChangedEventArgs : EventArgs
    {
        public PageChangedEventArgs(int page)
        {
            _page = page;
        }

        public int CurrentPage
        {
            get { return _page; }
        }

        private int _page;
    }
}
