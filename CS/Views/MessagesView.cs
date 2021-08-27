namespace DXHtmlMessengerSample.Views {
    using System;
    using System.Drawing;
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Utils.Html;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DXHtmlMessengerSample.ViewModels;

    public partial class MessagesView : XtraUserControl {
        public MessagesView() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                InitializeStyles();
                InitializeBindings();
                InitializeMessageEdit();
            }
        }
        void InitializeStyles() {
            Styles.Menu.Apply(messageMenuPopup);
            Styles.Toolbar.Apply(toolbarPanel);
            Styles.TypingBox.Apply(typingBox);
            messagesItemsView.HtmlImages = DXHtmlMessenger.SvgImages;
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<MessagesViewModel>();
            // Bind the messages and contact
            fluent.SetBinding(gridControl, gc => gc.DataSource, x => x.Messages);
            fluent.SetBinding(messagesItemsView, mv => mv.FocusedRowObject, x => x.SelectedMessage);
            fluent.SetBinding(toolbarPanel, tp => tp.DataContext, x => x.Contact);
            // We need update chat when the ViewModel detect changes
            fluent.SetTrigger(x => x.Messages, contacts => messagesItemsView.RefreshData());
            // Bind life-cycle events
            fluent.WithEvent(this, nameof(HandleCreated))
                .EventToCommand(x => x.OnCreate);
            fluent.WithEvent(this, nameof(HandleDestroyed))
                .EventToCommand(x => x.OnDestroy);
            // Bind toolbar elements
            fluent.BindCommandToElement(toolbarPanel, "btnPhoneCall", x => x.PhoneCall);
            fluent.BindCommandToElement(toolbarPanel, "btnVideoCall", x => x.VideoCall);
            fluent.BindCommandToElement(toolbarPanel, "btnContact", x => x.ShowContact);
            fluent.BindCommandToElement(toolbarPanel, "btnUser", x => x.ShowUser);
            // Bind typingBox elements
            fluent.BindCommandToElement(typingBox, "btnSend", x => x.SendMessage);
            // Bind editors
            fluent.SetObjectDataSourceBinding(messageBindingSource, x => x.Update);
            // Bind context items
            fluent.BindCommandToElement(messageMenuPopup, "miLike", x => x.LikeMessage);
            fluent.BindCommandToElement(messageMenuPopup, "miCopy", x => x.CopyMessage);
            fluent.BindCommandToElement(messageMenuPopup, "miCopyText", x => x.CopyMessageText);
            fluent.BindCommandToElement(messageMenuPopup, "miDelete", x => x.DeleteMessage);
            // Bind popup menu showing/hiding
            messagesItemsView.ElementMouseClick += OnMessagesViewElementMouseClick;
            messageMenuPopup.ElementMouseClick += OnMessageMenuElementMouseClick;
        }
        void OnMessagesViewElementMouseClick(object sender, DevExpress.XtraGrid.Views.Items.ItemsViewHtmlElementMouseEventArgs e) {
            if(e.ElementId == "btnAction") {
                var size = ScaleDPI.ScaleSize(new Size(144, 180));
                var menuBounds = new Rectangle(new Point(e.Bounds.X - (size.Width - e.Bounds.Width) / 2, e.Bounds.Y - size.Height), size);
                messageMenuPopup.Show(gridControl, gridControl.RectangleToScreen(menuBounds));
            }
        }
        void OnMessageMenuElementMouseClick(object sender, HtmlElementMouseEventArgs e) {
            if(IsHandleCreated) BeginInvoke(new Action(messageMenuPopup.Hide));
        }
        void InitializeMessageEdit() {
            var autoHeightEdit = messageEdit as IAutoHeightControlEx;
            autoHeightEdit.AutoHeightEnabled = true;
            autoHeightEdit.HeightChanged += OnMessageHeightChanged;
        }
        void OnMessageHeightChanged(object sender, System.EventArgs e) {
            var contentSize = typingBox.GetContentSize();
            typingBox.Height = contentSize.Height;
        }
        void OnQueryItemTemplate(object sender, DevExpress.XtraGrid.Views.Items.QueryItemTemplateArgs e) {
            var message = e.Row as Message;
            if(message == null)
                return;
            if(message.IsOwnMessage)
                Styles.MyMessage.Apply(e.Template);
            else
                Styles.Message.Apply(e.Template);
            var fluent = mvvmContext.OfType<MessagesViewModel>();
            fluent.ViewModel.OnMessageRead(message);
        }
        void OnCustomizeItem(object sender, DevExpress.XtraGrid.Views.Items.CustomizeItemArgs e) {
            var message = e.Row as Message;
            if(message == null || message.IsFirstMessageOfBlock)
                return;
            if(!message.IsOwnMessage) {
                var avatar = e.ElementInfo.FindElementById("avatar");
                if(avatar != null)
                    avatar.Hidden = true;
            }
            var name = e.ElementInfo.FindElementById("name");
            if(name != null)
                name.Hidden = true;
            if(!message.IsFirstMessageOfReply) {
                var sent = e.ElementInfo.FindElementById("sent");
                if(sent != null)
                    sent.Hidden = true;
            }
        }
        sealed class Styles {
            public static Style Toolbar = new ToolbarStyle();
            public static Style Message = new MessageStyle();
            public static Style MyMessage = new MyMessageStyle();
            public static Style Menu = new MenuStyle();
            public static Style TypingBox = new TypingBoxStyle();
            //
            sealed class ToolbarStyle : Style { }
            sealed class MessageStyle : Style { }
            sealed class MyMessageStyle : Style { }
            sealed class MenuStyle : Style { }
            sealed class TypingBoxStyle : Style { }
        }
    }
}