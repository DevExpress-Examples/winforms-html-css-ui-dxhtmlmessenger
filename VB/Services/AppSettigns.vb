Namespace DXHtmlMessengerSample.Services
    Friend NotInheritable Class AppSettigns
        Implements ISettingsService
        Public Shared Sub Register()
            DevExpress.Mvvm.ServiceContainer.Default.RegisterService(New AppSettigns())
        End Sub
        Public ReadOnly Property CurrentUser() As String Implements ISettingsService.CurrentUser
            Get
                Return Settings.Default.CurrentUser
            End Get
        End Property
        Public ReadOnly Property Theme() As String Implements ISettingsService.Theme
            Get
                Return Settings.Default.Theme
            End Get
        End Property
    End Class
End Namespace