namespace DevExpress.DevAV.Chat.Events {
    using DevExpress.DevAV.Chat.Model;

    public abstract class MessageEvent {
        public long Id {
            get;
        }
        public virtual void Apply(Message entity) {
            /* do nothing */
        }
    }
    //
    public class MessageDeleted : MessageEvent {
        public override void Apply(Message entity) {
            entity.SentOrUpdated = System.DateTime.Now;
            entity.Text = string.Empty;
            entity.MarkAsDeleted();
        }
    }
    public class MessageLiked : MessageEvent {
        public override void Apply(Message entity) {
            entity.MarkAsLiked();
        }
    }
    public class MessageTextChanged : MessageEvent {
        public string Text {
            get;
        }
        public override void Apply(Message entity) {
            entity.SentOrUpdated = System.DateTime.Now;
            entity.Text = Text;
            entity.MarkAsEdited();
        }
    }
}