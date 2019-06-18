Imports BudgeteerObjects
Imports System.Data
Imports System.Data.Common

Public Class Transactions

    Public Function getTransactionsByUserIDWithCriteria(ByRef dTran As DistributedTransaction, ByVal userID As String, ByVal criteria As BudgeteerObjects.TransactionFilterCriteria) As List(Of BudgeteerObjects.Transaction)

        Try
            Dim transactionList As List(Of BudgeteerObjects.Transaction) = New List(Of BudgeteerObjects.Transaction)
            Dim params(9) As DbParameter

            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            params(1) = DbHelper.CreateParameter("@year", DbType.Int32, IIf(criteria.TransactionYear = Nothing, DBNull.Value, criteria.TransactionYear))
            params(2) = DbHelper.CreateParameter("@month", DbType.Int32, IIf(criteria.TransactionMonth = Nothing, DBNull.Value, criteria.TransactionMonth))
            params(3) = DbHelper.CreateParameter("@day", DbType.Int32, IIf(criteria.TransactionDay = Nothing, DBNull.Value, criteria.TransactionDay))
            params(4) = DbHelper.CreateParameter("@check_number", DbType.Int32, IIf(criteria.CheckNumber = Nothing, DBNull.Value, criteria.CheckNumber))
            params(5) = DbHelper.CreateParameter("@description", DbType.String, IIf(String.IsNullOrEmpty(Trim(criteria.Description)), DBNull.Value, Trim(criteria.Description)))
            params(6) = DbHelper.CreateParameter("@amount", DbType.Decimal, IIf(criteria.Amount = Nothing, DBNull.Value, criteria.Amount))
            params(7) = DbHelper.CreateParameter("@tax_amount", DbType.Decimal, IIf(criteria.TaxAmount = Nothing, DBNull.Value, criteria.TaxAmount))
            params(8) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, IIf(criteria.SubcategoryID = Nothing, DBNull.Value, criteria.SubcategoryID))
            params(9) = DbHelper.CreateParameter("@account_id", DbType.Int64, IIf(criteria.AccountID = Nothing, DBNull.Value, criteria.AccountID))

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_transactions(?,?,?,?,?,?,?,?,?,?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

                transaction.TransactionID = dr("Transaction_ID")
                transaction.TransactionDate = dr("Date")
                transaction.CheckNumber = IIf(IsDBNull(dr("Check_Number")), Nothing, dr("Check_Number"))
                transaction.Description = dr("Description")
                transaction.Amount = dr("Amount")
                transaction.TaxAmount = dr("Tax_Amount")
                transaction.SubcategoryID = IIf(IsDBNull(dr("Subcategory_ID")), Nothing, dr("Subcategory_ID"))
                transaction.AccountID = IIf(IsDBNull(dr("Account_ID")), Nothing, dr("Account_ID"))
                transaction.UserID = dr("User_ID")
                transactionList.Add(transaction)
            Next

            If transactionList.Count > 0 Then
                Return transactionList
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getTransactionFirstYear(ByRef dTran As DistributedTransaction, ByVal userID As String) As Integer

        Dim row As Object
        Dim firstYear As Integer
        Dim params(0) As DbParameter

        Try
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)
            row = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_get_first_transaction_year(?)", params, dTran)

            firstYear = IIf(IsDBNull(row), Nothing, CType(row, Integer))

            Return firstYear

        Catch ex As Exception

        End Try

    End Function

    Public Function getTransaction(ByRef dTran As DistributedTransaction, ByVal transactionID As Int64) As BudgeteerObjects.Transaction

        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim params(0) As DbParameter
        Dim dr As DataRow

        Try
            params(0) = DbHelper.CreateParameter("@transaction_id", DbType.Int64, transactionID)

            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_transaction(?)", params, dTran)

            If dr IsNot Nothing Then
                transaction.TransactionID = dr("Transaction_ID")
                transaction.TransactionDate = dr("Date")
                transaction.CheckNumber = IIf(IsDBNull(dr("Check_Number")), Nothing, dr("Check_Number"))
                transaction.Description = dr("Description")
                transaction.Amount = dr("Amount")
                transaction.TaxAmount = dr("Tax_Amount")
                transaction.SubcategoryID = IIf(IsDBNull(dr("Subcategory_ID")), Nothing, dr("Subcategory_ID"))
                transaction.AccountID = IIf(IsDBNull(dr("Account_ID")), Nothing, dr("Account_ID"))
                transaction.UserID = dr("User_ID")
                Return transaction
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getTransactionCountBySubcategoryID(ByRef dTran As DistributedTransaction, ByVal subcategoryID As Int64) As Integer

        Dim count As Integer = 0
        Dim params(0) As DbParameter

        Try
            params(0) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, subcategoryID)
            count = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_get_transaction_count_by_subcategoryID(?)", params, dTran)
            Return count
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getTransactionCountByAccountID(ByRef dTran As DistributedTransaction, ByVal accountID As Int64) As Integer

        Dim count As Integer = 0
        Dim params(0) As DbParameter

        Try
            params(0) = DbHelper.CreateParameter("@account_id", DbType.Int64, accountID)
            count = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_get_transaction_count_by_accountID(?)", params, dTran)
            Return count
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function addTransaction(ByRef dTran As DistributedTransaction, ByVal transaction As BudgeteerObjects.Transaction) As Int64

        Try
            Dim params(7) As DbParameter
            params(0) = DbHelper.CreateParameter("@date", DbType.Date, IIf(transaction.TransactionDate Is Nothing, DBNull.Value, transaction.TransactionDate))
            params(1) = DbHelper.CreateParameter("@check_number", DbType.Int32, IIf(transaction.CheckNumber = Nothing, DBNull.Value, transaction.CheckNumber))
            params(2) = DbHelper.CreateParameter("@description", DbType.String, IIf(String.IsNullOrEmpty(Trim(transaction.Description)), DBNull.Value, Trim(transaction.Description)))
            params(3) = DbHelper.CreateParameter("@amount", DbType.Decimal, transaction.Amount)
            params(4) = DbHelper.CreateParameter("@tax_amount", DbType.Decimal, transaction.TaxAmount)
            params(5) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, IIf(transaction.SubcategoryID = Nothing, DBNull.Value, transaction.SubcategoryID))
            params(6) = DbHelper.CreateParameter("@account_id", DbType.Int64, IIf(transaction.AccountID = Nothing, DBNull.Value, transaction.AccountID))
            params(7) = DbHelper.CreateParameter("@user_id", DbType.String, transaction.UserID)

            Dim row_inserted As Int64 = 0
            row_inserted = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_add_transaction(?,?,?,?,?,?,?,?)", params, dTran)

            Return row_inserted

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function updateTransaction(ByRef dTran As DistributedTransaction, ByVal transaction As BudgeteerObjects.Transaction) As Integer

        Try
            Dim params(7) As DbParameter

            params(0) = DbHelper.CreateParameter("@transaction_id", DbType.Int64, transaction.TransactionID)
            params(1) = DbHelper.CreateParameter("@date", DbType.Date, transaction.TransactionDate)
            params(2) = DbHelper.CreateParameter("@check_number", DbType.Int32, IIf(transaction.CheckNumber = Nothing, DBNull.Value, transaction.CheckNumber))
            params(3) = DbHelper.CreateParameter("@description", DbType.String, IIf(String.IsNullOrEmpty(Trim(transaction.Description)), DBNull.Value, Trim(transaction.Description)))
            params(4) = DbHelper.CreateParameter("@amount", DbType.Decimal, transaction.Amount)
            params(5) = DbHelper.CreateParameter("@tax_amount", DbType.Decimal, transaction.TaxAmount)
            params(6) = DbHelper.CreateParameter("@subcategory_id", DbType.Int64, IIf(transaction.SubcategoryID = Nothing, DBNull.Value, transaction.SubcategoryID))
            params(7) = DbHelper.CreateParameter("@account_id", DbType.Int64, IIf(transaction.AccountID = Nothing, DBNull.Value, transaction.AccountID))

            Dim rows_effected As Integer = 0
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_update_transaction(?,?,?,?,?,?,?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function deleteTransaction(ByRef dTran As DistributedTransaction, ByVal transactionID As Int64) As Integer

        Try
            Dim params(0) As DbParameter

            params(0) = DbHelper.CreateParameter("@transaction_id", DbType.Int64, transactionID)

            Dim rows_effected As Integer = 0
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_delete_transaction(?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
