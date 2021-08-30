using System;
using System.ComponentModel;

namespace DevExpress.DevAV.Chat.Model {

    public class Message {
        [Browsable(false)]
        public long ID {
            get;
        }
        public Contact Owner {
            get;
        }
        public string Text {
            get;
        }
        public DateTime SentOrUpdated {
            get;
        }
        public string StatusText {
            get;
        }
        [Browsable(false)]
        public bool IsEdited {
            get;
        }
        [Browsable(false)]
        public bool IsDeleted {
            get;
        }
        [Browsable(false)]
        public bool IsLiked {
            get;
        }
        [Browsable(false)]
        public bool IsOwnMessage {
            get;
        }
        [Browsable(false)]
        public bool IsFirstMessageOfReply {
            get;
        }
        [Browsable(false)]
        public bool IsFirstMessageOfBlock {
            get;
        }
    }
}