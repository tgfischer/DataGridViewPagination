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
