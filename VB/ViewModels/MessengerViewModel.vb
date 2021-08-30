Imports System.Threading.Tasks
Imports DevExpress.DevAV.Chat
Imports DevExpress.DevAV.Chat.Commands
Imports DevExpress.DevAV.Chat.Events
Imports DevExpress.DevAV.Chat.Model
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DXHtmlMessengerSample.Services

Namespace DXHtmlMessengerSample.ViewModels
    Public Class MessengerViewModel
        Public Overridable Property Title() As String
        Private dispatcher As IDispatcherService
        Private channel As IChannel
        Public Async Function OnLoad() As Task
            dispatcher = Me.GetRequiredService(Of IDispatcherService)()
            ' Load settings and initialize
            Dim settingsService = Me.GetRequiredService(Of ISettingsService)()
            Dim theme = If(settingsService.Theme, "Light")
            Dim currentUser = If(settingsService.CurrentUser, "John Heart")
            ' Open messenger channel
            Dim messageServer = Me.GetRequiredService(Of IMessageServer)()
            channel = Await messageServer.Create(currentUser)
            channel.Subscribe(AddressOf OnChannelEvent)
            ' Pass the channel into dependent ViewModels
            Messenger.Default.Send(channel)
            Await dispatcher.BeginInvoke(Sub() Title = $"DX HTML MESSENGER (VB) - [{currentUser.ToUpper()}]")
        End Function
        Private AuthCounter As Integer = 0
        Sub OnChannelEvent(ByVal [event] As ChannelEvent)
            Dim credentialsRequired = TryCast([event], CredentialsRequiredEvent)
            If credentialsRequired IsNot Nothing Then
                AuthCounter += 1
                If AuthCounter = 1 Then
                    ' provide the access token from local cache without interaction
                    Dim cacheQuery = QueryAccessTokenFromLocalAuthCache([event].UserName, credentialsRequired.Salt)
                    credentialsRequired.SetAccessTokenQuery(cacheQuery)
                Else
                    ' or query access-token asynchronously for the specific user
                    Dim userQuery = QueryAccessTokenFromUser([event].UserName, credentialsRequired.Salt)
                    credentialsRequired.SetAccessTokenQuery(userQuery)
                End If
            End If
        End Sub
        Function QueryAccessTokenFromLocalAuthCache(ByVal userName As String, ByVal salt As String) As Task(Of String)
            ' simple emulation of local auth cache
            Return Task.FromResult(DevAVEmpployeesInMemoryServer.GetPasswordHash(String.Empty, salt))
        End Function
        Function QueryAccessTokenFromUser(ByVal userName As String, ByVal salt As String) As Task(Of String)
            Dim accessTokenQueryCompletionSource = New TaskCompletionSource(Of String)()
            dispatcher.BeginInvoke(Sub()
                                       Dim signInViewModel = ViewModels.SignInViewModel.Create(userName, salt)
                                       signInViewModel.ShowDialog()
                                       If Not String.IsNullOrEmpty(signInViewModel.AccessToken) Then
                                           accessTokenQueryCompletionSource.SetResult(signInViewModel.AccessToken)
                                       Else
                                           accessTokenQueryCompletionSource.SetCanceled()
                                       End If
                                   End Sub)
            Return accessTokenQueryCompletionSource.Task
        End Function
        Public Sub OnClosed()
            If channel IsNot Nothing Then
                channel.Dispose()
            End If
            channel = Nothing
        End Sub
        Public Sub LogOff()
            If channel IsNot Nothing Then
                channel.Send(New LogOff(channel))
            End If
        End Sub
        Private userViewModelCore As UserViewModel
        <Command(False)>
        Public Sub ShowUserInfo(ByVal userInfo As UserInfo)
            If userViewModelCore IsNot Nothing Then
                ShowPopup(userViewModelCore, userInfo)
            Else
                userViewModelCore = ViewModels.UserViewModel.Create(userInfo)
                ShowPopup(userViewModelCore, userInfo)
            End If
        End Sub
        Private contactViewModelCore As ContactViewModel
        <Command(False)>
        Public Sub ShowContactInfo(ByVal contactInfo As UserInfo)
            If contactViewModelCore IsNot Nothing Then
                ShowPopup(contactViewModelCore, contactInfo)
            Else
                contactViewModelCore = ViewModels.ContactViewModel.Create(contactInfo)
                ShowPopup(contactViewModelCore, contactInfo)
            End If
        End Sub
        Sub ShowPopup(ByVal viewModel As UserInfoViewModel, ByVal info As UserInfo)
            viewModel.SetParentViewModel(Me)
            Dim popup = Me.GetService(Of IWindowService)(viewModel.ServiceKey)
            popup.Show(Nothing, viewModel, info, Me)
        End Sub
        Public Shared Sub ShowUserInfo(ByVal viewModel As Object, ByVal userInfo As UserInfo)
            Dim messenger = POCOViewModelExtensions.GetParentViewModel(Of MessengerViewModel)(viewModel)
            If messenger IsNot Nothing Then
                messenger.ShowUserInfo(userInfo)
            End If
        End Sub
        Public Shared Sub ShowContactInfo(ByVal viewModel As Object, ByVal contactInfo As UserInfo)
            Dim messenger = POCOViewModelExtensions.GetParentViewModel(Of MessengerViewModel)(viewModel)
            If messenger IsNot Nothing Then
                messenger.ShowContactInfo(contactInfo)
            End If
        End Sub
    End Class
End Namespace