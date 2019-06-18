Imports System.ServiceModel
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Net.Mail
Imports System.Net

Partial Public Class ChangeEmail
    Inherits System.Web.UI.Page

    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
    End Sub

    Protected Sub UpdateCancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/MemberPages/UserAccount/Index.aspx")
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim result As Integer = 0
        Dim email As String = txtNewEmail.Text
        Dim user As BudgeteerService.User = New BudgeteerService.User
        Dim previousEmail As String = Nothing

        If Not validateEmail(email) Then
            ShowError("Please enter a valid email address")
            Return
        End If

        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    ' Get the current user email address
                    user = wcfProxy.GetUser()
                    If user IsNot Nothing Then
                        previousEmail = user.EmailAddress
                    End If
                    result = wcfProxy.UpdateUserEmailAddress(email)
                    If result > 0 Then
                        ' Try to send email
                        Try
                            SendEmail(previousEmail, email)
                        Catch ex As Exception
                            ' Do nothing if send fails
                        Finally
                            ' Redirect back to account page
                            Response.Redirect("~/MemberPages/UserAccount/Index.aspx")
                        End Try
                    End If
                Catch timeoutEx As TimeoutException
                    ShowError(timeoutEx.Message)
                    wcfProxy.Abort()
                    Return
                Catch ex As FaultException
                    ShowError(ex.Message)
                    Return
                Catch commEx As CommunicationException
                    ShowError(commEx.Message)
                    wcfProxy.Abort()
                    Return
                End Try
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If
        Response.Redirect("~/MemberPages/UserAccount/Index.aspx")
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

    Private Function validateEmail(ByVal emailAddress) As Boolean
        Dim emailRegEx As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim match As Match = Regex.Match(emailAddress, emailRegEx)
        If match.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SendEmail(ByVal originalEmail As String, ByVal newEmail As String)
        Dim info As NameValueCollection = New NameValueCollection
        Dim message As MailMessage = New MailMessage
        Dim plainTextView As AlternateView
        Dim htmlView As AlternateView
        Dim client As SmtpClient = New SmtpClient()

        Try
            info.Add("URL", Request.Url.GetLeftPart(UriPartial.Authority))
            info.Add("newEmail", newEmail)
            info.Add("budgeteerEmail", "budgeteer.app@gmail.com")

            plainTextView = AlternateView.CreateAlternateViewFromString(formatEmail(info, "txt"), Nothing, "text/plain")
            htmlView = AlternateView.CreateAlternateViewFromString(formatEmail(info, "html"), Nothing, "text/html")

            message.AlternateViews.Add(plainTextView)
            message.AlternateViews.Add(htmlView)
            message.From = New MailAddress("budgeteer.app@gmail.com")
            message.To.Add(New MailAddress(originalEmail))
            message.Subject = "Budgeteer Email Change"

            client.EnableSsl = True
            client.Send(message)

        Catch ex As Exception
            ' Don't throw exception. Don't do anything. If user didn't receive email they're 
            ' still registered And won't be able to correct any information from here. If user
            ' doesn't receive email they'll contact support.
        End Try
    End Sub

    Private Function formatEmail(ByVal info As NameValueCollection, ByVal format As String) As String
        Dim template As String = ""
        Dim filename As String = Server.MapPath("EmailChangeEmailTemplate." & format)
        Dim objStreamReader As StreamReader

        objStreamReader = File.OpenText(filename)
        template = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        template = template.Replace("{URL}", info("URL"))
        template = template.Replace("{NEWEMAIL}", info("newEmail"))
        template = template.Replace("{BUDGETEEREMAIL}", info("budgeteerEmail"))
        Return template

    End Function

End Class