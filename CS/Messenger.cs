namespace DXHtmlMessengerSample {
    using System;
    using System.Drawing;
    using DevExpress.Utils.MVVM.Services;
    using DevExpress.XtraBars.Docking2010.Customization;
    using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
    using DevExpress.XtraEditors;
    using DXHtmlMessengerSample.ViewModels;
    using DXHtmlMessengerSample.Views;

    public partial class MessengerForm : XtraForm {
        public MessengerForm() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                Styles.ContactInfo.Apply(contactInfoPopup);
                Styles.UserInfo.Apply(userInfoPopup);
                InitializeNavigation();
                InitializeBindings();
            }
        }
        void InitializeNavigation() {
            // Flyout Service for all child views
            var flyoutService = WindowedDocumentManagerService.CreateFlyoutFormService();
            flyoutService.FormStyle = (form) => {
                var flyout = form as FlyoutDialog;
                flyout.CornerRadius = new DevExpress.Utils.Drawing.CornerRadius(8);
                flyout.Properties.Style = FlyoutStyle.Popup;
                flyout.Properties.Appearance.BorderColor = Color.FromArgb(0x66, Color.Black);
            };
            mvvmContext.RegisterDefaultService("Flyout", flyoutService);
            //  Window Service for showing user info
            var userInfoDialog = userInfoPopup.CreateWindowService();
            userInfoDialog.ShowMode = WindowService.WindowShowMode.Modal;
            userInfoDialog.WindowStyle = (window) => {
                var popup = window as IPopupWindow;
                popup.PopupSize = new Size(400, 300);
                popup.DestroyOnHide = false;
            };
            mvvmContext.RegisterDefaultService("UserInfoDialog", userInfoDialog);
            //  Window Service for showing contact info
            var contactInfoFlyout = contactInfoPopup.CreateWindowService();
            contactInfoFlyout.WindowStyle = (window) => {
                var popup = window as IPopupWindow;
                popup.PopupSize = new Size(400, 300);
            };
            mvvmContext.RegisterDefaultService("ContactInfoFlyout", contactInfoFlyout);
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<MessengerViewModel>();
            // Bind life-cycle events
            fluent.WithEvent(this, nameof(Load))
                .EventToCommand(x => x.OnLoad);
            fluent.WithEvent(this, nameof(FormClosed))
                .EventToCommand(x => x.OnClosed);
            // Bind application title
            fluent.SetBinding(this, x => x.Text, x => x.Title);
        }
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            var fluent = mvvmContext.OfType<MessengerViewModel>();
            // Set the relationship with child views and their ViewModels
            var viewModel = fluent.ViewModel;
            DevExpress.Utils.MVVM.MVVMContext.SetParentViewModel(this.contactsView, viewModel);
            DevExpress.Utils.MVVM.MVVMContext.SetParentViewModel(this.messagesView, viewModel);
        }
        void userInfoPopup_ViewModelSet(object sender, DevExpress.Utils.MVVM.ViewModelSetEventArgs e) {
            var fluent = userInfoPopup.OfType<UserViewModel>();
            fluent.BindCommand("lnkLogOff", x => x.LogOff);
            fluent.BindCommand("btnClose", x => x.Close);
        }
        void contactInfoPopup_ViewModelSet(object sender, DevExpress.Utils.MVVM.ViewModelSetEventArgs e) {
            var fluent = contactInfoPopup.OfType<ContactViewModel>();
            fluent.BindCommand("btnPhoneCall", x => x.PhoneCall);
            fluent.BindCommand("btnVideoCall", x => x.VideoCall);
            fluent.BindCommand("btnMessage", x => x.TextMessage);
        }
        sealed class Styles {
            public static Style ContactInfo = new ContactInfoStyle();
            public static Style UserInfo = new UserInfoStyle();
            //
            sealed class ContactInfoStyle : Style { }
            sealed class UserInfoStyle : Style { }
        }
    }
}