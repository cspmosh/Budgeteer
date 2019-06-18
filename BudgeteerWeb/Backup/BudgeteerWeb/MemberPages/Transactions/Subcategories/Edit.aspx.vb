Imports System.ServiceModel

Partial Public Class Edit4
    Inherits System.Web.UI.Page

    Dim subcategories As List(Of BudgeteerService.Subcategory) = New List(Of BudgeteerService.Subcategory)
    Dim categories As List(Of BudgeteerService.Category) = New List(Of BudgeteerService.Category)
    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Protected Sub SubcategoryFormView_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles SubcategoryFormView.ItemUpdating
        Dim txtBalance As TextBox = SubcategoryFormView.FindControl("BalanceTextBox")
        If txtBalance.Text = "" Then
            txtBalance.Text = "0"
        End If
        e.NewValues("categoryID") = CType(CType(sender, FormView).FindControl("ddlCategories"), DropDownList).SelectedValue
        e.NewValues("type") = CType(CType(sender, FormView).FindControl("ddlType"), DropDownList).SelectedValue
    End Sub

    Protected Sub UpdateCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/Transactions/Categories/Index.aspx")
    End Sub

    Protected Sub SubcategoryFormView_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles SubcategoryFormView.ItemUpdated
        If e.Exception IsNot Nothing Then
            ShowError(e.Exception.InnerException.Message)
            e.ExceptionHandled = True
            e.KeepInEditMode = True
        Else
            Response.Redirect("~/MemberPages/Transactions/Categories/Index.aspx")
        End If
    End Sub

    Protected Sub ValidateSubcategoryDescriptionUniqueness(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        For Each subcategory As BudgeteerService.Subcategory In subcategories
            If subcategory.SubcategoryID = SubcategoryFormView.DataKey.Values(0) Then
                Continue For
            End If
            If String.Compare(Trim(subcategory.Description), Trim(e.Value), True) = 0 Then
                e.IsValid = False
                Exit Sub
            End If
        Next
        e.IsValid = True
    End Sub

    Protected Sub SubcategoryFormView_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles SubcategoryFormView.DataBound
        Dim typeList As DropDownList = CType(SubcategoryFormView.FindControl("ddlType"), DropDownList)
        Dim subcategory As BudgeteerService.Subcategory = SubcategoryFormView.DataItem
        Dim category As BudgeteerService.Category = New BudgeteerService.Category
        Dim lblError As Label = SubcategoryFormView.FindControl("lblError")
        Dim alphaIndex As Integer = 0

        If subcategory IsNot Nothing And typeList IsNot Nothing Then
            typeList.SelectedValue = subcategory.Type
        End If

        Dim categoryList As DropDownList
        categoryList = CType(SubcategoryFormView.FindControl("ddlCategories"), DropDownList)
        If subcategory IsNot Nothing Then
            Dim foundItem As ListItem = categoryList.Items.FindByValue(subcategory.CategoryID)
            If foundItem IsNot Nothing Then
                ' Category was found in drop down list so select it
                categoryList.SelectedValue = foundItem.Value
            Else
                ' category was not found in drop down list. Assume it's
                ' inactive and add it to the list in alpha order, then select it
                If wcfProxy IsNot Nothing Then
                    If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                        wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                        Try
                            category = wcfProxy.GetCategory(subcategory.CategoryID)
                            ' insert it alphabetically into the drop down list
                            For Each catg As ListItem In categoryList.Items
                                If String.Compare(category.Description, catg.Text, True) < 0 Then
                                    Exit For
                                End If
                                alphaIndex += 1
                            Next
                            categoryList.Items.Insert(alphaIndex, New ListItem(category.Description, category.CategoryID))
                            ' select the inserted list item
                            categoryList.SelectedIndex = alphaIndex
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
            End If
        End If
        ' Enable or disable balance textbox based on sinking fund checkbox
        chk_CheckedChanged()
    End Sub

    Protected Sub chk_CheckedChanged()
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