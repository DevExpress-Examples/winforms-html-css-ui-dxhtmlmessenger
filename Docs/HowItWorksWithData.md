# Work with data

DXHtmlMessenger has three separate layers — a Data Layer, Model, and User Interface. The Data Layer uses interfaces to communicate with the Model layer. This allows you to implement your own Data Layer for DXHtmlMessenger to interact with your servers while leaving the Model and UI layers intact.

The following diagram shows the communication between layers:

![server-client](./Images/dxhtmlmessenger-server-client.png)


### Server and Channels

The application defines the _IMessageServer_ interface. It contains the _Connect_ method that creates a channel—an object that transfers data, server events, and client commands.

```cs
public interface IMessageServer {
    Task<IChannel> Create(string userName);
}
```

A channel (an _IChannel_ object) returned by the _Connect_ method is associated with a specific user.

```cs
public interface IChannel : IDisposable {
    string UserName { get; }
}
```

The following code uses the _Connect_ method to asynchronously create a channel. The returned object contains methods to work with data.

```cs
IChannel channel = await server.Create("Jonh Heart");
```

When the channel is no longer needed, you can dispose of it:
```cs
channel.Dispose();   
```

The current implementation of the data layer does not require a network connection. A local in-memory server obtains sample data from the DevAV database. All server events are emulated on your local machine.

The in-memory server is registered at startup, as follows.

```cs
static Program() {
    // Register global dependencies
    Data.DevAVEmpployeesInMemoryServer.Register();
}
...
sealed partial class DevAVEmpployeesInMemoryServer : IMessageServer {
    public static void Register() {
        Func<DevExpress.DevAV.DevAVDb> createDB = () => new DevExpress.DevAV.DevAVDb();
        DevExpress.Mvvm.ServiceContainer.Default.RegisterService(new DevAVEmpployeesInMemoryServer(createDB));
    }
}
```

If you create your own implementation of the data layer, register it here.

### Channel and Data

A channel provides means for two-way communication:
 - You can use _Subscribe_ methods to listen to the channel's events.
 - You can use the _Send_ method to send commands to the channel.

The channel contains three types of _Send_ and _Subscribe_ methods: general, contact-aware, and message-aware.

```cs
public interface IChannel : IDisposable {
    // Common operations.
    void Subscribe(Action<ChannelEvent> onEvent);
    void Send(ChannelCommand command);
    // Contact-aware operations.
    void Subscribe(Action<Dictionary<long, ContactEvent>> onEvents);
    void Send(ContactCommand command);
    // Message-aware operations.
    void Subscribe(Action<Dictionary<long, MessageEvent>> onEvents);
    void Send(MessageCommand command);
    ...
}
```

A channel also contains methods that asynchronously obtain data from the server:

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


Data is retrieved from the server when the channel is ready, and then it is displayed within the UI layer.

```cs
protected override async void OnChannelReady() {
    Contacts = await Channel.GetContacts();
}
```

You can request data again when a specific server-side event is received.

```cs
channel.Subscribe(OnMessageEvents);
//...
void OnMessageEvents(Dictionary<long, MessageEvent> events) {
    MessageEvent @event;
    foreach(Message message in Messages) {
        if(events.TryGetValue(message.ID, out @event))
            @event.Apply(message);
    }
}
```

### Events

The first group of events is related to authentication:

```cs
// The server sends this event when a user access token is required for authentication.
public class CredentialsRequiredEvent : ChannelEvent { 
    /*methods*/ 
}

// The server sends this event when user authentication is complete, and the channel is ready for data requests.
public class ChannelReadyEvent : ChannelEvent { 
    /* this event has no data or methods */ 
}
```

When a client receives the _CredentialsRequiredEvent_ event, it should call the _SetAccessTokenQuery_ method. The method specifies a **task** that asynchronously provides an access-token for a specific user:

```cs
void OnChannelEvent(ChannelEvent @event) {
    var credentialsRequired = @event as CredentialsRequiredEvent;
    if(credentialsRequired != null) {
        var query = AccessTokenQuery(@event.UserName, credentialsRequired.Salt);
        credentialsRequired.SetAccessTokenQuery(query);
    }
}

```

This task can either obtain the access token from the client-side cache, or show a Sign-In dialog to return credentials from the user.

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

The second group of events are data-aware. The server sends these events when it encounters changes on the server:

```cs
public class StatusChanged : ContactEvent {
    /* data fields */
}
```

When the client receives these events, it applies the changes to the corresponding UI controls.

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

Commands are sent from clients to the server. For example, a command can initiate a log off procedure.

```cs
public void LogOff() {
    channel.Send(new LogOff(channel));
}
```

Another example of a command is sending a new chat message to the server:

```cs
public void SendMessage() {
    if(Channel != null)
        Channel.Send(new AddMessage(Contact, MessageText));
    MessageText = null;
}
```
The server should respond to these commands (for instance, update data on the server and send notification events to client channels).


## See Also
- [DXHtmlMessenger Demo Overview](../README.md)
- [Application UI Design](ApplicationPartsDesign.md)
