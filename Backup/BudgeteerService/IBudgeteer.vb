' NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in App.config.
<ServiceContract()> _
Public Interface IBudgeteer

    'Operations For Application Settings
    <OperationContract()> _
    Function GetApplicationSettings() As List(Of BudgeteerObjects.ApplicationSetting)
    <OperationContract()> _
    Function GetApplicationSetting(ByVal setting As String, ByVal defaultVal As String) As String
    <OperationContract()> _
    Function SetApplicationSetting(ByVal setting As String, ByVal value As String) As Integer

    'Operations For Bank Accounts
    <OperationContract()> _
    Function GetBankAccount(ByVal accountID As Int64) As BudgeteerObjects.BankAccount
    <OperationContract()> _
    Function GetBankAccounts() As List(Of BudgeteerObjects.BankAccount)
    <OperationContract()> _
    Function GetPagedBankAccounts(ByRef balanceTotal As Decimal, ByRef paginator As Paginator) As List(Of BudgeteerObjects.BankAccount)
    <OperationContract()> _
    Function GetActiveBankAccounts() As List(Of BudgeteerObjects.BankAccount)
    <OperationContract()> _
    Function AddBankAccount(ByVal account As BudgeteerObjects.BankAccount) As Int64
    <OperationContract()> _
    Function UpdateBankAccount(ByVal account As BudgeteerObjects.BankAccount) As Integer
    <OperationContract()> _
    Function DeleteBankAccount(ByVal accountID As Int64) As Integer

    'Operations For Budgets
    <OperationContract()> _
    Function GetBudget(ByVal budgetID As Int64) As BudgeteerObjects.Budget
    <OperationContract()> _
    Function GetBudgets(ByRef paginator As Paginator) As List(Of BudgeteerObjects.Budget)
    <OperationContract()> _
    Function GetBudgetsWithCriteria(ByVal startDate As Date?, ByVal subcategoryType As String, ByRef budgetTotal As Decimal, ByRef paginator As Paginator) As List(Of BudgeteerObjects.Budget)
    <OperationContract()> _
    Function GetBudgetFirstYear() As Integer
    <OperationContract()> _
    Function AddBudget(ByVal budget As BudgeteerObjects.Budget) As Int64
    <OperationContract()> _
    Function CopyBudgets(ByVal fromDate As Date, ByVal toDate As Date) As Integer
    <OperationContract()> _
    Function UpdateBudget(ByVal budget As BudgeteerObjects.Budget) As Integer
    <OperationContract()> _
    Function DeleteBudget(ByVal budgetID As Int64) As Integer
    <OperationContract()> _
    Function GetUtilizedDollars(ByRef paginator As Paginator) As List(Of BudgeteerObjects.UtilizedDollars)
    <OperationContract()> _
    Function GetUtilizedDollarsWithCriteria(ByVal budgetDate As Date?, ByVal subcategoryType As String, ByRef totalUtilized As Decimal, ByRef totalBudget As Decimal, ByRef totalAvailableDollars As Decimal, ByRef paginator As Paginator) As List(Of BudgeteerObjects.UtilizedDollars)

    'Operations For Budget Categories
    <OperationContract()> _
    Function GetCategory(ByVal categoryID As Int64) As BudgeteerObjects.Category
    <OperationContract()> _
    Function GetActiveCategories() As List(Of BudgeteerObjects.Category)
    <OperationContract()> _
    Function GetCategories() As List(Of BudgeteerObjects.Category)
    <OperationContract()> _
    Function AddCategory(ByVal category As BudgeteerObjects.Category) As Int64
    <OperationContract()> _
    Function UpdateCategory(ByVal category As BudgeteerObjects.Category) As Integer
    <OperationContract()> _
    Function DeleteCategory(ByVal categoryID As Int64) As Integer

    'Operations For Budget Subcategories
    <OperationContract()> _
    Function GetSubcategory(ByVal subcategoryID As Int64) As BudgeteerObjects.Subcategory
    <OperationContract()> _
    Function GetActiveSubcategories() As List(Of BudgeteerObjects.Subcategory)
    <OperationContract()> _
    Function GetSubcategories() As List(Of BudgeteerObjects.Subcategory)
    <OperationContract()> _
    Function GetSubcategoriesByCategoryID(ByVal categoryID As Int64) As List(Of BudgeteerObjects.Subcategory)
    <OperationContract()> _
    Function GetActiveSubcategoriesByCategoryID(ByVal categoryID As Int64) As List(Of BudgeteerObjects.Subcategory)
    <OperationContract()> _
    Function AddSubcategory(ByVal subcategory As BudgeteerObjects.Subcategory) As Int64
    <OperationContract()> _
    Function UpdateSubcategory(ByVal subcategory As BudgeteerObjects.Subcategory) As Integer
    <OperationContract()> _
    Function DeleteSubcategory(ByVal subcategoryID As Int64) As Integer

    'Operations For Transactions
    <OperationContract()> _
    Function GetFirstTransactionYear() As Integer
    <OperationContract()> _
    Function GetTransaction(ByVal transactionID As Int64) As BudgeteerObjects.Transaction
    <OperationContract()> _
    Function GetTransactions(ByRef paginator As Paginator) As List(Of BudgeteerObjects.Transaction)
    <OperationContract()> _
    Function GetTransactionsWithCriteria(ByVal searchCriteria As BudgeteerObjects.TransactionFilterCriteria, ByRef paginator As Paginator) As List(Of BudgeteerObjects.Transaction)
    <OperationContract()> _
    Function AddTransaction(ByVal transaction As BudgeteerObjects.Transaction) As Int64
    <OperationContract()> _
    Function UpdateTransaction(ByVal transaction As BudgeteerObjects.Transaction) As Integer
    <OperationContract()> _
    Function DeleteTransaction(ByVal transactionID As Int64) As Integer

    'Operations For Users
    <OperationContract()> _
    Function GetUser() As BudgeteerObjects.User
    <OperationContract()> _
    Function UpdateUserPassword(ByVal oldPassword As String, ByVal newPassword As String) As Integer
    <OperationContract()> _
    Function UpdateUserEmailAddress(ByVal newEmail As String) As Integer

    'Operations For Security
    <OperationContract()> _
    Function AuthenticateUser(ByVal userID As String, ByVal password As String) As Integer

End Interface

<ServiceContract()> _
Public Interface IBudgeteerUnauthenticatedServices
    <OperationContract()> _
    Function AddUser(ByVal User As BudgeteerObjects.User, ByVal Password As String, ByVal Questions As List(Of BudgeteerObjects.UserSecurityQuestionAnswer)) As String
    <OperationContract()> _
    Function GetAvailableSecurityQuestions() As List(Of BudgeteerObjects.SecurityQuestion)
    <OperationContract()> _
    Function ActivateUser(ByVal userID As String, ByVal emailAddress As String, ByVal registrationKey As String) As Integer
    <OperationContract()> _
    Function GetUserSecurityQuestions(ByVal user As BudgeteerObjects.User) As List(Of BudgeteerObjects.SecurityQuestion)
    <OperationContract()> _
    Function ValidateSecurityQuestions(ByVal user As BudgeteerObjects.User, ByVal userQuestionAnswers As List(Of BudgeteerObjects.UserSecurityQuestionAnswer)) As Boolean
    <OperationContract()> _
    Function ResetPassword(ByVal user As BudgeteerObjects.User, ByVal userQuestionAnswers As List(Of BudgeteerObjects.UserSecurityQuestionAnswer), ByVal newPassword As String) As Integer
    <OperationContract()> _
    Function GetUserName(ByVal email As String) As String
End Interface


