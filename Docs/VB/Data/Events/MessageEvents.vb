Imports DevExpress.DevAV.Chat.Model

Namespace DevExpress.DevAV.Chat.Events

    Public MustInherit Class MessageEvent
        Public ReadOnly Property Id() As Long
        Public Overridable Sub Apply(ByVal entity As Message)
            ' do nothing 
        End Sub
    End Class
    '
    Public Class MessageDeleted
        Inherits MessageEvent

        Public Overrides Sub Apply(ByVal entity As Message)
            entity.SentOrUpdated = DateTime.Now
            entity.Text = String.Empty
            entity.MarkAsDeleted()
        End Sub
    End Class
    Public Class MessageLiked
        Inherits MessageEvent

        Public Overrides Sub Apply(ByVal entity As Message)
            entity.MarkAsLiked()
        End Sub
    End Class
    Public Class MessageTextChanged
        Inherits MessageEvent

        Public ReadOnly Property Text() As String
        Public Overrides Sub Apply(ByVal entity As Message)
            entity.SentOrUpdated = DateTime.Now
            entity.Text = Text
            entity.MarkAsEdited()
        End Sub
    End Class
End Namespace