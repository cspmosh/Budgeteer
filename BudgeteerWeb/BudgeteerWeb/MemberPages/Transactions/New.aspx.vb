Imports System.ServiceModel

Partial Public Class _New
    Inherits System.Web.UI.Page

    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If
        If Not Page.IsPostBack Then
            Dim txtAmount As TextBox = FormView1.FindControl("AmountTextBox")
            Dim txtTaxAmount As TextBox = FormView1.FindControl("TaxAmountTextBox")
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

        If Not Page.IsPostBack Then
            ddlSubcategories.Items.Insert(0, New ListItem("(None)", 0))
            ddlSubcategories.SelectedValue = 0
            ddlSubcategories.Enabled = False
            Return
        Else
            categoryID = ddlCategories.SelectedValue
        End If

        Try
            ddlSubcategories.Items.Clear()
            ddlSubcategories.Enabled = True
            e.InputParameters("CategoryID") = categoryID
        Catch ex As Exception
            lblError.Text = ex.Message()
        End Try

    End Sub

    Protected Sub subcategoryListDataBound()
        Dim ddlSubcategories As DropDownList = FormView1.FindControl("ddlSubcategories")
        Dim subcategory As BudgeteerService.Subcategory = New BudgeteerService.Subcategory
        Dim alphaIndex As Integer = 0
        Try
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
        Dim accountList As DropDownList
        accountList = CType(FormView1.FindControl("ddlBankAccounts"), DropDownList)
        accountList.Items.Insert(0, New ListItem("(None)", 0))
        If e.ReturnValue IsNot Nothing Then
            For Each i As BudgeteerService.BankAccount In e.ReturnValue
                If i.Number <> Nothing Then
                    i.Name &= " (" & i.Number & ")"
                End If
            Next
        End If
    End Sub

    Protected Sub CategoryDataSource_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles CategoryDataSource.Selected
        Dim categoryList As DropDownList
        categoryList = CType(FormView1.FindControl("ddlCategories"), DropDownList)
        categoryList.Items.Insert(0, New ListItem("(None)", 0))
    End Sub

    Protected Sub FormView1_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormView1.ItemInserting
        Dim checkNumber As String
        Dim ddlType As DropDownList = FormView1.FindControl("ddlType")
        Dim txtAmount As TextBox = FormView1.FindControl("AmountTextBox")
        Dim txtTaxAmount As TextBox = FormView1.FindControl("TaxAmountTextBox")
        If txtAmount.Text = "" Then
            txtAmount.Text = "0"
        End If
        If txtTaxAmount.Text = "" Then
            txtTaxAmount.Text = "0"
        End If
        checkNumber = CType(CType(sender, FormView).FindControl("CheckNumberTextBox"), TextBox).Text
        e.Values("CheckNumber") = IIf(checkNumber = "", 0, checkNumber)
        e.Values("amount") = IIf(ddlType.SelectedValue = 1, "-" & txtAmount.Text, txtAmount.Text)
        e.Values("taxAmount") = IIf(txtTaxAmount.Text = "", 0, txtTaxAmount.Text)
        e.Values("subcategoryID") = CType(FormView1.FindControl("ddlSubcategories"), DropDownList).SelectedValue
    End Sub

    Protected Sub FormView1_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormView1.ItemInserted
        If e.Exception IsNot Nothing Then
            ShowError(e.Exception.Message)
            e.ExceptionHandled = True
            e.KeepInInsertMode = True
        Else
            Response.Redirect("~/MemberPages/Transactions/Index.aspx")
        End If
    End Sub

    Protected Sub InsertCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/Transactions/Index.aspx")
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class