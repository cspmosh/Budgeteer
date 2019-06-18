Imports BudgeteerObjects
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class SecurityQuestions

    Public Function GetSecurityQuestions(ByRef dTran As DistributedTransaction) As List(Of BudgeteerObjects.SecurityQuestion)

        Try
            Dim questionList As List(Of BudgeteerObjects.SecurityQuestion) = New List(Of BudgeteerObjects.SecurityQuestion)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_security_questions()", dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim question As SecurityQuestion = New SecurityQuestion
                question.SecurityQuestionID = dr("security_question_id")
                question.Question = dr("question")
                questionList.Add(question)
            Next

            If questionList.Count > 0 Then
                Return questionList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function GetSecurityQuestionByID(ByRef dTran As DistributedTransaction, ByVal questionID As Int64) As BudgeteerObjects.SecurityQuestion

        Try
            Dim question As BudgeteerObjects.SecurityQuestion = New BudgeteerObjects.SecurityQuestion
            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@security_question_id", DbType.Int64, questionID)

            Dim dr As DataRow
            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_security_question_by_id(?)", params, dTran)

            If dr IsNot Nothing Then
                question.SecurityQuestionID = dr("security_question_id")
                question.Question = dr("question")
                Return question
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
