DataGridViewPagination
======================

This creates pagination functionality for WinForm's DataGridView. It allows you to move through the pages by  ```System.Windows.Forms.Button``` controls.

The first step is to set up the ```DataGridViewPaginationAdapter``` in your form

```c#
// Sets up an adapter with a DataTable
this.adapter = new DataGridViewPaginationAdapter(dataTable);

// Event when the page changes
this.adapter.PageChanged += new PageChangedEventHandler(this.adapter_PageChanged);

// Move to the first page
this.adapter.MoveFirst();

// ... Later in your form

private void adapter_PageChanged(object sender, PageChangedEventArgs e)
{
  DataTable dataTable = this.adapter.GetPage(this.adapter.CurrentPage);   // DataTable to hold all of this page's data
  this.dataGridView.DataSource = dataTable.AsDataView();                  // Display the dats in the DataGridView
  
  bool hasNext = this.adapter.HasNext;        // Check if there is another page after this one
  bool hasPrev = this.adapter.HasPrevious;    // Check if there is a page before this one
  
  // Enable/disable the buttons depending on the page number
  this.nextButton.Enabled = hasNext;
  this.lastButton.Enabled = hasNext;
  this.previousButton.Enabled = hasPrev;
  this.firstButton.Enabled = hasPrev;
}
```



You can use the ```DataGridViewPaginationAdapter``` to move to another page, like so

```c#
private void lastButton_Click(object sender, EventArgs e)
{
  this.adapter.MoveLast();        // Move to the last page
}

private void nextButton_Click(object sender, EventArgs e)
{
    this.adapter.MoveNext();        // Move to the next page
}

private void previousButton_Click(object sender, EventArgs e)
{
    this.adapter.MovePrevious();    // Move to the previous page
}

private void firstButton_Click(object sender, EventArgs e)
{
    this.adapter.MoveFirst();       // Move to the first page
}
```
