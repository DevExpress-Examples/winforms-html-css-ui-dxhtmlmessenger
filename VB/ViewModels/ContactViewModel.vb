Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.Chat.Model

Namespace DXHtmlMessengerSample.ViewModels

    Public Class ContactViewModel
        Inherits UserInfoViewModel

        Protected Friend Overrides ReadOnly Property ServiceKey() As String
            Get
                Return "ContactInfoFlyout"
            End Get
        End Property
        Public Shared Function Create(ByVal contact As UserInfo) As ContactViewModel
            Return ViewModelSource.Create(Function() New ContactViewModel(contact))
        End Function
        '
        Protected Sub New(ByVal contact As UserInfo)
            MyBase.New(contact)
        End Sub
        Public Sub MailTo()
            Dim msgService = Me.GetRequiredService(Of IMessageBoxService)()
            msgService.ShowMessage("Mail To: " & Email)
        End Sub
        Public Sub VideoCall()
            Dim msgService = Me.GetRequiredService(Of IMessageBoxService)()
            Dim index As Integer = Email.IndexOf("@"c)
            msgService.ShowMessage("Video Call: " & Email.Substring(0, index))
        End Sub
        Public Sub PhoneCall()
            Dim msgService = Me.GetRequiredService(Of IMessageBoxService)()
            msgService.ShowMessage("Phone Call: " & MobilePhone)
        End Sub
        Public Sub TextMessage()
            CloseDocument()
            Messenger.Default.Send(New Contact(Id, Name, Photo))
        End Sub
    End Class
End Namespace