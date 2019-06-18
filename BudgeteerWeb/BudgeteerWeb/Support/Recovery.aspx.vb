Public Partial Class Index4
    Inherits System.Web.UI.Page

    Dim wcfProxy As UnauthenticatedService.BudgeteerUnauthenticatedServicesClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Visible = False
    End Sub

    Protected Sub getUserID()
        Dim email As String = txtEmailAddress.Text
        Dim userID As String = Nothing
        Try
            If String.IsNullOrEmpty(Trim(email)) Then
                lblError.Visible = True
                lblError.Text = "Please fill in your email address in order to retrieve your user name"
            Else
                ' Call the web service to get the user name 
                wcfProxy = New UnauthenticatedService.BudgeteerUnauthenticatedServicesClient
                userID = wcfProxy.GetUserName(email)
                wcfProxy.Close()
            End If
        Catch ex As Exception
            lblError.Visible = True
            lblError.Text = ex.Message()
        End Try
        If userID Is Nothing Then
            lblError.Visible = True
            lblError.Text = "This email address does not appear to be registered in our system."
        Else
            ' Store the userName in a session and redirect to email page
            Session("Budgeteer.Recovery.UserName") = userID
            Dim redirectURL As String = "UserNameRecovery.aspx?email=" & email
            Response.Redirect(redirectURL)
        End If
    End Sub

    Protected Sub redirectToVerify()
        Response.Redirect("PasswordRecovery.aspx")
    End Sub

End Class