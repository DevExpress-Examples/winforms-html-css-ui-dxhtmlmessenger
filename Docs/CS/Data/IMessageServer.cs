using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExpress.DevAV.Chat.Commands;
using DevExpress.DevAV.Chat.Events;
using DevExpress.DevAV.Chat.Model;


namespace DevExpress.DevAV.Chat {

    public interface IMessageServer {
        Task<IChannel> Connect(string userName);
    }
    public interface IChannel : IDisposable {
        // Common
        void Subscribe(Action<ChannelEvent> onEvent);
        string UserName { get; }
        void Send(ChannelCommand command);
        // Contacts
        void Subscribe(Action<Dictionary<long, ContactEvent>> onEvents);
        Task<UserInfo> GetUserInfo(string userName);
        Task<UserInfo> GetUserInfo(long id);
        Task<IReadOnlyCollection<Contact>> GetContacts();
        void Send(ContactCommand command);
        // Messages
        void Subscribe(Action<Dictionary<long, MessageEvent>> onEvents);
        Task<IReadOnlyCollection<Message>> GetHistory(Contact contact);
        void Send(MessageCommand command);
    }
}