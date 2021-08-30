using System;
using DevExpress.DevAV.Chat.Model;


namespace DevExpress.DevAV.Chat.Commands {

    public abstract class ContactCommand {
        public Contact Contact {
            get;
        }
    }
    //
    public class AddMessage : ContactCommand {
        public AddMessage(Contact contact)
            : base(contact) {
        }
        public string Message {
            get;
        }
        public DateTime Sent {
            get;
        }
    }
    public class ReadMessages : ContactCommand {
        public ReadMessages(Contact contact)
            : base(contact) {
        }
    }
}