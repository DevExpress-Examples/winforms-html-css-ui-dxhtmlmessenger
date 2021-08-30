Imports System
Imports DevExpress.DevAV.Chat.Model

Namespace DevExpress.DevAV.Chat.Events

    Public MustInherit Class ContactEvent
        Public ReadOnly Property Id() As Long
        Public MustOverride Sub Apply(ByVal contact As Contact)
    End Class
    '
    Public Class StatusChanged
        Inherits ContactEvent

        Public ReadOnly Property Status() As Contact.Status
        Public ReadOnly Property LastOnline() As DateTime
        Public Overrides Sub Apply(ByVal contact As Contact)
            contact.StatusCore = Status
            contact.LastOnline = If(Status = Contact.Status.Inactive, LastOnline, DateTime.MinValue)
        End Sub
    End Class
    Public Class UnreadChanged
        Inherits ContactEvent

        Public ReadOnly Property UnreadCount() As Integer
        Public Overrides Sub Apply(ByVal contact As Contact)
            contact.UnreadCount += UnreadCount
        End Sub
    End Class
    Public Class AllMessagesRead
        Inherits ContactEvent

        Public Sub New(ByVal id As Long)
            MyBase.New(id)
        End Sub
        Public Overrides Sub Apply(ByVal contact As Contact)
            contact.UnreadCount = 0
        End Sub
    End Class
    Public Class NewMessages
        Inherits ContactEvent

        Public Sub New(ByVal id As Long)
            MyBase.New(id)
        End Sub
        Public Overrides Sub Apply(ByVal entity As Contact)
            ' do nothing 
        End Sub
    End Class
End Namespace