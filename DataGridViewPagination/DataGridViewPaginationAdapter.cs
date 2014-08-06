using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DataGridViewPagination
{
    class DataGridViewPaginationAdapter
    {
        public DataGridViewPaginationAdapter(DataTable dataTable)
        {
            _dataTable = dataTable;
        }

        /// <summary>
        /// Moves to the next record
        /// </summary>
        /// <returns>Return the new index</returns>
        public bool MoveNext()
        {
            if (this.HasNext)                   // Check is there is more records
            {
                ++_page;

                if (this.PageChanged != null)   // Make sure the event exists
                {
                    // Run this event
                    this.PageChanged(this, new PageChangedEventArgs(_page));
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves to the previous record
        /// </summary>
        /// <returns>Return the new index</returns>
        public bool MovePrevious()
        {
            if (this.HasPrevious)               // Check if there are previous records
            {
                --_page;

                if (this.PageChanged != null)   // Make sure the event exists
                {
                    // Run this event
                    this.PageChanged(this, new PageChangedEventArgs(_page));
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Move to the first record
        /// </summary>
        /// <returns>Return the new index</returns>
        public bool MoveFirst()
        {
            if (_dataTable.Rows.Count > 0)
            {
                _page = 0;                      // Move to the first index

                if (this.PageChanged != null)   // Make sure the event exists
                {
                    // Run this event
                    this.PageChanged(this, new PageChangedEventArgs(_page));
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Move to the last record
        /// </summary>
        /// <returns>Return the new index</returns>
        public bool MoveLast()
        {
            if (_dataTable.Rows.Count > 0)
            {
                _page = _dataTable.Rows.Count / _pageSize;  // Move to the last index

                // If the total records is divisible by the page size, the user can get to page [TotalPage] + 1
                if (_dataTable.Rows.Count % _pageSize == 0) _page--;

                if (this.PageChanged != null)               // Make sure the event exists
                {
                    // Run this event
                    this.PageChanged(this, new PageChangedEventArgs(_page));
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves to the specified page number
        /// </summary>
        /// <param name="n">The page number directly from the text box</param>
        /// <returns>Returns whether or not the adapter moved to the specified record</returns>
        public bool MoveTo(string n) 
        {
            int newPage = 0;

            if (!Int32.TryParse(n, out newPage))      // Saves the page number as an integer if it can
                throw new DataGridViewPaginationAdapterException("The inputted value is invalid");

            return this.MoveTo(--newPage);
        }

        /// <summary>
        /// Moves to the specified page number
        /// </summary>
        /// <param name="n">The page number</param>
        /// <returns>Returns whether or not the adapter moved to the specified record</returns>
        public bool MoveTo(int n)
        {
            if (n > -1 && n < this.TotalPages)
                _page = n;                            // Set the adapter to the specified record
            else
                throw new DataGridViewPaginationAdapterException("The inputted value is out of range");

            if (this.PageChanged != null)             // Make sure the event exists
            {
                // Run this event
                this.PageChanged(this, new PageChangedEventArgs(_page));
            }

            return true;
        }

        /// <summary>
        /// Fills a table with all of the data from this page
        /// </summary>
        /// <param name="page">The page number</param>
        /// <returns>Returns a DataTable filled with this page's data</returns>
        public DataTable GetPage(int page)
        {
            DataTable newTable = _dataTable.Copy();         // Creates a copy of the table so the schema is the same
            newTable.Rows.Clear();                          // Empties the table, leaving just the fields

            int size = this.GetPageSize();                  // Figure out how many records are on this page

            // Start the index at the current page * the page size (e.g. page 3 * page size 100 = record 300), and loop through
            // another [size] amount of records (e.g. start at record 300, loop through another 100 records. End at record 400)
            for (int i = page * _pageSize; i < page * _pageSize + size; i++)
            {
                newTable.ImportRow(_dataTable.Rows[i]);     // Add the record from the original datatable to the new table
            }

            return newTable;
        }

        /// <summary>
        /// Gets the number of records on this page
        /// </summary>
        /// <returns></returns>
        private int GetPageSize()
        {
            if (_dataTable.Rows.Count - _page * _pageSize >= _pageSize) // Occurs when the total remaining records exceeds the page size
                return _pageSize;
            else                                                        // Occurs when the total remaining records is less than the page size
                return _dataTable.Rows.Count % _pageSize;
        }

        /// <summary>
        /// Check if there is another record after the current one
        /// </summary>
        public bool HasNext
        {
            get { return _page + 1 < this.TotalPages; }
        }

        /// <summary>
        /// Check if there is a previous record
        /// </summary>
        public bool HasPrevious
        {
            get { return _page > 0; }
        }

        /// <summary>
        /// Get the adapter's DataTable
        /// </summary>
        public DataTable Table
        {
            get { return _dataTable; }
        }

        /// <summary>
        /// Get the number of records in the DataTable
        /// </summary>
        public int RecordCount
        {
            get { return _dataTable.Rows.Count; }
        }

        /// <summary>
        /// Gets the current record's index
        /// </summary>
        public int CurrentPage
        {
            get { return _page; }
            set { _page = value; }
        }

        /// <summary>
        /// Gets the maximum number of pages
        /// </summary>
        public int MaximumPageSize
        {
            get { return _pageSize; }
        }

        /// <summary>
        /// Gets the total number of pages
        /// </summary>
        public double TotalPages
        {
            get { return Math.Ceiling(_dataTable.Rows.Count * 1.0 / _pageSize); }
        }

        public event PageChangedEventHandler PageChanged;
        private int _page = 0;
        private int _pageSize = 100;
        private DataTable _dataTable;
    }
}
