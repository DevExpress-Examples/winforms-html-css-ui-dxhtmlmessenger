Imports DevExpress.LookAndFeel
Imports DevExpress.Utils.MVVM.Services
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Docking2010.Customization
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports DevExpress.XtraBars.ToolbarForm
Imports DevExpress.XtraEditors
Imports DXHtmlMessengerSample.ViewModels
Imports DXHtmlMessengerSample.Views

Namespace DXHtmlMessengerSample
    Partial Public Class MessengerForm
        Inherits ToolbarForm
        Public Sub New()
            InitializeComponent()
            If Not mvvmContext.IsDesignMode Then
                InitializeStyles()
                InitializeNavigation()
                InitializeBindings()
            End If
        End Sub
        Sub InitializeStyles()
            darkThemeBBI.ImageOptions.SvgImage = DXHtmlMessenger.SvgImages("DarkTheme")
            Styles.ContactInfo.Apply(contactInfoPopup)
            Styles.UserInfo.Apply(userInfoPopup)
        End Sub
        Sub InitializeNavigation()
            ' Flyout Service for all child views
            Dim flyoutService = WindowedDocumentManagerService.CreateFlyoutFormService()
            flyoutService.FormStyle = Sub(form)
                                          Dim flyout = TryCast(form, FlyoutDialog)
                                          flyout.CornerRadius = New DevExpress.Utils.Drawing.CornerRadius(8)
                                          flyout.Properties.Style = FlyoutStyle.Popup
                                          flyout.Properties.Appearance.BorderColor = Color.FromArgb(&H66, Color.Black)
                                      End Sub
            mvvmContext.RegisterDefaultService("Flyout", flyoutService)
            '  Window Service for showing user info
            Dim userInfoDialog = userInfoPopup.CreateWindowService()
            userInfoDialog.ShowMode = WindowService.WindowShowMode.Modal
            userInfoDialog.WindowStyle = Sub(window)
                                             Dim popup = TryCast(window, IPopupWindow)
                                             popup.PopupSize = New Size(516, 306)
                                             popup.DestroyOnHide = False
                                         End Sub
            mvvmContext.RegisterDefaultService("UserInfoDialog", userInfoDialog)
            '  Window Service for showing contact info
            Dim contactInfoFlyout = contactInfoPopup.CreateWindowService()
            contactInfoFlyout.WindowStyle = Sub(window)
                                                Dim popup = TryCast(window, IPopupWindow)
                                                popup.PopupSize = New Size(368, 374)
                                            End Sub
            mvvmContext.RegisterDefaultService("ContactInfoFlyout", contactInfoFlyout)
        End Sub
        Sub InitializeBindings()
            Dim fluent = mvvmContext.OfType(Of MessengerViewModel)()
            ' Bind life-cycle events
            fluent.WithEvent(Me, NameOf(Load)).EventToCommand(Function(x) x.OnLoad())
            fluent.WithEvent(Me, NameOf(FormClosed)).EventToCommand(Sub(x) x.OnClosed())
            ' Bind application title
            fluent.SetBinding(Me, Function(x) x.Text, Function(x) x.Title)
        End Sub
        Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
            MyBase.OnHandleCreated(e)
            Dim fluent = mvvmContext.OfType(Of MessengerViewModel)()
            ' Set the relationship with child views and their ViewModels
            Dim viewModel = fluent.ViewModel
            DevExpress.Utils.MVVM.MVVMContext.SetParentViewModel(Me.contactsView, viewModel)
            DevExpress.Utils.MVVM.MVVMContext.SetParentViewModel(Me.messagesView, viewModel)
        End Sub
        Sub userInfoPopup_ViewModelSet(ByVal sender As Object, ByVal e As DevExpress.Utils.MVVM.ViewModelSetEventArgs) Handles userInfoPopup.ViewModelSet
            Dim fluent = userInfoPopup.OfType(Of UserViewModel)()
            fluent.BindCommand("lnkLogOff", Sub(x) x.LogOff())
            fluent.BindCommand("btnClose", Sub(x) x.Close())
        End Sub
        Sub contactInfoPopup_ViewModelSet(ByVal sender As Object, ByVal e As DevExpress.Utils.MVVM.ViewModelSetEventArgs) Handles contactInfoPopup.ViewModelSet
            Dim fluent = contactInfoPopup.OfType(Of ContactViewModel)()
            fluent.BindCommand("lnkEmail", Sub(x) x.MailTo())
            fluent.BindCommand("btnPhoneCall", Sub(x) x.PhoneCall())
            fluent.BindCommand("btnVideoCall", Sub(x) x.VideoCall())
            fluent.BindCommand("btnMessage", Sub(x) x.TextMessage())
        End Sub
        Dim isDarkTheme As Boolean
        Sub OnDarkThemeClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles darkThemeBBI.ItemClick
            isDarkTheme = Not isDarkTheme
            If isDarkTheme Then
                WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle(SkinStyle.Bezier, SkinSvgPalette.Bezier.ArtHouse)
            Else
                WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle(SkinStyle.Bezier, SkinSvgPalette.Bezier.Default)
            End If
        End Sub
        Private NotInheritable Class Styles
            Public Shared ContactInfo As Style = New ContactInfoStyle()
            Public Shared UserInfo As Style = New UserInfoStyle()
            '
            Private NotInheritable Class ContactInfoStyle
                Inherits Style
            End Class
            Private NotInheritable Class UserInfoStyle
                Inherits Style
            End Class
        End Class
    End Class
End Namespace