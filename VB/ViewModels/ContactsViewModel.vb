Imports System.Threading.Tasks
Imports DevExpress.DevAV.Chat
Imports DevExpress.DevAV.Chat.Commands
Imports DevExpress.DevAV.Chat.Events
Imports DevExpress.DevAV.Chat.Model
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DXHtmlMessengerSample.ViewModels
    Public Class ContactsViewModel
        Inherits ChannelViewModel
        Public Sub New()
            MyBase.New()
            Contacts = New Contact() {}
            Messenger.Default.Register(Of Contact)(Me, AddressOf OnContact)
        End Sub
        Protected Overrides Sub OnConnected(ByVal channel As IChannel)
            MyBase.OnConnected(channel)
            channel.Subscribe(AddressOf OnContactEvents)
        End Sub
        Protected Overrides Async Sub OnChannelReady()
            Dim channelContacts = Await Channel.GetContacts()
            Await DispatcherService?.BeginInvoke(Sub() Contacts = channelContacts)
        End Sub
        Sub OnContact(ByVal contact As Contact)
            UpdateSelectedContact(contact)
        End Sub
        Async Sub OnContactEvents(ByVal events As Dictionary(Of Long, ContactEvent))
            Dim [event] As ContactEvent = Nothing
            For Each contact As Contact In Contacts
                If events.TryGetValue(contact.ID, [event]) Then
                    [event].Apply(contact)
                End If
            Next contact
            If events.Count > 0 Then
                Await DispatcherService?.BeginInvoke(AddressOf RaiseContactsChanged)
            End If
        End Sub
        Sub RaiseContactsChanged()
            Me.RaisePropertyChanged(Function(x) x.Contacts)
        End Sub
        Public Overridable Property Contacts() As IReadOnlyCollection(Of Contact)
        Protected Sub OnContactsChanged()
            If SelectedContact Is Nothing Then
                SelectedContact = Contacts.FirstOrDefault()
            Else
                UpdateSelectedContact(SelectedContact)
            End If
        End Sub
        Public Overridable Property SelectedContact() As Contact
        Protected Sub OnSelectedContactChanged()
            NotifyContactSelected(SelectedContact)
            Me.RaiseCanExecuteChanged(Sub(x) x.ClearConversation())
            Me.RaiseCanExecuteChanged(Sub(x) x.CopyContact())
        End Sub
        Private lockContact As Integer = 0
        Sub UpdateSelectedContact(ByVal contact As Contact)
            If lockContact > 0 OrElse contact Is Nothing Then
                Return
            End If
            lockContact += 1
            Try
                Dim id As Long = contact.ID
                SelectedContact = If(Contacts.Where(Function(x) x.ID = id).FirstOrDefault(), Contacts.FirstOrDefault())
            Finally
                lockContact -= 1
            End Try
        End Sub
        Sub NotifyContactSelected(ByVal contact As Contact)
            If lockContact > 0 OrElse contact Is Nothing Then
                Return
            End If
            lockContact += 1
            Try
                Messenger.Default.Send(contact)
            Finally
                lockContact -= 1
            End Try
        End Sub
        Public Async Sub ShowContact(ByVal contact As Contact)
            Dim contactInfo = Await Channel.GetUserInfo(contact.ID)
            MessengerViewModel.ShowContactInfo(Me, contactInfo)
        End Sub
        <Command(False)>
        Public Function HasContact() As Boolean
            Return SelectedContact IsNot Nothing
        End Function
        <Command(CanExecuteMethodName:=NameOf(HasContact))>
        Public Sub ClearConversation()
            If Channel IsNot Nothing Then
                Channel.Send(New ClearConversation(SelectedContact))
            End If
        End Sub
        <Command(CanExecuteMethodName:=NameOf(HasContact))>
        Public Async Sub CopyContact()
            Try
                Dim info = Await Channel.GetUserInfo(SelectedContact.ID)
                Dim contact As String = info.Name & System.Environment.NewLine & $"Email: {info.Email}" & System.Environment.NewLine & $"Phone: {info.MobilePhone}"
                System.Windows.Forms.Clipboard.SetText(contact)
            Catch
            End Try
        End Sub
        Private contactTooltipViewModel As ContactViewModel
        Public Async Function EnsureTooltipViewModel(ByVal contact As Contact) As Task(Of ContactViewModel)
            Dim contactInfo = Await Channel.GetUserInfo(contact.ID)
            If contactTooltipViewModel Is Nothing Then
                contactTooltipViewModel = ContactViewModel.Create(contactInfo)
            Else
                DirectCast(contactTooltipViewModel, ISupportParameter).Parameter = contactInfo
            End If
            Return contactTooltipViewModel
        End Function
    End Class
End Namespace