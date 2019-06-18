Imports BudgeteerObjects
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class Subcategories

    Public Function getSubcategoriesByUserID(ByRef dTran As DistributedTransaction, ByVal userID As String) As List(Of BudgeteerObjects.Subcategory)

        Try
            Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)

            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_subcategories(?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim subcategory As Subcategory = New Subcategory
                subcategory.SubcategoryID = dr("subcategory_id")
                subcategory.CategoryID = dr("category_id")
                subcategory.Description = dr("description")
                subcategory.Type = dr("type")
                subcategory.SinkingFund = Convert.ToBoolean(dr("sinking_fund"))
                subcategory.Balance = dr("balance")
                subcategory.Notes = IIf(IsDBNull(dr("notes")), Nothing, dr("notes"))
                subcategory.Active = Convert.ToBoolean(dr("active"))
                subcategory.UserID = dr("user_id")
                subcategoryList.Add(subcategory)
            Next

            If subcategoryList.Count > 0 Then
                Return subcategoryList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getActiveSubcategoriesByUserID(ByRef dTran As DistributedTransaction, ByVal userID As String) As List(Of BudgeteerObjects.Subcategory)

        Try
            Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)

            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_active_subcategories(?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim subcategory As Subcategory = New Subcategory
                subcategory.SubcategoryID = dr("subcategory_id")
                subcategory.CategoryID = dr("category_id")
                subcategory.Description = dr("description")
                subcategory.Type = dr("type")
                subcategory.SinkingFund = Convert.ToBoolean(dr("sinking_fund"))
                subcategory.Balance = dr("balance")
                subcategory.Notes = IIf(IsDBNull(dr("notes")), Nothing, dr("notes"))
                subcategory.Active = Convert.ToBoolean(dr("active"))
                subcategory.UserID = dr("user_id")
                subcategoryList.Add(subcategory)
            Next

            If subcategoryList.Count > 0 Then
                Return subcategoryList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getSubcategoriesByCategoryID(ByRef dTran As DistributedTransaction, ByVal categoryID As Int64, ByVal userID As String) As List(Of BudgeteerObjects.Subcategory)

        Try
            Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)

            Dim params(1) As DbParameter
            params(0) = DbHelper.CreateParameter("@category_id", DbType.Int64, categoryID)
            params(1) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_subcategories_by_categoryID(?, ?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim subcategory As Subcategory = New Subcategory
                subcategory.SubcategoryID = dr("subcategory_id")
                subcategory.CategoryID = dr("category_id")
                subcategory.Description = dr("description")
                subcategory.Type = dr("type")
                subcategory.SinkingFund = Convert.ToBoolean(dr("sinking_fund"))
                subcategory.Balance = dr("balance")
                subcategory.Notes = IIf(IsDBNull(dr("notes")), Nothing, dr("notes"))
                subcategory.Active = Convert.ToBoolean(dr("active"))
                subcategory.UserID = dr("user_id")
                subcategoryList.Add(subcategory)
            Next

            If subcategoryList.Count > 0 Then
                Return subcategoryList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getActiveSubcategoriesByCategoryID(ByRef dTran As DistributedTransaction, ByVal categoryID As Int64, ByVal userID As String) As List(Of BudgeteerObjects.Subcategory)

        Try
            Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)

            Dim params(1) As DbParameter
            params(0) = DbHelper.CreateParameter("@category_id", DbType.Int64, categoryID)
            params(1) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_active_subcategories_by_categoryID(?, ?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim subcategory As Subcategory = New Subcategory
                subcategory.SubcategoryID = dr("subcategory_id")
                subcategory.CategoryID = dr("category_id")
                subcategory.Description = dr("description")
                subcategory.Type = dr("type")
                subcategory.SinkingFund = Convert.ToBoolean(dr("sinking_fund"))
                subcategory.Balance = dr("balance")
                subcategory.Notes = IIf(IsDBNull(dr("notes")), Nothing, dr("notes"))
                subcategory.Active = Convert.ToBoolean(dr("active"))
                subcategory.UserID = dr("user_id")
                subcategoryList.Add(subcategory)
            Next

            If subcategoryList.Count > 0 Then
                Return subcategoryList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getSubcategoryCountByCategoryID(ByRef dTran As DistributedTransaction, ByVal categoryID As Int64) As Integer
        Dim params(0) As DbParameter
        Dim count As Integer = 0
        Try
            params(0) = DbHelper.CreateParameter("@category_id", DbType.Int64, categoryID)
            count = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_get_subcategory_count_by_categoryID(?)", params, dTran)
            Return count
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Public Function getSubcategoryByID(ByRef dTran As DistributedTransaction, ByVal subcategoryID As Int64) As BudgeteerObjects.Subcategory

        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim params(0) As DbParameter
        Dim dr As DataRow

        Try
            params(0) = DbHelper.CreateParameter("@subcategory_ID", DbType.Int64, subcategoryID)
            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_subcategory(?)", params, dTran)

            If dr IsNot DBNull.Value Then
                subcategory.SubcategoryID = dr("subcategory_id")
                subcategory.CategoryID = dr("category_id")
                subcategory.Description = dr("description")
                subcategory.Type = dr("type")
                subcategory.SinkingFund = Convert.ToBoolean(dr("sinking_fund"))
                subcategory.Balance = dr("balance")
                subcategory.Notes = IIf(IsDBNull(dr("notes")), Nothing, dr("notes"))
                subcategory.Active = Convert.ToBoolean(dr("active"))
                subcategory.UserID = dr("user_id")
                Return subcategory
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function addSubcategory(ByRef dTran As DistributedTransaction, ByVal subcategory As BudgeteerObjects.Subcategory) As Int64

        Try
            Dim params(7) As DbParameter

            params(0) = DbHelper.CreateParameter("@category_id", DbType.Int64, subcategory.CategoryID)
            params(1) = DbHelper.CreateParameter("@description", DbType.String, IIf(String.IsNullOrEmpty(Trim(subcategory.Description)), DBNull.Value, Trim(subcategory.Description)))
            params(2) = DbHelper.CreateParameter("@type", DbType.String, IIf(String.IsNullOrEmpty(Trim(subcategory.Type)), DBNull.Value, Trim(subcategory.Type)))
            params(3) = DbHelper.CreateParameter("@sinking_fund", DbType.Boolean, subcategory.SinkingFund)
            params(4) = DbHelper.CreateParameter("@balance", DbType.Decimal, subcategory.Balance)
            params(5) = DbHelper.CreateParameter("@notes", DbType.String, IIf(String.IsNullOrEmpty(Trim(subcategory.Notes)), DBNull.Value, Trim(subcategory.Notes)))
            params(6) = DbHelper.CreateParameter("@active", DbType.Boolean, subcategory.Active)
            params(7) = DbHelper.CreateParameter("@user_id", DbType.String, subcategory.UserID)

            Dim rows_inserted As Int64 = 0

            rows_inserted = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_add_subcategory(?,?,?,?,?,?,?,?)", params, dTran)

            Return rows_inserted

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function updateSubcategory(ByRef dTran As DistributedTransaction, ByVal subcategory As BudgeteerObjects.Subcategory) As Integer

        Try
            Dim params(7) As DbParameter

            params(0) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, subcategory.SubcategoryID)
            params(1) = DbHelper.CreateParameter("@category_id", DbType.Int64, subcategory.CategoryID)
            params(2) = DbHelper.CreateParameter("@description", DbType.String, IIf(String.IsNullOrEmpty(Trim(subcategory.Description)), DBNull.Value, Trim(subcategory.Description)))
            params(3) = DbHelper.CreateParameter("@type", DbType.String, IIf(String.IsNullOrEmpty(Trim(subcategory.Type)), DBNull.Value, Trim(subcategory.Type)))
            params(4) = DbHelper.CreateParameter("@sinking_fund", DbType.Byte, IIf(subcategory.SinkingFund, 1, 0))
            params(5) = DbHelper.CreateParameter("@balance", DbType.Decimal, subcategory.Balance)
            params(6) = DbHelper.CreateParameter("@notes", DbType.String, IIf(String.IsNullOrEmpty(Trim(subcategory.Notes)), DBNull.Value, Trim(subcategory.Notes)))
            params(7) = DbHelper.CreateParameter("@active", DbType.Byte, IIf(subcategory.Active, 1, 0))

            Dim rows_effected As Integer = 0

            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_update_subcategory(?,?,?,?,?,?,?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Public Function deleteSubcategory(ByRef dTran As DistributedTransaction, ByVal subcategoryID As Int64) As Integer

        Try
            Dim params(0) As DbParameter

            params(0) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, subcategoryID)

            Dim rows_effected As Integer = 0

            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_delete_subcategory(?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
