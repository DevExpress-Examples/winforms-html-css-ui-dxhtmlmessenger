namespace DevExpress.DevAV.Chat.Events {
    using System;
    using DevExpress.DevAV.Chat.Model;

    public abstract class ContactEvent {
        public long Id {
            get;
        }
        public abstract void Apply(Contact contact);
    }
    //
    public class StatusChanged : ContactEvent {
        public Contact.Status Status {
            get;
        }
        public DateTime LastOnline {
            get;
        }
        public override void Apply(Contact contact) {
            contact.StatusCore = Status;
            contact.LastOnline = Status == Contact.Status.Inactive ? LastOnline : DateTime.MinValue;
        }
    }
    public class UnreadChanged : ContactEvent {
        public int UnreadCount {
            get;
        }
        public override void Apply(Contact contact) {
            contact.UnreadCount += UnreadCount;
        }
    }
    public class AllMessagesRead : ContactEvent {
        public AllMessagesRead(long id)
            : base(id) {
        }
        public override void Apply(Contact contact) {
            contact.UnreadCount = 0;
        }
    }
    public class NewMessages : ContactEvent {
        public NewMessages(long id)
            : base(id) {
        }
        public override void Apply(Contact entity) {
            /* do nothing */
        }
    }
}