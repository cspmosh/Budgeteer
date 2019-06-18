Imports System.ServiceModel

Partial Public Class _New1
    Inherits System.Web.UI.Page

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
        Dim ddlSubcategories As DropDownList = FormView1.FindControl("ddlSubcategories")
        If ddlCategories.SelectedValue = "" Then
            ddlSubcategories.SelectedValue = ""
            ddlSubcategories.Enabled = False
            Return
        Else
            SubcategoryDataSource.SelectParameters(0).DefaultValue = ddlCategories.SelectedValue
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
            ddlSubcategories.Items.Insert(0, New ListItem(" -- Choose -- ", ""))
            ddlSubcategories.SelectedValue = ""
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
            ShowError(ex.Message)
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

    Protected Sub FormView1_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormView1.ItemInserting
        Dim txtAmount As TextBox = FormView1.FindControl("AmountTextBox")
        If txtAmount.Text = "" Then
            txtAmount.Text = "0"
        End If
        e.Values("subcategoryID") = CType(FormView1.FindControl("ddlSubcategories"), DropDownList).SelectedValue
    End Sub

    Protected Sub FormView1_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormView1.ItemInserted
        If e.Exception IsNot Nothing Then
            ShowError(e.Exception.Message)
            e.ExceptionHandled = True
            e.KeepInInsertMode = True
        Else
            Response.Redirect("~/MemberPages/Budgets/Index.aspx")
        End If
    End Sub

    Protected Sub InsertCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/Budgets/Index.aspx")
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class