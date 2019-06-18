Imports System.ServiceModel

Partial Public Class Edit
    Inherits System.Web.UI.Page

    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If
    End Sub

    Protected Sub ddlCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        SubcategoryDataSource.SelectParameters(0).DefaultValue = CType(sender, DropDownList).SelectedValue
        SubcategoryDataSource.DataBind()
    End Sub

    Protected Sub SubcategoryDataSource_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles SubcategoryDataSource.Selecting

        Dim categoryID As Int64 = 0
        Dim subcategoryID As Int64 = 0
        Dim subcategory As BudgeteerService.Subcategory = New BudgeteerService.Subcategory
        Dim ddlCategories As DropDownList = FormView1.FindControl("ddlCategories")
        Dim ddlSubcategories As DropDownList = FormView1.FindControl("ddlSubcategories")
        Dim foundItem As ListItem
        Dim category As BudgeteerService.Category = New BudgeteerService.Category
        Dim alphaIndex As Integer = 0

        If Not Page.IsPostBack Then
            Dim budget As BudgeteerService.Budget = FormView1.DataItem
            subcategoryID = budget.SubcategoryID
            If wcfProxy IsNot Nothing Then
                If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                    Try
                        subcategory = wcfProxy.GetSubcategory(subcategoryID)
                        categoryID = subcategory.CategoryID
                        foundItem = ddlCategories.Items.FindByValue(categoryID)
                        If foundItem IsNot Nothing Then
                            ddlCategories.SelectedValue = foundItem.Value
                        Else
                            ' Retrieve inactive category
                            category = wcfProxy.GetCategory(categoryID)
                            ' insert it alphabetically into the drop down list
                            For Each catg As ListItem In ddlCategories.Items
                                If String.Compare(category.Description, catg.Text, True) < 0 Then
                                    Exit For
                                End If
                                alphaIndex += 1
                            Next
                            ddlCategories.Items.Insert(alphaIndex, New ListItem(category.Description, category.CategoryID))
                            ' select the inserted list item
                            ddlCategories.SelectedIndex = alphaIndex
                            ddlSubcategories.Enabled = True
                        End If
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
        Else
            categoryID = ddlCategories.SelectedValue
        End If
        Try
            ddlSubcategories.Items.Clear()
            e.InputParameters("CategoryID") = categoryID
        Catch ex As Exception
            ShowError(ex.Message)
        End Try
    End Sub

    Protected Sub subcategoryListDataBound()
        Dim ddlSubcategories As DropDownList = FormView1.FindControl("ddlSubcategories")
        Dim budget As BudgeteerService.Budget = FormView1.DataItem
        Dim foundItem As ListItem
        Dim subcategory As BudgeteerService.Subcategory = New BudgeteerService.Subcategory
        Dim alphaIndex As Integer = 0
        Try
            If budget IsNot Nothing Then
                foundItem = ddlSubcategories.Items.FindByValue(budget.SubcategoryID)
                If foundItem IsNot Nothing Then
                    ddlSubcategories.SelectedValue = foundItem.Value
                Else
                    If wcfProxy IsNot Nothing Then
                        If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                        wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                            Try
                                ' Retrieve inactive subcategory
                                subcategory = wcfProxy.GetSubcategory(budget.SubcategoryID)
                                ' insert it alphabetically into the drop down list
                                For Each subcatg As ListItem In ddlSubcategories.Items
                                    If String.Compare(subcategory.Description, subcatg.Text, True) < 0 Then
                                        Exit For
                                    End If
                                    alphaIndex += 1
                                Next
                                ddlSubcategories.Items.Insert(alphaIndex, New ListItem(subcategory.Description, subcategory.CategoryID))
                                ' select the inserted list item
                                ddlSubcategories.SelectedIndex = alphaIndex
                            Catch timeoutEx As TimeoutException
                                ShowError(timeoutEx.Message)
                                wcfProxy.Abort()
                            Catch ex As FaultException
                                ShowError(ex.Message)
                            Catch commEx As CommunicationException
                                ShowError(commEx.Message)
                                wcfProxy.Abort()
                            End Try
                        End If
                    End If
                End If
            End If
            ddlSubcategories.Items.Insert(0, New ListItem(" -- Choose -- ", ""))
        Catch ex As Exception
            lblError.Text = ex.Message()
        End Try
    End Sub

    Protected Sub BudgetDataSource_ObjectCreating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceEventArgs) Handles BudgetDataSource.ObjectCreating
        'Uses the in-session client proxy
        e.ObjectInstance = wcfProxy
    End Sub

    Protected Sub BudgetDataSource_ObjectDisposing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs) Handles BudgetDataSource.ObjectDisposing
        'Cancels the object from being destroyed after use
        e.Cancel = True
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

    Protected Sub FormView1_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles FormView1.ItemUpdating
        Dim txtAmount As TextBox = FormView1.FindControl("AmountTextBox")
        If txtAmount.Text = "" Then
            txtAmount.Text = "0"
        End If
        e.NewValues("subcategoryID") = CType(CType(sender, FormView).FindControl("ddlSubcategories"), DropDownList).SelectedValue
    End Sub

    Protected Sub FormView1_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles FormView1.ItemUpdated
        If e.Exception IsNot Nothing Then
            ShowError(e.Exception.Message)
            e.ExceptionHandled = True
            e.KeepInEditMode = True
        Else
            Response.Redirect("~/MemberPages/Budgets/Index.aspx")
        End If
    End Sub

    Protected Sub UpdateCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/Budgets/Index.aspx")
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class