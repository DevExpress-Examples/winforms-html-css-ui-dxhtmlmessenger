Imports DevExpress.DevAV.Chat.Model
Imports DevExpress.XtraEditors
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
                ' Setup default sorting for contacts
                Dim colLastActivity = contactsTileView.Columns("LastActivity")
                If (colLastActivity IsNot Nothing) Then
                    contactsTileView.SortInfo.Add(colLastActivity, DevExpress.Data.ColumnSortOrder.Descending)
                End If
            End If
        End Sub
        Sub InitializeStyles()
            Styles.SearchPanel.Apply(searchPanel)
            contactsTileView.HtmlImages = DXHtmlMessenger.SvgImages
        End Sub
        Sub InitializeBindings()
            Dim fluent = mvvmContext.OfType(Of ContactsViewModel)()
            ' Bind the contacts
            fluent.SetBinding(gridControl, Function(gc) gc.DataSource, Function(x) x.Contacts)
            fluent.SetBinding(contactsTileView, Function(tv) tv.FocusedRowObject, Function(x) x.SelectedContact)
            ' We need update our contacts list when the ViewModel detect changes
            fluent.SetTrigger(Function(x) x.Contacts, Sub(contacts) contactsTileView.RefreshData())
            ' Bind life-cycle events
            fluent.WithEvent(Me, NameOf(HandleCreated)).EventToCommand(Sub(x) x.OnCreate())
            fluent.WithEvent(Me, NameOf(HandleDestroyed)).EventToCommand(Sub(x) x.OnDestroy())
        End Sub
        Sub OnContactItemTemplate(ByVal sender As Object, ByVal e As TileViewCustomItemTemplateEventArgs) Handles contactsTileView.CustomItemTemplate
            Styles.Contact.Apply(e.HtmlTemplate)
        End Sub
        Sub OnContactItemTemplateCustomize(ByVal sender As Object, ByVal e As TileViewItemCustomizeEventArgs) Handles contactsTileView.ItemCustomize
            Dim contact = TryCast(contactsTileView.GetRow(e.RowHandle), Contact)
            If contact IsNot Nothing Then
                Dim statusBadge = e.HtmlBlockInfo.FindElementById("statusBadge")
                If statusBadge IsNot Nothing Then
                    statusBadge.SetActiveState((Not contact.IsInactive))
                End If
                If Not contact.HasUnreadMessages Then
                    Dim unreadBadge = e.HtmlBlockInfo.FindElementById("unreadBadge")
                    If unreadBadge IsNot Nothing Then
                        unreadBadge.Hidden = True
                    End If
                End If
            End If
        End Sub
        Private NotInheritable Class Styles
            Public Shared SearchPanel As Style = New SearchPanelStyle()
            Public Shared Contact As Style = New ContactStyle()
            '
            Private NotInheritable Class SearchPanelStyle
                Inherits Style
            End Class
            Private NotInheritable Class ContactStyle
                Inherits Style
            End Class
        End Class
    End Class
End Namespace