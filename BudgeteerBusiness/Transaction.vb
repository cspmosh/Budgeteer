Imports BudgeteerDAL

Public Class Transaction

    Public Enum TransactionErrorType
        TransactionSelectFailure = -1
        TransactionInsertFailure = -2
        TransactionUpdateFailure = -3
        TransactionDeleteFailure = -4
    End Enum

    Public Function getTransactionFirstYear(ByVal userID As String) As Integer

        Dim firstYear As Integer = Nothing
        Dim transactionData As BudgeteerDAL.Transactions = New BudgeteerDAL.Transactions

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                firstYear = transactionData.getTransactionFirstYear(dTran, userID)
                dTran.Commit()
                Return firstYear
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function getTransactionsWithCriteria(ByVal userID As String, ByVal criteria As BudgeteerObjects.TransactionFilterCriteria) As List(Of BudgeteerObjects.Transaction)

        Dim transactionList As List(Of BudgeteerObjects.Transaction) = New List(Of BudgeteerObjects.Transaction)
        Dim transactionData As BudgeteerDAL.Transactions = New BudgeteerDAL.Transactions

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        If criteria Is Nothing Then
            Throw New ArgumentException("Instantiated Transaction object expected")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                transactionList = transactionData.getTransactionsByUserIDWithCriteria(dTran, userID, criteria)
                dTran.Commit()
                Return transactionList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function getTransactions(ByVal userID As String) As List(Of BudgeteerObjects.Transaction)

        Dim transactionList As List(Of BudgeteerObjects.Transaction) = New List(Of BudgeteerObjects.Transaction)
        Dim transactionData As BudgeteerDAL.Transactions = New BudgeteerDAL.Transactions
        Dim emptySearchCriteria As BudgeteerObjects.TransactionFilterCriteria = New BudgeteerObjects.TransactionFilterCriteria

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                transactionList = transactionData.getTransactionsByUserIDWithCriteria(dTran, userID, emptySearchCriteria)
                dTran.Commit()
                Return transactionList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function getTransaction(ByVal transactionID As Int64) As BudgeteerObjects.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim transactionData As BudgeteerDAL.Transactions = New BudgeteerDAL.Transactions

        If transactionID = Nothing Then
            Throw New ArgumentException("Transaction ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                transaction = transactionData.getTransaction(dTran, transactionID)
                dTran.Commit()
                Return transaction
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function addTransaction(ByVal transaction As BudgeteerObjects.Transaction) As Int64

        Dim transactionData As BudgeteerDAL.Transactions = New BudgeteerDAL.Transactions
        Dim accountBusiness As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim subcategoryBusiness As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim row As Int64 = Nothing
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory

        If transaction Is Nothing Then
            Throw New ArgumentException("Invalid reference to a transaction")
        End If

        If transaction.TransactionDate Is Nothing Then
            Throw New ArgumentException("Transaction Date is required")
        End If

        If String.IsNullOrEmpty(Trim(transaction.Description)) Then
            Throw New ArgumentException("Transaction Description is required")
        Else
            If transaction.Description.Length() > BudgeteerObjects.Transaction.MaxFieldLength.Description Then
                Throw New ArgumentException("Description cannot exceed " + BudgeteerObjects.Transaction.MaxFieldLength.Description.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(transaction.UserID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Insert the new transaction
                row = transactionData.addTransaction(dTran, transaction)
                If row > 0 Then 'Successful Insert
                    ' If transaction was tied to an account, adjust the account balance automatically
                    If (transaction.AccountID <> 0) Then
                        account = accountBusiness.GetAccount(transaction.AccountID)
                        accountBusiness.AdjustBalance(dTran, account, transaction.Amount)
                    End If
                    ' If transaction was tied to a sinking fund subcategory, adjust the balance automatically
                    If (transaction.SubcategoryID <> 0) Then
                        subcategory = subcategoryBusiness.GetSubcategory(transaction.SubcategoryID)
                        ' Only adjust subcategory balance if subcategory is a sinking fund
                        If subcategory.SinkingFund = True Then
                            subcategoryBusiness.AdjustSubcategoryBalance(dTran, subcategory, transaction.Amount)
                        End If
                    End If
                    dTran.Commit()
                    Return row
                Else
                    Return TransactionErrorType.TransactionInsertFailure
                End If
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function updateTransaction(ByVal newTransaction As BudgeteerObjects.Transaction) As Integer

        Dim transactionData As BudgeteerDAL.Transactions = New BudgeteerDAL.Transactions
        Dim oldTransaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim accountBusiness As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim subcategoryBusiness As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim result As Integer = Nothing
        Dim currentAccount As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim newAccount As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim currentSubcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim newSubcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory

        If newTransaction Is Nothing Then
            Throw New ArgumentException("Invalid reference to a transaction")
        End If

        If newTransaction.TransactionID = Nothing Then
            Throw New ArgumentException("Transaction ID cannot be null")
        End If

        If newTransaction.TransactionDate Is Nothing Then
            Throw New ArgumentException("Transaction Date cannot be null")
        End If

        If String.IsNullOrEmpty(Trim(newTransaction.Description)) Then
            Throw New ArgumentException("Transaction Description is required")
        Else
            If newTransaction.Description.Length() > BudgeteerObjects.Transaction.MaxFieldLength.Description Then
                Throw New ArgumentException("Description cannot exceed " + BudgeteerObjects.Transaction.MaxFieldLength.Description.ToString() + " characters")
            End If
        End If

        If newTransaction.UserID = Nothing Then
            Throw New ArgumentException("User ID cannot be null")
        End If

        ' Start the distributed Transaction and open DB connection
        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)

            Try
                ' Retrieve current transaction data (before update)
                oldTransaction = transactionData.getTransaction(dTran, newTransaction.TransactionID)
                If oldTransaction Is Nothing Then
                    ' Couldn't retrieve the current Transaction from the database
                    Return TransactionErrorType.TransactionSelectFailure
                End If

                If oldTransaction.UserID.ToUpper() <> newTransaction.UserID.ToUpper() Then
                    ' The UserID of the updated transaction does not match the userID of the current transaction
                    Return TransactionErrorType.TransactionSelectFailure
                End If

                result = transactionData.updateTransaction(dTran, newTransaction)

                If result = 1 Then 'Update Successful
                    ' If transaction amount, accound ID, or subcategory ID changed, recalculate account balance
                    If (oldTransaction.Amount <> newTransaction.Amount) Or _
                       (oldTransaction.AccountID <> newTransaction.AccountID) Or _
                       (oldTransaction.SubcategoryID <> newTransaction.SubcategoryID) Then
                        ' If current transaction was tied to an account, adjust the account balance automatically
                        If (oldTransaction.AccountID <> 0) Then
                            currentAccount = accountBusiness.GetAccount(oldTransaction.AccountID)
                            'Reverse the amount from the current account
                            accountBusiness.AdjustBalance(dTran, currentAccount, -oldTransaction.Amount)
                        End If
                        ' If new transaction was tied to an account, adjust the account balance automatically
                        If (newTransaction.AccountID <> 0) Then
                            If oldTransaction.AccountID = newTransaction.AccountID Then
                                newAccount = currentAccount
                            Else
                                newAccount = accountBusiness.GetAccount(newTransaction.AccountID)
                            End If
                            'Adjust the amount for the new account
                            accountBusiness.AdjustBalance(dTran, newAccount, newTransaction.Amount)
                        End If
                        ' If current transaction was tied to a sinking fund subcategory, adjust the balance automatically
                        If (oldTransaction.SubcategoryID <> 0) Then
                            currentSubcategory = subcategoryBusiness.GetSubcategory(oldTransaction.SubcategoryID)
                            If currentSubcategory.SinkingFund = True Then
                                'Reverse the previous transaction amount from the current subcategory
                                subcategoryBusiness.AdjustSubcategoryBalance(dTran, currentSubcategory, -oldTransaction.Amount)
                            End If
                        End If
                        ' If new transaction was tied to a sinking fund subcategory, adjust the balance automatically
                        If (newTransaction.SubcategoryID <> 0) Then
                            If oldTransaction.SubcategoryID = newTransaction.SubcategoryID Then
                                newSubcategory = currentSubcategory
                            Else
                                newSubcategory = subcategoryBusiness.GetSubcategory(newTransaction.SubcategoryID)
                            End If
                            If newSubcategory.SinkingFund = True Then
                                subcategoryBusiness.AdjustSubcategoryBalance(dTran, newSubcategory, newTransaction.Amount)
                            End If
                        End If
                    End If
                Else
                    ' No new data
                    Return TransactionErrorType.TransactionUpdateFailure
                End If

                ' Save all the changes
                dTran.Commit()
                Return result

            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using
    End Function

    Public Function deleteTransaction(ByVal transactionID As ULong) As Integer

        Dim transactionData As BudgeteerDAL.Transactions = New BudgeteerDAL.Transactions
        Dim result As Integer = Nothing
        Dim accountBusiness As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim subcategoryBusiness As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        If transactionID = Nothing Then
            Throw New ArgumentException("Transaction ID cannot be null")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                transaction = transactionData.getTransaction(dTran, transactionID)
                If transaction Is Nothing Then
                    Return TransactionErrorType.TransactionSelectFailure
                End If
                ' Delete the new transaction
                result = transactionData.deleteTransaction(dTran, transactionID)
                If result = 1 Then 'Successful Delete
                    ' If transaction was tied to an account, adjust the account balance automatically
                    If (transaction.AccountID <> 0) Then
                        account = accountBusiness.GetAccount(transaction.AccountID)
                        'Reverse the amount from the account balance
                        accountBusiness.AdjustBalance(dTran, account, -transaction.Amount)
                    End If
                    ' If transaction was tied to a sinking fund subcategory, adjust the balance automatically
                    If (transaction.SubcategoryID <> 0) Then
                        subcategory = subcategoryBusiness.GetSubcategory(transaction.SubcategoryID)
                        ' Only adjust subcategory balance if subcategory is a sinking fund
                        If subcategory.SinkingFund = True Then
                            'Reverse the amount from the subcategory balance
                            subcategoryBusiness.AdjustSubcategoryBalance(dTran, subcategory, -transaction.Amount)
                        End If
                    End If
                Else
                    Return TransactionErrorType.TransactionDeleteFailure
                End If
                ' Save the changes
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try

        End Using
    End Function

End Class
