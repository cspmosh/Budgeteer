Imports BudgeteerDAL

Public Class Subcategory

    Public Shared Function isSinkingFund(ByVal subcategoryId As Int64) As Boolean
        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                subcategory = subcategoryData.getSubcategoryByID(dTran, subcategoryId)
                If subcategory IsNot Nothing Then
                    If subcategory.SinkingFund Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Throw New ArgumentException("Invalid Subcategory ID")
                End If
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetSubcategories(ByVal UserID As String) As List(Of BudgeteerObjects.Subcategory)

        Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)
        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories

        If String.IsNullOrEmpty(Trim(UserID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                subcategoryList = subcategoryData.getSubcategoriesByUserID(dTran, UserID)
                dTran.Commit()
                Return subcategoryList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetActiveSubcategories(ByVal UserID As String) As List(Of BudgeteerObjects.Subcategory)

        Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)
        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories

        If String.IsNullOrEmpty(Trim(UserID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                subcategoryList = subcategoryData.getActiveSubcategoriesByUserID(dTran, UserID)
                dTran.Commit()
                Return subcategoryList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetSubcategoriesByCategoryID(ByVal categoryID As Int64, ByVal UserID As String) As List(Of BudgeteerObjects.Subcategory)

        Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)
        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories

        If String.IsNullOrEmpty(Trim(UserID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                subcategoryList = subcategoryData.getSubcategoriesByCategoryID(dTran, categoryID, UserID)
                dTran.Commit()
                Return subcategoryList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetActiveSubcategoriesByCategoryID(ByVal categoryID As Int64, ByVal UserID As String) As List(Of BudgeteerObjects.Subcategory)

        Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)
        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories

        If String.IsNullOrEmpty(Trim(UserID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                subcategoryList = subcategoryData.getActiveSubcategoriesByCategoryID(dTran, categoryID, UserID)
                dTran.Commit()
                Return subcategoryList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetSubcategory(ByVal subcategoryID As ULong) As BudgeteerObjects.Subcategory

        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory

        If subcategoryID = Nothing Then
            Throw New ArgumentException("Subcategory ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                subcategory = subcategoryData.getSubcategoryByID(dTran, subcategoryID)
                dTran.Commit()
                Return subcategory
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function AddSubcategory(ByVal newSubcategory As BudgeteerObjects.Subcategory) As Int64

        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
        Dim result As Int64 = Nothing
        Dim uniqueSubcategories As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)

        If newSubcategory Is Nothing Then
            Throw New ArgumentException("Subcategory cannot be null")
        End If

        If newSubcategory.CategoryID = Nothing Then
            Throw New ArgumentException("Category ID is required")
        End If

        If String.IsNullOrEmpty(Trim(newSubcategory.Description)) Then
            Throw New ArgumentException("Description is required")
        Else
            If newSubcategory.Description.Length() > BudgeteerObjects.Subcategory.MaxFieldLength.Description Then
                Throw New ArgumentException("Subcategory Description cannot exceed " + BudgeteerObjects.Subcategory.MaxFieldLength.Description.ToString() + " characters")
            End If
        End If

        'Validate arguments
        If newSubcategory.Type <> "Expense" And newSubcategory.Type <> "Income" Then
            Throw New ArgumentException("Subcategory Type must be either 'Expense' or 'Income'")
        End If

        If newSubcategory.Type <> "Expense" And newSubcategory.SinkingFund = True Then
            Throw New ArgumentException("Only expense-based subcategories can be marked as a sinking fund")
        End If

        If newSubcategory.Notes IsNot Nothing Then
            If newSubcategory.Notes.Length() > BudgeteerObjects.Subcategory.MaxFieldLength.Notes Then
                Throw New ArgumentException("Subcategory Notes cannot exceed " + BudgeteerObjects.Subcategory.MaxFieldLength.Notes.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(newSubcategory.UserID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Unique Record Constraints
                uniqueSubcategories = subcategoryData.getSubcategoriesByCategoryID(dTran, newSubcategory.CategoryID, newSubcategory.UserID)
                If uniqueSubcategories IsNot Nothing Then
                    For Each uniqueSubcategory As BudgeteerObjects.Subcategory In uniqueSubcategories
                        If String.Compare(newSubcategory.Description, uniqueSubcategory.Description, True) = 0 Then
                            ' Subcategory is not unique- throw exception
                            Throw New ArgumentException("Subcategory Is Not Unique")
                        End If
                    Next
                End If
                result = subcategoryData.addSubcategory(dTran, newSubcategory)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Private Function UpdateSubcategory(ByRef dTran As DistributedTransaction, ByVal subcategory As BudgeteerObjects.Subcategory) As Integer

        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
        Dim subcategoryBusiness As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim result As Integer = Nothing
        Dim uniqueSubcategories As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)
        Dim oldSubcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim budgetData As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets
        Dim currentBudget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        Dim utilizedDollars As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim rolloverBudget As Decimal = 0.0

        ' Required Field Constraints
        If subcategory Is Nothing Then
            Throw New ArgumentException("Subcategory cannot be null")
        End If

        If subcategory.SubcategoryID = Nothing Then
            Throw New ArgumentException("Subcategory ID is required")
        End If

        If subcategory.CategoryID = Nothing Then
            Throw New ArgumentException("Category ID is required")
        End If

        If String.IsNullOrEmpty(Trim(subcategory.Description)) Then
            Throw New ArgumentException("Description is required")
        Else
            If subcategory.Description.Length() > BudgeteerObjects.Subcategory.MaxFieldLength.Description Then
                Throw New ArgumentException("Subcategory Description cannot exceed " + BudgeteerObjects.Subcategory.MaxFieldLength.Description.ToString() + " characters")
            End If
        End If

        'Validate arguments
        If subcategory.Type <> "Expense" And subcategory.Type <> "Income" Then
            Throw New ArgumentException("Subcategory Type must be either 'Expense' or 'Income'")
        End If

        If subcategory.Type <> "Expense" And subcategory.SinkingFund = True Then
            Throw New ArgumentException("Only expense-based subcategories can be marked as a sinking fund")
        End If

        If subcategory.Notes IsNot Nothing Then
            If subcategory.Notes.Length() > BudgeteerObjects.Subcategory.MaxFieldLength.Notes Then
                Throw New ArgumentException("Subcategory Notes cannot exceed " + BudgeteerObjects.Subcategory.MaxFieldLength.Notes.ToString() + " characters")
            End If
        End If

        Try
            ' Unique Record Constraints
            uniqueSubcategories = subcategoryData.getSubcategoriesByCategoryID(dTran, subcategory.CategoryID, subcategory.UserID)
            If uniqueSubcategories IsNot Nothing Then
                For Each uniqueSubcategory As BudgeteerObjects.Subcategory In uniqueSubcategories
                    If uniqueSubcategory.SubcategoryID = subcategory.SubcategoryID Then
                        ' Skip the current subcategory
                        Continue For
                    End If
                    If String.Compare(subcategory.Description, uniqueSubcategory.Description, True) = 0 Then
                        ' Subcategory is not unique- throw exception
                        Throw New ArgumentException("Subcategory Is Not Unique")
                    End If
                Next
            End If
            ' Get the pre-updated subcategory
            oldSubcategory = subcategoryData.getSubcategoryByID(dTran, subcategory.SubcategoryID)
            ' Update the subcategory
            result = subcategoryData.updateSubcategory(dTran, subcategory)
            If result = 1 Then ' Successful Update
                ' If a current budget exists with this subcategory and the field changed was sinking fund
                ' then make the necessary subcategory balance adjustment
                If oldSubcategory.SinkingFund <> subcategory.SinkingFund Then
                    currentBudget = budgetData.getCurrentBudgetBySubcategoryID(dTran, subcategory.SubcategoryID)
                    If currentBudget IsNot Nothing Then
                        'Get the utilized dollars for the current month
                        utilizedDollars = budgetData.getUtilizedDollarsBySubcategoryID(dTran, subcategory.UserID, currentBudget.StartDate, subcategory.SubcategoryID)
                        If utilizedDollars IsNot Nothing Then
                            rolloverBudget = currentBudget.Amount + utilizedDollars(0).Amount
                        Else
                            rolloverBudget = currentBudget.Amount
                        End If
                        If subcategory.SinkingFund Then
                            subcategoryBusiness.AdjustSubcategoryBalance(dTran, subcategory, rolloverBudget)
                        Else
                            subcategoryBusiness.AdjustSubcategoryBalance(dTran, subcategory, -rolloverBudget)
                        End If
                    End If
                End If
            End If
            Return result
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Function UpdateSubcategory(ByVal subcategory As BudgeteerObjects.Subcategory) As Integer

        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
        Dim result As Integer = Nothing

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                result = updateSubcategory(dTran, subcategory)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function DeleteSubcategory(ByVal subcategoryID As ULong) As Integer

        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
        Dim budgetData As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets
        Dim transactionData As BudgeteerDAL.Transactions = New BudgeteerDAL.Transactions
        Dim result As Integer = Nothing
        Dim existingBudgets As Integer = 0
        Dim existingTransactions As Integer = 0

        If subcategoryID = Nothing Then
            Throw New ArgumentException("Subcategory ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Delete Restrict Constraints
                ' Check for Budgets with this subcategoryID
                existingBudgets = budgetData.getBudgetCountBySubcategoryID(dTran, subcategoryID)
                If existingBudgets > 0 Then
                    Throw New ArgumentException("Subcategory is used by existing budgets. If you no longer want to use this subcategory you can mark it as 'Inactive' and it will no longer show up as a choice in drop down boxes.")
                End If
                ' Check for Transactions with this subcategoryID
                existingTransactions = transactionData.getTransactionCountBySubcategoryID(dTran, subcategoryID)
                If existingTransactions > 0 Then
                    Throw New ArgumentException("Subcategory is used by existing transactions. If you no longer want to use this subcategory you can mark it as 'Inactive' and it will no longer show up as a choice in drop down boxes.")
                End If
                result = subcategoryData.deleteSubcategory(dTran, subcategoryID)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Friend Function AdjustSubcategoryBalance(ByRef dTran As DistributedTransaction, ByVal subcategory As BudgeteerObjects.Subcategory, ByVal amount As Decimal) As Integer

        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
        Dim result As Integer = Nothing

        If subcategory Is Nothing Then
            Throw New ArgumentException("Subcategory cannot be nothing")
        End If

        Try
            subcategory.Balance += amount
            result = updateSubcategory(dTran, subcategory)
            Return result
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
