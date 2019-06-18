Imports BudgeteerDAL

Public Class Budget

    Public Enum BudgetErrorType
        BudgetSelectFailure = -1
        BudgetInsertFailure = -2
        BudgetUpdateFailure = -3
        BudgetDeleteFailure = -4
    End Enum

    Public Function GetBudgets(ByVal userID As String) As List(Of BudgeteerObjects.Budget)

        Dim budgetList As List(Of BudgeteerObjects.Budget) = New List(Of BudgeteerObjects.Budget)
        Dim data As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be blank")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                budgetList = data.getBudgetsByUserIDWithCriteria(dTran, userID, Nothing, Nothing)
                dTran.Commit()
                Return budgetList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetBudget(ByVal budgetID As Int64) As BudgeteerObjects.Budget

        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        Dim data As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets

        If budgetID = Nothing Then
            Throw New ArgumentException("Budget ID cannot be null")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                budget = data.getBudget(dTran, budgetID)
                dTran.Commit()
                Return budget
            Catch ex As Exception
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetBudgetsWithCriteria(ByVal userID As String, ByVal startDate As Date?, ByVal subcategoryType As String) As List(Of BudgeteerObjects.Budget)

        Dim budgetList As List(Of BudgeteerObjects.Budget) = New List(Of BudgeteerObjects.Budget)
        Dim data As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be blank")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                budgetList = data.getBudgetsByUserIDWithCriteria(dTran, userID, startDate, subcategoryType)
                dTran.Commit()
                Return budgetList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetBudgetFirstYear(ByVal userID As String) As Integer
        Dim data As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets
        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be blank")
        End If
        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                Return data.getBudgetFirstYear(dTran, userID)
                dTran.Commit()
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using
    End Function

    Public Function AddBudget(ByRef dTran As DistributedTransaction, ByVal budget As BudgeteerObjects.Budget) As Int64

        Dim data As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets
        Dim existingBudgets As List(Of BudgeteerObjects.Budget) = New List(Of BudgeteerObjects.Budget)
        Dim result As Int64 = Nothing
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim subcategoryBusiness As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim utilizedDollars As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim rolloverBudget As Decimal = 0.0

        If budget Is Nothing Then
            Throw New ArgumentException("Budget Object cannot be nothing")
        End If

        If budget.StartDate Is Nothing Then
            Throw New ArgumentException("Budget Start Date cannot be null")
        End If

        If budget.SubcategoryID = Nothing Then
            Throw New ArgumentException("Budget Subcategory cannot be empty")
        End If

        Try
            ' Unique Constraints
            existingBudgets = data.getBudgetsByUserIDWithCriteria(dTran, budget.UserID, budget.StartDate, Nothing)
            If existingBudgets IsNot Nothing Then
                For Each existingBudget As BudgeteerObjects.Budget In existingBudgets
                    If budget.SubcategoryID = existingBudget.SubcategoryID Then
                        Throw New ArgumentException("A Budget with this Subcategory already exists for this month")
                    End If
                Next
            End If
            result = data.addBudget(dTran, budget)
            If result > 0 Then
                ' Successful Insert
                ' If budget was tied to a sinking fund subcategory, adjust the balance automatically
                If (budget.SubcategoryID <> 0) Then
                    subcategory = subcategoryBusiness.GetSubcategory(budget.SubcategoryID)
                    ' Only adjust subcategory balance if subcategory is a sinking fund
                    If subcategory.SinkingFund = True Then
                        ' Get difference between budget amount and utilized dollars and adjust the SF balance
                        utilizedDollars = data.getUtilizedDollarsBySubcategoryID(dTran, budget.UserID, budget.StartDate, budget.SubcategoryID)
                        If utilizedDollars IsNot Nothing Then
                            rolloverBudget = budget.Amount + utilizedDollars(0).Amount
                        End If
                        subcategoryBusiness.AdjustSubcategoryBalance(dTran, subcategory, rolloverBudget)
                    End If
                End If
                Return result
            Else
                Throw New ApplicationException("An error occured while trying to add a budget")
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
        

    End Function

    Public Function AddBudget(ByVal budget As BudgeteerObjects.Budget) As Int64
        Dim result As Int64

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                result = AddBudget(dTran, budget)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function UpdateBudget(ByVal newBudget As BudgeteerObjects.Budget) As Integer

        Dim data As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets
        Dim result As Integer = Nothing
        Dim existingBudgets As List(Of BudgeteerObjects.Budget) = New List(Of BudgeteerObjects.Budget)
        Dim oldBudget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        Dim currentSubcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim newSubcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim subcategoryBusiness As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim utilizedDollars As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim rolloverBudget As Decimal = 0.0

        If newBudget Is Nothing Then
            Throw New ArgumentException("Budget Object cannot be nothing")
        End If

        If newBudget.BudgetID = Nothing Then
            Throw New ArgumentException("Budget ID cannot be null or 0")
        End If

        If newBudget.SubcategoryID = Nothing Then
            Throw New ArgumentException("Budget Subcategory ID cannot be null or 0")
        End If

        If newBudget.StartDate Is Nothing Then
            Throw New ArgumentException("Budget Start Date cannot be nothing")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Unique Constraints
                existingBudgets = data.getBudgetsByUserIDWithCriteria(dTran, newBudget.UserID, newBudget.StartDate, Nothing)
                For Each existingBudget As BudgeteerObjects.Budget In existingBudgets
                    If newBudget.BudgetID = existingBudget.BudgetID Then
                        Continue For
                    End If
                    If newBudget.SubcategoryID = existingBudget.SubcategoryID Then
                        Throw New ArgumentException("A Budget with this Subcategory already exists for this month")
                    End If
                Next
                ' Retrieve pre-updated budget
                oldBudget = data.getBudget(dTran, newBudget.BudgetID)
                If oldBudget Is Nothing Then
                    ' Couldn't retrieve the current budget from the database
                    Throw New ApplicationException("An error occured while trying to retrieve the budget from the database")
                End If
                If String.Compare(oldBudget.UserID, newBudget.UserID, True) <> 0 Then
                    ' User ID of current budget doesn't match user ID of updated budget
                    Throw New ApplicationException("An error occured while trying to retrieve the budget from the database")
                End If
                ' Update the Budget
                result = data.updateBudget(dTran, newBudget)
                If result > 0 Then ' Successful update
                    ' If budget amount, subcategory, or type changed, recalculate balance
                    If (newBudget.Amount <> oldBudget.Amount) Or _
                        (newBudget.SubcategoryID <> oldBudget.SubcategoryID) Then
                        ' Adjust (undo) the sinking fund balance of the old budget subcategory
                        currentSubcategory = subcategoryBusiness.GetSubcategory(oldBudget.SubcategoryID)
                        If currentSubcategory.SinkingFund = True Then
                            ' Get difference between budget amount and utilized dollars and adjust the SF balance
                            rolloverBudget = 0.0
                            utilizedDollars = data.getUtilizedDollarsBySubcategoryID(dTran, oldBudget.UserID, oldBudget.StartDate, oldBudget.SubcategoryID)
                            If utilizedDollars IsNot Nothing Then
                                rolloverBudget = oldBudget.Amount + utilizedDollars(0).Amount
                            End If
                            subcategoryBusiness.AdjustSubcategoryBalance(dTran, currentSubcategory, -rolloverBudget)
                        End If
                        If oldBudget.SubcategoryID = newBudget.SubcategoryID Then
                            newSubcategory = currentSubcategory
                        Else
                            newSubcategory = subcategoryBusiness.GetSubcategory(newBudget.SubcategoryID)
                        End If
                        ' Adjust the sinking fund balance of the new budget subcategory
                        If newSubcategory.SinkingFund = True Then
                            ' Get difference between budget amount and utilized dollars and adjust the SF balance
                            rolloverBudget = 0.0
                            utilizedDollars = data.getUtilizedDollarsBySubcategoryID(dTran, newBudget.UserID, newBudget.StartDate, newBudget.SubcategoryID)
                            If utilizedDollars IsNot Nothing Then
                                rolloverBudget = newBudget.Amount + utilizedDollars(0).Amount
                            End If
                            subcategoryBusiness.AdjustSubcategoryBalance(dTran, newSubcategory, rolloverBudget)
                        End If
                    End If
                Else
                    ' No new data
                End If
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function deleteBudget(ByVal budgetID As ULong) As Integer

        Dim data As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets
        Dim result As Integer = Nothing
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim subcategoryBusiness As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim utilizedDollars As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim rolloverBudget As Decimal = 0.0

        If budgetID = Nothing Then
            Throw New ArgumentException("Budget ID cannot be null or 0")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Retrieve the budget to be deleted
                budget = data.getBudget(dTran, budgetID)
                ' Retrieve the utilized dollars
                utilizedDollars = data.getUtilizedDollarsBySubcategoryID(dTran, budget.UserID, budget.StartDate, budget.SubcategoryID)
                ' Delete the budget
                result = data.deleteBudget(dTran, budgetID)
                If result = 1 Then ' Successful delete
                    If budget IsNot Nothing Then
                        subcategory = subcategoryBusiness.GetSubcategory(budget.SubcategoryID)
                        If subcategory.SinkingFund = True Then
                            ' Get difference between budget amount and utilized dollars and adjust the SF balance
                            rolloverBudget = 0.0
                            If utilizedDollars IsNot Nothing Then
                                rolloverBudget = budget.Amount + utilizedDollars(0).Amount
                            End If
                            subcategoryBusiness.AdjustSubcategoryBalance(dTran, subcategory, -rolloverBudget)
                        End If
                    Else
                        Throw New ArgumentException("Could not retrieve budget")
                    End If
                Else
                    Throw New ApplicationException("An error occured while trying to delete this budget")
                End If
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetUtilizedDollars(ByVal userID As String) As List(Of BudgeteerObjects.UtilizedDollars)

        Dim utilizedDollarsList As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim data As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be blank")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                utilizedDollarsList = data.getUtilizedDollarsByUserIDWithCriteria(dTran, userID, Nothing, Nothing)
                dTran.Commit()
                Return utilizedDollarsList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function GetUtilizedDollarsByDate(ByVal userID As String, ByVal budgetDate As Date?, ByVal subcategoryType As String) As List(Of BudgeteerObjects.UtilizedDollars)

        Dim utilizedDollarsList As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim data As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID cannot be blank")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                utilizedDollarsList = data.getUtilizedDollarsByUserIDWithCriteria(dTran, userID, budgetDate, subcategoryType)
                dTran.Commit()
                Return utilizedDollarsList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function CopyBudgets(ByVal UserID As String, ByVal startDate As Date, ByVal endDate As Date) As Integer
        Dim returnValue As Integer = 0
        Dim budgetDAL As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets
        Dim budgetBusiness As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budgetsFrom As List(Of BudgeteerObjects.Budget) = New List(Of BudgeteerObjects.Budget)
        Dim budgetsTo As List(Of BudgeteerObjects.Budget) = New List(Of BudgeteerObjects.Budget)
        Dim budgetFound As Boolean = False

        If String.IsNullOrEmpty(Trim(UserID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        If startDate = Nothing Then
            Throw New ArgumentException("Start Date is required")
        End If

        If endDate = Nothing Then
            Throw New ArgumentException("End Date is required")
        End If

        If startDate = endDate Then
            Return 0
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Get all budgets for the date to copy from
                budgetsFrom = budgetDAL.getBudgetsByUserIDWithCriteria(dTran, UserID, startDate, Nothing)
                ' Get all budgets for the date to copy to
                budgetsTo = budgetDAL.getBudgetsByUserIDWithCriteria(dTran, UserID, endDate, Nothing)
                If budgetsFrom IsNot Nothing Then
                    For Each budgetFrom As BudgeteerObjects.Budget In budgetsFrom
                        If budgetsTo IsNot Nothing Then
                            budgetFound = False
                            For Each budgetTo As BudgeteerObjects.Budget In budgetsTo
                                ' Look to see if budget with this subcategory already exists
                                If budgetFrom.SubcategoryID = budgetTo.SubcategoryID Then
                                    budgetFound = True
                                    Exit For
                                End If
                            Next
                            If Not budgetFound Then
                                ' If the budget with this subcategory doesn't exist then change the date add it
                                budgetFrom.StartDate = endDate
                                budgetBusiness.AddBudget(dTran, budgetFrom)
                                Continue For
                            End If
                        Else
                            budgetFrom.StartDate = endDate
                            budgetBusiness.AddBudget(dTran, budgetFrom)
                            Continue For
                        End If
                    Next
                Else
                    ' No budgets to copy
                    Return 0
                End If
                dTran.Commit()
                Return 1
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using
    End Function

End Class
