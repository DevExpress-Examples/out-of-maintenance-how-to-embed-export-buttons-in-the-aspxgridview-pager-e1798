Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace Grid
	<Flags> _
	Public Enum ExportType
		none = 0
		XLS = 1
		PDF = 2
		CSV = 4
		RTF = 8
	End Enum
End Namespace
