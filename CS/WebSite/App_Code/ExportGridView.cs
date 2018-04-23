using System;
using System.Data;
using System.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridView.Export;
using DevExpress.Web.ASPxPager;
using System.Diagnostics;
using System.Web.UI;
using System.Collections.Generic;
using System.ComponentModel;

namespace Grid
{
    public class ExportGridView : DevExpress.Web.ASPxGridView.ASPxGridView
    {
        #region Fields (1)

        protected ExportType _ExportTypes = ExportType.none;

        #endregion Fields

        #region Constructors (1)

        public ExportGridView()
        {
            this.RegisterEvents();
        }

        #endregion Constructors

        #region Properties (6)

        public ExportType ExportTypes
        {
            get { return _ExportTypes; }
            set { _ExportTypes = value; }
        }

        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
                this.ClientInstanceName = ID;
            }
        }

        protected byte[] SessionExportData
        {
            get
            {
                if (HttpContext.Current.Session[this.SessionExportDataKey] == null)
                    HttpContext.Current.Session[this.SessionExportDataKey] = new byte[] { };
                return (byte[])HttpContext.Current.Session[this.SessionExportDataKey];
            }
            set { HttpContext.Current.Session[this.SessionExportDataKey] = value; }
        }

        protected String SessionExportDataKey { get { return String.Format("{0}_ExportData", this.ID); } }

        protected ExportType SessionExportType
        {
            get
            {
                if (HttpContext.Current.Session[this.SessionExportTypeKey] == null)
                    HttpContext.Current.Session[this.SessionExportTypeKey] = ExportType.none;
                return (ExportType)HttpContext.Current.Session[this.SessionExportTypeKey];
            }
            set { HttpContext.Current.Session[this.SessionExportTypeKey] = value; }
        }

        protected String SessionExportTypeKey { get { return String.Format("{0}_ExportType", this.ID); } }

        #endregion Properties

        #region Methods (8)

        // Protected Methods (8) 

        protected void CheckExport()
        {
            ExportType type = this.SessionExportType;
            if (type != ExportType.none)
            {
                this.SessionExportType = ExportType.none;
                byte[] data = this.SessionExportData;
                this.SessionExportData = null;
                if (data != null)
                {
                    ExportHelper.WriteDataToResponse(data, type);
                }
            }
        }

        protected ASPxGridViewExporter GetGridExporter()
        {
            foreach (Control item in this.Controls)
            {
                ASPxGridViewExporter exp = item as ASPxGridViewExporter;
                if (exp != null)
                {
                    return exp;
                }
            }
            return null;
        }

        protected void LoadPager()
        {
            this.SettingsPager.AlwaysShowPager = true;
            ExportGridViewPager pager = new ExportGridViewPager();
            pager.ExportTypes = this.ExportTypes;
            this.Templates.PagerBar = pager;
        }

        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);
            this.CheckExport();
        }

        protected virtual void OnEventExportData(ExportType args)
        {
            this.DataBind();
            this.SessionExportType = args;
            this.SessionExportData = ExportHelper.GetExportData(this.GetGridExporter(), args);
            ASPxGridView.RedirectOnCallback(Request.Url.AbsolutePath);
        }

        protected void OnGridCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Parameters))
            {
                String[] values = e.Parameters.Split(new String[] { "||||" }, StringSplitOptions.RemoveEmptyEntries);
                Trace.WriteLine(String.Format("OnGridCustomCallBack - {0}", String.Join(", ", values)));
                if (values.Length == 3)
                {
                    switch (values[0])
                    {
                        case "EXPORT":
                            this.OnEventExportData((ExportType)Enum.Parse(typeof(ExportType), values[2]));
                            break;
                    }
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.LoadPager();
        }

        protected void RegisterEvents()
        {
            this.CustomCallback += this.OnGridCustomCallback;
        }

        #endregion Methods
    }
}
