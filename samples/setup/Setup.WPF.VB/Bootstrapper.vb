Imports Caliburn.Micro

Public Class Bootstrapper
    Inherits BootstrapperBase

    Private container As SimpleContainer

    Public Sub New()
        Dim init As Func(Of Type, ILog) = Function(value As Type)
                                              Return New DebugLogger(value)
                                          End Function
        LogManager.GetLog = init
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

Public Class DebugLogger
    Implements ILog

    Public Sub New(type As Type)

    End Sub
    Private Function GetMessage(format As String, args As Object()) As String
        Dim result As String
        result = String.Format("[{0}] {1}", DateTime.Now.ToString("o"), String.Format(format, args))
        Return result
    End Function

    Public Sub ILog_Info(format As String, ParamArray args As Object()) Implements ILog.Info
        Debug.WriteLine(GetMessage(format, args), "Info")
    End Sub

    Public Sub ILog_Warn(format As String, ParamArray args As Object()) Implements ILog.Warn
        Debug.WriteLine(GetMessage(format, args), "Warn")
    End Sub

    Public Sub [Error](exception As Exception) Implements ILog.[Error]
        Debug.WriteLine(GetMessage(exception.ToString(), Nothing), "Error")
    End Sub
End Class

