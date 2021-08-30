Imports System
Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Drawing

Namespace DevExpress.DevAV.Chat.Model

    Public Class Contact
        <Browsable(False)>
        Public ReadOnly Property ID() As Long
        Public ReadOnly Property UserName() As String
        Public ReadOnly Property Avatar() As Image
        <Browsable(False)>
        Public ReadOnly Property LastOnline() As DateTime
        Public ReadOnly Property LastOnlineText() As String
        Public ReadOnly Property UnreadCount() As Integer
        <Browsable(False)>
        Public ReadOnly Property HasUnreadMessages() As Boolean
        Public ReadOnly Property IsInactive() As Boolean
    End Class
End Namespace