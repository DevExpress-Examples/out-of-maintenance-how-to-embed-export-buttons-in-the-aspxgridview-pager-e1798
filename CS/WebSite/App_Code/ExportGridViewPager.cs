using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallback;
using System.Web;
using DevExpress.Web.ASPxGridView.Export;

namespace Grid
{
    public class ExportGridViewPager : ITemplate
    {
		#region?Fields?(2)?

        private ExportType _ExportTypes = ExportType.none;
        private ExportGridView grid;

		#endregion?Fields?

		#region?Properties?(1)?

        public ExportType ExportTypes { get { return this._ExportTypes; } set { this._ExportTypes = value; } }

		#endregion?Properties?



        #region ITemplate Member

        public void InstantiateIn(Control container)
        {
            this.grid = (ExportGridView)((GridViewPagerBarTemplateContainer)container).Grid;

            Table tblContent = new Table();
            tblContent.Width = Unit.Percentage(100);
            TableRow rwContent = new TableRow();
            TableCell cellPager = new TableCell();
            ASPxGridViewTemplateReplacement replacement = new ASPxGridViewTemplateReplacement();
            replacement.ID = "Pager";
            replacement.ReplacementType = GridViewTemplateReplacementType.Pager;
            cellPager.Controls.Add(replacement);
            rwContent.Cells.Add(cellPager);
            if (this.ExportTypes != ExportType.none)
            {
                String callBackID = String.IsNullOrEmpty(this.grid.ClientInstanceName) ? this.grid.UniqueID.Remove(0, this.grid.UniqueID.LastIndexOf("$") + 1) : this.grid.ClientInstanceName;

                ASPxGridViewExporter exporter = new ASPxGridViewExporter();
                exporter.ID = callBackID + "_Exporter";
                exporter.GridViewID = this.grid.ID;
                this.grid.Controls.Add(exporter);

                TableCell cellExportTable = new TableCell();
                cellExportTable.Width = Unit.Percentage(100);
                Table tblExport = new Table();
                tblExport.HorizontalAlign = HorizontalAlign.Right;
                TableRow rwExport = new TableRow();
                TableCell exportCell = new TableCell();
                exportCell.Text = "Export";
                rwExport.Cells.Add(exportCell);
                rwExport.Cells.AddRange(this.GetExportCells(callBackID));

                cellExportTable.Controls.Add(tblExport);
                tblExport.Rows.Add(rwExport);
                rwContent.Cells.Add(cellExportTable);
            }
            tblContent.Rows.Add(rwContent);
            container.Controls.Add(tblContent);
        }

        protected TableCell[] GetExportCells(String callBackID)
        {
            List<TableCell> cells = new List<TableCell>();
            foreach (ExportType item in Enum.GetValues(typeof(ExportType)))
            {
                ExportType type = (ExportType)item;
                if (type != ExportType.none &&
                    ((this.ExportTypes & type) == type))
                {
                    cells.Add(this.GetExportCell(type, callBackID));
                }
            }
            return cells.ToArray();
        }

        protected TableCell GetExportCell(ExportType type, String callBackID)
        {
            ASPxImage btn = new ASPxImage();
            //btn.Text = type.ToString();
            btn.ImageAlign = ImageAlign.Middle;
            btn.ImageUrl = String.Format("~/img/filetypes/{0}.png", type.ToString());
            btn.Height = Unit.Pixel(16);
            btn.Width = Unit.Pixel(16);
            btn.ID = String.Format("{0}_btnExport{1}", callBackID, type.ToString());
            btn.ClientInstanceName = btn.ID;
            btn.SetClientSideEventHandler("Click", "function(s,e){" + callBackID + ".PerformCallback('EXPORT||||" + this.grid.UniqueID + "||||" + type.ToString() + "');}");
            TableCell cell = new TableCell();
            cell.Controls.Add(btn);
            return cell;
        }

        #endregion
    }
}
