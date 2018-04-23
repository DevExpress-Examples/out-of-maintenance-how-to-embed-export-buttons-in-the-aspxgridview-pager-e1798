Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.UI
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxCallback
Imports System.Web
Imports DevExpress.Web.ASPxGridView.Export

Namespace Grid
	Public Class ExportGridViewPager
		Implements ITemplate

        Private _ExportTypes As ExportType = ExportType.none
        Private grid As ExportGridView

        Public Property ExportTypes() As ExportType
            Get
                Return Me._ExportTypes
            End Get
            Set(ByVal value As ExportType)
                Me._ExportTypes = value
            End Set
        End Property


#Region "ITemplate Member"

        Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
            Me.grid = CType((CType(container, GridViewPagerBarTemplateContainer)).Grid, ExportGridView)

            Dim tblContent As New Table()
            tblContent.Width = Unit.Percentage(100)
            Dim rwContent As New TableRow()
            Dim cellPager As New TableCell()
            Dim replacement As New ASPxGridViewTemplateReplacement()
            replacement.ID = "Pager"
            replacement.ReplacementType = GridViewTemplateReplacementType.Pager
            cellPager.Controls.Add(replacement)
            rwContent.Cells.Add(cellPager)
            If Me.ExportTypes <> ExportType.none Then
                Dim callBackID As String
                If (String.IsNullOrEmpty(Me.grid.ClientInstanceName)) Then
                    callBackID = Me.grid.UniqueID.Remove(0, Me.grid.UniqueID.LastIndexOf("$") + 1)
                Else
                    callBackID = Me.grid.ClientInstanceName
                End If

                Dim exporter As New ASPxGridViewExporter()
                exporter.ID = callBackID & "_Exporter"
                exporter.GridViewID = Me.grid.ID
                Me.grid.Controls.Add(exporter)

                Dim cellExportTable As New TableCell()
                cellExportTable.Width = Unit.Percentage(100)
                Dim tblExport As New Table()
                tblExport.HorizontalAlign = HorizontalAlign.Right
                Dim rwExport As New TableRow()
                Dim exportCell As New TableCell()
                exportCell.Text = "Export"
                rwExport.Cells.Add(exportCell)
                rwExport.Cells.AddRange(Me.GetExportCells(callBackID))

                cellExportTable.Controls.Add(tblExport)
                tblExport.Rows.Add(rwExport)
                rwContent.Cells.Add(cellExportTable)
            End If
            tblContent.Rows.Add(rwContent)
            container.Controls.Add(tblContent)
        End Sub

        Protected Function GetExportCells(ByVal callBackID As String) As TableCell()
            Dim cells As List(Of TableCell) = New List(Of TableCell)()
            For Each item As ExportType In System.Enum.GetValues(GetType(ExportType))
                Dim type As ExportType = CType(item, ExportType)
                If type <> ExportType.none AndAlso ((Me.ExportTypes And type) = type) Then
                    cells.Add(Me.GetExportCell(type, callBackID))
                End If
            Next item
            Return cells.ToArray()
        End Function

        Protected Function GetExportCell(ByVal type As ExportType, ByVal callBackID As String) As TableCell
            Dim btn As New ASPxImage()
            'btn.Text = type.ToString();
            btn.ImageAlign = ImageAlign.Middle
            btn.ImageUrl = String.Format("~/img/filetypes/{0}.png", type.ToString())
            btn.Height = Unit.Pixel(16)
            btn.Width = Unit.Pixel(16)
            btn.ID = String.Format("{0}_btnExport{1}", callBackID, type.ToString())
            btn.ClientInstanceName = btn.ID
            btn.SetClientSideEventHandler("Click", "function(s,e){" & callBackID & ".PerformCallback('EXPORT||||" & Me.grid.UniqueID & "||||" & type.ToString() & "');}")
            Dim cell As New TableCell()
            cell.Controls.Add(btn)
            Return cell
        End Function

#End Region
    End Class
End Namespace
