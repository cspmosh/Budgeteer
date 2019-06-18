Imports BudgeteerDAL

Public Class Frequency

    Public Function getFrequency(ByVal frequencyID As Int64) As BudgeteerObjects.Frequency

        Dim frequency As BudgeteerObjects.Frequency = New BudgeteerObjects.Frequency
        Dim frequencyData As BudgeteerDAL.Frequencies = New BudgeteerDAL.Frequencies

        If frequencyID = Nothing Then
            Throw New ArgumentException("Frequency ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                frequency = frequencyData.getFrequency(dTran, frequencyID)
                dTran.Commit()
                Return frequency
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function getFrequencies(ByVal userID As String) As List(Of BudgeteerObjects.Frequency)

        Dim frequencyList As List(Of BudgeteerObjects.Frequency) = New List(Of BudgeteerObjects.Frequency)
        Dim frequencyData As BudgeteerDAL.Frequencies = New BudgeteerDAL.Frequencies

        If String.IsNullOrEmpty(Trim(userID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                frequencyList = frequencyData.getFrequenciesByUserID(dTran, userID)
                dTran.Commit()
                Return frequencyList
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function addFrequency(ByVal frequency As BudgeteerObjects.Frequency) As Int64

        Dim frequencyData As BudgeteerDAL.Frequencies = New BudgeteerDAL.Frequencies
        Dim result As Int64 = Nothing
        Dim existingFrequencies As List(Of BudgeteerObjects.Frequency) = New List(Of BudgeteerObjects.Frequency)

        If frequency Is Nothing Then
            Throw New ArgumentException("Frequency is required")
        End If

        If String.IsNullOrEmpty(Trim(frequency.Description)) Then
            Throw New ArgumentException("Description is required")
        Else
            If frequency.Description.Length() > BudgeteerObjects.Frequency.MaxFieldLength.Description Then
                Throw New ArgumentException("Frequency Description cannot exceed " + BudgeteerObjects.Frequency.MaxFieldLength.Description.ToString() + " characters")
            End If
        End If

        If frequency.Months < 0 Then
            Throw New ArgumentException("Negative months are not allowed")
        End If

        If String.IsNullOrEmpty(Trim(frequency.UserID)) Then
            Throw New ArgumentException("User ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Unique Constraints
                existingFrequencies = frequencyData.getFrequenciesByUserID(dTran, frequency.UserID)
                If existingFrequencies IsNot Nothing Then
                    For Each existingFrequency As BudgeteerObjects.Frequency In existingFrequencies
                        If String.Compare(Trim(existingFrequency.Description), Trim(frequency.Description), True) = 0 Then
                            Throw New ArgumentException("A Frequency with this description already exists")
                        End If
                    Next
                End If
                result = frequencyData.addFrequency(dTran, frequency)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function updateFrequency(ByVal frequency As BudgeteerObjects.Frequency) As Integer

        Dim frequencyData As BudgeteerDAL.Frequencies = New BudgeteerDAL.Frequencies
        Dim result As Integer = Nothing
        Dim existingFrequencies As List(Of BudgeteerObjects.Frequency) = New List(Of BudgeteerObjects.Frequency)

        If frequency Is Nothing Then
            Throw New ArgumentException("Frequency is required")
        End If

        If frequency.FrequencyID = Nothing Then
            Throw New ArgumentException("Frequency ID is required")
        End If

        If String.IsNullOrEmpty(Trim(frequency.Description)) Then
            Throw New ArgumentException("Description is required")
        Else
            If frequency.Description.Length() > BudgeteerObjects.Frequency.MaxFieldLength.Description Then
                Throw New ArgumentException("Frequency Description cannot exceed " + BudgeteerObjects.Frequency.MaxFieldLength.Description.ToString() + " characters")
            End If
        End If

        If frequency.Months < 0 Then
            Throw New ArgumentException("Months must be a positive number")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Unique Constraints
                existingFrequencies = frequencyData.getFrequenciesByUserID(dTran, frequency.UserID)
                For Each existingFrequency As BudgeteerObjects.Frequency In existingFrequencies
                    If existingFrequency.FrequencyID = frequency.FrequencyID Then
                        Continue For
                    End If
                    If String.Compare(Trim(existingFrequency.Description), Trim(frequency.Description), True) = 0 Then
                        Throw New ArgumentException("A Frequency with this description already exists")
                    End If
                Next
                result = frequencyData.updateFrequency(dTran, frequency)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

    Public Function deleteFrequency(ByVal frequencyID As ULong) As Integer

        Dim frequencyData As BudgeteerDAL.Frequencies = New BudgeteerDAL.Frequencies
        Dim result As Integer = Nothing
        Dim budgetData As BudgeteerDAL.Budgets = New BudgeteerDAL.Budgets
        Dim existingBudgets As Integer = 0

        If frequencyID = Nothing Then
            Throw New ArgumentException("Frequency ID is required")
        End If

        Using dTran As DistributedTransaction = New DistributedTransaction(Utils.ConnectionString)
            Try
                ' Delete Restrict Constraints
                ' Check for Budgets with this subcategoryID
                existingBudgets = budgetData.getBudgetCountByFrequencyID(dTran, frequencyID)
                If existingBudgets > 0 Then
                    Throw New ArgumentException("Unable to delete this frequency. This frequency is used by existing budgets.")
                End If
                result = frequencyData.deleteFrequency(dTran, frequencyID)
                dTran.Commit()
                Return result
            Catch ex As Exception
                dTran.Rollback()
                Throw (ex)
            End Try
        End Using

    End Function

End Class
