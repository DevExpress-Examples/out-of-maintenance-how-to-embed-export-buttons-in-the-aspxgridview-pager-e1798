Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Web.ASPxGridView.Export
Imports DevExpress.Web.ASPxGridView
Imports System.Web
Imports System.IO
Imports DevExpress.XtraPrinting

Namespace Grid
	Public NotInheritable Class ExportHelper
        ' Public Methods (3) 

        Private Sub New()
        End Sub
        Public Shared Sub ExportData(ByVal exporter As ASPxGridViewExporter, ByVal type As ExportType)
            If exporter IsNot Nothing Then
                exporter.DataBind()
                Select Case type
                    Case ExportType.XLS
                        exporter.WriteXlsToResponse()
                    Case ExportType.PDF
                        exporter.WritePdfToResponse()
                    Case ExportType.CSV
                        exporter.WriteCsvToResponse()
                    Case ExportType.RTF
                        exporter.WriteRtfToResponse()
                    Case Else
                End Select
            End If
        End Sub

        Public Shared Function GetExportData(ByVal exporter As ASPxGridViewExporter, ByVal type As ExportType) As Byte()
            Dim data() As Byte = Nothing
            If exporter IsNot Nothing Then
                exporter.DataBind()
                Using ms As New MemoryStream()
                    Select Case type
                        Case ExportType.XLS
                            exporter.WriteXls(ms)
                        Case ExportType.PDF
                            exporter.WritePdf(ms)
                        Case ExportType.CSV
                            exporter.WriteCsv(ms)
                        Case ExportType.RTF
                            exporter.WriteRtf(ms)
                        Case Else
                    End Select
                    data = ms.GetBuffer()
                End Using
            End If
            Return data
        End Function

        Public Shared Sub WriteDataToResponse(ByVal data() As Byte, ByVal type As ExportType)
            If data IsNot Nothing AndAlso data.Length > 0 AndAlso type <> ExportType.none Then
                Dim fileEnding As String = String.Empty
                Dim fileContent As String = String.Empty
                Select Case type
                    Case ExportType.XLS
                        fileContent = "application/ms-excel"
                        fileEnding = "xls"
                    Case ExportType.PDF
                        fileContent = "application/pdf"
                        fileEnding = "pdf"
                    Case ExportType.CSV
                        fileContent = "text/plain"
                        fileEnding = "csv"
                    Case ExportType.RTF
                        fileContent = "text/enriched"
                        fileEnding = "rtf"
                End Select
                HttpContext.Current.Response.Clear()
                HttpContext.Current.Response.Buffer = False
                HttpContext.Current.Response.ClearHeaders()
                HttpContext.Current.Response.AppendHeader("Content-Type", fileContent)
                HttpContext.Current.Response.AppendHeader("Content-Transfer-Encoding", "binary")
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=Export." & fileEnding)
                HttpContext.Current.Response.BinaryWrite(data)
                HttpContext.Current.Response.End()
            End If
        End Sub

    End Class
End Namespace
