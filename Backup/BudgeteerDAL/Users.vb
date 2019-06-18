Imports BudgeteerObjects
Imports System.Data
Imports System.Data.Common

Public Class Users

    Public Function getUser(ByRef dTran As DistributedTransaction, ByVal userID As String) As BudgeteerObjects.User

        Try

            Dim params(0) As DbParameter
            Dim dr As DataRow
            Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_user(?)", params, dTran)

            If dr IsNot Nothing Then
                user.UserID = dr("user_id")
                user.Password = dr("password")
                user.Salt = dr("salt")
                user.FirstName = dr("first_name")
                user.LastName = dr("last_name")
                user.EmailAddress = dr("email_address")
                Return user
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getUserByEmail(ByRef dTran As DistributedTransaction, ByVal emailAddress As String) As BudgeteerObjects.User

        Try
            Dim params(0) As DbParameter
            Dim dr As DataRow
            Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

            params(0) = DbHelper.CreateParameter("@email_address", DbType.String, emailAddress)

            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_user_by_email(?)", params, dTran)

            If dr IsNot Nothing Then
                user.UserID = dr("user_id")
                user.Password = dr("password")
                user.Salt = dr("salt")
                user.FirstName = dr("first_name")
                user.LastName = dr("last_name")
                user.EmailAddress = dr("email_address")
                Return user
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getUserRegistration(ByRef dTran As DistributedTransaction, ByVal userID As String) As BudgeteerObjects.Registration
        Try
            Dim params(0) As DbParameter
            Dim dr As DataRow
            Dim registration As BudgeteerObjects.Registration = New BudgeteerObjects.Registration
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_registration(?)", params, dTran)
            If dr IsNot Nothing Then
                registration.RegistrationKey = dr("registration_key")
                registration.UserID = dr("user_id")
                registration.EmailAddress = dr("email_address")
                Return registration
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Public Function addUserRegistration(ByRef dTran As DistributedTransaction, ByVal userRegistration As BudgeteerObjects.Registration) As Integer
        Try
            Dim params(2) As DbParameter
            Dim rows_effected As Integer = 0
            params(0) = DbHelper.CreateParameter("@registration_key", DbType.String, userRegistration.RegistrationKey)
            params(1) = DbHelper.CreateParameter("@user_id", DbType.String, userRegistration.UserID)
            params(2) = DbHelper.CreateParameter("@email_address", DbType.String, userRegistration.EmailAddress)
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_add_registration(?,?,?)", params, dTran)
            Return rows_effected
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Public Function getUserSecurityQuestionAnswers(ByRef dTran As DistributedTransaction, ByVal userID As String) As List(Of BudgeteerObjects.UserSecurityQuestionAnswer)

        Dim QuestionAnswers As List(Of BudgeteerObjects.UserSecurityQuestionAnswer) = New List(Of BudgeteerObjects.UserSecurityQuestionAnswer)
        Dim params(0) As DbParameter

        params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

        Try
            Dim ds As DataSet = New DataSet

            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_user_security_questions(?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim questionAnswer As BudgeteerObjects.UserSecurityQuestionAnswer = New BudgeteerObjects.UserSecurityQuestionAnswer
                questionAnswer.UserID = dr("user_id")
                questionAnswer.SecurityQuestionID = dr("security_question_id")
                questionAnswer.Answer = dr("answer")
                QuestionAnswers.Add(questionAnswer)
            Next

            If QuestionAnswers.Count > 0 Then
                Return QuestionAnswers
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function addUserSecurityQuestion(ByRef dTran As DistributedTransaction, ByVal questionAnswer As BudgeteerObjects.UserSecurityQuestionAnswer) As Integer

        Dim params(2) As DbParameter
        Dim rows_effected As Integer = Nothing

        Try
            params(0) = DbHelper.CreateParameter("@userID", DbType.String, questionAnswer.UserID)
            params(1) = DbHelper.CreateParameter("@securityQuestionID", DbType.Int64, questionAnswer.SecurityQuestionID)
            params(2) = DbHelper.CreateParameter("@answer", DbType.String, questionAnswer.Answer)
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_add_user_security_question(?,?,?)", params, dTran)
            Return rows_effected
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getUserSecurityQuestion(ByRef dTran As DistributedTransaction, ByVal userID As String, ByVal securityQuestionID As Int64) As BudgeteerObjects.UserSecurityQuestionAnswer

        Dim params(1) As DbParameter
        Dim questionAnswer As BudgeteerObjects.UserSecurityQuestionAnswer = New BudgeteerObjects.UserSecurityQuestionAnswer
        Dim dr As DataRow

        Try
            params(0) = DbHelper.CreateParameter("@userID", DbType.String, userID)
            params(1) = DbHelper.CreateParameter("@securityQuestionID", DbType.Int64, securityQuestionID)
            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_user_security_question_answer(?,?)", params, dTran)
            If dr IsNot Nothing Then
                questionAnswer.UserID = dr("user_id")
                questionAnswer.SecurityQuestionID = dr("security_question_id")
                questionAnswer.Answer = dr("answer")
            Else
                questionAnswer = Nothing
            End If
            Return questionAnswer
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Public Function deleteUserRegistration(ByRef dTran As DistributedTransaction, ByVal userID As String) As Integer
        Try
            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim rows_effected As Integer = 0
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_delete_registration(?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Public Function activateUser(ByRef dTran As DistributedTransaction, ByVal userID As String) As Integer
        Try
            Dim params(0) As DbParameter
            Dim rows_effected As Integer = 0
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_activate_user(?)", params, dTran)
            Return rows_effected
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Public Function addUser(ByRef dTran As DistributedTransaction, ByVal user As BudgeteerObjects.User) As Integer

        Try

            Dim params(5) As DbParameter
            Dim rows_effected As Integer = 0

            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, user.UserID)
            params(1) = DbHelper.CreateParameter("@password", DbType.String, user.Password)
            params(2) = DbHelper.CreateParameter("@salt", DbType.String, user.Salt)
            params(3) = DbHelper.CreateParameter("@first_name", DbType.String, user.FirstName)
            params(4) = DbHelper.CreateParameter("@last_name", DbType.String, user.LastName)
            params(5) = DbHelper.CreateParameter("@email_address", DbType.String, user.EmailAddress)

            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_add_user(?,?,?,?,?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function updatePassword(ByRef dTran As DistributedTransaction, ByVal userID As String, ByVal newPassword As String) As Integer

        Try
            Dim params(1) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            params(1) = DbHelper.CreateParameter("@new_password", DbType.String, newPassword)

            Dim rows_effected As Integer = 0
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_update_user_password(?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function updateEmailAddress(ByRef dTran As DistributedTransaction, ByVal userID As String, ByVal newEmail As String) As Integer

        Try
            Dim params(1) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            params(1) = DbHelper.CreateParameter("@new_email", DbType.String, newEmail)

            Dim rows_effected As Integer = 0
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_update_user_email(?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
