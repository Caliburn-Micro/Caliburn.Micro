Imports Caliburn.Micro

Public Class ShellViewModel
    Inherits Screen

    Private _title1 As String

    Public Property Title As String
        Get
            Return _title1
        End Get
        Set
            _title1 = Value
        End Set
    End Property

    Public Sub New()
        Title = "Welcome to Caliburn Micro in WPF using VB and .net 5"
    End Sub
End Class
