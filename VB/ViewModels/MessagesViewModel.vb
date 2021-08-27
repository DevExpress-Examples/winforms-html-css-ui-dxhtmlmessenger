Imports System.Threading.Tasks
Imports DevExpress.DevAV.Chat
Imports DevExpress.DevAV.Chat.Commands
Imports DevExpress.DevAV.Chat.Events
Imports DevExpress.DevAV.Chat.Model
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DXHtmlMessengerSample.ViewModels
    Public Class MessagesViewModel
        Inherits ChannelViewModel
        Public Sub New()
            MyBase.New()
            Messages = New Message() {}
            Messenger.Default.Register(Of Contact)(Me, AddressOf OnContact)
        End Sub
        Public Overrides Sub OnDestroy()
            MyBase.OnDestroy()
            Messenger.Default.Unregister(Of Contact)(Me, AddressOf OnContact)
        End Sub
        Protected Overrides Sub OnConnected(ByVal channel As IChannel)
            MyBase.OnConnected(channel)
            channel.Subscribe(AddressOf OnMessageEvents)
            channel.Subscribe(AddressOf OnContactEvents)
        End Sub
        Protected Overrides Async Sub OnChannelReady()
            Await LoadMessages(Channel, Contact)
            Await DispatcherService?.BeginInvoke(AddressOf UpdateUIOnChannelReady)
        End Sub
        Async Sub OnContactEvents(ByVal events As Dictionary(Of Long, ContactEvent))
            If Contact IsNot Nothing Then
                Dim [event] As ContactEvent = Nothing
                If events.TryGetValue(Contact.ID, [event]) Then
                    If TypeOf [event] Is UnreadChanged OrElse TypeOf [event] Is NewMessages Then
                        Await LoadMessages(Channel, Contact)
                    End If
                End If
                If events.Count > 0 Then
                    Await DispatcherService?.BeginInvoke(AddressOf RaiseMessagesChanged)
                End If
            End If
        End Sub
        Async Sub OnMessageEvents(ByVal events As Dictionary(Of Long, MessageEvent))
            Dim [event] As MessageEvent = Nothing
            For Each message As Message In Messages
                If events.TryGetValue(message.ID, [event]) Then
                    [event].Apply(message)
                End If
            Next message
            If events.Count > 0 Then
                Await DispatcherService?.BeginInvoke(AddressOf RaiseMessagesChanged)
            End If
        End Sub
        Sub UpdateUIOnChannelReady()
            Me.RaisePropertyChanged(Function(x) x.Messages)
            UpdateActions()
        End Sub
        Sub RaiseMessagesChanged()
            Me.RaisePropertyChanged(Function(x) x.Messages)
        End Sub
        Async Sub OnContact(ByVal contact As Contact)
            Await DispatcherService?.BeginInvoke(Sub() Me.Contact = contact)
            Await LoadMessages(Channel, Me.Contact)
        End Sub
        Async Function LoadMessages(ByVal channel As IChannel, ByVal contact As Contact) As Task
            If channel IsNot Nothing AndAlso contact IsNot Nothing Then
                Dim history = Await channel.GetHistory(contact)
                Await DispatcherService?.BeginInvoke(Sub() Messages = history)
            End If
        End Function
        Public Overridable Property Contact() As Contact
        Protected Sub OnContactChanged()
            UpdateActions()
        End Sub
        Public Function CanExecuteActions() As Boolean
            Return (Channel IsNot Nothing) AndAlso (Contact IsNot Nothing)
        End Function
        Protected Sub UpdateActions()
            Me.RaiseCanExecuteChanged(Sub(x) x.SendMessage())
            Me.RaiseCanExecuteChanged(Sub(x) x.PhoneCall())
            Me.RaiseCanExecuteChanged(Sub(x) x.VideoCall())
            Me.RaiseCanExecuteChanged(Sub(x) x.ShowContact())
            Me.RaiseCanExecuteChanged(Sub(x) x.ShowUser())
        End Sub
        Public Overridable Property Messages() As IReadOnlyCollection(Of Message)
        Private lastMessage As Message
        Protected Sub OnMessagesChanged()
            lastMessage = Messages.LastOrDefault()
        End Sub
        Public Overridable Property MessageText() As String
        Protected Sub OnMessageTextChanged()
            Me.RaiseCanExecuteChanged(Sub(x) x.SendMessage())
        End Sub
        Public Function CanSendMessage() As Boolean
            Return CanExecuteActions() AndAlso Not String.IsNullOrEmpty(MessageText)
        End Function
        Public Sub SendMessage()
            If Channel IsNot Nothing Then
                Channel.Send(New AddMessage(Contact, MessageText))
            End If
            MessageText = Nothing
        End Sub
        Public Sub Update()
            Me.RaiseCanExecuteChanged(Sub(x) x.SendMessage())
        End Sub
        <Command(CanExecuteMethodName:=NameOf(CanExecuteActions))>
        Public Async Sub PhoneCall()
            Dim contactInfo = Await Channel.GetUserInfo(Contact.ID)
            DoCall("Phone Call: " & contactInfo.MobilePhone)
        End Sub
        <Command(CanExecuteMethodName:=NameOf(CanExecuteActions))>
        Public Async Sub VideoCall()
            Dim contactInfo = Await Channel.GetUserInfo(Contact.ID)
            DoCall("Video Call: " & contactInfo.MobilePhone)
        End Sub
        Sub DoCall(ByVal [call] As String)
            Dim msgService = Me.GetRequiredService(Of IMessageBoxService)()
            msgService.ShowMessage([call])
        End Sub
        <Command(CanExecuteMethodName:=NameOf(CanExecuteActions))>
        Public Async Sub ShowContact()
            Dim contactInfo = Await Channel.GetUserInfo(Contact.ID)
            MessengerViewModel.ShowContactInfo(Me, contactInfo)
        End Sub
        <Command(CanExecuteMethodName:=NameOf(CanExecuteActions))>
        Public Async Sub ShowUser()
            Dim userInfo = Await Channel.GetUserInfo(Channel.UserName)
            MessengerViewModel.ShowUserInfo(Me, userInfo)
        End Sub
        Public Overridable Property SelectedMessage() As Message
        Protected Sub OnSelectedMessageChanged()
            Me.RaiseCanExecuteChanged(Sub(x) x.DeleteMessage())
            Me.RaiseCanExecuteChanged(Sub(x) x.CopyMessage())
            Me.RaiseCanExecuteChanged(Sub(x) x.LikeMessage())
        End Sub
        Public Function CanDeleteMessage() As Boolean
            Return (SelectedMessage IsNot Nothing) AndAlso Not SelectedMessage.IsDeleted
        End Function
        Public Sub DeleteMessage()
            If Channel IsNot Nothing Then
                Channel.Send(New DeleteMessage(SelectedMessage.ID))
            End If
        End Sub
        Public Function CanCopyMessage() As Boolean
            Return (SelectedMessage IsNot Nothing) AndAlso Not SelectedMessage.IsDeleted
        End Function
        Public Sub CopyMessage()
            Try
                Dim message As String = "[" & SelectedMessage.StatusText & "] " & SelectedMessage.Owner.UserName & System.Environment.NewLine & SelectedMessage.Text
                System.Windows.Forms.Clipboard.SetText(message)
            Catch
            End Try
        End Sub
        Public Function CanCopyMessageText() As Boolean
            Return (SelectedMessage IsNot Nothing) AndAlso Not SelectedMessage.IsDeleted
        End Function
        Public Sub CopyMessageText()
            Try
                System.Windows.Forms.Clipboard.SetText(SelectedMessage.Text)
            Catch
            End Try
        End Sub
        Public Function CanLike() As Boolean
            Return (SelectedMessage IsNot Nothing) AndAlso Not SelectedMessage.IsLiked
        End Function
        Public Sub LikeMessage()
            If Channel IsNot Nothing Then
                Channel.Send(New LikeMessage(SelectedMessage.ID))
            End If
        End Sub
        <Command(False)>
        Public Sub OnMessageRead(ByVal message As Message)
            If lastMessage IsNot Nothing AndAlso message Is lastMessage Then
                lastMessage = Nothing
                If Channel IsNot Nothing Then
                    Channel.Send(New ReadMessages(Contact))
                End If
            End If
        End Sub
    End Class
End Namespace