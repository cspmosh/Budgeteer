Imports BudgeteerObjects
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class BankAccounts

    Public Function getAccountsByUserID(ByRef dTran As DistributedTransaction, ByVal userID As String) As List(Of BankAccount)

        Try
            Dim accounts As List(Of BankAccount) = New List(Of BankAccount)

            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_accounts(?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim acc As BankAccount = New BankAccount
                acc.AccountID = dr("Account_ID")
                acc.Number = IIf(IsDBNull(dr("Number")), Nothing, dr("Number"))
                acc.Name = dr("Name")
                acc.Type = IIf(IsDBNull(dr("Type")), Nothing, dr("Type"))
                acc.Balance = dr("Balance")
                acc.Active = Convert.ToBoolean(dr("Active"))
                acc.UserID = dr("User_ID")
                accounts.Add(acc)
            Next

            If accounts.Count > 0 Then
                Return accounts
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getActiveAccountsByUserID(ByRef dTran As DistributedTransaction, ByVal userID As String) As List(Of BankAccount)

        Try
            Dim accounts As List(Of BankAccount) = New List(Of BankAccount)

            Dim params(0) As DbParameter
            params(0) = DbHelper.CreateParameter("@user_id", DbType.String, userID)

            Dim ds As DataSet = New DataSet
            ds = DbHelper.ExecuteDataSet(CommandType.StoredProcedure, "call bsp_get_active_accounts(?)", params, dTran)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim acc As BankAccount = New BankAccount
                acc.AccountID = dr("Account_ID")
                acc.Number = IIf(IsDBNull(dr("Number")), Nothing, dr("Number"))
                acc.Name = dr("Name")
                acc.Type = IIf(IsDBNull(dr("Type")), Nothing, dr("Type"))
                acc.Balance = dr("Balance")
                acc.Active = Convert.ToBoolean(dr("Active"))
                acc.UserID = dr("User_ID")
                accounts.Add(acc)
            Next

            If accounts.Count > 0 Then
                Return accounts
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function getAccountByID(ByRef dTran As DistributedTransaction, ByVal accountID As Int64) As BudgeteerObjects.BankAccount

        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim params(0) As DbParameter
        Dim dr As DataRow

        Try
            params(0) = DbHelper.CreateParameter("@account_ID", DbType.Int64, accountID)

            dr = DbHelper.ExecuteRow(CommandType.StoredProcedure, "call bsp_get_account(?)", params, dTran)

            If dr IsNot Nothing Then
                account.AccountID = dr("Account_ID")
                account.Number = IIf(IsDBNull(dr("Number")), Nothing, dr("Number"))
                account.Name = dr("Name")
                account.Type = IIf(IsDBNull(dr("Type")), Nothing, dr("Type"))
                account.Balance = dr("Balance")
                account.Active = Convert.ToBoolean(dr("Active"))
                account.UserID = dr("User_ID")
                Return account
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function addAccount(ByRef dTran As DistributedTransaction, ByVal account As BankAccount) As Int64

        Try
            Dim params(4) As DbParameter

            params(0) = DbHelper.CreateParameter("@number", DbType.String, IIf(String.IsNullOrEmpty(account.Number), DBNull.Value, account.Number))
            params(1) = DbHelper.CreateParameter("@name", DbType.String, IIf(String.IsNullOrEmpty(account.Name), DBNull.Value, account.Name))
            params(2) = DbHelper.CreateParameter("@type", DbType.String, IIf(String.IsNullOrEmpty(account.Type), DBNull.Value, account.Type))
            params(3) = DbHelper.CreateParameter("@balance", DbType.Decimal, account.Balance)
            params(4) = DbHelper.CreateParameter("@user_id", DbType.String, account.UserID)

            Dim rows_effected As Int64 = 0
            rows_effected = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "call bsp_add_account(?,?,?,?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function updateAccount(ByRef dTran As DistributedTransaction, ByVal account As BankAccount) As Integer

        Try
            Dim rows_effected As Integer
            Dim params(5) As DbParameter

            params(0) = DbHelper.CreateParameter("@account_id", DbType.Int64, IIf(account.AccountID = Nothing, DBNull.Value, account.AccountID))
            params(1) = DbHelper.CreateParameter("@number", DbType.String, IIf(String.IsNullOrEmpty(account.Number), DBNull.Value, account.Number))
            params(2) = DbHelper.CreateParameter("@name", DbType.String, IIf(String.IsNullOrEmpty(account.Name), DBNull.Value, account.Name))
            params(3) = DbHelper.CreateParameter("@type", DbType.String, IIf(String.IsNullOrEmpty(account.Type), DBNull.Value, account.Type))
            params(4) = DbHelper.CreateParameter("@balance", DbType.Decimal, account.Balance)
            params(5) = DbHelper.CreateParameter("@active", DbType.Boolean, account.Active)

            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_update_account(?,?,?,?,?,?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function deleteAccount(ByRef dTran As DistributedTransaction, ByVal accountID As Int64) As Integer

        Try
            Dim params(0) As DbParameter

            params(0) = DbHelper.CreateParameter("@account_id", DbType.Int64, accountID)

            Dim rows_effected As Integer
            rows_effected = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "call bsp_delete_account(?)", params, dTran)

            Return rows_effected

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
