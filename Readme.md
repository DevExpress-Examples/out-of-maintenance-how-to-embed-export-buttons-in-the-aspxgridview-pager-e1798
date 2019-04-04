<!-- default file list -->
*Files to look at*:

* [ExportGridView.cs](./CS/WebSite/App_Code/ExportGridView.cs) (VB: [ExportGridView.vb](./VB/WebSite/App_Code/ExportGridView.vb))
* [ExportGridViewPager.cs](./CS/WebSite/App_Code/ExportGridViewPager.cs) (VB: [ExportGridViewPager.vb](./VB/WebSite/App_Code/ExportGridViewPager.vb))
* [ExportHelper.cs](./CS/WebSite/App_Code/ExportHelper.cs) (VB: [ExportHelper.vb](./VB/WebSite/App_Code/ExportHelper.vb))
* [ExportType.cs](./CS/WebSite/App_Code/ExportType.cs) (VB: [ExportType.vb](./VB/WebSite/App_Code/ExportType.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# [OBSOLETE] How to embed export buttons in the ASPxGridView pager

#### Starting with v17.2, ASPxGridView provides built-in Toolbar with Export buttons: [Exporting to Different Formats Demo](https://demos.devexpress.com/ASPxGridViewDemos/Exporting/Exporting.aspx). Set the [ASPxGridExportSettings.EnableClientSideExportAPI](https://documentation.devexpress.com/AspNet/DevExpress.Web.ASPxGridExportSettings.EnableClientSideExportAPI.property) property to True to use it. 

 The approach in this example is **obsolete**. We recommend using the built-in API in new versions.

<p>This example was imported from the <a href="https://www.devexpress.com/Support/Center/p/S132943">S132943</a> report to provide users with export buttons in the grid's pager.<br />
The ASPxGridView is created and bound at runtime in the Page_Init event handler.<br />
The inherited control can also be imported into a separate assembly, as it was firstly kindly presented in the <a href="https://www.devexpress.com/Support/Center/p/S132943">S132943</a> article.</p><p><strong>Note:</strong> The example is not recommended, because according to its design the exporting is performed during callbacks.<br />
It is possible to download a file without callbacks using a solution from the <a href="https://www.devexpress.com/Support/Center/p/E2577">How to load a file on the callback of the ASPxGridView using the ASPxWebControl.RedirectOnCallback method</a> example.</p>

<br/>


