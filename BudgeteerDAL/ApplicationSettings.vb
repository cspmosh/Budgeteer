Imports BudgeteerObjects
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class ApplicationSettings

    Public Function getApplicationSettings(ByRef dTran As DistributedTransaction, ByVal userID As String) As List(Of BudgeteerObjects.ApplicationSetting)

        Try

            Dim applicationSettings As List(Of BudgeteerObjects.ApplicationSetting) = New List(Of BudgeteerObjects.ApplicationSetting)

            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_application_settings(?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim setting As BudgeteerObjects.ApplicationSetting = New BudgeteerObjects.ApplicationSetting
                setting.Setting = dr("setting")
                setting.Value = dr("value")
                setting.UserID = dr("user_id")
                applicationSettings.Add(setting)
            Next

            If applicationSettings.Count > 0 Then
                Return applicationSettings
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getApplicationSetting(ByRef dTran As DistributedTransaction, ByVal userID As String, ByVal setting As String, ByVal defaultValue As String) As String

        Try

            Dim params(1) As DbParameter
            params(0) = DbHelper.CreateParameter("@setting", DbType.String, setting)
            params(1) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim value As String = Nothing
            value = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_get_application_setting(?,?)", params, dTran)

            If value IsNot Nothing Then
                Return value
            Else
                Return defaultValue
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function setApplicationSetting(ByRef dTran As DistributedTransaction, ByVal userID As String, ByVal setting As String, ByVal value As String) As Integer

        Try

            Dim params(2) As DbParameter
            params(0) = DbHelper.CreateParameter("@setting", DbType.String, IIf(String.IsNullOrEmpty(Trim(setting)), DBNull.Value, Trim(setting)))
            params(1) = DbHelper.CreateParameter("@value", DbType.String, value.ToUpper)
            params(2) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim rows_effected As Integer = 0
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_set_application_setting(?,?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
