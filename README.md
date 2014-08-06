DataGridViewPagination
======================

This creates pagination functionality for WinForm's DataGridView. It allows you to move through the pages by  ```System.Windows.Forms.Button``` controls.

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
