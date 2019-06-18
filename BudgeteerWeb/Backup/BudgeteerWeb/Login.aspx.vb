Imports System.ServiceModel
Imports System.ServiceModel.Security

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblFailureText.Visible = False
    End Sub

    Protected Sub Login1_Authenticate(ByVal sender As Object, ByVal e As AuthenticateEventArgs) Handles Login1.Authenticate
        Dim budgeteerServiceClient As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient
        Dim authenticate As Integer = 0

        budgeteerServiceClient.ClientCredentials.UserName.UserName = Me.Login1.UserName
        budgeteerServiceClient.ClientCredentials.UserName.Password = Me.Login1.Password
        budgeteerServiceClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = ServiceModel.Security.X509CertificateValidationMode.None

        Try
            budgeteerServiceClient.Open()
            e.Authenticated = True
            'Store the client proxy in session to use throughout application 
            Session("Budgeteer.ClientProxy") = budgeteerServiceClient
        Catch msgSecurityEx As MessageSecurityException
            e.Authenticated = False
            lblFailureText.Text = msgSecurityEx.InnerException.Message
            lblFailureText.Visible = True
            budgeteerServiceClient.Abort()
        Catch ex As Exception
            budgeteerServiceClient.Abort()
            lblFailureText.Text = ex.InnerException.Message()
            lblFailureText.Visible = True
            Throw ex
        End Try

    End Sub

End Class