# Application UI Controls

### Search Panel

Search Panel is implemented by the **HtmlContentControl**. It uses HTML markup and CSS from the following files to render an icon and an input box (a SearchControl). 

- Assets/searchPanel.html
- Assets/searchPanel.css

The HTML code from the `searchPanel.html` file is shown below:

```html
<div class='panel'>
    <img src='Search' class='searchButton'/>
    <input name="searchControl" class="searchInput">
</div>
```

#### Input Box
The following steps are used to place an input box within the **HtmlContentControl**:

- Define the `input` tag within HTML markup.
- Place any text editor (the **SearchControl** in this example) onto the **HtmlContentControl**. Set the text editor's name ("searchControl" in this example).
- Set the `name` property of the `input` tag to the name of the created text editor (the ""searchControl" string).

The **SearchControl** automatically filters its client control (**SeachControl.Client**) according to the typed text. In this example, the SearchControl's client is the GridControl that displays contacts.

### Typing Box

The Typing Box is implemented by the **HtmlContentControl**. It uses HTML markup and CSS from the following files to display an input box (a MemoEdit control) and a _Send_ button. 

- Assets/typingbox.html
- Assets/typingbox.css

The HTML code from the `typingbox.html` file is shown below:

```html
<div class='typingBox'>
    <div class='container'>
        <input name="messageEdit" class="message" />
        <div class='separator-Container'></div>
        <img id="btnSend" src='Send' class='button' />
    </div>
</div>
```
#### Input Box
The input box is added to the **HtmlContentControl** in the same way as described in the Search Panel section.

#### Buttons
The following steps are used to render a button:
- Define an HTML element that renders your button (the `img` tag in this example). Specify the element's class (`button` in this example).
- In the corresponding CSS file, define the `button` class to specify the element's display properties.
- Also, define the `hover` state for the `button` class to highlight the element when it's hovered over.

The following snippet from the typingbox.css file demonstrates this approach:


```css
.button {
	width: 20px;
	height: 20px;
	min-width: 20px;
	padding: 8px;
	margin-left: 2px;
	opacity: 0.5;
}
	.button:hover {
		border-radius: 4px;
		background-color: #F2F2F2;
	}
```

You can handle the **HtmlContentControl.ElementMouseClick** event to perform actions when an element (button) is clicked. The current application uses Fluent API to define the button's action on a click.

```cs
fluent.BindCommandToElement(typingBox, "btnSend", x => x.SendMessage);
//...
public void SendMessage() {
    if(Channel != null)
        Channel.Send(new NewMessage(Contact, MessageText));
    MessageText = null;
}
```

### Toolbar

The Toolbar is implemented by the **HtmlContentControl**. It uses HTML markup and CSS from the following files to display a contact name, and render buttons. 

- Assets/toolbar.html
- Assets/toolbar.css

The HTML code from the `toolbar.html` file is shown below:

```html
<div class='toolBar'>
    <div class='contactName'>${UserName}</div>
    <div class='buttonPanel'>
        <img id="btnPhoneCall" src='PhoneCall' class='button' />
        <img id="btnVideoCall" src='VideoCall' class='button' />
        <img id="btnContact" src='Contact' class='button' />
        <div class='separator-Container'>
            <div class='separator'></div>
        </div>
        <img id="btnUser" src='User' class='button' />
    </div>
</div>
```

#### Data Binding - Display Field Values

The example uses the **HtmlContentControl.DataContext** property to bind the **HtmlContentControl** to the _MessagesViewModel.Contact_ business object—this data context supplies data to the control. 

Take note of the "${UserName}" string. The '$' character in the beginning specifies that the string that follows is an [interpolated string](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated)—an expression that the control needs to evaluate. The "{_FieldName_}" form is the syntax for data binding. It is used to insert a value of the specified field in the output. So, the "${UserName}" text inserts a value of the _UserName_ field from the data context.

An interpolated string can contain static text, data binding to multiple fields, and field value formatting (see [string interpolation](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated) for more information). The following examples adds the 'Welcome' string before the user name:

```html
<h1>$Welcome {UserName}!</h1>
```

#### Buttons
Buttons are added to the **HtmlContentControl** in the same way as described in the Typing Box section.


### Contacts

The contact list is implemented by a GridControl's TileView. Tiles are rendered using HTML markup and CSS from the following files: 

- Assets/contact.html
- Assets/contact.css

The HTML code from the `contact.html` file is shown below:

```html
<div class="contact">
    <div class="avatar-container">
        <img src="${Avatar}" class="avatar" />
        <div class="status" />
    </div>
    <div class="container">
        <div class="contact-info">
            <div class="name">${UserName}</div>
            <div class="time">${LastOnline}</div>
        </div>
        <div class="indicators-container">
            <img src='Contact' class='info' />
            <div class="counter">${UnreadCount}</div>
        </div>
    </div>
</div>
```

#### Data Binding - Display Field Values

The "$FieldName" syntax is used to display values of corresponding fields from a bound data source (GridControl.DataSource). The same approach is used for data binding in **HtmlContentControl** (see the Toolbar section above).

#### Customize Individual Elements 

The application handles the **TileView.ItemCustomize** event to dynamically control the visibility of tile elements.

```cs
void OnContactItemTemplateCustomize(object sender, TileViewItemCustomizeEventArgs e) {
    var contact = contactsTileView.GetRow(e.RowHandle) as Contact;                    
    if(contact != null) {                                                             
        var statusBadge = e.HtmlBlockInfo.FindElementById("statusBadge");             
        if(statusBadge != null)                                                       
            statusBadge.SetActiveState(!contact.IsInactive);                          
        if(!contact.HasUnreadMessages) {                                              
            var unreadBadge = e.HtmlBlockInfo.FindElementById("unreadBadge");         
            if(unreadBadge != null)                                                   
                unreadBadge.Hidden = true;                                            
        }                                                                             
    }                                                                                 
}                                                                                     
```



### Messages

The message list is implemented by a GridControl's ItemsView. Items are rendered using HTML markup and CSS from the following files: 

- Assets/message.html
- Assets/message.css
- Assets/mymessage.html (applied to messages written by the current user).
- Assets/mymessage.css (applied to messages written by the current user).

The HTML code from the `message.html` file is shown below:

```html
<div class='messageRoot'>
    <img src='${Owner.Avatar}' class='avatar' />
    <div class='text'>
        <div class='contactName'>${Owner.UserName}</div>
        <div class='message'>${Text}</div>
    </div>
</div>
```

#### Data Binding - Display Field Values

The MessagesViewModel.Messages collection is the data source for the GridControl's items.
The "${Owner.Avatar}" syntax in HTML markup is used to bind the HTML element to the _Owner.Avatar_ field on the underlying data store. This data binding technique is described in the Toolbar section above. 

#### Use Different Tempates for Different Items

The **ItemsView.QueryItemTemplate** event is handled to assign different templates to messages of the current user and messages of another person.

```cs
void OnQueryItemTemplate(object sender, DevExpress.XtraGrid.Views.Items.QueryItemTemplateArgs e) {
    var message = e.RowData as Message;
    if(message != null && message.IsOwnMessage)
        Styles.MyMessage.Apply(e.Template);
    else Styles.Message.Apply(e.Template);
}      
```



### Toolbar: How to create user interface with using the HtmlContentControl style and template

// TODO

### SignIn View: How to layout simple editors within the HtmlContent control

// TODO

### Contacts: How to use predefined templates and styles to create a list of scrollable tiles with the same layout based on the GridControl's TileView

// TODO

### Messages: How to use predefined templates and styles to create scrollable items list with advanced layout and behavior based on the GridControl's ItemsView

// TODO

### User and Contact infoss: How to use predefined templates and styles to create a popup which display the specific information within the HtmlPopup control

// TODO
