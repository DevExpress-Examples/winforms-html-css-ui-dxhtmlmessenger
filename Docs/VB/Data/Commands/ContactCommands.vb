Imports System
Imports DevExpress.DevAV.Chat.Model


Namespace DevExpress.DevAV.Chat.Commands

    Public MustInherit Class ContactCommand
        Public ReadOnly Property Contact() As Contact
    End Class
    '
    Public Class AddMessage
        Inherits ContactCommand

        Public Sub New(ByVal contact As Contact)
            MyBase.New(contact)
        End Sub
        Public ReadOnly Property Message() As String
        Public ReadOnly Property Sent() As DateTime
    End Class
    Public Class ReadMessages
        Inherits ContactCommand

        Public Sub New(ByVal contact As Contact)
            MyBase.New(contact)
        End Sub
    End Class
End Namespace