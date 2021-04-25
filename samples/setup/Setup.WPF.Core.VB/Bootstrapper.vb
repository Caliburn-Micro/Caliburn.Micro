Imports Caliburn.Micro

Public Class Bootstrapper
    Inherits BootstrapperBase

    Private container As SimpleContainer

    Public Sub New()
        Initialize()
    End Sub

    Protected Overrides Sub Configure()
        container = New SimpleContainer()

        container.Singleton(Of IWindowManager, WindowManager)()

        container.PerRequest(Of ShellViewModel)()
    End Sub

    Protected Overrides Sub OnStartup(sender As Object, e As StartupEventArgs)
        DisplayRootViewFor(Of ShellViewModel)()
    End Sub

    Protected Overrides Function GetInstance(service As Type, key As String) As Object
        Return container.GetInstance(service, key)
    End Function

    Protected Overrides Function GetAllInstances(service As Type) As IEnumerable(Of Object)
        Return container.GetAllInstances(service)
    End Function

    Protected Overrides Sub BuildUp(instance As Object)
        container.BuildUp(instance)
    End Sub
End Class
