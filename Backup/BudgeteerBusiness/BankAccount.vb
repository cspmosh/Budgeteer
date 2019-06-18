Imports BudgeteerDAL

Public Class BankAccount

    Public Function GetAccounts(ByVal userID As String) As List(Of BudgeteerObjects.BankAccount)

        Dim accounts As List(Of BudgeteerObjects.BankAccount) = New List(Of BudgeteerObjects.BankAccount)
        Dim accountData As BudgeteerDAL.BankAccounts = New BudgeteerDAL.BankAccounts

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                accounts = accountData.getAccountsByUserID(dTran, userID)
                dTran.Commit()
                Return accounts
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetActiveAccounts(ByVal userID As String) As List(Of BudgeteerObjects.BankAccount)

        Dim accounts As List(Of BudgeteerObjects.BankAccount) = New List(Of BudgeteerObjects.BankAccount)
        Dim accountData As BudgeteerDAL.BankAccounts = New BudgeteerDAL.BankAccounts

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                accounts = accountData.getActiveAccountsByUserID(dTran, userID)
                dTran.Commit()
                Return accounts
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetAccount(ByVal accountID As Int64) As BudgeteerObjects.BankAccount

        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim accountData As BudgeteerDAL.BankAccounts = New BudgeteerDAL.BankAccounts

        If accountID = Nothing Then
            Throw New ArgumentException("Account ID cannot be null or 0")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                account = accountData.getAccountByID(dTran, accountID)
                dTran.Commit()
                Return account
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function AddAccount(ByVal account As BudgeteerObjects.BankAccount) As Int64

        Dim result As Int64 = Nothing
        Dim accountData As BudgeteerDAL.BankAccounts = New BudgeteerDAL.BankAccounts
        Dim existingAccounts As List(Of BudgeteerObjects.BankAccount) = New List(Of BudgeteerObjects.BankAccount)

        If account Is Nothing Then
            Throw New ArgumentException("Bank Account object is expected")
        End If

        If account.Number IsNot Nothing Then
            If account.Number.Length() > BudgeteerObjects.BankAccount.MaxFieldLength.Number Then
                Throw New ArgumentException("Bank Account Number cannot exceed " + BudgeteerObjects.BankAccount.MaxFieldLength.Number.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(account.Name)) Then
            Throw New ArgumentException("Bank Account Name is required")
        End If

        If account.Name.Length() > BudgeteerObjects.BankAccount.MaxFieldLength.Name Then
            Throw New ArgumentException("Bank Account Name cannot exceed " + BudgeteerObjects.BankAccount.MaxFieldLength.Name.ToString() + " characters")
        End If

        If account.Type IsNot Nothing Then
            If account.Type.Length() > BudgeteerObjects.BankAccount.MaxFieldLength.Type Then
                Throw New ArgumentException("Bank Account Type cannot exceed " + BudgeteerObjects.BankAccount.MaxFieldLength.Type.ToString() + " characters")
            End If
        End If

        If Trim(account.UserID) = "" Then
            Throw New ArgumentException("Bank Account User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                'Unique Constraints
                existingAccounts = accountData.getAccountsByUserID(dTran, account.UserID)
                If existingAccounts IsNot Nothing Then
                    For Each existingAccount As BudgeteerObjects.BankAccount In existingAccounts
                        If account.Name = existingAccount.Name And account.Number = existingAccount.Number Then
                            Throw New ArgumentException("A Bank Account with that Name and account Number already exist")
                        End If
                    Next
                End If
                result = accountData.addAccount(dTran, account)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Private Function UpdateAccount(ByRef dTran As DistributedTransaction, ByVal account As BudgeteerObjects.BankAccount) As Integer

        Dim result As Integer = Nothing
        Dim accountData As BudgeteerDAL.BankAccounts = New BudgeteerDAL.BankAccounts
        Dim existingAccounts As List(Of BudgeteerObjects.BankAccount) = New List(Of BudgeteerObjects.BankAccount)

        If account Is Nothing Then
            Throw New ArgumentException("Account object cannot be nothing")
        End If

        If account.AccountID = Nothing Then
            Throw New ArgumentException("Account ID cannot be null or 0")
        End If

        If account.Number IsNot Nothing Then
            If account.Number.Length() > BudgeteerObjects.BankAccount.MaxFieldLength.Number Then
                Throw New ArgumentException("Account Number cannot exceed " + BudgeteerObjects.BankAccount.MaxFieldLength.Number.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(account.Name)) Then
            Throw New ArgumentException("Account Name cannot be null or empty")
        End If

        If account.Name.Length() > BudgeteerObjects.BankAccount.MaxFieldLength.Name Then
            Throw New ArgumentException("Account Name length cannot exceed " + BudgeteerObjects.BankAccount.MaxFieldLength.Name.ToString() + " characters")
        End If

        If account.Type IsNot Nothing Then
            If account.Type.Length() > BudgeteerObjects.BankAccount.MaxFieldLength.Type Then
                Throw New ArgumentException("Account Type length cannot exceed " + BudgeteerObjects.BankAccount.MaxFieldLength.Type.ToString() + " characters")
            End If
        End If

        Try
            'Unique Constraints
            existingAccounts = accountData.getAccountsByUserID(dTran, account.UserID)
            For Each existingAccount As BudgeteerObjects.BankAccount In existingAccounts
                If account.AccountID = existingAccount.AccountID Then
                    Continue For
                End If
                If account.Name = existingAccount.Name And account.Number = existingAccount.Number Then
                    Throw New ArgumentException("A Bank Account with that Name and account Number already exist")
                End If
            Next
            result = accountData.updateAccount(dTran, account)
            Return result
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function UpdateAccount(ByVal account As BudgeteerObjects.BankAccount) As Integer

        Dim result As Integer = Nothing

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                result = UpdateAccount(dTran, account)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function DeleteAccount(ByVal accountID As Int64) As Integer

        Dim result As Integer = Nothing
        Dim accountData As BudgeteerDAL.BankAccounts = New BudgeteerDAL.BankAccounts
        Dim transactionData As BudgeteerDAL.Transactions = New BudgeteerDAL.Transactions
        Dim existingTransactions As Integer = 0

        If accountID = Nothing Then
            Throw New ArgumentException("Account ID cannot be null or 0")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Delete Restrict Constraints
                ' Check for Transactions with this accountID
                existingTransactions = transactionData.getTransactionCountByAccountID(dTran, accountID)
                If existingTransactions > 0 Then
                    Throw New ArgumentException("Account is used by existing transactions. If you no longer want to use this account you can mark it as 'Inactive' and it will no longer show up as a choice in drop down boxes.")
                End If
                result = accountData.deleteAccount(dTran, accountID)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Friend Function AdjustBalance(ByRef dTran As DistributedTransaction, ByVal account As BudgeteerObjects.BankAccount, ByVal amount As Decimal) As Integer

        Dim result As Integer = Nothing

        Try
            account.Balance += amount
            result = UpdateAccount(dTran, account)
            Return result
        Catch ex As ArgumentException
            Throw (ex)
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
