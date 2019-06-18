Imports System.ServiceModel

Partial Public Class PasswordRecovery
    Inherits System.Web.UI.Page

    Dim currentStep As Integer = 1
    Dim currentStepInstructions As String = ""
    Dim wcfProxy As UnauthenticatedService.BudgeteerUnauthenticatedServicesClient
    Dim budgeteerUser As UnauthenticatedService.User = New UnauthenticatedService.User
    Dim securityQuestions As List(Of UnauthenticatedService.SecurityQuestion) = New List(Of UnauthenticatedService.SecurityQuestion)
    Dim securityQuestionAnswers As List(Of UnauthenticatedService.UserSecurityQuestionAnswer) = New List(Of UnauthenticatedService.UserSecurityQuestionAnswer)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ShowStep(currentStep)
        Else
            If ViewState("Budgeteer.PasswordRecovery.User") IsNot Nothing Then
                budgeteerUser = ViewState("Budgeteer.PasswordRecovery.User")
            End If
            If ViewState("Budgeteer.PasswordRecovery.SecurityQuestionAnswers") IsNot Nothing Then
                securityQuestionAnswers = ViewState("Budgeteer.PasswordRecovery.SecurityQuestionAnswers")
            End If
            If ViewState("Budgeteer.PasswordRecovery.Step") IsNot Nothing Then
                currentStep = ViewState("Budgeteer.PasswordRecovery.Step")
            End If
        End If
    End Sub

    Protected Sub VerifyUser()

        Dim isError As Boolean = False

        lblError.Text = "<ul>"

        If String.IsNullOrEmpty(Trim(txtUserName.Text)) Then
            lblError.Text &= "<li>User Name is a required field</li>"
            isError = True
        End If

        If String.IsNullOrEmpty(Trim(txtFirstName.Text)) Then
            lblError.Text &= "<li>First Name is a required field</li>"
            isError = True
        End If

        If String.IsNullOrEmpty(Trim(txtLastName.Text)) Then
            lblError.Text &= "<li>Last Name is a required field</li>"
            isError = True
        End If

        If String.IsNullOrEmpty(Trim(txtEmailAddress.Text)) Then
            lblError.Text &= "<li>Email Address is a required field</li>"
            isError = True
        End If

        lblError.Text &= "</ul>"

        If isError Then
            lblError.Visible = True
            Return
        End If

        wcfProxy = New UnauthenticatedService.BudgeteerUnauthenticatedServicesClient

        Try
            ' Pass the fields as a user object to verify and retrieve security questions
            budgeteerUser = New UnauthenticatedService.User
            budgeteerUser.UserID = Trim(txtUserName.Text)
            budgeteerUser.FirstName = txtFirstName.Text
            budgeteerUser.LastName = txtLastName.Text
            budgeteerUser.EmailAddress = txtEmailAddress.Text
            ' Retrieve security questions (if user is validated)
            securityQuestions = wcfProxy.GetUserSecurityQuestions(budgeteerUser)
            ViewState.Add("Budgeteer.PasswordRecovery.User", budgeteerUser)
            wcfProxy.Close()
        Catch timeoutEx As TimeoutException
            lblError.Text = timeoutEx.Message
            isError = True
            wcfProxy.Abort()
        Catch faultEx As FaultException
            lblError.Text = faultEx.Message
            isError = True
            wcfProxy.Abort()
        Catch commEx As CommunicationException
            lblError.Text = commEx.Message
            isError = True
            wcfProxy.Abort()
        End Try

        If isError Then
            lblError.Visible = True
            Return
        End If

        If securityQuestions IsNot Nothing Then
            securityQuestionRepeater.DataSource = securityQuestions
            securityQuestionRepeater.DataBind()
            currentStep += 1
            ViewState.Add("Budgeteer.PasswordRecovery.Step", currentStep)
            ShowStep(currentStep)
        Else
            lblError.Text = "Invalid User Data. Please check your information and try again."
            lblError.Visible = True
            Return
        End If

    End Sub

    Protected Sub ConfirmUser()

        Dim isError As Boolean = False
        Dim valid As Boolean = False

        lblError.Text = ""
        securityQuestionAnswers = New List(Of UnauthenticatedService.UserSecurityQuestionAnswer)
        For Each item As RepeaterItem In securityQuestionRepeater.Items
            Dim txtAnswer As TextBox = item.FindControl("txtSecurityQuestionAnswer")
            Dim questionID As HiddenField = item.FindControl("hiddenQuestionID")
            Dim securityQuestionID As Int64 = CType(questionID.Value, Int64)

            Dim answer As UnauthenticatedService.UserSecurityQuestionAnswer = New UnauthenticatedService.UserSecurityQuestionAnswer

            If String.IsNullOrEmpty(Trim(txtAnswer.Text)) Then
                lblError.Text &= "Please answer all security questions"
                lblError.Visible = True
                Return
            End If

            answer.Answer = Trim(txtAnswer.Text)
            answer.UserID = budgeteerUser.UserID
            answer.SecurityQuestionID = securityQuestionID

            securityQuestionAnswers.Add(answer)
        Next

        ViewState.Add("Budgeteer.PasswordRecovery.SecurityQuestionAnswers", securityQuestionAnswers)

        wcfProxy = New UnauthenticatedService.BudgeteerUnauthenticatedServicesClient

        Try
            ' Validate security question answers
            valid = wcfProxy.ValidateSecurityQuestions(budgeteerUser, securityQuestionAnswers)
            If Not valid Then
                lblError.Text = "One or more security questions were answered incorrectly. Please try again"
                isError = True
            End If
            wcfProxy.Close()
        Catch timeoutEx As TimeoutException
            lblError.Text = timeoutEx.Message
            isError = True
            wcfProxy.Abort()
        Catch faultEx As FaultException
            lblError.Text = faultEx.Message
            isError = True
            wcfProxy.Abort()
        Catch commEx As CommunicationException
            lblError.Text = commEx.Message
            isError = True
            wcfProxy.Abort()
        End Try

        If isError Then
            lblError.Visible = True
            Return
        End If

        'Security questions are validated, show next step
        currentStep += 1
        ViewState.Add("Budgeteer.PasswordRecovery.Step", currentStep)
        ShowStep(currentStep)

    End Sub

    Protected Sub ResetPassword()
        Dim isError As Boolean = False

        lblError.Text = "<ul>"

        If String.IsNullOrEmpty(Trim(txtPassword.Text)) Then
            isError = True
            lblError.Text &= "<li>Please enter a new password</li>"
        End If

        If String.IsNullOrEmpty(Trim(txtConfirmPassword.Text)) Then
            isError = True
            lblError.Text &= "<li>Please confirm your password</li>"
        End If

        lblError.Text &= "</ul>"

        If isError Then
            lblError.Visible = True
            Return
        End If

        lblError.Text = ""

        If String.Compare(Trim(txtPassword.Text), Trim(txtConfirmPassword.Text), False) <> 0 Then
            lblError.Text = "Passwords do not match!"
            lblError.Visible = True
            Return
        End If

        wcfProxy = New UnauthenticatedService.BudgeteerUnauthenticatedServicesClient

        Try
            ' Validate security question answers
            If wcfProxy.ResetPassword(budgeteerUser, securityQuestionAnswers, Trim(txtPassword.Text)) <> 1 Then
                isError = True
                lblError.Text = "An error occured while trying to update your password. Please try again later"
            End If
            wcfProxy.Close()
        Catch timeoutEx As TimeoutException
            lblError.Text = timeoutEx.Message
            isError = True
            wcfProxy.Abort()
        Catch faultEx As FaultException
            lblError.Text = faultEx.Message
            isError = True
            wcfProxy.Abort()
        Catch commEx As CommunicationException
            lblError.Text = commEx.Message
            isError = True
            wcfProxy.Abort()
        End Try

        If isError Then
            lblError.Visible = True
            Return
        End If

        'Password Reset!
        currentStep += 1
        ShowStep(currentStep)

    End Sub

    Private Sub ShowStep(ByVal currentStep)
        lblStep.Text = currentStep.ToString
        Select Case currentStep
            Case 1
                'currentStepInstructions = "Verify your identity"
                lblStepInstructions.Text = "Verify your identity"
                pnlVerify.Visible = "True"
                pnlConfirm.Visible = "False"
                pnlReset.Visible = "False"
            Case 2
                lblStepInstructions.Text = "Confirm your identity"
                'currentStepInstructions = "Confirm your identity"
                pnlVerify.Visible = "False"
                pnlConfirm.Visible = "True"
                pnlReset.Visible = "False"
            Case 3
                lblStepInstructions.Text = "Reset your password"
                'currentStepInstructions = "Reset your password"
                pnlVerify.Visible = "False"
                pnlConfirm.Visible = "False"
                pnlReset.Visible = "True"
            Case 4
                ' Password Reset
                pnlVerify.Visible = "False"
                pnlConfirm.Visible = "False"
                pnlReset.Visible = "False"
                instructionPanel.Visible = "False"
                lblSuccess.Visible = "True"
        End Select
    End Sub

End Class