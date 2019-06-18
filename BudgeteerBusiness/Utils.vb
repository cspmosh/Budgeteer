Imports System.Configuration

Public Class Utils

    Public Shared Property ConnectionString() As String
        Get
            Return ConfigurationManager.ConnectionStrings("BudgeteerConn").ToString()
        End Get
        Set(ByVal value As String)
            '
        End Set
    End Property

End Class
