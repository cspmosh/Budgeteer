Imports System.ServiceModel
Imports System.Text.RegularExpressions

Partial Public Class ChangePassword
    Inherits System.Web.UI.Page

    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
    End Sub

    Protected Sub UpdateCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/UserAccount/Index.aspx")
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim result As Integer = Nothing
        Dim oldPassword As String = txtOldPassword.Text
        Dim newPassword As String = txtNewPassword.Text
        Dim confirmPassword As String = txtConfirmPassword.Text

        If String.IsNullOrEmpty(oldPassword) Then
            ShowError("Old Password is required")
            Return
        End If

        If String.IsNullOrEmpty(newPassword) Then
            ShowError("Old Password is required")
            Return
        End If

        If String.IsNullOrEmpty(confirmPassword) Then
            ShowError("Old Password is required")
            Return
        End If

        If String.Compare(newPassword, confirmPassword, False) <> 0 Then
            ShowError("Passwords do not match!")
            Return
        End If

        ' Try to reset password
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    ' Reset password
                    result = wcfProxy.UpdateUserPassword(oldPassword, newPassword)
                    If result >= 0 Then
                        Response.Redirect("~/MemberPages/UserAccount/Index.aspx")
                    End If
                Catch timeoutEx As TimeoutException
                    ShowError(timeoutEx.Message)
                    wcfProxy.Abort()
                    Return
                Catch ex As FaultException
                    ShowError(ex.Message)
                    Return
                Catch commEx As CommunicationException
                    ShowError(commEx.Message)
                    wcfProxy.Abort()
                    Return
                End Try
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If
        Response.Redirect("~/MemberPages/UserAccount/Index.aspx")
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

    Public Function validateStrongPassword(ByVal password As String) As Boolean
        ' Regular expression that requires 7 characters with at least one number or symbol
        Dim passwordRegEx As String = "^.*(?=.{7,})(?=.*[\d\W]).*$"
        Dim match As Match = Regex.Match(password, passwordRegEx)
        If match.Success Then
            Return True
        Else
            Return False
        End If
    End Function

End Class