using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Grid;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ExportGridView grid = new ExportGridView();
        grid.ID = "grid";
        grid.ExportTypes = ExportType.CSV | ExportType.PDF | ExportType.RTF | ExportType.XLS;

        form1.Controls.Add(grid);

        grid.DataSource = this.GetValues();
        grid.DataBind();
    }

    protected String[] GetValues()
    {
        List<String> items = new List<String>();
        for (int i = 0; i < 100; i++)
        {
            items.Add(String.Format("Item ({0})", i));
        }
        return items.ToArray();
    }
}
