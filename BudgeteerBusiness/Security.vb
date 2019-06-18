Imports System.Security.Cryptography
Imports System.Web.Security
Imports System.Web.Configuration
Imports BudgeteerDAL

Public Class Security

    Public Const minimumSecurityQuestions As Integer = 3

    Public Enum authenticationReturnType
        Success = 0
        InvaidUserName = 1
        InvalidPassword = 2
    End Enum

    Public Function getSecurityQuestions() As List(Of BudgeteerObjects.SecurityQuestion)

        Dim data As BudgeteerDAL.SecurityQuestions = New BudgeteerDAL.SecurityQuestions
        Dim questions As List(Of BudgeteerObjects.SecurityQuestion) = New List(Of BudgeteerObjects.SecurityQuestion)

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                questions = data.GetSecurityQuestions(dTran)
                dTran.Commit()
                Return questions
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using
        
    End Function

    Public Function getSecurityQuestions(ByVal user As BudgeteerObjects.User) As List(Of BudgeteerObjects.SecurityQuestion)

        Dim data As BudgeteerDAL.SecurityQuestions = New BudgeteerDAL.SecurityQuestions
        Dim userData As BudgeteerDAL.Users = New BudgeteerDAL.Users
        Dim userQuestions As List(Of BudgeteerObjects.UserSecurityQuestionAnswer) = New List(Of BudgeteerObjects.UserSecurityQuestionAnswer)
        Dim questions As List(Of BudgeteerObjects.SecurityQuestion) = New List(Of BudgeteerObjects.SecurityQuestion)
        Dim dbuser As BudgeteerObjects.User = New BudgeteerObjects.User

        If user Is Nothing Then
            Throw New ArgumentException("User object is expected")
        End If

        If String.IsNullOrEmpty(Trim(user.UserID)) Then
            Throw New ArgumentException("User ID is a required field")
        End If

        If String.IsNullOrEmpty(Trim(user.EmailAddress)) Then
            Throw New ArgumentException("Email Address is a required field")
        End If

        If String.IsNullOrEmpty(Trim(user.FirstName)) Then
            Throw New ArgumentException("First Name is a required field")
        End If

        If String.IsNullOrEmpty(Trim(user.LastName)) Then
            Throw New ArgumentException("Last Name is a required field")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Get the user and validate fields
                dbuser = userData.getUser(dTran, user.UserID)
                If dbuser IsNot Nothing Then
                    If (String.Compare(dbuser.EmailAddress, user.EmailAddress, True) <> 0) Or _
                        (String.Compare(dbuser.FirstName, user.FirstName, True) <> 0) Or _
                        (String.Compare(dbuser.LastName, user.LastName, True) <> 0) Then
                        Throw New ArgumentException("Invalid data")
                    End If
                Else
                    Throw New ArgumentException("Invalid data")
                End If
                ' Get security question ids/answers for this user
                userQuestions = userData.getUserSecurityQuestionAnswers(dTran, user.UserID)
                ' Only return the questions (not the answers)
                For Each question As BudgeteerObjects.UserSecurityQuestionAnswer In userQuestions
                    Dim secQuestion As BudgeteerObjects.SecurityQuestion = New BudgeteerObjects.SecurityQuestion
                    secQuestion = data.GetSecurityQuestionByID(dTran, question.SecurityQuestionID)
                    questions.Add(secQuestion)
                Next
                dTran.Commit()
                Return questions
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function getSecurityQuestionByID(ByVal securityQuestionID As Int64) As BudgeteerObjects.SecurityQuestion
        Dim question As BudgeteerObjects.SecurityQuestion = New BudgeteerObjects.SecurityQuestion
        Dim secQuestData As BudgeteerDAL.SecurityQuestions = New BudgeteerDAL.SecurityQuestions

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                question = secQuestData.GetSecurityQuestionByID(dTran, securityQuestionID)
                dTran.Commit()
                Return question
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function validateSecurityQuestions(ByVal user As BudgeteerObjects.User, ByVal securityQuestionAnswers As List(Of BudgeteerObjects.UserSecurityQuestionAnswer)) As Boolean

        Dim dbuserQuestions As List(Of BudgeteerObjects.UserSecurityQuestionAnswer) = New List(Of BudgeteerObjects.UserSecurityQuestionAnswer)
        Dim dbuser As BudgeteerObjects.User = New BudgeteerObjects.User
        Dim userData As BudgeteerDAL.Users = New BudgeteerDAL.Users
        Dim questionAnswerFound As BudgeteerObjects.UserSecurityQuestionAnswer = Nothing

        ' Validate user object 
        If user Is Nothing Then
            Throw New ArgumentException("User object is expected")
        End If

        If String.IsNullOrEmpty(Trim(user.UserID)) Then
            Throw New ArgumentException("User ID is a required field")
        End If

        If String.IsNullOrEmpty(Trim(user.EmailAddress)) Then
            Throw New ArgumentException("Email Address is a required field")
        End If

        If String.IsNullOrEmpty(Trim(user.FirstName)) Then
            Throw New ArgumentException("First Name is a required field")
        End If

        If String.IsNullOrEmpty(Trim(user.LastName)) Then
            Throw New ArgumentException("Last Name is a required field")
        End If

        If securityQuestionAnswers Is Nothing Then
            Throw New ArgumentException("Security Question Answer List is expected")
        End If

        If securityQuestionAnswers.Count < BudgeteerBusiness.Security.minimumSecurityQuestions Then
            Throw New ArgumentException("You must answer all security questions")
        End If

        ' Validate security question answers
        For Each answer As BudgeteerObjects.UserSecurityQuestionAnswer In securityQuestionAnswers
            If answer.SecurityQuestionID = Nothing Then
                Throw New ArgumentException("A security question ID is required for each security question answer")
            End If
            If answer.UserID = Nothing Then
                Throw New ArgumentException("A user ID is required for each security question answer")
            End If
            If String.IsNullOrEmpty(Trim(answer.Answer)) Then
                Throw New ArgumentException("An answer is expected for each security question")
            Else
                If Len(Trim(answer.Answer)) > BudgeteerObjects.UserSecurityQuestionAnswer.MaxFieldLength.Answer Then
                    Throw New ArgumentException("Answer cannot exceed " & BudgeteerObjects.UserSecurityQuestionAnswer.MaxFieldLength.Answer.ToString() & " characters")
                End If
            End If
        Next

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Get the user and validate fields
                dbuser = userData.getUser(dTran, user.UserID)
                If dbuser IsNot Nothing Then
                    If (String.Compare(dbuser.EmailAddress, user.EmailAddress, True) <> 0) Or _
                        (String.Compare(dbuser.FirstName, user.FirstName, True) <> 0) Or _
                        (String.Compare(dbuser.LastName, user.LastName, True) <> 0) Then
                        Throw New ArgumentException("Invalid user data")
                    End If
                Else
                    Throw New ArgumentException("Invalid user data")
                End If
                ' Get security question ids/answers for this user
                dbuserQuestions = userData.getUserSecurityQuestionAnswers(dTran, user.UserID)
                If dbuserQuestions Is Nothing Then
                    ' This user doesn't have any questions stored in the database
                    Throw New ApplicationException("No security questions were found")
                End If
                ' Loop through service supplied answers
                For Each answer As BudgeteerObjects.UserSecurityQuestionAnswer In securityQuestionAnswers
                    questionAnswerFound = Nothing
                    ' Loop through the database answers and look for match
                    For Each dbAnswer As BudgeteerObjects.UserSecurityQuestionAnswer In dbuserQuestions
                        ' Compare records
                        If answer.SecurityQuestionID = dbAnswer.SecurityQuestionID Then
                            ' Question found, store it and remove from list
                            questionAnswerFound = dbAnswer
                            dbuserQuestions.Remove(dbAnswer)
                            Exit For
                        End If
                    Next
                    If questionAnswerFound Is Nothing Then
                        ' Question ID was not found in the list of questions retrieved for this user
                        Return False
                    Else
                        If String.Compare(Trim(answer.Answer), Trim(questionAnswerFound.Answer), True) <> 0 Then
                            ' Answer does not match answer from database
                            Return False
                        End If
                    End If
                Next
                ' All data is validated and security questions were answered
                Return True
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function resetPassword(ByVal user As BudgeteerObjects.User, ByVal securityQuestionAnswers As List(Of BudgeteerObjects.UserSecurityQuestionAnswer), ByVal newPassword As String) As Integer

        Dim dbuserQuestions As List(Of BudgeteerObjects.UserSecurityQuestionAnswer) = New List(Of BudgeteerObjects.UserSecurityQuestionAnswer)
        Dim dbuser As BudgeteerObjects.User = New BudgeteerObjects.User
        Dim userData As BudgeteerDAL.Users = New BudgeteerDAL.Users
        Dim questionAnswerFound As BudgeteerObjects.UserSecurityQuestionAnswer = Nothing

        ' Validate user object 
        If user Is Nothing Then
            Throw New ArgumentException("User object is expected")
        End If

        If String.IsNullOrEmpty(Trim(user.UserID)) Then
            Throw New ArgumentException("User ID is a required field")
        End If

        If String.IsNullOrEmpty(Trim(user.EmailAddress)) Then
            Throw New ArgumentException("Email Address is a required field")
        End If

        If String.IsNullOrEmpty(Trim(user.FirstName)) Then
            Throw New ArgumentException("First Name is a required field")
        End If

        If String.IsNullOrEmpty(Trim(user.LastName)) Then
            Throw New ArgumentException("Last Name is a required field")
        End If

        If securityQuestionAnswers Is Nothing Then
            Throw New ArgumentException("Security Question Answer List is expected")
        End If

        If securityQuestionAnswers.Count < BudgeteerBusiness.Security.minimumSecurityQuestions Then
            Throw New ArgumentException("You must answer all security questions")
        End If

        ' Validate security question answers
        For Each answer As BudgeteerObjects.UserSecurityQuestionAnswer In securityQuestionAnswers
            If answer.SecurityQuestionID = Nothing Then
                Throw New ArgumentException("A security question ID is required for each security question answer")
            End If
            If answer.UserID = Nothing Then
                Throw New ArgumentException("A user ID is required for each security question answer")
            End If
            If String.IsNullOrEmpty(Trim(answer.Answer)) Then
                Throw New ArgumentException("An answer is expected for each security question")
            Else
                If Len(Trim(answer.Answer)) > BudgeteerObjects.UserSecurityQuestionAnswer.MaxFieldLength.Answer Then
                    Throw New ArgumentException("Answer cannot exceed " & BudgeteerObjects.UserSecurityQuestionAnswer.MaxFieldLength.Answer.ToString() & " characters")
                End If
            End If
        Next

        ' Validate new password
        If String.IsNullOrEmpty(Trim(newPassword)) Then
            Throw New ArgumentException("Password is a requried field")
        Else
            If Len(Trim(newPassword)) > BudgeteerObjects.User.MaxFieldLength.Password Then
                Throw New ArgumentException("Password cannot exceed " & BudgeteerObjects.User.MaxFieldLength.Password.ToString() & " characters")
            End If
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Get the user and validate fields
                dbuser = userData.getUser(dTran, user.UserID)
                If dbuser IsNot Nothing Then
                    If (String.Compare(dbuser.EmailAddress, user.EmailAddress, True) <> 0) Or _
                        (String.Compare(dbuser.FirstName, user.FirstName, True) <> 0) Or _
                        (String.Compare(dbuser.LastName, user.LastName, True) <> 0) Then
                        Throw New ArgumentException("Invalid data")
                    End If
                Else
                    Throw New ArgumentException("Invalid data")
                End If
                ' Get security question ids/answers for this user
                dbuserQuestions = userData.getUserSecurityQuestionAnswers(dTran, user.UserID)
                If dbuserQuestions Is Nothing Then
                    ' This user doesn't have any questions stored in the database
                    Throw New ArgumentException("Invalid data")
                End If
                ' Loop through service supplied answers
                For Each answer As BudgeteerObjects.UserSecurityQuestionAnswer In securityQuestionAnswers
                    questionAnswerFound = Nothing
                    ' Loop through the database answers and look for match
                    For Each dbAnswer As BudgeteerObjects.UserSecurityQuestionAnswer In dbuserQuestions
                        ' Compare records
                        If answer.SecurityQuestionID = dbAnswer.SecurityQuestionID Then
                            ' Question found, store it and remove from list
                            questionAnswerFound = dbAnswer
                            dbuserQuestions.Remove(dbAnswer)
                            Exit For
                        End If
                    Next
                    If questionAnswerFound Is Nothing Then
                        ' Question ID was not found in the list of questions retrieved for this user
                        Throw New ArgumentException("Invalid data")
                    Else
                        If String.Compare(Trim(answer.Answer), Trim(questionAnswerFound.Answer), True) <> 0 Then
                            ' Answer does not match answer from database
                            Throw New ArgumentException("Invalid data")
                        End If
                    End If
                Next
                ' All data is validated and security questions were answered. Reset password
                newPassword = Security.CreatePasswordHash(newPassword, dbuser.Salt)
                If userData.updatePassword(dTran, user.UserID, newPassword) = 1 Then
                    dTran.Commit()
                    Return 1
                Else
                    Throw New ApplicationException("An error occured while updating the password")
                End If
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function authenticateUser(ByVal userID As String, ByVal password As String)

        Dim business As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        If String.IsNullOrEmpty(Trim(userID)) Then
            Return authenticationReturnType.InvaidUserName
        End If

        If String.IsNullOrEmpty(Trim(password)) Then
            Return authenticationReturnType.InvalidPassword
        End If

        Try
            user = business.getUser(userID)
        Catch ex As Exception
            Throw (ex)
        End Try

        If user IsNot Nothing Then
            If String.Compare(CreatePasswordHash(password, user.Salt), user.Password, False) = 0 Then
                Return authenticationReturnType.Success
            Else
                Return authenticationReturnType.InvalidPassword
            End If
        Else
            Return authenticationReturnType.InvaidUserName
        End If

    End Function

    Public Shared Function CreateSalt(ByVal size As Integer) As String
        'Generate a cryptographic random number
        Dim rng As RNGCryptoServiceProvider = New RNGCryptoServiceProvider
        Dim buff(size) As Byte
        rng.GetBytes(buff)
        ' Return a Base64 string representation of the random number
        Return Convert.ToBase64String(buff)
    End Function

    Public Shared Function CreatePasswordHash(ByVal pwd As String, ByVal salt As String)
        Dim saltAndPwd As String = String.Concat(pwd, salt)
        Dim hashedPwd As String = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1")
        Return hashedPwd
    End Function

End Class
