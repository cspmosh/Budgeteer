Imports System.ServiceModel

Partial Public Class Index3
    Inherits System.Web.UI.Page

    Public BalanceTotal As Decimal = 0

    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
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

    Protected Function GetSubcategories(ByVal categoryID As Int64) As List(Of BudgeteerService.Subcategory)
        Dim subcategories As List(Of BudgeteerService.Subcategory) = New List(Of BudgeteerService.Subcategory)
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    subcategories = wcfProxy.GetSubcategoriesByCategoryID(categoryID)
                    If subcategories IsNot Nothing Then
                        BalanceTotal = CType(ViewState("Budgeteer.SFTotal"), Decimal)
                        For Each subcategory As BudgeteerService.Subcategory In subcategories
                            If subcategory.SinkingFund = True Then
                                BalanceTotal += subcategory.Balance
                            End If
                        Next
                        ViewState("Budgeteer.SFTotal") = BalanceTotal
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
        Return subcategories
    End Function

    Protected Sub deleteCategory(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkBtn As ImageButton = CType(sender, ImageButton)
        If lnkBtn.CommandArgument <> "" Then
            Dim result As Integer
            If wcfProxy IsNot Nothing Then
                If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                    wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                    Try
                        result = wcfProxy.DeleteCategory(CType(lnkBtn.CommandArgument, Int64))
                        Response.Redirect("Index.aspx")
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
    End Sub

    Protected Sub deleteSubcategory(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkBtn As ImageButton = CType(sender, ImageButton)
        If lnkBtn.CommandArgument <> "" Then
            Dim result As Integer
            If wcfProxy IsNot Nothing Then
                If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                    wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                    Try
                        result = wcfProxy.DeleteSubcategory(CType(lnkBtn.CommandArgument, Int64))
                        Response.Redirect("Index.aspx")
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
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class