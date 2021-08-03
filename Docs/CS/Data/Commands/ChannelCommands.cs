namespace DevExpress.DevAV.Chat.Commands {
    public abstract class ChannelCommand {
        public IChannel Channel {
            get;
        }
    }
    //
    public class LogOff : ChannelCommand {
        public LogOff(IChannel channel) 
            : base(channel) {
        }
    }
}