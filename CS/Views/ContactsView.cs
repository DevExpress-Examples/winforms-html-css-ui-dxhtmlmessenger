namespace DXHtmlMessengerSample.Views {
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Utils.Html.Internal;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Tile;
    using DXHtmlMessengerSample.ViewModels;

    public partial class ContactsView : XtraUserControl {
        public ContactsView() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                InitializeStyles();
                InitializeBindings();
            }
        }
        void InitializeStyles() {
            Styles.SearchPanel.Apply(searchPanel);
            contactsTileView.HtmlImages = DXHtmlMessenger.SvgImages;
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<ContactsViewModel>();
            // Bind the contacts
            fluent.SetBinding(gridControl, gc => gc.DataSource, x => x.Contacts);
            fluent.SetBinding(contactsTileView, tv => tv.FocusedRowObject, x => x.SelectedContact);
            // We need update our contacts list when the ViewModel detect changes
            fluent.SetTrigger(x => x.Contacts, contacts => contactsTileView.RefreshData());
            // Bind life-cycle events
            fluent.WithEvent(this, nameof(HandleCreated))
                .EventToCommand(x => x.OnCreate);
            fluent.WithEvent(this, nameof(HandleDestroyed))
                .EventToCommand(x => x.OnDestroy);
        }
        void OnContactItemTemplate(object sender, TileViewCustomItemTemplateEventArgs e) {
            Styles.Contact.Apply(e.HtmlTemplate);
        }
        void OnContactItemTemplateCustomize(object sender, TileViewItemCustomizeEventArgs e) {
            var contact = contactsTileView.GetRow(e.RowHandle) as Contact;
            if(contact != null) {
                var statusBadge = e.HtmlBlockInfo.FindElementById("statusBadge");
                if(statusBadge != null)
                    statusBadge.SetActiveState(!contact.IsInactive);
                if(!contact.HasUnreadMessages) {
                    var unreadBadge = e.HtmlBlockInfo.FindElementById("unreadBadge");
                    if(unreadBadge != null)
                        unreadBadge.Hidden = true;
                }
            }
        }
        sealed class Styles {
            public static Style SearchPanel = new SearchPanelStyle();
            public static Style Contact = new ContactStyle();
            //
            sealed class SearchPanelStyle : Style { }
            sealed class ContactStyle : Style { }
        }
    }
}