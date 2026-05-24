Imports Caliburn.Micro

''' <summary>
''' Provides application-specific behavior to supplement the default Application class.
''' </summary>
Public NotInheritable Class App
    Inherits CaliburnApplication

    Private container As WinRTContainer

    Public Sub New()
        Initialize()
    End Sub

    Protected Overrides Sub Configure()
        container = New WinRTContainer()
        container.RegisterWinRTServices()
        container.PerRequest(Of HomeViewModel)()
    End Sub

    Protected Overrides Sub PrepareViewFirst(rootFrame As Frame)
        container.RegisterNavigationService(rootFrame)
    End Sub

    Protected Overrides Sub OnLaunched(args As LaunchActivatedEventArgs)
        If args.PreviousExecutionState = ApplicationExecutionState.Running Then
            Return
        End If

        DisplayRootView(Of HomeView)()
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
