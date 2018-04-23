using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Web.ASPxGridView.Export;
using DevExpress.Web.ASPxGridView;
using System.Web;
using System.IO;
using DevExpress.XtraPrinting;

namespace Grid
{
    public static class ExportHelper
    {
		#region Methods (3) 

		// Public Methods (3) 

         public static void ExportData(ASPxGridViewExporter exporter, ExportType type)
        {
            if (exporter != null)
            {
                exporter.DataBind();
                switch (type)
                {
                    case ExportType.XLS:
                        exporter.WriteXlsToResponse();
                        break;
                    case ExportType.PDF:
                        exporter.WritePdfToResponse();
                        break;
                    case ExportType.CSV:
                        exporter.WriteCsvToResponse();
                        break;
                    case ExportType.RTF:
                        exporter.WriteRtfToResponse();
                        break;
                    default:
                        break;
                }
            }
        }

        public static byte[] GetExportData(ASPxGridViewExporter exporter, ExportType type)
        {
            byte[] data = null;
            if (exporter != null)
            {
                exporter.DataBind();
                using (MemoryStream ms = new MemoryStream())
                {
                    switch (type)
                    {
                        case ExportType.XLS:
                            exporter.WriteXls(ms);
                            break;
                        case ExportType.PDF:
                            exporter.WritePdf(ms);
                            break;
                        case ExportType.CSV:
                            exporter.WriteCsv(ms);
                            break;
                        case ExportType.RTF:
                            exporter.WriteRtf(ms);
                            break;
                        default:
                            break;
                    }
                    data = ms.GetBuffer();
                }
            }
            return data;
        }

        public static void WriteDataToResponse(byte[] data, ExportType type)
        {
            if (data != null && data.Length > 0 && type != ExportType.none)
            {
                String fileEnding = String.Empty;
                String fileContent = String.Empty;
                switch (type)
                {
                    case ExportType.XLS:
                        fileContent = "application/ms-excel";
                        fileEnding = "xls";
                        break;
                    case ExportType.PDF:
                        fileContent = "application/pdf";
                        fileEnding = "pdf";
                        break;
                    case ExportType.CSV:
                        fileContent = "text/plain";
                        fileEnding = "csv";
                        break;
                    case ExportType.RTF:
                        fileContent = "text/enriched";
                        fileEnding = "rtf";
                        break;
                }
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.AppendHeader("Content-Type", fileContent);
                HttpContext.Current.Response.AppendHeader("Content-Transfer-Encoding", "binary");
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=Export." + fileEnding);
                HttpContext.Current.Response.BinaryWrite(data);
                HttpContext.Current.Response.End();
            }
        }

		#endregion Methods 
    }
}
