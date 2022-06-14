Imports DevExpress.Utils.Html
Imports DevExpress.Utils.Html.Internal
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Items
Imports DXHtmlMessengerSample.ViewModels

Namespace DXHtmlMessengerSample.Views
    Partial Public Class MessagesView
        Inherits XtraUserControl
        Public Sub New()
            InitializeComponent()
            If Not mvvmContext.IsDesignMode Then
                InitializeStyles()
                InitializeBindings()
                InitializeMessageEdit()
            End If
        End Sub
        Sub InitializeStyles()
            Styles.Menu.Apply(messageMenuPopup)
            Styles.Toolbar.Apply(toolbarPanel)
            Styles.TypingBox.Apply(typingBox)
            Styles.NoMessages.Apply(messagesItemsView.EmptyViewHtmlTemplate)
            messagesItemsView.HtmlImages = DXHtmlMessenger.SvgImages
        End Sub
        Sub InitializeBindings()
            Dim fluent = mvvmContext.OfType(Of MessagesViewModel)()
            ' Bind the messages and contact
            fluent.SetBinding(gridControl, Function(gc) gc.DataSource, Function(x) x.Messages)
            fluent.SetBinding(messagesItemsView, Function(mv) mv.FocusedRowObject, Function(x) x.SelectedMessage)
            fluent.SetBinding(toolbarPanel, Function(tp) tp.DataContext, Function(x) x.Contact)
            ' We need update chat when the ViewModel detect changes
            fluent.SetTrigger(Function(x) x.Messages, Sub(contacts) messagesItemsView.RefreshData())
            fluent.SetTrigger(Function(x) x.Contact, Sub(contact) messagesItemsView.MoveLast())
            ' Bind life-cycle events
            fluent.WithEvent(Me, NameOf(HandleCreated)).EventToCommand(Sub(x) x.OnCreate())
            fluent.WithEvent(Me, NameOf(HandleDestroyed)).EventToCommand(Sub(x) x.OnDestroy())
            ' Bind toolbar elements
            fluent.BindCommandToElement(toolbarPanel, "btnPhoneCall", Sub(x) x.PhoneCall())
            fluent.BindCommandToElement(toolbarPanel, "btnVideoCall", Sub(x) x.VideoCall())
            fluent.BindCommandToElement(toolbarPanel, "btnContact", Sub(x) x.ShowContact())
            fluent.BindCommandToElement(toolbarPanel, "btnUser", Sub(x) x.ShowUser())
            ' Bind typingBox elements
            fluent.BindCommandToElement(typingBox, "btnSend", Sub(x) x.SendMessage())
            fluent.WithKey(messageEdit, Keys.Control Or Keys.Enter).KeyToCommand(Sub(x) x.SendMessage())
            ' Bind editors
            fluent.SetObjectDataSourceBinding(messageBindingSource, Sub(x) x.Update())
            ' Bind context items
            fluent.BindCommandToElement(messageMenuPopup, "miLike", Sub(x) x.LikeMessage())
            fluent.BindCommandToElement(messageMenuPopup, "miCopy", Sub(x) x.CopyMessage())
            fluent.BindCommandToElement(messageMenuPopup, "miCopyText", Sub(x) x.CopyMessageText())
            fluent.BindCommandToElement(messageMenuPopup, "miDelete", Sub(x) x.DeleteMessage())
            ' Bind popup menu showing/hiding
            AddHandler messagesItemsView.ElementMouseClick, AddressOf OnMessagesViewElementMouseClick
            AddHandler messagesItemsView.TopRowPixelChanged, AddressOf OnMessagesTopRowPixelChanged
            AddHandler messageMenuPopup.Hidden, AddressOf OnMessageMenuPopupHidden
        End Sub
        Dim activeMoreStyle As CssStyle
        Sub OnMessagesViewElementMouseClick(ByVal sender As Object, ByVal e As ItemsViewHtmlElementMouseEventArgs)
            If e.ElementId = "btnMore" Then
                activeMoreStyle = e.Element.Style
                activeMoreRowHandle = e.RowHandle
                activeMoreStyle.SetProperty("opacity", "1")
            End If
            If e.ElementId = "btnMore" Or e.ElementId = "btnLike" Then
                ShowMenu(e)
            End If
        End Sub
        Sub ShowMenu(ByVal e As ItemsViewHtmlElementMouseEventArgs)
            Dim size = ScaleDPI.ScaleSize(New Size(212, 180))
            Dim location = New Point(e.Bounds.X - (size.Width - e.Bounds.Width) \ 2, e.Bounds.Y - size.Height + ScaleDPI.ScaleVertical(8))
            Dim menuScreenBounds = gridControl.RectangleToScreen(New Rectangle(location, size))
            messageMenuPopup.Show(gridControl, menuScreenBounds)
        End Sub
        Sub OnMessagesTopRowPixelChanged(ByVal sender As Object, ByVal e As EventArgs)
            messageMenuPopup.Hide()
        End Sub
        Dim activeMoreRowHandle As Integer?
        Sub OnMessageMenuPopupHidden(ByVal sender As Object, ByVal e As EventArgs)
            activeMoreStyle = Nothing
            If activeMoreRowHandle.HasValue Then
                messagesItemsView.RefreshRow(activeMoreRowHandle.Value)
            End If
            activeMoreRowHandle = Nothing
        End Sub
        Sub InitializeMessageEdit()
            Dim autoHeightEdit = TryCast(messageEdit, IAutoHeightControlEx)
            autoHeightEdit.AutoHeightEnabled = True
            AddHandler autoHeightEdit.HeightChanged, AddressOf OnMessageHeightChanged
        End Sub
        Sub OnMessageHeightChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim contentSize = typingBox.GetContentSize()
            typingBox.Height = contentSize.Height
        End Sub
        Sub OnQueryItemTemplate(ByVal sender As Object, ByVal e As QueryItemTemplateEventArgs) Handles messagesItemsView.QueryItemTemplate
            Dim message = TryCast(e.Row, DevExpress.DevAV.Chat.Model.Message)
            If message Is Nothing Then
                Return
            End If
            If message.IsOwnMessage Then
                Styles.MyMessage.Apply(e.Template)
            Else
                Styles.Message.Apply(e.Template)
            End If
            Dim fluent = mvvmContext.OfType(Of MessagesViewModel)()
            fluent.ViewModel.OnMessageRead(message)
        End Sub
        Sub OnCustomizeItem(ByVal sender As Object, ByVal e As CustomizeItemArgs) Handles messagesItemsView.CustomizeItem
            Dim message = TryCast(e.Row, DevExpress.DevAV.Chat.Model.Message)
            If message Is Nothing OrElse message.IsFirstMessageOfBlock Then
                Return
            End If
            If message.IsLiked Then
                Dim btnLike = e.Element.FindElementById("btnLike")
                Dim btnMore = e.Element.FindElementById("btnMore")
                If btnLike IsNot Nothing And btnMore IsNot Nothing Then
                    btnLike.Hidden = False
                    btnMore.Hidden = True
                End If
            End If
            If message.IsFirstMessageOfBlock Then
                Return
            End If
            If Not message.IsOwnMessage Then
                Dim avatar = e.Element.FindElementById("avatar")
                If avatar IsNot Nothing Then
                    avatar.Style.SetVisibility(CssVisibility.Hidden)
                End If
            End If
            Dim name = e.Element.FindElementById("name")
            If name IsNot Nothing Then
                name.Hidden = True
            End If
            If Not message.IsFirstMessageOfReply Then
                Dim sent = e.Element.FindElementById("sent")
                If sent IsNot Nothing Then
                    sent.Hidden = True
                End If
            End If
        End Sub
        Private NotInheritable Class Styles
            Public Shared Toolbar As Style = New ToolbarStyle()
            Public Shared Message As Style = New MessageStyle()
            Public Shared MyMessage As Style = New MyMessageStyle()
            Public Shared NoMessages As Style = New NoMessagesStyle()
            Public Shared Menu As Style = New MenuStyle()
            Public Shared TypingBox As Style = New TypingBoxStyle()
            '
            Private NotInheritable Class ToolbarStyle
                Inherits Style
            End Class
            Private NotInheritable Class MessageStyle
                Inherits Style
            End Class
            Private NotInheritable Class MyMessageStyle
                Inherits Style
            End Class
            Private NotInheritable Class MenuStyle
                Inherits Style
            End Class
            Private NotInheritable Class TypingBoxStyle
                Inherits Style
            End Class
            Private NotInheritable Class NoMessagesStyle
                Inherits Style
            End Class
        End Class
    End Class
End Namespace