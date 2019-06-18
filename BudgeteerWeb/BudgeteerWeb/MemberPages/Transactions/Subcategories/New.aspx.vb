Imports System.ServiceModel

Partial Public Class _New5
    Inherits System.Web.UI.Page

    Dim subcategories As List(Of BudgeteerService.Subcategory) = New List(Of BudgeteerService.Subcategory)
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
                    subcategories = wcfProxy.GetSubcategories()
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
            Dim active As CheckBox = SubcategoryFormView.FindControl("cbxActive")
            active.Checked = True
            Dim txtBalance As TextBox = SubcategoryFormView.FindControl("BalanceTextBox")
            If txtBalance IsNot Nothing Then
                txtBalance.Enabled = False
            End If
        End If
    End Sub

    Protected Sub SubcategoryDataSource_ObjectCreating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceEventArgs) Handles SubcategoryDataSource.ObjectCreating
        'Uses the in-session client proxy
        e.ObjectInstance = wcfProxy
    End Sub

    Protected Sub SubcategoryDataSource_ObjectDisposing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs) Handles SubcategoryDataSource.ObjectDisposing
        'Cancels the object from being destroyed after use
        e.Cancel = True
    End Sub

    Protected Sub CategoryDataSource_ObjectCreating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceEventArgs) Handles CategoryDataSource.ObjectCreating
        'Uses the in-session client proxy
        e.ObjectInstance = wcfProxy
    End Sub

    Protected Sub CategoryDataSource_ObjectDisposing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs) Handles CategoryDataSource.ObjectDisposing
        'Cancels the object from being destroyed after use
        e.Cancel = True
    End Sub

    Protected Sub SubcategoryFormView_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles SubcategoryFormView.ItemInserting
        Dim txtBalance As TextBox = SubcategoryFormView.FindControl("BalanceTextBox")
        Dim balance As Decimal = 0.0
        If String.IsNullOrEmpty(Trim(txtBalance.Text)) Then
            balance = 0.0
        Else
            balance = txtBalance.Text
        End If
        e.Values("balance") = balance
        e.Values("categoryID") = CType(CType(sender, FormView).FindControl("ddlCategories"), DropDownList).SelectedValue
        e.Values("type") = CType(CType(sender, FormView).FindControl("ddlType"), DropDownList).SelectedValue
    End Sub

    Protected Sub InsertCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/Transactions/Categories/Index.aspx")
    End Sub

    Protected Sub SubcategoryFormView_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles SubcategoryFormView.ItemInserted
        If e.Exception IsNot Nothing Then
            ShowError(e.Exception.Message)
            e.ExceptionHandled = True
            e.KeepInInsertMode = True
        Else
            Response.Redirect("~/MemberPages/Transactions/Categories/Index.aspx")
        End If
    End Sub

    Protected Sub ValidateSubcategoryDescriptionUniqueness(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        If subcategories IsNot Nothing Then
            For Each subcategory As BudgeteerService.Subcategory In subcategories
                If String.Compare(Trim(subcategory.Description), Trim(e.Value), True) = 0 Then
                    e.IsValid = False
                    Exit Sub
                End If
            Next
        End If
        e.IsValid = True
    End Sub

    Protected Sub chk_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim checkBox As CheckBox = SubcategoryFormView.FindControl("cbxSinkingFund")
        Dim txtBalance As TextBox = SubcategoryFormView.FindControl("BalanceTextBox")
        If (checkBox IsNot Nothing) And (txtBalance IsNot Nothing) Then
            If checkBox.Checked Then
                txtBalance.Enabled = True
            Else
                txtBalance.Enabled = False
            End If
        End If
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class