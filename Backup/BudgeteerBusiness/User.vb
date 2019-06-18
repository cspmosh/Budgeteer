Imports BudgeteerDAL
Imports System.Text.RegularExpressions

Public Class User

    Public Function validateEmail(ByVal emailAddress As String) As Boolean
        Dim emailRegEx As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim match As Match = Regex.Match(emailAddress, emailRegEx)
        If match.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function validateStrongPassword(ByVal password As String) As Boolean
        ' Regular expression that requires 7 characters with at least one number or symbol
        Dim passwordRegEx As String = "^.*(?=.{7,})(?=.*[\d\W]).*$"
        Dim match As Match = Regex.Match(password, passwordRegEx)
        If match.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function validateUserNameRequirements(ByVal userID As String) As Boolean
        ' Regular expression that requires 7 characters with at least one number or symbol
        Dim userNameRegEx As String = "^[a-zA-Z0-9]+$"
        Dim match As Match = Regex.Match(userID, userNameRegEx)
        If match.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function getUser(ByVal userID As String) As BudgeteerObjects.User

        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User
        Dim userData As BudgeteerDAL.Users = New BudgeteerDAL.Users

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be null or empty")
        Else
            If userID.Length() > BudgeteerObjects.User.MaxFieldLength.UserID Then
                Throw New ArgumentException("User ID cannot exceed " + BudgeteerObjects.User.MaxFieldLength.UserID.ToString() + " characters")
            End If
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                user = userData.getUser(dTran, userID)
                dTran.Commit()
                Return user
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function getUserName(ByVal emailAddress As String) As String
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User
        Dim userData As BudgeteerDAL.Users = New BudgeteerDAL.Users

        If emailAddress Is Nothing Then
            Throw New ArgumentException("Email Address is required")
        End If

        If Not validateEmail(emailAddress) Then
            Throw New ArgumentException("Please enter a valid email address")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                user = userData.getUserByEmail(dTran, emailAddress)
                If user IsNot Nothing Then
                    Return user.UserID
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New ArgumentException("An error occured while trying to retrieve the user")
            End Try
        End Using

    End Function

    Public Function addUser(ByVal user As BudgeteerObjects.User, ByVal password As String, ByVal securityQuestions As List(Of BudgeteerObjects.UserSecurityQuestionAnswer)) As String

        Dim userData As BudgeteerDAL.Users = New BudgeteerDAL.Users
        Dim result As Integer = Nothing
        Dim existingUser As BudgeteerObjects.User = New BudgeteerObjects.User
        Dim userRegistration As BudgeteerObjects.Registration = New BudgeteerObjects.Registration
        Dim guid As Guid = guid.NewGuid
        Dim salt As String = ""

        If String.IsNullOrEmpty(Trim(user.UserID)) Then
            Throw New ArgumentException("User ID cannot be null or empty")
        Else
            If user.UserID.Length() > BudgeteerObjects.User.MaxFieldLength.UserID Then
                Throw New ArgumentException("User ID cannot exceed " + BudgeteerObjects.User.MaxFieldLength.UserID.ToString() + " characters")
            End If
        End If

        If Not validateUserNameRequirements(user.UserID) Then
            Throw New ArgumentException("User ID does not meet requirements. Please make sure your User ID only contains alphanumeric characters without spaces")
        End If

        If String.IsNullOrEmpty(Trim(password)) Then
            Throw New ArgumentException("Password cannot be null or empty")
        Else
            If password.Length() > BudgeteerObjects.User.MaxFieldLength.Password Then
                Throw New ArgumentException("Password cannot exceed " + BudgeteerObjects.User.MaxFieldLength.Password.ToString() + " characters")
            End If
        End If

        If Not validateStrongPassword(password) Then
            Throw New ArgumentException("Password does not meet requirements. Please make sure your password is at least 7 characters long with at least one number or symbol")
        End If

        If String.IsNullOrEmpty(Trim(user.FirstName)) Then
            Throw New ArgumentException("First Name cannot be null or empty")
        Else
            If user.FirstName.Length() > BudgeteerObjects.User.MaxFieldLength.FirstName Then
                Throw New ArgumentException("First Name cannot exceed " + BudgeteerObjects.User.MaxFieldLength.FirstName.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(user.LastName)) Then
            Throw New ArgumentException("Last Name cannot be null or empty")
        Else
            If user.LastName.Length() > BudgeteerObjects.User.MaxFieldLength.LastName Then
                Throw New ArgumentException("Last Name cannot exceed " + BudgeteerObjects.User.MaxFieldLength.LastName.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(user.EmailAddress)) Then
            Throw New ArgumentException("Email Address cannot be null or empty")
        Else
            If user.EmailAddress.Length() > BudgeteerObjects.User.MaxFieldLength.EmailAddress Then
                Throw New ArgumentException("Email Address cannot exceed " + BudgeteerObjects.User.MaxFieldLength.EmailAddress.ToString() + " characters")
            End If
        End If

        If validateEmail(user.EmailAddress) = False Then
            Throw New ArgumentException("Not a valid email address")
        End If

        If securityQuestions Is Nothing Then
            Throw New ArgumentException("You must specify at least two security questions")
        Else
            If securityQuestions.Count < BudgeteerBusiness.Security.minimumSecurityQuestions Then
                Throw New ArgumentException("You must specify at least " & BudgeteerBusiness.Security.minimumSecurityQuestions & " security questions")
            End If
        End If

        For Each question As BudgeteerObjects.UserSecurityQuestionAnswer In securityQuestions
            If String.IsNullOrEmpty(Trim(question.UserID)) Then
                Throw New ArgumentException("User ID for security questions cannot be null or empty")
            Else
                If question.UserID.Length() > BudgeteerObjects.UserSecurityQuestionAnswer.MaxFieldLength.UserID Then
                    Throw New ArgumentException("User ID for security questions cannot exceed " + BudgeteerObjects.UserSecurityQuestionAnswer.MaxFieldLength.UserID.ToString() + " characters")
                End If
            End If
            If question.SecurityQuestionID = Nothing Then
                Throw New ArgumentException("Security Question ID is required for each security question")
            End If
            If String.IsNullOrEmpty(Trim(question.Answer)) Then
                Throw New ArgumentException("Answers are required for all security questions")
            Else
                If Len(question.Answer) > BudgeteerObjects.UserSecurityQuestionAnswer.MaxFieldLength.Answer Then
                    Throw New ArgumentException("Question answers cannot exceed " + BudgeteerObjects.UserSecurityQuestionAnswer.MaxFieldLength.Answer.ToString() + " characters")
                End If
            End If
        Next

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Unique Constraints
                existingUser = Nothing
                existingUser = userData.getUser(dTran, user.UserID)
                If existingUser IsNot Nothing Then
                    Throw New ArgumentException("User Name is already in use. Please choose another User Name.")
                End If
                existingUser = Nothing
                existingUser = userData.getUserByEmail(dTran, user.EmailAddress)
                If existingUser IsNot Nothing Then
                    Throw New ArgumentException("Email Address is already in use. Please use another Email Address.")
                End If
                ' Add User
                salt = Security.CreateSalt(8)
                user.Salt = salt
                user.Password = Security.CreatePasswordHash(password, user.Salt)
                result = userData.addUser(dTran, user)
                If result = 1 Then
                    ' Add security questions for forgotten password
                    For Each question As BudgeteerObjects.UserSecurityQuestionAnswer In securityQuestions
                        Dim dbAnswer As BudgeteerObjects.UserSecurityQuestionAnswer = New BudgeteerObjects.UserSecurityQuestionAnswer
                        dbAnswer = userData.getUserSecurityQuestion(dTran, user.UserID, question.SecurityQuestionID)
                        If dbAnswer IsNot Nothing Then
                            Throw New ArgumentException("This security question is already in use. Please select a different security question")
                        End If
                        result = userData.addUserSecurityQuestion(dTran, question)
                        If result <> 1 Then
                            Throw New ArgumentException("Failed to create security question")
                        End If
                    Next
                    ' Add registration
                    userRegistration.UserID = user.UserID
                    userRegistration.EmailAddress = user.EmailAddress
                    userRegistration.RegistrationKey = guid.ToString
                    result = userData.addUserRegistration(dTran, userRegistration)
                    If result <> 1 Then
                        Throw New ApplicationException("Failed to create user registration info")
                    End If
                Else
                    Throw New ApplicationException("Failed to create user")
                End If
                dTran.Commit()
                Return userRegistration.RegistrationKey
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function activateUser(ByVal userID As String, ByVal emailAddress As String, ByVal registrationKey As String)
        Dim registration As BudgeteerObjects.Registration = New BudgeteerObjects.Registration
        Dim userData As BudgeteerDAL.Users = New BudgeteerDAL.Users
        Dim result As Integer = 0

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be null or empty")
        Else
            If userID.Length() > BudgeteerObjects.Registration.MaxFieldLength.UserID Then
                Throw New ArgumentException("User ID cannot exceed " + BudgeteerObjects.Registration.MaxFieldLength.UserID.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(emailAddress)) Then
            Throw New ArgumentException("Email cannot be null or empty")
        Else
            If emailAddress.Length() > BudgeteerObjects.Registration.MaxFieldLength.EmailAddress Then
                Throw New ArgumentException("Email cannot exceed " + BudgeteerObjects.Registration.MaxFieldLength.EmailAddress.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(registrationKey)) Then
            Throw New ArgumentException("Registration Key cannot be null or empty")
        Else
            If registrationKey.Length() > BudgeteerObjects.Registration.MaxFieldLength.RegistrationKey Then
                Throw New ArgumentException("Registration Key cannot exceed " + BudgeteerObjects.Registration.MaxFieldLength.RegistrationKey.ToString() + " characters")
            End If
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                registration = userData.getUserRegistration(dTran, userID)
                If String.Compare(registration.RegistrationKey, registrationKey, True) = 0 And _
                    String.Compare(registration.UserID, userID, True) = 0 And _
                    String.Compare(registration.EmailAddress, emailAddress, True) = 0 Then
                    result = userData.activateUser(dTran, userID)
                    If result < 1 Then
                        Throw New ApplicationException("User could not be activated at this time")
                    End If
                    result = userData.deleteUserRegistration(dTran, userID)
                Else
                    Throw New ArgumentException("System could not location the registration information provided")
                End If
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using
    End Function

    Public Function updateUserEmail(ByVal userID As String, ByVal newEmail As String) As Integer

        Dim userData As BudgeteerDAL.Users = New BudgeteerDAL.Users
        Dim result As Integer = Nothing
        Dim existingUser As BudgeteerObjects.User = New BudgeteerObjects.User

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be null or empty")
        Else
            If userID.Length() > BudgeteerObjects.User.MaxFieldLength.UserID Then
                Throw New ArgumentException("User ID cannot exceed " + BudgeteerObjects.User.MaxFieldLength.UserID.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(newEmail)) Then
            Throw New ArgumentException("Email Address cannot be null or empty")
        Else
            If newEmail.Length() > BudgeteerObjects.User.MaxFieldLength.EmailAddress Then
                Throw New ArgumentException("Email Address cannot exceed " + BudgeteerObjects.User.MaxFieldLength.EmailAddress.ToString() + " characters")
            End If
        End If

        If validateEmail(newEmail) = False Then
            Throw New ArgumentException("Not a valid email address")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Unique Constraints
                existingUser = Nothing
                existingUser = userData.getUserByEmail(dTran, newEmail)
                If existingUser IsNot Nothing Then
                    Throw New ArgumentException("Email Address is already in use. Please use another Email Address.")
                End If
                ' Update Email Address
                result = userData.updateEmailAddress(dTran, userID, newEmail)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function updateUserPassword(ByVal userID As String, ByVal oldPassword As String, ByVal newPassword As String) As Integer

        Dim userData As BudgeteerDAL.Users = New BudgeteerDAL.Users
        Dim result As Integer = Nothing
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be null or empty")
        Else
            If userID.Length() > BudgeteerObjects.User.MaxFieldLength.UserID Then
                Throw New ArgumentException("User ID cannot exceed " + (BudgeteerObjects.User.MaxFieldLength.UserID).ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(oldPassword)) Then
            Throw New ArgumentException("Current password cannot be null or empty")
        Else
            If newPassword.Length() > BudgeteerObjects.User.MaxFieldLength.Password Then
                Throw New ArgumentException("Current password cannot exceed " + BudgeteerObjects.User.MaxFieldLength.Password.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(newPassword)) Then
            Throw New ArgumentException("New password cannot be null or empty")
        Else
            If newPassword.Length() > BudgeteerObjects.User.MaxFieldLength.Password Then
                Throw New ArgumentException("New password cannot exceed " + BudgeteerObjects.User.MaxFieldLength.Password.ToString() + " characters")
            End If
        End If

        If Not validateStrongPassword(newPassword) Then
            Throw New ArgumentException("New password does not meet requirements. Please make sure your password is at least 7 characters long with at least one number or symbol")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                user = userData.getUser(dTran, userID)
                If user Is Nothing Then
                    Throw New ArgumentException("The user ID specified could not be found in the database")
                End If
                If String.Compare(user.Password, Security.CreatePasswordHash(oldPassword, user.Salt), False) <> 0 Then
                    Throw New ArgumentException("Invalid current password")
                End If
                newPassword = Security.CreatePasswordHash(newPassword, user.Salt)
                userData.updatePassword(dTran, userID, newPassword)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

End Class
