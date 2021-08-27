Imports DevExpress.DevAV.Chat
Imports DevExpress.DevAV.Chat.Events
Imports DevExpress.DevAV.Chat.Model
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DXHtmlMessengerSample.ViewModels
    Public Class ContactsViewModel
        Inherits ChannelViewModel
        Public Sub New()
            MyBase.New()
            Contacts = New Contact() {}
        End Sub
        Protected Overrides Sub OnConnected(ByVal channel As IChannel)
            MyBase.OnConnected(channel)
            channel.Subscribe(AddressOf OnContactEvents)
        End Sub
        Protected Overrides Async Sub OnChannelReady()
            Dim channelContacts = Await Channel.GetContacts()
            Await DispatcherService?.BeginInvoke(Sub() Contacts = channelContacts)
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
        Public Overridable Property SelectedContact() As Contact
        Protected Sub OnContactsChanged()
            If SelectedContact Is Nothing Then
                SelectedContact = Contacts.FirstOrDefault()
            Else
                Dim id As Long = SelectedContact.ID
                SelectedContact = If(Contacts.Where(Function(x) x.ID = id).FirstOrDefault(), Contacts.FirstOrDefault())
            End If
        End Sub
        Protected Sub OnSelectedContactChanged()
            If SelectedContact IsNot Nothing Then
                Messenger.Default.Send(SelectedContact)
            End If
        End Sub
        Public Async Sub ShowContact(ByVal contact As Contact)
            Dim contactInfo = Await Channel.GetUserInfo(contact.ID)
            MessengerViewModel.ShowContactInfo(Me, contactInfo)
        End Sub
    End Class
End Namespace