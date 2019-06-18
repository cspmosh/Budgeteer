Public Partial Class Logout
    Inherits System.Web.UI.Page

    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State = System.ServiceModel.CommunicationState.Opened Then
                wcfProxy.Close()
                wcfProxy = Nothing
            End If
        End If
        Session.Abandon()
        FormsAuthentication.SignOut()
        Response.Redirect("login.aspx")
    End Sub

End Class