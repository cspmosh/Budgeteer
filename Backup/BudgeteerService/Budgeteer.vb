Imports System.Web
Imports System.ServiceModel

Public Class Budgeteer
    Implements IBudgeteer

    'Operations For Application Settings
    Public Function GetApplicationSettings() As List(Of BudgeteerObjects.ApplicationSetting) Implements IBudgeteer.GetApplicationSettings

        Dim settingsList As List(Of BudgeteerObjects.ApplicationSetting) = New List(Of BudgeteerObjects.ApplicationSetting)
        Dim settings As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            settingsList = settings.getSettings(securityContext.PrimaryIdentity.Name)
            Return settingsList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetApplicationSetting(ByVal setting As String, ByVal defaultVal As String) As String Implements IBudgeteer.GetApplicationSetting

        Dim business As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim value As String = ""
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            value = business.getSetting(securityContext.PrimaryIdentity.Name, setting, defaultVal)
            Return value
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function SetApplicationSetting(ByVal setting As String, ByVal value As String) As Integer Implements IBudgeteer.SetApplicationSetting

        Dim business As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim rows_effected As Integer
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            rows_effected = business.setSetting(securityContext.PrimaryIdentity.Name, setting, value)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    'Operations For Bank Accounts
    Public Function GetBankAccount(ByVal accountID As Int64) As BudgeteerObjects.BankAccount Implements IBudgeteer.GetBankAccount

        Dim business As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            account = business.GetAccount(accountID)

            If account IsNot Nothing Then
                If String.Compare(account.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    Return Nothing
                End If
            End If
            Return account
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetBankAccounts() As List(Of BudgeteerObjects.BankAccount) Implements IBudgeteer.GetBankAccounts

        Dim business As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim accountList As List(Of BudgeteerObjects.BankAccount) = New List(Of BudgeteerObjects.BankAccount)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            accountList = business.GetAccounts(securityContext.PrimaryIdentity.Name)
            Return accountList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetPagedBankAccounts(ByRef balanceTotal As Decimal, ByRef paginator As Paginator) As List(Of BudgeteerObjects.BankAccount) Implements IBudgeteer.GetPagedBankAccounts

        Dim business As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim accountList As List(Of BudgeteerObjects.BankAccount) = New List(Of BudgeteerObjects.BankAccount)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If paginator Is Nothing Then
            Throw New FaultException("Paginator Object Expected")
        End If

        If paginator.PageSize = Nothing Then
            Throw New FaultException("PageSize is a required field")
        End If

        If paginator.PageNumber = Nothing Then
            Throw New FaultException("PageNumber is a required field")
        End If

        Try
            accountList = business.GetAccounts(securityContext.PrimaryIdentity.Name)

            If accountList IsNot Nothing Then
                paginator.SetFirstPage(1)
                ' Calculate the last page
                paginator.SetLastPage(Math.Ceiling(accountList.Count / paginator.PageSize))
                If paginator.PageNumber > paginator.LastPage Then
                    paginator.PageNumber = paginator.LastPage
                End If
                ' Initialize the total
                balanceTotal = Nothing
                For Each account As BudgeteerObjects.BankAccount In accountList
                    balanceTotal += account.Balance
                Next
                ' Paginate the budget list and only send back the paged data requested
                accountList = accountList.Skip((paginator.PageNumber - 1) * paginator.PageSize).Take(paginator.PageSize).ToList()
            Else
                paginator.SetFirstPage(0)
                paginator.SetLastPage(0)
            End If

            Return accountList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetActiveBankAccounts() As List(Of BudgeteerObjects.BankAccount) Implements IBudgeteer.GetActiveBankAccounts

        Dim business As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim accountList As List(Of BudgeteerObjects.BankAccount) = New List(Of BudgeteerObjects.BankAccount)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            accountList = business.GetActiveAccounts(securityContext.PrimaryIdentity.Name)
            Return accountList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function AddBankAccount(ByVal account As BudgeteerObjects.BankAccount) As Int64 Implements IBudgeteer.AddBankAccount

        Dim business As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim result As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If account IsNot Nothing Then
            account.UserID = securityContext.PrimaryIdentity.Name
        End If

        Try
            result = business.AddAccount(account)
            Return result
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function UpdateBankAccount(ByVal account As BudgeteerObjects.BankAccount) As Integer Implements IBudgeteer.UpdateBankAccount

        Dim business As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If account IsNot Nothing Then
            Dim originalAccount As BudgeteerObjects.BankAccount = business.GetAccount(account.AccountID)
            If originalAccount IsNot Nothing Then
                If String.Compare(originalAccount.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    ' Account does not belong to the user 
                    Throw New FaultException("Invalid accountID was specified")
                End If
            Else
                Throw New FaultException("The account provided could not be found. Account was not updated")
            End If
        Else
            Throw New FaultException("Account object expected.")
        End If

        Try
            rows_effected = business.UpdateAccount(account)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function DeleteBankAccount(ByVal accountID As Int64) As Integer Implements IBudgeteer.DeleteBankAccount

        Dim business As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If accountID <> Nothing Then
            Dim originalAccount As BudgeteerObjects.BankAccount = business.GetAccount(accountID)
            If originalAccount IsNot Nothing Then
                If String.Compare(originalAccount.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    ' Account does not belong to the user 
                    Throw New FaultException("Invalid accountID was specified")
                End If
            Else
                Throw New FaultException("The Bank Account ID provided could not be found. Account was not deleted")
            End If
        Else
            Throw New FaultException("Bank Account ID expected.")
        End If

        Try
            rows_effected = business.DeleteAccount(accountID)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    'Operations For Budgets
    Public Function GetBudget(ByVal budgetID As Int64) As BudgeteerObjects.Budget Implements IBudgeteer.GetBudget

        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If budgetID = Nothing Then
            Throw New FaultException("Budget ID is required")
        End If

        Try
            budget = business.GetBudget(budgetID)

            If budget IsNot Nothing Then
                If String.Compare(budget.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    Return Nothing
                End If
            End If

            Return budget

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetBudgets(ByRef paginator As Paginator) As List(Of BudgeteerObjects.Budget) Implements IBudgeteer.GetBudgets

        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budgetList As List(Of BudgeteerObjects.Budget) = New List(Of BudgeteerObjects.Budget)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If paginator Is Nothing Then
            Throw New FaultException("Paginator Object Expected")
        End If

        If paginator.PageSize = Nothing Then
            Throw New FaultException("PageSize is a required field")
        End If

        If paginator.PageNumber = Nothing Then
            Throw New FaultException("PageNumber is a required field")
        End If

        Try
            ' Get Budget List
            budgetList = business.GetBudgetsWithCriteria(securityContext.PrimaryIdentity.Name, Nothing, Nothing)

            If budgetList IsNot Nothing Then
                paginator.SetFirstPage(1)
                ' Calculate the last page
                paginator.SetLastPage(Math.Ceiling(budgetList.Count / paginator.PageSize))
                If paginator.PageNumber > paginator.LastPage Then
                    paginator.PageNumber = paginator.LastPage
                End If
                ' Paginate the budget list and only send back the paged data requested
                budgetList = budgetList.Skip((paginator.PageNumber - 1) * paginator.PageSize).Take(paginator.PageSize).ToList()
            Else
                paginator.SetFirstPage(0)
                paginator.SetLastPage(0)
            End If

            Return budgetList

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetBudgetsWithCriteria(ByVal startDate As Date?, ByVal subcategoryType As String, ByRef budgetTotal As Decimal, ByRef paginator As Paginator) As List(Of BudgeteerObjects.Budget) Implements IBudgeteer.GetBudgetsWithCriteria

        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budgetList As List(Of BudgeteerObjects.Budget) = New List(Of BudgeteerObjects.Budget)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If paginator Is Nothing Then
            Throw New FaultException("Paginator Object Expected")
        End If

        If paginator.PageSize = Nothing Then
            Throw New FaultException("PageSize is a required field")
        End If

        If paginator.PageNumber = Nothing Then
            Throw New FaultException("PageNumber is a required field")
        End If

        Try
            ' Get Budget List
            budgetList = business.GetBudgetsWithCriteria(securityContext.PrimaryIdentity.Name, startDate, subcategoryType)

            If budgetList IsNot Nothing Then
                paginator.SetFirstPage(1)
                ' Calculate the last page
                paginator.SetLastPage(Math.Ceiling(budgetList.Count / paginator.PageSize))
                If paginator.PageNumber > paginator.LastPage Then
                    paginator.PageNumber = paginator.LastPage
                End If

                budgetTotal = Nothing
                For Each budget As BudgeteerObjects.Budget In budgetList
                    budgetTotal += budget.Amount
                Next

                ' Paginate the budget list and only send back the paged data requested
                budgetList = budgetList.Skip((paginator.PageNumber - 1) * paginator.PageSize).Take(paginator.PageSize).ToList()
            Else
                paginator.SetFirstPage(0)
                paginator.SetLastPage(0)
            End If

            Return budgetList

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetBudgetFirstYear() As Integer Implements IBudgeteer.GetBudgetFirstYear
        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current
        Try
            Return business.GetBudgetFirstYear(securityContext.PrimaryIdentity.Name)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try
    End Function

    Public Function GetUtilizedDollars(ByRef paginator As Paginator) As List(Of BudgeteerObjects.UtilizedDollars) Implements IBudgeteer.GetUtilizedDollars

        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim utilizedDollarsList As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim pagedData As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If paginator Is Nothing Then
            Throw New FaultException("Paginator Object Expected")
        End If

        If paginator.PageSize = Nothing Then
            Throw New FaultException("PageSize is a required field")
        End If

        If paginator.PageNumber = Nothing Then
            Throw New FaultException("PageNumber is a required field")
        End If

        Try
            utilizedDollarsList = business.GetUtilizedDollars(securityContext.PrimaryIdentity.Name)

            If utilizedDollarsList IsNot Nothing Then
                paginator.SetFirstPage(1)
                ' Calculate the last page
                paginator.SetLastPage(Math.Ceiling(utilizedDollarsList.Count / paginator.PageSize))
                If paginator.PageNumber > paginator.LastPage Then
                    paginator.PageNumber = paginator.LastPage
                End If
                ' Paginate the utilized dollars list and only send back the paged data requested
                utilizedDollarsList = utilizedDollarsList.Skip((paginator.PageNumber - 1) * paginator.PageSize).Take(paginator.PageSize).ToList()
            Else
                paginator.SetFirstPage(0)
                paginator.SetLastPage(0)
            End If

            Return utilizedDollarsList

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetUtilizedDollarsWithCriteria(ByVal budgetDate As Date?, ByVal subcategoryType As String, ByRef totalUtilized As Decimal, ByRef totalBudget As Decimal, ByRef totalAvailableDollars As Decimal, ByRef paginator As Paginator) As List(Of BudgeteerObjects.UtilizedDollars) Implements IBudgeteer.GetUtilizedDollarsWithCriteria

        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim utilizedDollarsList As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim pagedData As List(Of BudgeteerObjects.UtilizedDollars) = New List(Of BudgeteerObjects.UtilizedDollars)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If paginator Is Nothing Then
            Throw New FaultException("Paginator Object Expected")
        End If

        If paginator.PageSize = Nothing Then
            Throw New FaultException("PageSize is a required field")
        End If

        If paginator.PageNumber = Nothing Then
            Throw New FaultException("PageNumber is a required field")
        End If

        Try
            utilizedDollarsList = business.GetUtilizedDollarsByDate(securityContext.PrimaryIdentity.Name, budgetDate, subcategoryType)

            If utilizedDollarsList IsNot Nothing Then
                paginator.SetFirstPage(1)
                ' Calculate the last page
                paginator.SetLastPage(Math.Ceiling(utilizedDollarsList.Count / paginator.PageSize))
                If paginator.PageNumber > paginator.LastPage Then
                    paginator.PageNumber = paginator.LastPage
                End If

                ' Initialize totals
                totalUtilized = Nothing
                totalBudget = Nothing
                totalAvailableDollars = Nothing

                ' Sum the total utilized, budgeted, and available dollars
                For Each dollar As BudgeteerObjects.UtilizedDollars In utilizedDollarsList
                    totalAvailableDollars += dollar.Available
                    totalUtilized += dollar.Amount
                    totalBudget += dollar.Budget
                Next

                ' Paginate the utilized dollars list and only send back the paged data requested
                utilizedDollarsList = utilizedDollarsList.Skip((paginator.PageNumber - 1) * paginator.PageSize).Take(paginator.PageSize).ToList()
            Else
                paginator.SetFirstPage(0)
                paginator.SetLastPage(0)
            End If

            Return utilizedDollarsList

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function AddBudget(ByVal budget As BudgeteerObjects.Budget) As Int64 Implements IBudgeteer.AddBudget

        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If budget IsNot Nothing Then
            budget.UserID = securityContext.PrimaryIdentity.Name
        End If

        Try
            rows_effected = business.AddBudget(budget)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function UpdateBudget(ByVal budget As BudgeteerObjects.Budget) As Integer Implements IBudgeteer.UpdateBudget

        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If budget IsNot Nothing Then
            Dim originalBudget As BudgeteerObjects.Budget = business.GetBudget(budget.BudgetID)
            If originalBudget IsNot Nothing Then
                If String.Compare(originalBudget.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    ' Budget does not belong to the user 
                    Throw New FaultException("Invalid budgetID was specified")
                End If
            Else
                Throw New FaultException("The Budget provided could not be found. Budget was not updated")
            End If
        Else
            Throw New FaultException("Budget object expected")
        End If

        Try
            rows_effected = business.UpdateBudget(budget)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function DeleteBudget(ByVal budgetID As Int64) As Integer Implements IBudgeteer.DeleteBudget

        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If budgetID <> Nothing Then
            Dim originalBudget As BudgeteerObjects.Budget = business.GetBudget(budgetID)
            If originalBudget IsNot Nothing Then
                If String.Compare(originalBudget.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    ' Budget does not belong to the user 
                    Throw New FaultException("Invalid budgetID was specified")
                End If
            Else
                Throw New FaultException("The Budget ID provided could not be found. Budget was not updated")
            End If
        Else
            Throw New FaultException("Budget ID expected")
        End If

        Try
            rows_effected = business.deleteBudget(budgetID)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function CopyBudgets(ByVal fromDate As Date, ByVal toDate As Date) As Integer Implements IBudgeteer.CopyBudgets

        Dim business As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            Return business.CopyBudgets(securityContext.PrimaryIdentity.Name, fromDate, toDate)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    'Operations For Budget Categories
    Public Function GetCategory(ByVal categoryID As Int64) As BudgeteerObjects.Category Implements IBudgeteer.GetCategory

        Dim business As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            category = business.getCategory(categoryID)

            If category IsNot Nothing Then
                If String.Compare(category.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    Return Nothing
                End If
            End If
            Return category
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetCategories() As List(Of BudgeteerObjects.Category) Implements IBudgeteer.GetCategories

        Dim business As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim categoryList As List(Of BudgeteerObjects.Category) = New List(Of BudgeteerObjects.Category)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            categoryList = business.getCategories(securityContext.PrimaryIdentity.Name)
            Return categoryList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetActiveCategories() As List(Of BudgeteerObjects.Category) Implements IBudgeteer.GetActiveCategories

        Dim business As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim categoryList As List(Of BudgeteerObjects.Category) = New List(Of BudgeteerObjects.Category)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            categoryList = business.getActiveCategories(securityContext.PrimaryIdentity.Name)
            Return categoryList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function AddCategory(ByVal category As BudgeteerObjects.Category) As Int64 Implements IBudgeteer.AddCategory

        Dim business As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If category IsNot Nothing Then
            category.UserID = securityContext.PrimaryIdentity.Name
        End If

        Try
            rows_effected = business.addCategory(category)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function UpdateCategory(ByVal category As BudgeteerObjects.Category) As Integer Implements IBudgeteer.UpdateCategory

        Dim business As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If category IsNot Nothing Then
            Dim originalCategory As BudgeteerObjects.Category = business.getCategory(category.CategoryID)
            If originalCategory IsNot Nothing Then
                If String.Compare(originalCategory.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    Throw New FaultException("Invalid Category ID")
                End If
            Else
                Throw New FaultException("Category could not be found. Category was not updated")
            End If
        Else
            Throw New FaultException("Category object expected")
        End If

        Try
            rows_effected = business.updateCategory(category)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function DeleteCategory(ByVal categoryID As Int64) As Integer Implements IBudgeteer.DeleteCategory

        Dim business As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If categoryID <> Nothing Then
            Dim originalCategory As BudgeteerObjects.Category = business.getCategory(categoryID)
            If originalCategory IsNot Nothing Then
                If String.Compare(originalCategory.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    Throw New FaultException("Invalid Category ID")
                End If
            Else
                Throw New FaultException("Category could not be found. Category was not deleted")
            End If
        Else
            Throw New FaultException("Category ID is required")
        End If

        Try
            rows_effected = business.deleteCategory(categoryID)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    'Operations For Budget Subcategories
    Public Function GetSubcategory(ByVal subcategoryID As Int64) As BudgeteerObjects.Subcategory Implements IBudgeteer.GetSubcategory

        Dim business As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            subcategory = business.GetSubcategory(subcategoryID)

            If subcategory IsNot Nothing Then
                If String.Compare(subcategory.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    Return Nothing
                End If
            End If
            Return subcategory
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetSubcategories() As List(Of BudgeteerObjects.Subcategory) Implements IBudgeteer.GetSubcategories

        Dim business As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            subcategoryList = business.GetSubcategories(securityContext.PrimaryIdentity.Name)
            Return subcategoryList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetActiveSubcategories() As List(Of BudgeteerObjects.Subcategory) Implements IBudgeteer.GetActiveSubcategories

        Dim business As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            subcategoryList = business.GetActiveSubcategories(securityContext.PrimaryIdentity.Name)
            Return subcategoryList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetSubcategoriesByCategoryID(ByVal categoryID As Int64) As List(Of BudgeteerObjects.Subcategory) Implements IBudgeteer.GetSubcategoriesByCategoryID

        Dim business As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            subcategoryList = business.GetSubcategoriesByCategoryID(categoryID, securityContext.PrimaryIdentity.Name)
            Return subcategoryList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetActiveSubcategoriesByCategoryID(ByVal categoryID As Int64) As List(Of BudgeteerObjects.Subcategory) Implements IBudgeteer.GetActiveSubcategoriesByCategoryID

        Dim business As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategoryList As List(Of BudgeteerObjects.Subcategory) = New List(Of BudgeteerObjects.Subcategory)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            subcategoryList = business.GetActiveSubcategoriesByCategoryID(categoryID, securityContext.PrimaryIdentity.Name)
            Return subcategoryList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function AddSubcategory(ByVal subcategory As BudgeteerObjects.Subcategory) As Int64 Implements IBudgeteer.AddSubcategory

        Dim business As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If subcategory IsNot Nothing Then
            subcategory.UserID = securityContext.PrimaryIdentity.Name
        End If

        Try
            rows_effected = business.AddSubcategory(subcategory)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function UpdateSubcategory(ByVal subcategory As BudgeteerObjects.Subcategory) As Integer Implements IBudgeteer.UpdateSubcategory

        Dim business As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If subcategory IsNot Nothing Then
            Dim originalSubcategory As BudgeteerObjects.Subcategory = business.GetSubcategory(subcategory.SubcategoryID)
            If originalSubcategory IsNot Nothing Then
                If String.Compare(originalSubcategory.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    Throw New FaultException("Invalid User ID")
                End If
            Else
                Throw New FaultException("Subcategory could not be found. Subcategory was not updated")
            End If
        Else
            Throw New FaultException("Subcategory object expected")
        End If

        subcategory.UserID = securityContext.PrimaryIdentity.Name

        Try
            rows_effected = business.UpdateSubcategory(subcategory)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function DeleteSubcategory(ByVal subcategoryID As Int64) As Integer Implements IBudgeteer.DeleteSubcategory

        Dim business As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If subcategoryID <> Nothing Then
            Dim originalSubcategory As BudgeteerObjects.Subcategory = business.GetSubcategory(subcategoryID)
            If originalSubcategory IsNot Nothing Then
                If String.Compare(originalSubcategory.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    Throw New FaultException("Invalid Subcategory ID")
                End If
            Else
                Throw New FaultException("Subcategory could not be found. Subcategory was not deleted")
            End If
        Else
            Throw New FaultException("Subcategory ID expected")
        End If

        Try
            rows_effected = business.DeleteSubcategory(subcategoryID)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    'Operations For Transactions
    Public Function GetFirstTransactionYear() As Integer Implements IBudgeteer.GetFirstTransactionYear

        Dim business As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current
        Dim firstTransactionYear As Integer

        Try
            firstTransactionYear = business.getTransactionFirstYear(securityContext.PrimaryIdentity.Name)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try
        Return firstTransactionYear

    End Function

    Public Function GetTransaction(ByVal transactionID As Int64) As BudgeteerObjects.Transaction Implements IBudgeteer.GetTransaction

        Dim business As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            transaction = business.getTransaction(transactionID)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

        If transaction IsNot Nothing Then
            If String.Compare(securityContext.PrimaryIdentity.Name, transaction.UserID, True) <> 0 Then
                'If transaction does not belong to this user, return nothing
                Return Nothing
            End If
        End If

        Return transaction

    End Function

    Public Function GetTransactions(ByRef paginator As Paginator) As List(Of BudgeteerObjects.Transaction) Implements IBudgeteer.GetTransactions

        Dim business As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transactionList As List(Of BudgeteerObjects.Transaction) = New List(Of BudgeteerObjects.Transaction)
        Dim searchCriteria As BudgeteerObjects.TransactionFilterCriteria = New BudgeteerObjects.TransactionFilterCriteria 'Empty search criteria
        Dim pagedData As List(Of BudgeteerObjects.Transaction) = New List(Of BudgeteerObjects.Transaction)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If paginator Is Nothing Then
            Throw New FaultException("Paginator Object Expected")
        End If

        If paginator.PageSize = Nothing Then
            Throw New FaultException("PageSize is a required field")
        End If

        If paginator.PageNumber = Nothing Then
            Throw New FaultException("PageNumber is a required field")
        End If

        Try
            ' Get the list of transactions for this user 
            transactionList = business.getTransactionsWithCriteria(securityContext.PrimaryIdentity.Name, searchCriteria)

            If transactionList IsNot Nothing Then
                paginator.SetFirstPage(1)
                ' Calculate the last page
                paginator.SetLastPage(Math.Ceiling(transactionList.Count / paginator.PageSize))
                If paginator.PageNumber > paginator.LastPage Then
                    paginator.PageNumber = paginator.LastPage
                End If
                ' Paginate the utilized dollars list and only send back the paged data requested
                transactionList = transactionList.Skip((paginator.PageNumber - 1) * paginator.PageSize).Take(paginator.PageSize).ToList()
            Else
                paginator.SetFirstPage(0)
                paginator.SetLastPage(0)
            End If

            Return transactionList

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetTransactionsWithCriteria(ByVal searchCriteria As BudgeteerObjects.TransactionFilterCriteria, ByRef paginator As Paginator) As List(Of BudgeteerObjects.Transaction) Implements IBudgeteer.GetTransactionsWithCriteria

        Dim business As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transactionList As List(Of BudgeteerObjects.Transaction) = New List(Of BudgeteerObjects.Transaction)
        Dim pagedData As List(Of BudgeteerObjects.Transaction) = New List(Of BudgeteerObjects.Transaction)
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If searchCriteria Is Nothing Then
            Throw New FaultException("Instantiated Transaction object expected")
        End If

        If paginator Is Nothing Then
            Throw New FaultException("Paginator Object Expected")
        End If

        If paginator.PageSize = Nothing Then
            Throw New FaultException("PageSize is a required field")
        End If

        If paginator.PageNumber = Nothing Then
            Throw New FaultException("PageNumber is a required field")
        End If

        Try
            ' Get transaction list based on user and search criteria
            transactionList = business.getTransactionsWithCriteria(securityContext.PrimaryIdentity.Name, searchCriteria)

            If transactionList IsNot Nothing Then
                paginator.SetFirstPage(1)
                ' Calculate the last page
                paginator.SetLastPage(Math.Ceiling(transactionList.Count / paginator.PageSize))
                If paginator.PageNumber > paginator.LastPage Then
                    paginator.PageNumber = paginator.LastPage
                End If
                ' Paginate the utilized dollars list and only send back the paged data requested
                transactionList = transactionList.Skip((paginator.PageNumber - 1) * paginator.PageSize).Take(paginator.PageSize).ToList()
            Else
                paginator.SetFirstPage(0)
                paginator.SetLastPage(0)
            End If

            Return transactionList

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function AddTransaction(ByVal transaction As BudgeteerObjects.Transaction) As Int64 Implements IBudgeteer.AddTransaction

        Dim business As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        'Set user ID to current authorized user
        transaction.UserID = securityContext.PrimaryIdentity.Name

        Try
            rows_effected = business.addTransaction(transaction)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function UpdateTransaction(ByVal transaction As BudgeteerObjects.Transaction) As Integer Implements IBudgeteer.UpdateTransaction

        Dim business As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If transaction IsNot Nothing Then
            Dim originalTransaction As BudgeteerObjects.Transaction = business.getTransaction(transaction.TransactionID)
            If originalTransaction IsNot Nothing Then
                If String.Compare(originalTransaction.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    ' The transaction requested for update does not belong to the user!
                    Throw New FaultException("Invalid transaction ID")
                End If
            Else
                Throw New FaultException("Transaction could not be found. Transaction was not updated.")
            End If
        Else
            Throw New FaultException("Transaction Object Expected")
        End If

        'Set user ID to current authorized user
        transaction.UserID = securityContext.PrimaryIdentity.Name

        Try
            rows_effected = business.updateTransaction(transaction)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function DeleteTransaction(ByVal transactionID As Int64) As Integer Implements IBudgeteer.DeleteTransaction

        Dim business As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        If transactionID <> Nothing Then
            Dim originalTransaction As BudgeteerObjects.Transaction = business.getTransaction(transactionID)
            If originalTransaction IsNot Nothing Then
                If String.Compare(originalTransaction.UserID, securityContext.PrimaryIdentity.Name, True) <> 0 Then
                    ' The transaction requested for deletion does not belong to the user!
                    Throw New FaultException("Invalid transaction ID")
                End If
            Else
                Throw New FaultException("Transaction could not be found to delete.")
            End If
        Else
            Throw New FaultException("Transaction Object Expected")
        End If

        Try
            rows_effected = business.deleteTransaction(transactionID)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    'Operations For Users
    Public Function GetUser() As BudgeteerObjects.User Implements IBudgeteer.GetUser

        Dim business As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            user = business.getUser(securityContext.PrimaryIdentity.Name)
            Return user
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function UpdateUserPassword(ByVal oldPassword As String, ByVal newPassword As String) As Integer Implements IBudgeteer.UpdateUserPassword

        Dim business As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            rows_effected = business.updateUserPassword(securityContext.PrimaryIdentity.Name, oldPassword, newPassword)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function UpdateUserEmailAddress(ByVal newEmail As String) As Integer Implements IBudgeteer.UpdateUserEmailAddress

        Dim business As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim rows_effected As Integer = Nothing
        Dim securityContext As ServiceSecurityContext = ServiceSecurityContext.Current

        Try
            rows_effected = business.updateUserEmail(securityContext.PrimaryIdentity.Name, newEmail)
            Return rows_effected
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ' Operations For Security
    Public Function AuthenticateUser(ByVal userID As String, ByVal password As String) As Integer Implements IBudgeteer.AuthenticateUser

        Dim business As BudgeteerBusiness.Security = New BudgeteerBusiness.Security
        Dim authenticationMsg As Integer = Nothing

        Try
            authenticationMsg = business.authenticateUser(userID, password)
            Return CType(authenticationMsg, BudgeteerBusiness.Security.authenticationReturnType)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

End Class

Public Class BudgeteerUnauthenticatedServices
    Implements IBudgeteerUnauthenticatedServices

    Public Function GetUserName(ByVal email As String) As String Implements IBudgeteerUnauthenticatedServices.GetUserName
        Dim userName As String = Nothing
        Dim business As BudgeteerBusiness.User = New BudgeteerBusiness.User

        Try
            userName = business.getUserName(email)
            Return userName
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function AddUser(ByVal user As BudgeteerObjects.User, ByVal password As String, ByVal questions As List(Of BudgeteerObjects.UserSecurityQuestionAnswer)) As String Implements IBudgeteerUnauthenticatedServices.AddUser

        Dim business As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim registrationKey As String = ""

        Try
            ' Make sure userID is the same for security question as whats in the user object
            For Each question As BudgeteerObjects.UserSecurityQuestionAnswer In questions
                ' Hijack the userID field and store the User ID from the user object 
                question.UserID = user.UserID
            Next
            ' Add the user
            registrationKey = business.addUser(user, password, questions)
            Return registrationKey
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetAvailableSecurityQuestions() As List(Of BudgeteerObjects.SecurityQuestion) Implements IBudgeteerUnauthenticatedServices.GetAvailableSecurityQuestions
        Dim business As BudgeteerBusiness.Security = New BudgeteerBusiness.Security
        Try
            Return business.getSecurityQuestions()
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try
    End Function

    Public Function ValidateSecurityQuestions(ByVal user As BudgeteerObjects.User, ByVal securityQuestionAnswers As List(Of BudgeteerObjects.UserSecurityQuestionAnswer)) As Boolean Implements IBudgeteerUnauthenticatedServices.ValidateSecurityQuestions
        Dim business As BudgeteerBusiness.Security = New BudgeteerBusiness.Security
        Try
            Return business.validateSecurityQuestions(user, securityQuestionAnswers)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try
    End Function

    Public Function ActivateUser(ByVal userID As String, ByVal emailAddress As String, ByVal registrationKey As String) As Integer Implements IBudgeteerUnauthenticatedServices.ActivateUser
        Dim business As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim activated As Integer = Nothing
        Try
            activated = business.activateUser(userID, emailAddress, registrationKey)
            Return activated
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try
    End Function

    Public Function GetUserSecurityQuestions(ByVal user As BudgeteerObjects.User) As List(Of BudgeteerObjects.SecurityQuestion) Implements IBudgeteerUnauthenticatedServices.GetUserSecurityQuestions
        Dim business As BudgeteerBusiness.Security = New BudgeteerBusiness.Security
        Dim questions As List(Of BudgeteerObjects.SecurityQuestion) = New List(Of BudgeteerObjects.SecurityQuestion)
        Try
            questions = business.getSecurityQuestions(user)
            Return questions
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try
    End Function

    Public Function ResetPassword(ByVal user As BudgeteerObjects.User, ByVal userQuestionAnswers As List(Of BudgeteerObjects.UserSecurityQuestionAnswer), ByVal newPassword As String) As Integer Implements IBudgeteerUnauthenticatedServices.ResetPassword
        Dim business As BudgeteerBusiness.Security = New BudgeteerBusiness.Security
        Try
            Return business.resetPassword(user, userQuestionAnswers, newPassword)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try
    End Function

End Class

Public Class UserNamePassValidator
    Inherits System.IdentityModel.Selectors.UserNamePasswordValidator

    Public Overrides Sub Validate(ByVal userName As String, ByVal password As String)

        Dim business As BudgeteerBusiness.Security = New BudgeteerBusiness.Security
        Dim authenticated As Integer

        Try
            authenticated = business.authenticateUser(userName, password)

            Select Case authenticated
                Case BudgeteerBusiness.Security.authenticationReturnType.Success
                    'Authenticated!
                Case BudgeteerBusiness.Security.authenticationReturnType.InvaidUserName
                    'Wrong User Name
                    Throw New FaultException("Invalid Username")
                Case BudgeteerBusiness.Security.authenticationReturnType.InvalidPassword
                    'Wrong Password
                    Throw New FaultException("Invalid Password")
            End Select

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub
End Class