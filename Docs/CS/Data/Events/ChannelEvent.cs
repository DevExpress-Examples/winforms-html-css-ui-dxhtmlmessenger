using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DevExpress.DevAV.Chat.Events {

    public abstract class ChannelEvent {
        protected ChannelEvent(IChannel channel) {
            this.Channel = channel;
        }
        public IChannel Channel {
            get;
        }
        public string UserName {
            get { return Channel.UserName; }
        }
    }
    //
    public class CredentialsRequiredEvent : ChannelEvent {
        public string Salt {
            get;
        }
        public void SetAccessTokenQuery(Task<string> query) {
            /* some code */
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Task<string> GetAccessTokenQuery() {
            /* some code*/
        }
    }
    public class ChannelReadyEvent : ChannelEvent {
        public ChannelReadyEvent(IChannel channel)
            : base(channel) {
        }
    }
}