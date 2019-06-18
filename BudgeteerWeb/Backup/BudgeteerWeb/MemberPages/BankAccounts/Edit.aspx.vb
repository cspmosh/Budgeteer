Imports System.ServiceModel

Partial Public Class Edit1
    Inherits System.Web.UI.Page

    Dim accounts As List(Of BudgeteerService.BankAccount) = New List(Of BudgeteerService.BankAccount)
    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    accounts = wcfProxy.GetBankAccounts()
                Catch timeoutEx As TimeoutException
                    ShowError(timeoutEx.Message)
                    wcfProxy.Abort()
                Catch ex As FaultException
                    ShowError(ex.Message)
                Catch commEx As CommunicationException
                    ShowError(commEx.Message)
                    wcfProxy.Abort()
                End Try
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If
    End Sub

    Protected Sub BankAccountDataSource_ObjectCreating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceEventArgs) Handles BankAccountDataSource.ObjectCreating
        'Uses the in-session client proxy
        e.ObjectInstance = wcfProxy
    End Sub

    Protected Sub BankAccountDataSource_ObjectDisposing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs) Handles BankAccountDataSource.ObjectDisposing
        'Cancels the object from being destroyed after use
        e.Cancel = True
    End Sub

    Protected Sub UpdateCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/BankAccounts/Index.aspx")
    End Sub

    Protected Sub BankAccountFormView_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles BankAccountFormView.ItemUpdated
        If e.Exception IsNot Nothing Then
            ShowError(e.Exception.Message)
            e.ExceptionHandled = True
            e.KeepInEditMode = True
        Else
            Response.Redirect("~/MemberPages/BankAccounts/Index.aspx")
        End If
    End Sub

    Protected Sub SubcategoryFormView_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles BankAccountFormView.ItemUpdating

        Dim txtBalance As TextBox = BankAccountFormView.FindControl("txtBalance")

        If txtBalance.Text = "" Then
            txtBalance.Text = "0"
        End If

        e.NewValues("balance") = txtBalance.Text
    End Sub

    Protected Sub ValidateBankAccountUniqueness(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        Dim accountNumber As TextBox = BankAccountFormView.FindControl("txtNumber")
        If accountNumber IsNot Nothing Then
            For Each account As BudgeteerService.BankAccount In accounts
                If account.AccountID = BankAccountFormView.DataKey.Values(0) Then
                    Continue For
                End If
                If String.Compare(Trim(account.Name), Trim(e.Value), True) = 0 Then
                    If String.Compare(Trim(account.Number), Trim(accountNumber.Text), True) = 0 Then
                        e.IsValid = False
                        Exit Sub
                    End If
                End If
            Next
        End If
        e.IsValid = True
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class