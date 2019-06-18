Imports System.IO
Imports System.Net.Mail
Imports System.Net

Partial Public Class UserNameRecovery
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userID As String
        Dim querystring As NameValueCollection = Request.QueryString

        If Not Page.IsPostBack Then

            userID = Session("Budgeteer.Recovery.UserName")
            Session("Budgeteer.Recovery.UserName") = Nothing

            If userID Is Nothing Then
                ' User came here through URL so redirect to recovery page
                Response.Redirect("Index.aspx")
            End If

            If querystring.HasKeys Then
                Dim email As String = querystring.Get("Email")

                If email IsNot Nothing Then
                    Try
                        ' Send email to user
                        SendConfirmationEmail(email, userID)
                        lblMessage.Text = "An email including your User Name was sent to " & email & ". Click <a href='/Login.aspx'>here</a> to go back to the login page."
                    Catch ex As Exception
                        lblMessage.Text = ex.Message() & ". Please go <a href='Index.aspx'>back to the recovery page</a> and try again."
                    End Try
                Else

                End If
            End If

        End If
    End Sub

    Private Sub SendConfirmationEmail(ByVal email As String, ByVal userID As String)
        Dim info As NameValueCollection = New NameValueCollection
        Dim message As MailMessage = New MailMessage
        Dim plainTextView As AlternateView
        Dim htmlView As AlternateView
        Dim client As SmtpClient = New SmtpClient()

        Try
            info.Add("URL", Request.Url.GetLeftPart(UriPartial.Authority))
            info.Add("userID", userID)

            plainTextView = AlternateView.CreateAlternateViewFromString(formatEmail(info, "txt"), Nothing, "text/plain")
            htmlView = AlternateView.CreateAlternateViewFromString(formatEmail(info, "html"), Nothing, "text/html")

            message.AlternateViews.Add(plainTextView)
            message.AlternateViews.Add(htmlView)
            message.From = New MailAddress("budgeteer.app@gmail.com")
            message.To.Add(New MailAddress(email))
            message.Subject = "Your Budgeteer User Name"

            client.EnableSsl = True
            client.Send(message)

        Catch ex As Exception
            Throw New Exception("An error occured while trying to send an email")
        End Try


    End Sub

    Private Function formatEmail(ByVal info As NameValueCollection, ByVal format As String) As String
        Dim template As String = ""
        Dim filename As String = Server.MapPath("UserNameRecoveryEmailTemplate." & format)
        Dim objStreamReader As StreamReader

        objStreamReader = File.OpenText(filename)
        template = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        template = template.Replace("{URL}", info("URL"))
        template = template.Replace("{USERNAME}", info("userID"))

        Return template

    End Function

End Class