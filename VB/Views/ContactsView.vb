Imports DevExpress.DevAV.Chat.Model
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Tile
Imports DXHtmlMessengerSample.ViewModels

Namespace DXHtmlMessengerSample.Views
    Partial Public Class ContactsView
        Inherits XtraUserControl
        Public Sub New()
            InitializeComponent()
            If Not mvvmContext.IsDesignMode Then
                InitializeStyles()
                InitializeBindings()
                InitializeBehavior()
                InitializeMenusAndTooltips()
            End If
        End Sub
        Sub InitializeBehavior()
            ' Setup default sorting for contacts
            Dim colLastActivity = contactsTileView.Columns("LastActivity")
            If colLastActivity IsNot Nothing Then
                contactsTileView.SortInfo.Add(colLastActivity, DevExpress.Data.ColumnSortOrder.Descending)
            End If
        End Sub
        Sub InitializeStyles()
            Styles.SearchPanel.Apply(searchPanel)
            Styles.ContactMenu.Apply(contactMenuPopup)
            Styles.ContactTooltip.Apply(contactTooltip)
            contactsTileView.HtmlImages = DXHtmlMessenger.SvgImages
        End Sub
        Sub InitializeBindings()
            Dim fluent = mvvmContext.OfType(Of ContactsViewModel)()
            ' Bind the contacts
            fluent.SetBinding(gridControl, Function(gc) gc.DataSource, Function(x) x.Contacts)
            fluent.WithEvent(Of TileView, FocusedRowObjectChangedEventArgs)(contactsTileView, NameOf(ColumnView.FocusedRowObjectChanged)).SetBinding(
                    Function(x) x.SelectedContact,
                    Function(args) CType(args.Row, Contact),
                    Sub(gView, Entity)
                        gView.FocusedRowHandle = gView.FindRow(Entity)
                    End Sub)
            ' We need update our contacts list when the ViewModel detect changes
            fluent.SetTrigger(Function(x) x.Contacts, Sub(contacts) contactsTileView.RefreshData())
            ' Bind life-cycle events
            fluent.WithEvent(Me, NameOf(HandleCreated)).EventToCommand(Sub(x) x.OnCreate())
            fluent.WithEvent(Me, NameOf(HandleDestroyed)).EventToCommand(Sub(x) x.OnDestroy())
            ' Bind context items
            fluent.BindCommandToElement(contactMenuPopup, "miClearConversation", Sub(x) x.ClearConversation())
            fluent.BindCommandToElement(contactMenuPopup, "miCopyContact", Sub(x) x.CopyContact())
        End Sub
        Sub OnContactTooltipViewModelSet(ByVal sender As Object, ByVal e As DevExpress.Utils.MVVM.ViewModelSetEventArgs) Handles contactTooltip.ViewModelSet
            Dim fluent = contactTooltip.OfType(Of ContactViewModel)()
            fluent.BindCommand("lnkEmail", Sub(x) x.MailTo())
            fluent.BindCommand("btnPhoneCall", Sub(x) x.PhoneCall())
            fluent.BindCommand("btnVideoCall", Sub(x) x.VideoCall())
            fluent.BindCommand("btnMessage", Sub(x) x.TextMessage())
        End Sub
        Sub InitializeMenusAndTooltips()
            ' Bind popup menu showing/hiding
            AddHandler contactsTileView.MouseUp, AddressOf OnContactsMouseUp
            ' Bind tooltip showing
            AddHandler contactsTileView.HtmlElementMouseOver, AddressOf OnContactsHtmlElementMouseOver
        End Sub
        Async Sub OnContactsHtmlElementMouseOver(ByVal sender As Object, ByVal e As TileViewHtmlElementMouseEventArgs)
            If e.ElementId = "info" Then
                Await System.Threading.Tasks.Task.Delay(500)
                If Not e.Bounds.Contains(gridControl.PointToClient(MousePosition)) Then
                    Return
                End If
                Dim fluent = mvvmContext.OfType(Of ContactsViewModel)()
                Dim tooltipViewModel = Await fluent.ViewModel.EnsureTooltipViewModel(TryCast(e.Row, Contact))
                If Not contactTooltip.IsViewModelCreated Then
                    contactTooltip.SetViewModel(GetType(ContactViewModel), tooltipViewModel)
                End If
                Dim size = ScaleDPI.ScaleSize(New Size(352, 360))
                Dim location = New Point(e.Bounds.Right - ScaleDPI.ScaleHorizontal(6), e.Bounds.Y + ScaleDPI.ScaleHorizontal(8) - (size.Height - e.Bounds.Height) \ 2)
                Dim tooltipScreenBounds = gridControl.RectangleToScreen(New Rectangle(location, size))
                contactTooltip.Show(gridControl, tooltipScreenBounds)
            End If
        End Sub
        Sub OnContactsMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim args = DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e)
            If e.Button = MouseButtons.Right Then
                Dim hitInfo = contactsTileView.CalcHitInfo(e.Location)
                If hitInfo.HitTest = TileControlHitTest.Item Then
                    Dim size = ScaleDPI.ScaleSize(New Size(192, 130))
                    Dim location = New Point(e.X - size.Width \ 2, e.Y - size.Height + ScaleDPI.ScaleVertical(8))
                    Dim menuScreenBounds = gridControl.RectangleToScreen(New Rectangle(location, size))
                    contactMenuPopup.Show(gridControl, menuScreenBounds)
                    args.Handled = True
                End If
            End If
        End Sub
        Sub OnContactItemTemplate(ByVal sender As Object, ByVal e As TileViewCustomItemTemplateEventArgs) Handles contactsTileView.CustomItemTemplate
            Styles.Contact.Apply(e.HtmlTemplate)
        End Sub
        Sub OnContactItemTemplateCustomize(ByVal sender As Object, ByVal e As TileViewItemCustomizeEventArgs) Handles contactsTileView.ItemCustomize
            Dim contact = TryCast(contactsTileView.GetRow(e.RowHandle), Contact)
            If contact IsNot Nothing Then
                Dim statusBadge = e.HtmlElementInfo.FindElementById("statusBadge")
                If statusBadge IsNot Nothing AndAlso (Not contact.IsInactive) Then
                    statusBadge.Style.SetBackgroundColor("@Green")
                End If
                If Not contact.HasUnreadMessages Then
                    Dim unreadBadge = e.HtmlElementInfo.FindElementById("unreadBadge")
                    If unreadBadge IsNot Nothing Then
                        unreadBadge.Hidden = True
                    End If
                End If
            End If
        End Sub
        Private NotInheritable Class Styles
            Public Shared SearchPanel As Style = New SearchPanelStyle()
            Public Shared Contact As Style = New ContactStyle()
            Public Shared ContactMenu As Style = New ContactMenuStyle()
            Public Shared ContactTooltip As Style = New ContactTooltipStyle()
            '
            Private NotInheritable Class SearchPanelStyle
                Inherits Style
            End Class
            Private NotInheritable Class ContactStyle
                Inherits Style
            End Class
            Private NotInheritable Class ContactMenuStyle
                Inherits Style
                Public Sub New()
                    MyBase.New(Nothing, "Menu")
                End Sub
            End Class
            Private NotInheritable Class ContactTooltipStyle
                Inherits Style
            End Class
        End Class
    End Class
End Namespace