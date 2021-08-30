using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;

namespace DevExpress.DevAV.Chat.Model {

    public class Contact {
        [Browsable(false)]
        public long ID {
            get;
        }
        public string UserName {
            get;
        }
        public Image Avatar {
            get;
        }
        [Browsable(false)]
        public DateTime LastOnline {
            get;
        }
        public string LastOnlineText {
            get;
        }
        public int UnreadCount {
            get;
        }
        [Browsable(false)]
        public bool HasUnreadMessages {
            get;
        }
        public bool IsInactive {
            get;
        }
    }
}