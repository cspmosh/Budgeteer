Imports System.IO
Imports System.Net.Mail
Imports System.Net

Partial Public Class Registration
    Inherits System.Web.UI.Page

    Dim wcfProxy As UnauthenticatedService.BudgeteerUnauthenticatedServicesClient
    Dim availableSecurityQuestions As List(Of UnauthenticatedService.SecurityQuestion) = New List(Of UnauthenticatedService.SecurityQuestion)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Visible = False
        If Not Page.IsPostBack Then
            Try
                wcfProxy = New UnauthenticatedService.BudgeteerUnauthenticatedServicesClient
                availableSecurityQuestions = wcfProxy.GetAvailableSecurityQuestions()
                wcfProxy.Close()
                PopulateSecurityQuestions(ddlSecurityQuestion1)
                PopulateSecurityQuestions(ddlSecurityQuestion2)
                PopulateSecurityQuestions(ddlSecurityQuestion3)
            Catch ex As Exception
                lblError.Text = ex.Message()
                lblError.Visible = True
            End Try
        End If
    End Sub

    Protected Sub RegisterUser()
        Dim user As UnauthenticatedService.User = New UnauthenticatedService.User
        Dim registrationKey As String = ""
        Dim password As String
        Dim securityQuestions As List(Of UnauthenticatedService.UserSecurityQuestionAnswer) = New List(Of UnauthenticatedService.UserSecurityQuestionAnswer)
        Dim securityQuestion As UnauthenticatedService.UserSecurityQuestionAnswer = New UnauthenticatedService.UserSecurityQuestionAnswer

        If String.Compare(txtPassword.Text, txtPasswordConfirm.Text, False) <> 0 Then
            lblError.Text = "Passwords do not match!"
            lblError.Visible = True
            Return
        End If

        If ddlSecurityQuestion1.SelectedIndex = 0 Then
            lblError.Text = "Please select a security question for Security Question #1"
            lblError.Visible = True
            Return
        End If

        If ddlSecurityQuestion2.SelectedIndex = 0 Then
            lblError.Text = "Please select a security question for Security Question #2"
            lblError.Visible = True
            Return
        End If

        If ddlSecurityQuestion3.SelectedIndex = 0 Then
            lblError.Text = "Please select a security question for Security Question #3"
            lblError.Visible = True
            Return
        End If

        lblError.Text = ""

        Try

            wcfProxy = New UnauthenticatedService.BudgeteerUnauthenticatedServicesClient

            user.UserID = txtUserID.Text
            user.FirstName = txtFirstName.Text
            user.LastName = txtLastName.Text
            user.EmailAddress = txtEmailAddress.Text
            password = txtPassword.Text

            securityQuestion.UserID = user.UserID
            securityQuestion.SecurityQuestionID = ddlSecurityQuestion1.SelectedValue
            securityQuestion.Answer = txtAnswer1.Text
            securityQuestions.Add(securityQuestion)

            securityQuestion = New UnauthenticatedService.UserSecurityQuestionAnswer
            securityQuestion.UserID = user.UserID
            securityQuestion.SecurityQuestionID = ddlSecurityQuestion2.SelectedValue
            securityQuestion.Answer = txtAnswer2.Text
            securityQuestions.Add(securityQuestion)

            securityQuestion = New UnauthenticatedService.UserSecurityQuestionAnswer
            securityQuestion.UserID = user.UserID
            securityQuestion.SecurityQuestionID = ddlSecurityQuestion3.SelectedValue
            securityQuestion.Answer = txtAnswer3.Text
            securityQuestions.Add(securityQuestion)

            registrationKey = wcfProxy.AddUser(user, password, securityQuestions)

            wcfProxy.Close()

            If registrationKey <> "" Then
                ' Send Email
                SendConfirmationEmail(user, registrationKey)
            Else
                ' Failed to register
                lblError.Text = "There was an error while trying to register your account. Please try again."
                lblError.Visible = True
            End If
        Catch ex As Exception
            lblError.Visible = True
            lblError.Text = ex.Message()
            Return
        End Try

        'Redirect to confirmation page (regardless of if email was sent successfully)
        lblSuccess.Visible = True
        Panel1.Visible = False

    End Sub

    Private Sub SendConfirmationEmail(ByVal user As UnauthenticatedService.User, ByVal registrationKey As String)
        Dim info As NameValueCollection = New NameValueCollection
        Dim message As MailMessage = New MailMessage
        Dim plainTextView As AlternateView
        Dim htmlView As AlternateView
        Dim client As SmtpClient = New SmtpClient()

        Try
            info.Add("URL", Request.Url.GetLeftPart(UriPartial.Authority))
            info.Add("userID", user.UserID)
            info.Add("firstName", user.FirstName)
            info.Add("lastName", user.LastName)
            info.Add("emailAddress", user.EmailAddress)
            info.Add("registrationKey", registrationKey)
            info.Add("sitePath", Request.Url.GetLeftPart(UriPartial.Authority) & "/Registration/Confirmation.aspx")

            plainTextView = AlternateView.CreateAlternateViewFromString(formatEmail(info, "txt"), Nothing, "text/plain")
            htmlView = AlternateView.CreateAlternateViewFromString(formatEmail(info, "html"), Nothing, "text/html")

            message.AlternateViews.Add(plainTextView)
            message.AlternateViews.Add(htmlView)
            message.From = New MailAddress("budgeteer.app@gmail.com")
            message.To.Add(New MailAddress(user.EmailAddress))
            message.Subject = "Budgeteer Registration"

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
        Dim filename As String = Server.MapPath("RegistrationEmailTemplate." & format)
        Dim objStreamReader As StreamReader

        objStreamReader = File.OpenText(filename)
        template = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        template = template.Replace("{URL}", info("URL"))
        template = template.Replace("{USERNAME}", info("userID"))
        template = template.Replace("{FIRSTNAME}", info("firstName"))
        template = template.Replace("{EMAIL}", info("emailAddress"))
        template = template.Replace("{KEY}", info("registrationKey"))
        template = template.Replace("{SITEPATH}", info("sitePath"))

        Return template

    End Function

    Private Sub PopulateSecurityQuestions(ByRef dropdownlist As DropDownList)
        dropdownlist.DataSource = availableSecurityQuestions
        dropdownlist.DataBind()
        dropdownlist.Items.Insert(0, New ListItem(" -- Select A Security Question -- ", ""))
    End Sub

End Class