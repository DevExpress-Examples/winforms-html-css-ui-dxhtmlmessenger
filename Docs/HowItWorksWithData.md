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
```vb
Public Interface IMessageServer
        Function Create(ByVal userName As String) As Task(Of IChannel)
End Interface
```

A channel (an _IChannel_ object) returned by the _Connect_ method is associated with a specific user.

```cs
public interface IChannel : IDisposable {
    string UserName { get; }
}
```
```vb
Public Interface IChannel
    Inherits IDisposable

    ReadOnly Property UserName() As String
End Interface
```

The following code uses the _Connect_ method to asynchronously create a channel. The returned object contains methods to work with data.

```cs
IChannel channel = await server.Create("Jonh Heart");
```
```vb
Dim channel As IChannel = Await server.Create("Jonh Heart")
```

When the channel is no longer needed, you can dispose of it:
```cs
channel.Dispose();   
```
```vb
channel.Dispose()
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
```vb
Shared Sub New()
    ' Register global dependencies
    Data.DevAVEmpployeesInMemoryServer.Register()
End Sub
'...
Friend NotInheritable Partial Class DevAVEmpployeesInMemoryServer
    Implements IMessageServer

    Public Shared Sub Register()
        Dim createDB As Func(Of DevExpress.DevAV.DevAVDb) = Function() New DevExpress.DevAV.DevAVDb()
        DevExpress.Mvvm.ServiceContainer.Default.RegisterService(New DevAVEmpployeesInMemoryServer(createDB))
    End Sub
End Class
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
```vb
Public Interface IChannel
    Inherits IDisposable

    ' Common operations.
    Sub Subscribe(ByVal onEvent As Action(Of ChannelEvent))
    Sub Send(ByVal command As ChannelCommand)
    ' Contact-aware operations.
    Sub Subscribe(ByVal onEvents As Action(Of Dictionary(Of Long, ContactEvent)))
    Sub Send(ByVal command As ContactCommand)
    ' Message-aware operations.
    Sub Subscribe(ByVal onEvents As Action(Of Dictionary(Of Long, MessageEvent)))
    Sub Send(ByVal command As MessageCommand)
    '...
End Interface
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
```vb
Public Interface IChannel
    Inherits IDisposable

    ' Contacts
    Function GetUserInfo(ByVal userName As String) As Task(Of UserInfo)
    Function GetUserInfo(ByVal id As Long) As Task(Of UserInfo)
    Function GetContacts() As Task(Of IReadOnlyCollection(Of Contact))
    ' Messages
    Function GetHistory(ByVal contact As Contact) As Task(Of IReadOnlyCollection(Of Message))
End Interface
```

Data is retrieved from the server when the channel is ready, and then it is displayed within the UI layer.

```cs
protected override async void OnChannelReady() {
    Contacts = await Channel.GetContacts();
}
```
```vb
Protected Overrides Async Sub OnChannelReady()
    Contacts = Await Channel.GetContacts()
End Sub
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
```vb
channel.Subscribe(OnMessageEvents)
'...
Private Sub OnMessageEvents(ByVal events As Dictionary(Of Long, MessageEvent))
    Dim [event] As MessageEvent = Nothing
    For Each message As Message In Messages
        If events.TryGetValue(message.ID, [event]) Then
            [event].Apply(message)
        End If
    Next message
End Sub
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
```vb
' The server sends this event when a user access token is required for authentication.
Public Class CredentialsRequiredEvent
    Inherits ChannelEvent

    'methods 
End Class

' The server sends this event when user authentication is complete, and the channel is ready for data requests.
Public Class ChannelReadyEvent
    Inherits ChannelEvent

    ' this event has no data or methods  
End Class
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
```vb
Sub OnChannelEvent(ByVal [event] As ChannelEvent)
    Dim credentialsRequired = TryCast([event], CredentialsRequiredEvent)
    If credentialsRequired IsNot Nothing Then
        Dim query = AccessTokenQuery([event].UserName, credentialsRequired.Salt)
        credentialsRequired.SetAccessTokenQuery(query)
    End If
End Sub
```

This task can either obtain the access token from the client-side cache, or show a Sign-In dialog to return credentials from the user.

```cs
Task<string> QueryAccessTokenFromUser(string userName, string salt) {
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
```vb
Function QueryAccessTokenFromUser(ByVal userName As String, ByVal salt As String) As Task(Of String)
    Dim accessTokenQueryCompletionSource = New TaskCompletionSource(Of String)()
    dispatcher.BeginInvoke(Sub()
                               Dim signInViewModel = ViewModels.SignInViewModel.Create(userName, salt)
                               signInViewModel.ShowDialog()
                               If Not String.IsNullOrEmpty(signInViewModel.AccessToken) Then
                                   accessTokenQueryCompletionSource.SetResult(signInViewModel.AccessToken)
                               Else
                                   accessTokenQueryCompletionSource.SetCanceled()
                               End If
                           End Sub)
    Return accessTokenQueryCompletionSource.Task
End Function
```

The second group of events are data-aware. The server sends these events when it encounters changes on the server:

```cs
public class StatusChanged : ContactEvent {
    /* data fields */
}
```
```vb
Public Class StatusChanged
    Inherits ContactEvent

    ' data fields 
End Class
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
```vb
Async Sub OnContactEvents(ByVal events As Dictionary(Of Long, ContactEvent))
    Dim [event] As ContactEvent = Nothing
    For Each contact As Contact In Contacts
        If events.TryGetValue(contact.ID, [event]) Then
            [event].Apply(contact)
        End If
    Next contact
End Sub
```

### Commands

Commands are sent from clients to the server. For example, a command can initiate a log off procedure.

```cs
public void LogOff() {
    channel.Send(new LogOff(channel));
}
```
```vb
Public Sub LogOff()
    channel.Send(New LogOff(channel))
End Sub
```

Another example of a command is sending a new chat message to the server:

```cs
public void SendMessage() {
    if(Channel != null)
        Channel.Send(new AddMessage(Contact, MessageText));
    MessageText = null;
}
```
```vb
Public Sub SendMessage()
    If Channel IsNot Nothing Then
        Channel.Send(New AddMessage(Contact, MessageText))
    End If
    MessageText = Nothing
End Sub
```

The server should respond to these commands (for instance, update data on the server and send notification events to client channels).


## See Also
- [DXHtmlMessenger Demo Overview](../README.md)
- [Application UI Design](ApplicationPartsDesign.md)
