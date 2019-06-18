Imports BudgeteerDAL

Public Class ApplicationSetting

    Public Function getSettings(ByVal userID As String) As List(Of BudgeteerObjects.ApplicationSetting)

        Dim data As BudgeteerDAL.ApplicationSettings = New BudgeteerDAL.ApplicationSettings
        Dim settings As List(Of BudgeteerObjects.ApplicationSetting) = New List(Of BudgeteerObjects.ApplicationSetting)

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be blank")
        End If

        Using dTran As distributedTransaction = New distributedtransaction(Utils.ConnectionString)
            Try
                settings = data.getApplicationSettings(dTran, userID)
                dTran.Commit()
                Return settings
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function getSetting(ByVal userID As String, ByVal setting As String, ByVal defaultVal As String) As String

        Dim data As BudgeteerDAL.ApplicationSettings = New BudgeteerDAL.ApplicationSettings
        Dim value As String = ""

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be blank")
        End If

        If String.IsNullOrEmpty(Trim(setting)) Then
            Throw New ArgumentException("Setting cannot be blank")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                value = data.getApplicationSetting(dTran, userID, setting, defaultVal)
                dTran.Commit()
                Return value
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function setSetting(ByVal userID As String, ByVal setting As String, ByVal value As String) As Integer

        Dim data As BudgeteerDAL.ApplicationSettings = New BudgeteerDAL.ApplicationSettings
        Dim result As Integer = Nothing

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be blank")
        End If

        If String.IsNullOrEmpty(Trim(setting)) Then
            Throw New ArgumentException("Setting cannot be blank")
        End If

        If setting.Length() > BudgeteerObjects.ApplicationSetting.MaxFieldLength.Setting Then
            Throw New ArgumentException("Application Setting length cannot exceed " + BudgeteerObjects.ApplicationSetting.MaxFieldLength.Setting.ToString() + " characters")
        End If

        If value.Length() > BudgeteerObjects.ApplicationSetting.MaxFieldLength.Value Then
            Throw New ArgumentException("Application Setting Value length cannot exceed " + BudgeteerObjects.ApplicationSetting.MaxFieldLength.Value.ToString() + " characters")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                result = data.setApplicationSetting(dTran, userID, setting, value.ToUpper)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

End Class
