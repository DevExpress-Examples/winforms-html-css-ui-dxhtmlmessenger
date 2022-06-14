namespace DXHtmlMessengerSample.Views {
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using DevExpress.Data;
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Utils.Html;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Base;
    using DevExpress.XtraGrid.Views.Tile;
    using DXHtmlMessengerSample.ViewModels;

    public partial class ContactsView : XtraUserControl {
        public ContactsView() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                InitializeStyles();
                InitializeBindings();
                InitializeBehavior();
                InitializeMenusAndTooltips();
            }
        }
        void InitializeStyles() {
            Styles.SearchPanel.Apply(searchPanel);
            Styles.ContactMenu.Apply(contactMenuPopup);
            Styles.ContactTooltip.Apply(contactTooltip);
            contactsTileView.HtmlImages = DXHtmlMessenger.SvgImages;
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<ContactsViewModel>();
            // Bind the contacts
            fluent.SetBinding(gridControl, gc => gc.DataSource, x => x.Contacts);
            fluent.WithEvent<TileView, FocusedRowObjectChangedEventArgs>(contactsTileView, nameof(ColumnView.FocusedRowObjectChanged))
                .SetBinding(x => x.SelectedContact,
                    (args) => args.Row as Contact,
                    (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));
            // We need update our contacts list when the ViewModel detect changes
            fluent.SetTrigger(x => x.Contacts, contacts => contactsTileView.RefreshData());
            // Bind life-cycle events
            fluent.WithEvent(this, nameof(HandleCreated))
                .EventToCommand(x => x.OnCreate);
            fluent.WithEvent(this, nameof(HandleDestroyed))
                .EventToCommand(x => x.OnDestroy);
            // Bind context items
            fluent.BindCommandToElement(contactMenuPopup, "miClearConversation", x => x.ClearConversation);
            fluent.BindCommandToElement(contactMenuPopup, "miCopyContact", x => x.CopyContact);
        }
        void OnContactTooltipViewModelSet(object sender, DevExpress.Utils.MVVM.ViewModelSetEventArgs e) {
            var fluent = contactTooltip.OfType<ContactViewModel>();
            fluent.BindCommand("lnkEmail", x => x.MailTo);
            fluent.BindCommand("btnPhoneCall", x => x.PhoneCall);
            fluent.BindCommand("btnVideoCall", x => x.VideoCall);
            fluent.BindCommand("btnMessage", x => x.TextMessage);
        }
        void InitializeBehavior() {
            // Setup default sorting for contacts
            var colLastActivity = contactsTileView.Columns["LastActivity"];
            if(colLastActivity != null)
                contactsTileView.SortInfo.Add(colLastActivity, ColumnSortOrder.Descending);
            // Setup search by user name only
            searchControl.QueryIsSearchColumn += OnQueryIsSearchColumn;
        }
        void OnQueryIsSearchColumn(object sender, QueryIsSearchColumnEventArgs e) {
            e.IsSearchColumn = (e.FieldName == "UserName");
        }
        void InitializeMenusAndTooltips() {
            // Bind popup menu showing/hiding
            contactsTileView.MouseUp += OnContactsMouseUp;
            contactsTileView.MouseDown += OnContactsMouseDown;
            // Bind tooltip showing/hiding
            contactsTileView.HtmlElementMouseOver += OnContactsHtmlElementMouseOver;
            contactsTileView.PositionChanged += OnContactsPositionChanged;
            contactTooltip.Hidden += ContactTooltip_Hidden;
        }
        int? activeInfoRowHandle;
        void ContactTooltip_Hidden(object sender, EventArgs e) {
            activeInfoStyle = null;
            if(activeInfoRowHandle.HasValue)
                contactsTileView.RefreshRow(activeInfoRowHandle.Value);
            activeInfoRowHandle = null;
        }
        CssStyle activeInfoStyle;
        async void OnContactsHtmlElementMouseOver(object sender, TileViewHtmlElementMouseEventArgs e) {
            if(e.ElementId == "info") {
                activeInfoStyle = e.Element.Style;
                activeInfoRowHandle = e.RowHandle;
                await System.Threading.Tasks.Task.Delay(500);
                if(!e.Bounds.Contains(gridControl.PointToClient(MousePosition)))
                    return;
                if(activeInfoStyle != null)
                    activeInfoStyle.SetProperty("opacity", "0.5");
                var fluent = mvvmContext.OfType<ContactsViewModel>();
                var tooltipViewModel = await fluent.ViewModel.EnsureTooltipViewModel(e.Row as Contact);
                if(!contactTooltip.IsViewModelCreated)
                    contactTooltip.SetViewModel(typeof(ContactViewModel), tooltipViewModel);
                var size = ScaleDPI.ScaleSize(new Size(352, 360));
                var location = new Point(
                    e.Bounds.Right - ScaleDPI.ScaleHorizontal(6),
                    e.Bounds.Y + ScaleDPI.ScaleHorizontal(8) - (size.Height - e.Bounds.Height) / 2);
                var tooltipScreenBounds = gridControl.RectangleToScreen(new Rectangle(location, size));
                contactTooltip.Show(gridControl, tooltipScreenBounds);
            }
        }
        void OnContactsPositionChanged(object sender, EventArgs e) {
            contactTooltip.Hide();
        }
        void OnContactsMouseDown(object sender, MouseEventArgs e) {
            var args = DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e);
            if(e.Button == MouseButtons.Right) {
                var hitInfo = contactsTileView.CalcHitInfo(e.Location);
                if(hitInfo.HitTest == TileControlHitTest.Item)
                    args.Handled = true;
            }
        }
        void OnContactsMouseUp(object sender, MouseEventArgs e) {
            var args = DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e);
            if(e.Button == MouseButtons.Right) {
                var hitInfo = contactsTileView.CalcHitInfo(e.Location);
                if(hitInfo.HitTest == TileControlHitTest.Item) {
                    var size = ScaleDPI.ScaleSize(new Size(212, 130));
                    var location = new Point(e.X - size.Width / 2,
                        e.Y - size.Height + ScaleDPI.ScaleVertical(8));
                    var menuScreenBounds = gridControl.RectangleToScreen(new Rectangle(location, size));
                    contactMenuPopup.Show(gridControl, menuScreenBounds);
                    args.Handled = true;
                }
            }
        }
        void OnContactItemTemplate(object sender, TileViewCustomItemTemplateEventArgs e) {
            Styles.Contact.Apply(e.HtmlTemplate);
        }
        void OnContactItemTemplateCustomize(object sender, TileViewItemCustomizeEventArgs e) {
            var contact = contactsTileView.GetRow(e.RowHandle) as Contact;
            if(contact != null) {
                var statusBadge = e.HtmlElement.FindElementById("statusBadge");
                if(statusBadge != null && !contact.IsInactive)
                    statusBadge.Style.SetBackgroundColor("@Green");
                if(!contact.HasUnreadMessages) {
                    var unreadBadge = e.HtmlElement.FindElementById("unreadBadge");
                    if(unreadBadge != null)
                        unreadBadge.Hidden = true;
                }
            }
        }
        sealed class Styles {
            public static Style SearchPanel = new SearchPanelStyle();
            public static Style Contact = new ContactStyle();
            public static Style ContactMenu = new ContactMenuStyle();
            public static Style ContactTooltip = new ContactTooltipStyle();
            //
            sealed class SearchPanelStyle : Style { }
            sealed class ContactStyle : Style { }
            sealed class ContactMenuStyle : Style {
                public ContactMenuStyle() : base(null, "Menu") { }
            }
            sealed class ContactTooltipStyle : Style { }
        }
    }
}