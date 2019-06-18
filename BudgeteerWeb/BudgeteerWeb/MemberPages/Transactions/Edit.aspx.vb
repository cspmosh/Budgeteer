Imports System.ServiceModel

Partial Public Class EditTransaction
    Inherits System.Web.UI.Page

    Dim categories As List(Of BudgeteerService.Category) = New List(Of BudgeteerService.Category)
    Dim subcategories As List(Of BudgeteerService.Subcategory) = New List(Of BudgeteerService.Subcategory)
    Dim transactionID As Int64
    Dim accounts As List(Of BudgeteerService.BankAccount) = New List(Of BudgeteerService.BankAccount)
    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If
    End Sub

    Protected Sub ddlCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddlCategories As DropDownList = CType(sender, DropDownList)
        Dim choice As Integer = ddlCategories.SelectedValue
        Dim ddlSubcategories As DropDownList = FormView1.FindControl("ddlSubcategories")
        If choice = 0 Then
            ddlSubcategories.Items.Insert(0, New ListItem("(None)", 0))
            ddlSubcategories.SelectedValue = 0
            ddlSubcategories.Enabled = False
        Else
            SubcategoryDataSource.SelectParameters(0).DefaultValue = choice
            SubcategoryDataSource.DataBind()
        End If
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
            Dim transaction As BudgeteerService.Transaction = FormView1.DataItem
            subcategoryID = transaction.SubcategoryID
            Try
                If subcategoryID = Nothing Then
                    ddlSubcategories.Items.Insert(0, New ListItem("(None)", 0))
                    ddlSubcategories.SelectedValue = 0
                    ddlSubcategories.Enabled = False
                    Return
                Else
                    ddlSubcategories.Enabled = True
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
                End If
            Catch ex As Exception
                ShowError(ex.Message)
            End Try
        Else
            categoryID = ddlCategories.SelectedValue
        End If
        Try
            ddlSubcategories.Items.Clear()
            ddlSubcategories.Enabled = True
            e.InputParameters("CategoryID") = categoryID
        Catch ex As Exception
            ShowError(ex.Message)
        End Try
    End Sub

    Protected Sub subcategoryListDataBound()
        Dim ddlSubcategories As DropDownList = FormView1.FindControl("ddlSubcategories")
        Dim transaction As BudgeteerService.Transaction = FormView1.DataItem
        Dim foundItem As ListItem
        Dim subcategory As BudgeteerService.Subcategory = New BudgeteerService.Subcategory
        Dim alphaIndex As Integer = 0
        Try
            If transaction IsNot Nothing Then
                foundItem = ddlSubcategories.Items.FindByValue(transaction.SubcategoryID)
                If foundItem IsNot Nothing Then
                    ddlSubcategories.SelectedValue = foundItem.Value
                Else
                    If wcfProxy IsNot Nothing Then
                        If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                            wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                            Try
                                ' Retrieve inactive subcategory
                                subcategory = wcfProxy.GetSubcategory(transaction.SubcategoryID)
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
                        Else
                            Response.Redirect("~/login.aspx")
                        End If
                    End If
                End If
            End If
            ddlSubcategories.Items.Insert(0, New ListItem(" -- Choose -- ", ""))
        Catch ex As Exception
            ShowError(ex.Message)
        End Try
    End Sub

    Protected Sub TransactionDataSource_ObjectCreating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceEventArgs) Handles TransactionDataSource.ObjectCreating
        'Uses the in-session client proxy
        e.ObjectInstance = wcfProxy
    End Sub

    Protected Sub TransactionDataSource_ObjectDisposing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs) Handles TransactionDataSource.ObjectDisposing
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

    Protected Sub AccountDataSource_ObjectCreating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceEventArgs) Handles AccountDataSource.ObjectCreating
        'Uses the in-session client proxy
        e.ObjectInstance = wcfProxy
    End Sub

    Protected Sub AccountDataSource_ObjectDisposing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs) Handles AccountDataSource.ObjectDisposing
        'Cancels the object from being destroyed after use
        e.Cancel = True
    End Sub

    Protected Sub AccountDataSource_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles AccountDataSource.Selected
        Dim accountList As DropDownList = CType(FormView1.FindControl("ddlBankAccounts"), DropDownList)
        Dim account As BudgeteerService.BankAccount = New BudgeteerService.BankAccount()
        Dim transaction As BudgeteerService.Transaction = FormView1.DataItem
        Dim foundItem As ListItem
        Dim alphaIndex As Integer = 0
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    If transaction IsNot Nothing Then
                        accountList.Items.Insert(0, New ListItem("(None)", 0))
                        foundItem = accountList.Items.FindByValue(transaction.AccountID)
                        If foundItem IsNot Nothing Then
                            accountList.SelectedValue = foundItem.Value
                        Else
                            ' Retrieve inactive account and add it to the drop down list
                            account = wcfProxy.GetBankAccount(transaction.AccountID)
                            ' insert it alphabetically into the drop down list
                            For Each acct As ListItem In accountList.Items
                                If String.Compare(account.Name, acct.Text, True) < 0 Then
                                    Exit For
                                End If
                                alphaIndex += 1
                            Next
                            If account.Number <> Nothing Then
                                accountList.Items.Insert(alphaIndex, New ListItem(account.Name & " (" & account.Number & ")", account.AccountID))
                            Else
                                accountList.Items.Insert(alphaIndex, New ListItem(account.Name, account.AccountID))
                            End If
                            ' select the inserted list item
                            accountList.SelectedIndex = alphaIndex
                        End If
                    End If
                    If e.ReturnValue IsNot Nothing Then
                        For Each i As BudgeteerService.BankAccount In e.ReturnValue
                            If i.Number <> Nothing Then
                                i.Name &= " (" & i.Number & ")"
                            End If
                        Next
                    End If
                Catch timeoutEx As TimeoutException
                    ShowError(timeoutEx.Message)
                    wcfProxy.Abort()
                Catch FaultEx As FaultException
                    ShowError(FaultEx.Message)
                Catch commEx As CommunicationException
                    ShowError(commEx.Message)
                    wcfProxy.Abort()
                Catch ex As Exception
                    ShowError(ex.Message)
                End Try
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If
    End Sub

    Protected Sub CategoryDataSource_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles CategoryDataSource.Selected
        Dim categoryList As DropDownList
        categoryList = CType(FormView1.FindControl("ddlCategories"), DropDownList)
        categoryList.Items.Insert(0, New ListItem("(None)", 0))
    End Sub

    Protected Sub FormView1_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles FormView1.ItemUpdating
        Dim checkNumber As String
        Dim ddlType As DropDownList = FormView1.FindControl("ddlType")
        Dim txtAmount As TextBox = FormView1.FindControl("AmountTextBox")
        Dim txtDate As TextBox = FormView1.FindControl("TransactionDateTextBox")
        If txtAmount.Text = "" Then
            txtAmount.Text = "0"
        End If
        checkNumber = CType(CType(sender, FormView).FindControl("CheckNumberTextBox"), TextBox).Text
        e.NewValues("subcategoryID") = CType(CType(sender, FormView).FindControl("ddlSubcategories"), DropDownList).SelectedValue
        e.NewValues("checkNumber") = IIf(checkNumber = "", 0, checkNumber)
        e.NewValues("amount") = IIf(ddlType.SelectedValue = 1, "-" & txtAmount.Text, txtAmount.Text)
    End Sub

    Protected Sub UpdateCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/Transactions/Index.aspx")
    End Sub

    Protected Sub FormView1_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles FormView1.ItemUpdated
        Response.Redirect("~/MemberPages/Transactions/Index.aspx")
    End Sub

    Protected Sub FormView1_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles FormView1.DataBound
        Dim ddlType As DropDownList = FormView1.FindControl("ddlType")
        Dim transaction As BudgeteerService.Transaction = FormView1.DataItem
        If transaction IsNot Nothing Then
            If transaction.Amount >= 0 Then
                ddlType.SelectedValue = 2
            Else
                ddlType.SelectedValue = 1
            End If
        End If
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class


