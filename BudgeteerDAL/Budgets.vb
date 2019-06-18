Imports BudgeteerObjects
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class Budgets

    Public Function getBudget(ByRef dTran As DistributedTransaction, ByVal budgetID As Int64) As BudgeteerObjects.Budget

        Try
            Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
            Dim dr As DataRow
            Dim params(0) As DbParameter

            params(0) = DbHelper.CreateParameter("@budget_id", DbType.Int64, budgetID)

            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_budget(?)", params, dTran)

            If dr IsNot Nothing Then
                budget.BudgetID = dr("Budget_ID")
                budget.StartDate = dr("start_date")
                budget.SubcategoryID = dr("subcategory_id")
                budget.Amount = dr("amount")
                budget.UserID = dr("user_id")
                Return budget
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getBudgetCountBySubcategoryID(ByRef dTran As DistributedTransaction, ByVal subcategoryID As Int64) As Integer

        Try
            Dim count As Integer = 0
            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, subcategoryID)
            count = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_get_budget_count_by_subcategoryID(?)", params, dTran)
            Return count
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getCurrentBudgetBySubcategoryID(ByRef dTran As DistributedTransaction, ByVal subcategoryID As Int64) As BudgeteerObjects.Budget

        Try
            Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
            Dim dr As DataRow
            Dim params(0) As DbParameter

            params(0) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, subcategoryID)

            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_current_budget(?)", params, dTran)

            If dr IsNot Nothing Then
                budget.BudgetID = dr("Budget_ID")
                budget.StartDate = dr("start_date")
                budget.SubcategoryID = dr("subcategory_id")
                budget.Amount = dr("amount")
                budget.UserID = dr("user_id")
                Return budget
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getBudgetsByUserIDWithCriteria(ByRef dTran As DistributedTransaction, ByVal userID As String, ByVal startDate As Date?, ByVal subcategoryType As String) As List(Of BudgeteerObjects.Budget)

        Try
            Dim budgetList As List(Of BudgeteerObjects.Budget) = New List(Of BudgeteerObjects.Budget)

            Dim params(2) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            params(1) = DbHelper.CreateParameter("@start_date", DbType.Date, IIf(startDate Is Nothing, DBNull.Value, startDate))
            params(2) = DbHelper.CreateParameter("@type", DbType.String, IIf(String.IsNullOrEmpty(Trim(subcategoryType)), DBNull.Value, Trim(subcategoryType)))

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_budgets(?,?,?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
                budget.BudgetID = dr("budget_id")
                budget.StartDate = dr("start_date")
                budget.SubcategoryID = dr("subcategory_id")
                budget.Amount = dr("amount")
                budget.UserID = dr("user_id")
                budgetList.Add(budget)
            Next

            If budgetList.Count > 0 Then
                Return budgetList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getBudgetFirstYear(ByRef dTran As DistributedTransaction, ByVal userID As String) As Integer

        Dim row As Object
        Dim firstYear As Integer
        Dim params(0) As DbParameter

        Try
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            row = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_get_first_budget_year(?)", params, dTran)

            firstYear = IIf(IsDBNull(row), Nothing, CType(row, Integer))

            Return firstYear

        Catch ex As Exception

        End Try

    End Function

    Public Function addBudget(ByRef dTran As DistributedTransaction, ByVal budget As BudgeteerObjects.Budget) As Int64

        Try

            Dim Params(3) As DbParameter
            Params(0) = DbHelper.CreateParameter("@start_date", DbType.Date, budget.StartDate)
            Params(1) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, budget.SubcategoryID)
            Params(2) = DbHelper.CreateParameter("@amount", DbType.Decimal, budget.Amount)
            Params(3) = DbHelper.CreateParameter("@user_id", DbType.String, budget.UserID)

            Dim row As Int64 = 0
            row = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_add_budget(?,?,?,?)", Params, dTran)

            Return row

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function updateBudget(ByRef dTran As DistributedTransaction, ByVal budget As BudgeteerObjects.Budget) As Integer

        Try
            Dim params(3) As DbParameter
            params(0) = DbHelper.CreateParameter("@budget_id", DbType.Int64, budget.BudgetID)
            params(1) = DbHelper.CreateParameter("@start_date", DbType.Date, budget.StartDate)
            params(2) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, budget.SubcategoryID)
            params(3) = DbHelper.CreateParameter("@amount", DbType.Decimal, budget.Amount)

            Dim rows_effected As Integer = 0
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_update_budget(?,?,?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function deleteBudget(ByRef dTran As DistributedTransaction, ByVal budgetID As Int64) As Integer

        Try
            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@budget_id", DbType.Int64, budgetID)

            Dim rows_effected As Integer = 0
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_delete_budget(?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getUtilizedDollarsByUserIDWithCriteria(ByRef dTran As DistributedTransaction, ByVal userID As String, ByVal budgetDate As Date?, ByVal subcategoryType As String) As List(Of BudgeteerObjects.UtilizedDollars)

        Try
            Dim utilizedDollarsList As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
            Dim params(2) As DbParameter
            Dim subcategoryDAL As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
            Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory

            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            params(1) = DbHelper.CreateParameter("@date", DbType.Date, IIf(budgetDate Is Nothing, DBNull.Value, budgetDate))
            params(2) = DbHelper.CreateParameter("@type", DbType.String, IIf(String.IsNullOrEmpty(Trim(subcategoryType)), DBNull.Value, Trim(subcategoryType)))

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_dollars_utilized(?,?,?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim dollars As BudgeteerObjects.UtilizedDollars = New BudgeteerObjects.UtilizedDollars
                dollars.BudgetDate = dr("start_date")
                dollars.CategoryID = dr("category_id")
                dollars.SubcategoryID = dr("subcategory_id")
                dollars.Amount = IIf(IsDBNull(dr("amount")), Nothing, dr("amount"))
                dollars.Budget = IIf(IsDBNull(dr("budget")), Nothing, dr("budget"))
                If subcategoryType = "Expense" Then
                    subcategory = subcategoryDAL.getSubcategoryByID(dTran, dollars.SubcategoryID)
                    ' By definition, sinking fund subcategories carry a balance from month to month so 
                    ' there shouldn't be any available dollars for these subcategories
                    If subcategory.SinkingFund Then
                        dollars.Available = Nothing '0
                    Else
                        ' Dollars available are dollars left after subtracting the used dollars from the budgeted dollars
                        dollars.Available = dollars.Budget + dollars.Amount
                    End If
                Else
                    ' Dollars available are the only dollars recorded as income for this month
                    dollars.Available = dollars.Amount
                End If
                utilizedDollarsList.Add(dollars)
            Next

            If utilizedDollarsList.Count > 0 Then
                Return utilizedDollarsList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getUtilizedDollarsBySubcategoryID(ByRef dTran As DistributedTransaction, ByVal userID As String, ByVal budgetDate As Date?, ByVal subcategoryID As Int64) As List(Of BudgeteerObjects.UtilizedDollars)

        Try
            Dim utilizedDollarsList As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
            Dim params(2) As DbParameter
            Dim subcategoryDAL As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
            Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory

            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            params(1) = DbHelper.CreateParameter("@date", DbType.Date, IIf(budgetDate Is Nothing, DBNull.Value, budgetDate))
            params(2) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, subcategoryID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_dollars_utilized_by_subcategoryID(?,?,?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim dollars As BudgeteerObjects.UtilizedDollars = New BudgeteerObjects.UtilizedDollars
                dollars.BudgetDate = dr("start_date")
                dollars.CategoryID = dr("category_id")
                dollars.SubcategoryID = dr("subcategory_id")
                dollars.Amount = IIf(IsDBNull(dr("amount")), Nothing, dr("amount"))
                dollars.Budget = IIf(IsDBNull(dr("budget")), Nothing, dr("budget"))
                dollars.Available = Nothing ' This field isn't used in the return of this function
                utilizedDollarsList.Add(dollars)
            Next

            If utilizedDollarsList.Count > 0 Then
                Return utilizedDollarsList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
