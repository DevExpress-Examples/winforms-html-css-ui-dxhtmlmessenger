Imports System
Imports System.ComponentModel

Namespace DevExpress.DevAV.Chat.Model

    Public Class Message
        <Browsable(False)>
        Public ReadOnly Property ID() As Long
        Public ReadOnly Property Owner() As Contact
        Public ReadOnly Property Text() As String
        Public ReadOnly Property SentOrUpdated() As DateTime
        Public ReadOnly Property StatusText() As String
        <Browsable(False)>
        Public ReadOnly Property IsEdited() As Boolean
        <Browsable(False)>
        Public ReadOnly Property IsDeleted() As Boolean
        <Browsable(False)>
        Public ReadOnly Property IsLiked() As Boolean
        <Browsable(False)>
        Public ReadOnly Property IsOwnMessage() As Boolean
        <Browsable(False)>
        Public ReadOnly Property IsFirstMessageOfReply() As Boolean
        <Browsable(False)>
        Public ReadOnly Property IsFirstMessageOfBlock() As Boolean
    End Class
End Namespace