Imports System.ServiceModel

Partial Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request.IsAuthenticated) Then
            Dim user As BudgeteerService.User = New BudgeteerService.User
            If wcfProxy.State = ServiceModel.CommunicationState.Opened Then
                Try
                    user = wcfProxy.GetUser()
                    If user IsNot Nothing Then
                        lblWelcome.Text = "Welcome, " & user.FirstName & "!"
                    End If
                Catch timeoutEx As TimeoutException
                    lblWelcome.Text = timeoutEx.InnerException.Message
                Catch faultEx As FaultException
                    lblWelcome.Text = faultEx.InnerException.Message
                Catch commEx As CommunicationException
                    lblWelcome.Text = commEx.InnerException.Message
                End Try
            Else
                lblWelcome.Text = "Web services are down. Tough luck, bud."
            End If
        End If
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

End Class