Imports System.ServiceModel

Partial Public Class _New3
    Inherits System.Web.UI.Page

    Dim categories As List(Of BudgeteerService.Category) = New List(Of BudgeteerService.Category)
    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Private Sub page_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    categories = wcfProxy.GetCategories()
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
        If Not Page.IsPostBack Then
            ' Default active to true
            Dim active As CheckBox = CategoryFormView.FindControl("cbxActive")
            active.Checked = True
        End If
    End Sub

    Protected Sub CategoryDataSource_ObjectCreating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceEventArgs) Handles CategoryDataSource.ObjectCreating
        'Uses the in-session client proxy
        e.ObjectInstance = wcfProxy
    End Sub

    Protected Sub CategoryDataSource_ObjectDisposing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs) Handles CategoryDataSource.ObjectDisposing
        'Cancels the object from being destroyed after use
        e.Cancel = True
    End Sub

    Protected Sub InsertCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/Transactions/Categories/Index.aspx")
    End Sub

    Protected Sub CategoryFormView_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles CategoryFormView.ItemInserted
        If e.Exception IsNot Nothing Then
            ShowError(e.Exception.Message)
            e.ExceptionHandled = True
            e.KeepInInsertMode = True
        Else
            Response.Redirect("~/MemberPages/Transactions/Categories/Index.aspx")
        End If
    End Sub

    Protected Sub ValidateCategoryDescriptionUniqueness(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        If categories IsNot Nothing Then
            For Each category As BudgeteerService.Category In categories
                If String.Compare(Trim(category.Description), Trim(e.Value), True) = 0 Then
                    e.IsValid = False
                    Exit Sub
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