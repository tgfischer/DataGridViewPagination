DataGridViewPagination
======================

This creates pagination functionality for WinForm's DataGridView. It allows you to move through the pages by  ```Button``` controls.

The first step is to set up the ```DataGridViewPaginationAdapter``` in your form. In my example form, I do this in the constructor

```c#
// Sets up an adapter with a DataTable
this.adapter = new DataGridViewPaginationAdapter(dataTable);

// Event when the page changes
this.adapter.PageChanged += new PageChangedEventHandler(this.adapter_PageChanged);

// Move to the first page
this.adapter.MoveFirst();
```

Then you have to set up the ```PageChanged``` event method.

```c#
private void adapter_PageChanged(object sender, PageChangedEventArgs e)
{
  // DataTable to hold all of this page's data
  DataTable dataTable = this.adapter.GetPage(this.adapter.CurrentPage);
  
  // Display the dats in the DataGridView
  this.dataGridView.DataSource = dataTable.AsDataView();
  
  bool hasNext = this.adapter.HasNext;        // Check for a proceeding page
  bool hasPrev = this.adapter.HasPrevious;    // Check for a preceding page
  
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
    this.adapter.MoveNext();      // Move to the next page
}

private void previousButton_Click(object sender, EventArgs e)
{
    this.adapter.MovePrevious();  // Move to the previous page
}

private void firstButton_Click(object sender, EventArgs e)
{
    this.adapter.MoveFirst();     // Move to the first page
}
```

You can also add a ```TextBox``` that shows what page the user is on, and it is editable so the user can jump to other pages. I have also included an example of a label that displays the total number of pages in the ```DataGridView```
