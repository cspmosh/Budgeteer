Public Partial Class FAQ
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (Not (Request.IsAuthenticated)) Then
            Me.MasterPageFile = "/GeneralMasterPage.master"
        Else
            Me.MasterPageFile = "/MemberPages/MasterPage.master"
        End If
    End Sub

End Class