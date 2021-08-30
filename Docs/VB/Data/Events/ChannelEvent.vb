Imports System
Imports System.ComponentModel
Imports System.Threading.Tasks

Namespace DevExpress.DevAV.Chat.Events

    Public MustInherit Class ChannelEvent
        Protected Sub New(ByVal channel As IChannel)
            Me.Channel = channel
        End Sub
        Public ReadOnly Property Channel() As IChannel
        Public ReadOnly Property UserName() As String
            Get
                Return Channel.UserName
            End Get
        End Property
    End Class
    '
    Public Class CredentialsRequiredEvent
        Inherits ChannelEvent

        Public ReadOnly Property Salt() As String
        Public Sub SetAccessTokenQuery(ByVal query As Task(Of String))
            ' some code 
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Function GetAccessTokenQuery() As Task(Of String)
            ' some code
        End Function
    End Class
    Public Class ChannelReadyEvent
        Inherits ChannelEvent

        Public Sub New(ByVal channel As IChannel)
            MyBase.New(channel)
        End Sub
    End Class
End Namespace