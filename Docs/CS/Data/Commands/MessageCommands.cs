namespace DevExpress.DevAV.Chat.Commands {
    public abstract class MessageCommand {
        public long MessageId {
            get;
        }
    }
    //
    public class DeleteMessage : MessageCommand {
        public DeleteMessage(long messageID)
            : base(messageID) {
        }
    }
    public class LikeMessage : MessageCommand {
        public LikeMessage(long messageID) 
            : base(messageID) {
        }
    }
}