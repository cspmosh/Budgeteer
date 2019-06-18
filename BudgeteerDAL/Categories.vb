Imports BudgeteerObjects
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class Categories

    Public Function getCategory(ByRef dTran As DistributedTransaction, ByVal categoryID As Int64) As BudgeteerObjects.Category

        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        Dim params(0) As DbParameter
        Dim dr As DataRow

        Try
            params(0) = DbHelper.CreateParameter("@category_id", DbType.Int64, categoryID)

            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_category(?)", params, dTran)

            If dr IsNot DBNull.Value Then
                category.CategoryID = dr("category_id")
                category.Description = dr("description")
                category.Active = Convert.ToBoolean(dr("active"))
                category.UserID = dr("user_id")
                Return category
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getCategoriesByUserID(ByRef dTran As DistributedTransaction, ByVal userID As String) As List(Of BudgeteerObjects.Category)

        Try
            Dim categoryList As List(Of BudgeteerObjects.Category) = New List(Of BudgeteerObjects.Category)

            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_categories(?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim category As Category = New Category
                category.CategoryID = dr("category_id")
                category.Description = dr("description")
                category.Active = Convert.ToBoolean(dr("active"))
                category.UserID = dr("user_id")
                categoryList.Add(category)
            Next

            If categoryList.Count > 0 Then
                Return categoryList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)

        End Try
    End Function

    Public Function getActiveCategoriesByUserID(ByRef dTran As DistributedTransaction, ByVal userID As String) As List(Of BudgeteerObjects.Category)

        Try
            Dim categoryList As List(Of BudgeteerObjects.Category) = New List(Of BudgeteerObjects.Category)

            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_active_categories(?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim category As Category = New Category
                category.CategoryID = dr("category_id")
                category.Description = dr("description")
                category.Active = Convert.ToBoolean(dr("active"))
                category.UserID = dr("user_id")
                categoryList.Add(category)
            Next

            If categoryList.Count > 0 Then
                Return categoryList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)

        End Try
    End Function

    Public Function addCategory(ByRef dTran As DistributedTransaction, ByVal category As Category) As Int64

        Try
            Dim params(2) As DbParameter

            params(0) = DbHelper.CreateParameter("@description", DbType.String, IIf(String.IsNullOrEmpty(Trim(category.Description)), DBNull.Value, Trim(category.Description)))
            params(1) = DbHelper.CreateParameter("@active", DbType.Boolean, category.Active)
            params(2) = DbHelper.CreateParameter("@user_id", DbType.String, category.UserID)

            Dim row_inserted As Int64 = 0

            row_inserted = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_add_category(?,?,?)", params, dTran)

            Return row_inserted

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function updateCategory(ByRef dTran As DistributedTransaction, ByVal category As Category) As Integer

        Try
            Dim params(2) As DbParameter
            params(0) = DbHelper.CreateParameter("@category_id", DbType.Int64, category.CategoryID)
            params(1) = DbHelper.CreateParameter("@description", DbType.String, IIf(String.IsNullOrEmpty(Trim(category.Description)), DBNull.Value, Trim(category.Description)))
            params(2) = DbHelper.CreateParameter("@active", DbType.Boolean, category.Active)

            Dim rows_effected As Integer = 0

            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_update_category(?,?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function deleteCategory(ByRef dTran As DistributedTransaction, ByVal categoryID As Int64) As Integer

        Try
            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@category_id", DbType.Int64, categoryID)

            Dim rows_effected As Integer = 0

            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_delete_category(?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
