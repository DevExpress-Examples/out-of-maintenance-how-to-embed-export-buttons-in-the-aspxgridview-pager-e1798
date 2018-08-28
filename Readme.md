# [OBSOLETE] How to embed export buttons in the ASPxGridView pager

#### Starting with v17.2, ASPxGridView provides built-in Toolbar with Export buttons: [Exporting to Different Formats Demo](https://demos.devexpress.com/ASPxGridViewDemos/Exporting/Exporting.aspx). Set the [ASPxGridExportSettings.EnableClientSideExportAPI](https://documentation.devexpress.com/AspNet/DevExpress.Web.ASPxGridExportSettings.EnableClientSideExportAPI.property) property to True to use it. 

 The approach in this example is **obsolete**. We recommend using the built-in API in new versions.

<p>This example was imported from the <a href="https://www.devexpress.com/Support/Center/p/S132943">S132943</a> report to provide users with export buttons in the grid's pager.<br />
The ASPxGridView is created and bound at runtime in the Page_Init event handler.<br />
The inherited control can also be imported into a separate assembly, as it was firstly kindly presented in the <a href="https://www.devexpress.com/Support/Center/p/S132943">S132943</a> article.</p><p><strong>Note:</strong> The example is not recommended, because according to its design the exporting is performed during callbacks.<br />
It is possible to download a file without callbacks using a solution from the <a href="https://www.devexpress.com/Support/Center/p/E2577">How to load a file on the callback of the ASPxGridView using the ASPxWebControl.RedirectOnCallback method</a> example.</p>

<br/>


