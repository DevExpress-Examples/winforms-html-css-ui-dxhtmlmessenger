Imports System
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports DevExpress.DevAV.Chat.Commands
Imports DevExpress.DevAV.Chat.Events
Imports DevExpress.DevAV.Chat.Model


Namespace DevExpress.DevAV.Chat

    Public Interface IMessageServer
        Function Connect(ByVal userName As String) As Task(Of IChannel)
    End Interface
    Public Interface IChannel
        Inherits IDisposable

        ' Common
        Sub Subscribe(ByVal onEvent As Action(Of ChannelEvent))
        ReadOnly Property UserName() As String
        Sub Send(ByVal command As ChannelCommand)
        ' Contacts
        Sub Subscribe(ByVal onEvents As Action(Of Dictionary(Of Long, ContactEvent)))
        Function GetUserInfo(ByVal userName As String) As Task(Of UserInfo)
        Function GetUserInfo(ByVal id As Long) As Task(Of UserInfo)
        Function GetContacts() As Task(Of IReadOnlyCollection(Of Contact))
        Sub Send(ByVal command As ContactCommand)
        ' Messages
        Sub Subscribe(ByVal onEvents As Action(Of Dictionary(Of Long, MessageEvent)))
        Function GetHistory(ByVal contact As Contact) As Task(Of IReadOnlyCollection(Of Message))
        Sub Send(ByVal command As MessageCommand)
    End Interface
End Namespace