Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Web
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxGridView.Export
Imports DevExpress.Web.ASPxPager
Imports System.Diagnostics
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.ComponentModel

Namespace Grid
	Public Class ExportGridView
		Inherits DevExpress.Web.ASPxGridView.ASPxGridView

        Protected _ExportTypes As ExportType = ExportType.none

        Public Sub New()
            Me.RegisterEvents()
        End Sub

        Public Property ExportTypes() As ExportType
            Get
                Return _ExportTypes
            End Get
            Set(ByVal value As ExportType)
                _ExportTypes = value
            End Set
        End Property

        Public Overrides Property ID() As String
            Get
                Return MyBase.ID
            End Get
            Set(ByVal value As String)
                MyBase.ID = value
                Me.ClientInstanceName = ID
            End Set
        End Property

        Protected Property SessionExportData() As Byte()
            Get
                If HttpContext.Current.Session(Me.SessionExportDataKey) Is Nothing Then
                    HttpContext.Current.Session(Me.SessionExportDataKey) = New Byte() {}
                End If
                Return CType(HttpContext.Current.Session(Me.SessionExportDataKey), Byte())
            End Get
            Set(ByVal value As Byte())
                HttpContext.Current.Session(Me.SessionExportDataKey) = value
            End Set
        End Property

        Protected ReadOnly Property SessionExportDataKey() As String
            Get
                Return String.Format("{0}_ExportData", Me.ID)
            End Get
        End Property

        Protected Property SessionExportType() As ExportType
            Get
                If HttpContext.Current.Session(Me.SessionExportTypeKey) Is Nothing Then
                    HttpContext.Current.Session(Me.SessionExportTypeKey) = ExportType.none
                End If
                Return CType(HttpContext.Current.Session(Me.SessionExportTypeKey), ExportType)
            End Get
            Set(ByVal value As ExportType)
                HttpContext.Current.Session(Me.SessionExportTypeKey) = value
            End Set
        End Property

        Protected ReadOnly Property SessionExportTypeKey() As String
            Get
                Return String.Format("{0}_ExportType", Me.ID)
            End Get
        End Property

        ' Protected Methods (8) 

        Protected Sub CheckExport()
            Dim type As ExportType = Me.SessionExportType
            If type <> ExportType.none Then
                Me.SessionExportType = ExportType.none
                Dim data() As Byte = Me.SessionExportData
                Me.SessionExportData = Nothing
                If data IsNot Nothing Then
                    ExportHelper.WriteDataToResponse(data, type)
                End If
            End If
        End Sub

        Protected Function GetGridExporter() As ASPxGridViewExporter
            For Each item As Control In Me.Controls
                Dim exp As ASPxGridViewExporter = TryCast(item, ASPxGridViewExporter)
                If exp IsNot Nothing Then
                    Return exp
                End If
            Next item
            Return Nothing
        End Function

        Protected Sub LoadPager()
            Me.SettingsPager.AlwaysShowPager = True
            Dim pager As New ExportGridViewPager()
            pager.ExportTypes = Me.ExportTypes
            Me.Templates.PagerBar = pager
        End Sub

        Protected Overrides Sub OnDataBound(ByVal e As EventArgs)
            MyBase.OnDataBound(e)
            Me.CheckExport()
        End Sub

        Protected Overridable Sub OnEventExportData(ByVal args As ExportType)
            Me.DataBind()
            Me.SessionExportType = args
            Me.SessionExportData = ExportHelper.GetExportData(Me.GetGridExporter(), args)
            ASPxGridView.RedirectOnCallback(Request.Url.AbsolutePath)
        End Sub

        Protected Sub OnGridCustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
            If (Not String.IsNullOrEmpty(e.Parameters)) Then
                Dim values() As String = e.Parameters.Split(New String() {"||||"}, StringSplitOptions.RemoveEmptyEntries)
                Trace.WriteLine(String.Format("OnGridCustomCallBack - {0}", String.Join(", ", values)))
                If values.Length = 3 Then
                    Select Case values(0)
                        Case "EXPORT"
                            Me.OnEventExportData(CType(System.Enum.Parse(GetType(ExportType), values(2)), ExportType))
                    End Select
                End If
            End If
        End Sub

        Protected Overrides Sub OnInit(ByVal e As EventArgs)
            MyBase.OnInit(e)
            Me.LoadPager()
        End Sub

        Protected Sub RegisterEvents()
            AddHandler Me.CustomCallback, AddressOf OnGridCustomCallback
        End Sub

    End Class
End Namespace
