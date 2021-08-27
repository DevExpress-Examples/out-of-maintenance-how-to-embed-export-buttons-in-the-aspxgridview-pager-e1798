<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1798)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [ExportGridView.cs](./CS/WebSite/App_Code/ExportGridView.cs) (VB: [ExportGridView.vb](./VB/WebSite/App_Code/ExportGridView.vb))
* [ExportGridViewPager.cs](./CS/WebSite/App_Code/ExportGridViewPager.cs) (VB: [ExportGridViewPager.vb](./VB/WebSite/App_Code/ExportGridViewPager.vb))
* [ExportHelper.cs](./CS/WebSite/App_Code/ExportHelper.cs) (VB: [ExportHelper.vb](./VB/WebSite/App_Code/ExportHelper.vb))
* [ExportType.cs](./CS/WebSite/App_Code/ExportType.cs) (VB: [ExportType.vb](./VB/WebSite/App_Code/ExportType.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# How to embed export buttons in the ASPxGridView pager


<p>This example was imported from the <a href="https://www.devexpress.com/Support/Center/p/S132943">S132943</a> report to provide users with export buttons in the grid's pager.<br />
The ASPxGridView is created and bound at runtime in the Page_Init event handler.<br />
The inherited control can also be imported into a separate assembly, as it was firstly kindly presented in the <a href="https://www.devexpress.com/Support/Center/p/S132943">S132943</a> article.</p><p><strong>Note:</strong> The example is not recommended, because according to its design the exporting is performed during callbacks.<br />
It is possible to download a file without callbacks using a solution from the <a href="https://www.devexpress.com/Support/Center/p/E2577">How to load a file on the callback of the ASPxGridView using the ASPxWebControl.RedirectOnCallback method</a> example.</p>

<br/>


