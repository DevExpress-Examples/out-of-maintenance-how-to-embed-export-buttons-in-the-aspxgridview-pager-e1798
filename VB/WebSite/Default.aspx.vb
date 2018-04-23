Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports Grid
Imports System.Collections.Generic

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim grid As New ExportGridView()
		grid.ID = "grid"
		grid.ExportTypes = ExportType.CSV Or ExportType.PDF Or ExportType.RTF Or ExportType.XLS

		form1.Controls.Add(grid)

		grid.DataSource = Me.GetValues()
		grid.DataBind()
	End Sub

	Protected Function GetValues() As String()
		Dim items As List(Of String) = New List(Of String)()
		For i As Integer = 0 To 99
			items.Add(String.Format("Item ({0})", i))
		Next i
		Return items.ToArray()
	End Function
End Class
