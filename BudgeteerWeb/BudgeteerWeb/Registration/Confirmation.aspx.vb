Public Partial Class Confirmation
    Inherits System.Web.UI.Page

    Dim wcfProxy As UnauthenticatedService.BudgeteerUnauthenticatedServicesClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim querystring As NameValueCollection = Request.QueryString
        Dim UserID As String
        Dim EmailAddress As String
        Dim RegistrationKey As String

        lblMessage.Text = ""

        Try
            If querystring.HasKeys Then
                UserID = querystring.Get("UserName")
                EmailAddress = querystring.Get("Email")
                RegistrationKey = querystring.Get("Confirmation")

                If String.IsNullOrEmpty(UserID) Then
                    Throw New ApplicationException("")
                End If

                If String.IsNullOrEmpty(EmailAddress) Then
                    Throw New ApplicationException("")
                End If

                If String.IsNullOrEmpty(RegistrationKey) Then
                    Throw New ApplicationException("")
                End If

                wcfProxy = New UnauthenticatedService.BudgeteerUnauthenticatedServicesClient
                If wcfProxy.ActivateUser(UserID, EmailAddress, RegistrationKey) = 1 Then
                    lblMessage.Text = "Congratulations on completion of your new budgeteer account! To get started, navigate to the Log In page. Your user name is: " & UserID
                End If

            End If

        Catch ex As Exception
            lblMessage.Text = ex.Message()
        Finally
            wcfProxy.Close()
        End Try

    End Sub

End Class