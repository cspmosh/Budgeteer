Imports BudgeteerDAL

Public Class Category

    Public Function getCategory(ByVal categoryID As Int64) As BudgeteerObjects.Category

        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        Dim categoryData As BudgeteerDAL.Categories = New BudgeteerDAL.Categories

        If categoryID = Nothing Then
            Throw New ArgumentException("Category ID is required")
        End If

        Using dtran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                category = categoryData.getCategory(dtran, categoryID)
                dtran.Commit()
                Return category
            Catch ex As Exception
                dtran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function getCategories(ByVal userID As String) As List(Of BudgeteerObjects.Category)

        Dim categoryList As List(Of BudgeteerObjects.Category) = New List(Of BudgeteerObjects.Category)
        Dim categoryData As BudgeteerDAL.Categories = New BudgeteerDAL.Categories

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                categoryList = categoryData.getCategoriesByUserID(dTran, userID)
                dTran.Commit()
                Return categoryList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function getActiveCategories(ByVal userID As String) As List(Of BudgeteerObjects.Category)

        Dim categoryList As List(Of BudgeteerObjects.Category) = New List(Of BudgeteerObjects.Category)
        Dim categoryData As BudgeteerDAL.Categories = New BudgeteerDAL.Categories

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                categoryList = categoryData.getActiveCategoriesByUserID(dTran, userID)
                dTran.Commit()
                Return categoryList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function addCategory(ByVal category As BudgeteerObjects.Category) As Int64

        Dim categoryData As BudgeteerDAL.Categories = New BudgeteerDAL.Categories
        Dim result As Int64 = Nothing
        Dim existingCategories As List(Of BudgeteerObjects.Category) = New List(Of BudgeteerObjects.Category)

        If category Is Nothing Then
            Throw New ArgumentException("A Category is required")
        End If

        If String.IsNullOrEmpty(Trim(category.Description)) Then
            Throw New ArgumentException("Description is required")
        Else
            If category.Description.Length() > BudgeteerObjects.Category.MaxFieldLength.Description Then
                Throw New ArgumentException("Category Description cannot exceed " + BudgeteerObjects.Category.MaxFieldLength.Description.ToString() + " characters")
            End If
        End If

        If String.IsNullOrEmpty(Trim(category.UserID)) Then
            Throw New ArgumentException("A User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Unique constraints
                existingCategories = categoryData.getCategoriesByUserID(dTran, category.UserID)
                If existingCategories IsNot Nothing Then
                    For Each existingCategory As BudgeteerObjects.Category In existingCategories
                        If String.Compare(Trim(existingCategory.Description), Trim(category.Description), True) = 0 Then
                            Throw New ArgumentException("This Budget Category already exists")
                        End If
                    Next
                End If
                result = categoryData.addCategory(dTran, category)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function updateCategory(ByVal category As BudgeteerObjects.Category) As Integer

        Dim categoryData As BudgeteerDAL.Categories = New BudgeteerDAL.Categories
        Dim result As Integer = Nothing
        Dim existingCategories As List(Of BudgeteerObjects.Category) = New List(Of BudgeteerObjects.Category)

        If category Is Nothing Then
            Throw New ArgumentException("A category is required")
        End If

        If category.CategoryID = Nothing Then
            Throw New ArgumentException("A category ID is required")
        End If

        If String.IsNullOrEmpty(Trim(category.Description)) Then
            Throw New ArgumentException("A category description is required")
        End If

        If String.IsNullOrEmpty(Trim(category.UserID)) Then
            Throw New ArgumentException("A user ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Unique constraints
                existingCategories = categoryData.getCategoriesByUserID(dTran, category.UserID)
                For Each existingCategory As BudgeteerObjects.Category In existingCategories
                    If existingCategory.CategoryID = category.CategoryID Then
                        Continue For
                    End If
                    If String.Compare(Trim(existingCategory.Description), Trim(category.Description), True) = 0 Then
                        Throw New ArgumentException("This Budget Category already exists")
                    End If
                Next
                result = categoryData.updateCategory(dTran, category)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function deleteCategory(ByVal categoryID As ULong) As Integer

        Dim categoryData As BudgeteerDAL.Categories = New BudgeteerDAL.Categories
        Dim result As Integer = Nothing
        Dim subcategoryData As BudgeteerDAL.Subcategories = New BudgeteerDAL.Subcategories
        Dim existingSubcategories As Integer = 0

        If categoryID = Nothing Then
            Throw New ArgumentException("Category ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Delete Restrict Constraints
                ' Check for Subcategories with this categoryID
                existingSubcategories = subcategoryData.getSubcategoryCountByCategoryID(dTran, categoryID)
                If existingSubcategories > 0 Then
                    Throw New ArgumentException("Category is used by existing subcategories. If you no longer want to use this category you can mark it as 'Inactive' and it will no longer show up as a choice in drop down boxes.")
                End If
                result = categoryData.deleteCategory(dTran, categoryID)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

End Class
