using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewPagination
{
    public partial class ExampleForm : Form
    {
        public ExampleForm()
        {
            InitializeComponent();

            this.dataGridView.AutoGenerateColumns = true;   // Automatically adds the columns depending on the record data
            this.dataGridView.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.DataBindingComplete);
            this.currentPageTextBox.KeyDown += new KeyEventHandler(this.currentPageTextBox_KeyDown);

            this.adapter = new DataGridViewPaginationAdapter(this.GenerateDataTable());              // Sets up an adapter for the test data
            this.adapter.PageChanged += new PageChangedEventHandler(this.adapter_PageChanged);  // Event when the page changes
            this.adapter.MoveFirst();                       // Move to the first page

            this.maxPagesLabel.Text = "of " + adapter.TotalPages;
        }

        #region [ Generate Test Data ]
        /// <summary>
        /// Generates some data to show how the pagination works
        /// </summary>
        /// <returns>Returns a DataTable filled with data</returns>
        private DataTable GenerateDataTable()
        {
            DataTable dataTable = new DataTable();

            for (int i = 0; i < 5; i++)                 // Add 5 columns
            {
                dataTable.Columns.Add(i.ToString());
            }

            for (int i = 0; i < 99955; i++)              // Add 9955 rows
            {
                DataRow row = dataTable.NewRow();

                for (int j = 0; j < 5; j++)             // Iterate through each column
                {
                    row[j.ToString()] = i;
                }

                dataTable.Rows.Add(row);                // Add the row to the table
            }

            return dataTable;
        }
        #endregion

        /// <summary>
        /// Last Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastButton_Click(object sender, EventArgs e)
        {
            this.adapter.MoveLast();        // Move to the last page
        }

        /// <summary>
        /// Next Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, EventArgs e)
        {
            this.adapter.MoveNext();        // Move to the next page
        }

        /// <summary>
        /// Previous Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousButton_Click(object sender, EventArgs e)
        {
            this.adapter.MovePrevious();    // Move to the previous page
        }

        /// <summary>
        /// First Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void firstButton_Click(object sender, EventArgs e)
        {
            this.adapter.MoveFirst();       // Move to the first page
        }

        /// <summary>
        /// Current Page TextBox Key Down Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void currentPageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)    // If the user hits the Enter key
            {
                try
                {
                    this.adapter.MoveTo(this.currentPageTextBox.Text);  // Move to the desired page
                }
                catch (DataGridViewPaginationAdapterException ex)
                {
                    MessageBox.Show(ex.Message);

                    // Reset the text of the currentPageTextBox with the current page number
                    this.currentPageTextBox.Text = this.adapter.CurrentPage + 1 + "";
                }

                // Removes the 'Ding' noise
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Adapter Page Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adapter_PageChanged(object sender, PageChangedEventArgs e)
        {
            DataTable dataTable = this.adapter.GetPage(this.adapter.CurrentPage);   // DataTable to hold all of this page's data
            this.dataGridView.DataSource = dataTable.AsDataView();                  // Display the dats in the DataGridView
            this.currentPageTextBox.Text = this.adapter.CurrentPage + 1 + "";       // Sets the current page number in the text box

            if (!this.adapter.HasNext)                  // Disable the next/last buttons if there are no more pages
            {
                this.nextButton.Enabled = false;
                this.lastButton.Enabled = false;
            }
            else
            {
                this.nextButton.Enabled = true;
                this.lastButton.Enabled = true;
            }

            if (!this.adapter.HasPrevious)              // Disable the previous/first buttons if there are no pages before the current
            {
                this.previousButton.Enabled = false;
                this.firstButton.Enabled = false;
            }
            else
            {
                this.previousButton.Enabled = true;
                this.firstButton.Enabled = true;
            }
        }

        /// <summary>
        /// Displays the row numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Loops through each row in the DataGridView, and adds the row number to the header
            foreach (DataGridViewRow dataGridViewRow in this.dataGridView.Rows)
            {
                // Calculates the row number to be displayed
                dataGridViewRow.HeaderCell.Value = String.Format("{0}", dataGridViewRow.Index + 1 + 
                    this.adapter.CurrentPage * this.adapter.MaximumPageSize);
            }

            // This resizes the width of the row headers to fit the numbers
            this.dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private DataGridViewPaginationAdapter adapter;   // Manages the pagination
    }
}
