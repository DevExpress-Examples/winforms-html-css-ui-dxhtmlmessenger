# How this application works with data

As we already said, the main idea of this sample is creating the data-layer agnostic app which uses clearly separated layers for Model, Data, and User Interface. 
The Data Layer is designed to be data-agnostic and belongs to some common interfaces.

In short, we use the following abstraction:

```
 Server                                  Client  
     [Channel]  --> [Events]   -->  [User Inteface]
                <-- [Commands] <--
```

Lets see how this parts works together.

### Server and Channel

The Server instance provide us with a way to connect to the specific message server's **channel**:

```cs
public interface IMessageServer {
    Task<IChannel> Connect(string userName);
}
```

The channel is associated to the specific user:

```cs
public interface IChannel : IDisposable {
    string UserName { get; }
}
```

The resulting `IChannel` instance provide all the required methods to work with data. Connection is performed asynchronously:

```cs
IChannel channel = await server.Connect("Jonh Heart");
```

When the channel is not needed, you can destroy this channes by disposing it:
```cs
channel.Dispose();   
```

This specific application's data-layer is implemented as the **in-memory emulation** based on some data from DevAV database.
All the additional data and server events are generated on the fly based on timer.

The application register this specific implementation in **service container** at startup:
```cs
static Program() {
    // Register global dependencies
    Data.DevAVEmpployeesInMemoryServer.Register();
}
...
sealed partial class DevAVEmpployeesInMemoryServer : IMessageServer {
    public static void Register() {
        DevExpress.Mvvm.ServiceContainer.Default.RegisterService(new DevAVEmpployeesInMemoryServer());
    }
}
```

You can register your own implementation in the same manner.

### Channel and Data



The channel is designed for two-way communication:
 - you can listen channel' events via the `Subscribe` methods
 - you can send some commands into the channel via the `Send` method

The Send/Subscribe methods are methods based on some specific typed arguments(for example MessageEvent/MessageCommand).

```cs
public interface IChannel : IDisposable {
    // Common
    void Subscribe(Action<ChannelEvent> onEvent);
    void Send(ChannelCommand command);
    // Contacts
    void Subscribe(Action<Dictionary<long, ContactEvent>> onEvents);
    // Messages
    void Subscribe(Action<Dictionary<long, MessageEvent>> onEvents);
    void Send(MessageCommand command);
    ...
}
```

There are also som methods which is designed to asynchronously obtain the **data** from the server when the channel is ready:

```cs
public interface IChannel : IDisposable {
    // Contacts
    Task<UserInfo> GetUserInfo(string userName);
    Task<UserInfo> GetUserInfo(long id);
    Task<IReadOnlyCollection<Contact>> GetContacts();
    // Messages
    Task<IReadOnlyCollection<Message>> GetHistory(Contact contact);
}
```

The data objects received from the server is just DTOs which contains the specific data for displaying within the Messenger UI.

```cs
protected override async void OnChannelReady() {
    Contacts = await Channel.GetContacts();
}
```

These data can be requested again if needed or updated locally if some specific event related to server-side changes is received.

```cs
void OnMessageEvents(Dictionary<long, MessageEvent> events) {
    MessageEvent @event;
    foreach(Message message in Messages) {
        if(events.TryGetValue(message.ID, out @event))
            @event.Apply(message);
    }
}
```

### Events

First group of events is a common events related to **authentification**. These events are:

```cs
// server sent this event when user access token required for authentification
public class CredentialsRequiredEvent : ChannelEvent { 
    /* some methods */ 
}

// server sent this event when authentification complete and channel can be used for data requests
public class ChannelReadyEvent : ChannelEvent { 
    /* this event has no data or methods */ 
}
```

When the client receive the CredentialsRequiredEvent it should use the `SetAccessTokenQuery` method to 
specify **a task** which can be used **to asynchronously provide access-token** for the specific user:

```cs
void OnChannelEvent(ChannelEvent @event) {
    var credentialsRequired = @event as CredentialsRequiredEvent;
    if(credentialsRequired != null) {
        var query = AccessTokenQuery(@event.UserName, credentialsRequired.Salt);
        credentialsRequired.SetAccessTokenQuery(query);
    }
}

```

This task can encapsulate either the obtaining the access-token from the client-side cache or the interaction with a user
via showing Sign-In dialog.

```cs
Task<string> AccessTokenQuery(string userName, string salt) {
    var accessTokenQueryCompletionSource = new TaskCompletionSource<string>();
    dispatcher.BeginInvoke(() => {
        var signInViewModel = SignInViewModel.Create(userName, salt);
        signInViewModel.ShowDialog();
        if(!string.IsNullOrEmpty(signInViewModel.AccessToken))
            accessTokenQueryCompletionSource.SetResult(signInViewModel.AccessToken);
        else
            accessTokenQueryCompletionSource.SetCanceled();
    });
    return accessTokenQueryCompletionSource.Task;
}
```

The second group of events is a data-related events. Server sent these events when there are some changes on server side:

```cs
public class StatusChanged : ContactEvent {
    /* some data fields */
}
```

After receiving these events client can just **apply** this event direcly to the already loaded data:

```cs
async void OnContactEvents(Dictionary<long, ContactEvent> events) {
    ContactEvent @event;
    foreach(Contact contact in Contacts) {
        if(events.TryGetValue(contact.ID, out @event))
            @event.Apply(contact);
    }
}
```

### Commands

The command can be sent from the client-side to manage something on the server side.
For example to initiate the Log Off:

```cs
public void LogOff() {
    channel.Send(new LogOff(channel));
}
```

or sent a chat message to the server:

```cs
public void SendMessage() {
    if(Channel != null)
        Channel.Send(new NewMessage(Contact, MessageText));
    Message = null;
}

```
The server will process the event and do something like updating server data and notifying client by sending some events into channels.


### User Interface

Here you can read [нow each parts of this app was designed](ApplicationPartsDesign.md)
