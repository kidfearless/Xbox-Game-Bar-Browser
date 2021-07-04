using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation.Metadata;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.Web;
using System;
using System.Net.Http;

public sealed class WebView : FrameworkElement
{
	//
	// Summary:
	//     Gets or sets the Uniform Resource Identifier (URI) source of the HTML content
	//     to display in the WebView control.
	//
	// Returns:
	//     The Uniform Resource Identifier (URI) source of the HTML content to display in
	//     the WebView control.
	public Uri Source { get; set; }
	//
	// Summary:
	//     Gets or sets a safe list of URIs that are permitted to fire ScriptNotify events
	//     to this WebView.
	//
	// Returns:
	//     The safe list of URIs that are permitted to fire ScriptNotify events.
	public IList<Uri> AllowedScriptNotifyUris { get; set; }
	//
	// Summary:
	//     Gets a clipboard DataPackage as passed to the WebView.
	//
	// Returns:
	//     A clipboard data package.
	public DataPackage DataTransferPackage { get; }
	//
	// Summary:
	//     Gets or sets the color to use as the WebView background when the HTML content
	//     does not specify a color.
	//
	// Returns:
	//     The background color.
	public Color DefaultBackgroundColor { get; set; }
	//
	// Summary:
	//     Gets a value that indicates whether there is at least one page in the backward
	//     navigation history.
	//
	// Returns:
	//     **true** if the WebView can navigate backward; otherwise, **false**.
	public bool CanGoBack { get; }
	//
	// Summary:
	//     Gets a value that indicates whether there is at least one page in the forward
	//     navigation history.
	//
	// Returns:
	//     **true** if the WebView can navigate forward; otherwise, **false**.
	public bool CanGoForward { get; }
	//
	// Summary:
	//     Gets the title of the page currently displayed in the WebView.
	//
	// Returns:
	//     The page title.
	public string DocumentTitle { get; }
	//
	// Summary:
	//     Gets a value that indicates whether the WebView contains an element that supports
	//     full screen.
	//
	// Returns:
	//     **true** if the WebView contains an element that supports full screen; otherwise,
	//     **false**.
	public bool ContainsFullScreenElement { get; }
	//
	// Summary:
	//     Gets a collection of permission requests that are waiting to be granted or denied.
	//
	// Returns:
	//     A collection of WebViewDeferredPermissionRequest objects that are waiting to
	//     be granted or denied.
	public IList<WebViewDeferredPermissionRequest> DeferredPermissionRequests { get; }
	//
	// Summary:
	//     Gets a value that indicates whether the WebView hosts content on the UI thread
	//     or a non-UI thread.
	//
	// Returns:
	//     A value of the enumeration that specifies whether the WebView hosts content on
	//     the UI thread or a non-UI thread.
	public WebViewExecutionMode ExecutionMode { get; }
	//
	// Summary:
	//     Gets a WebViewSettings object that contains properties to enable or disable WebView
	//     features.
	//
	// Returns:
	//     A WebViewSettings object that contains properties to enable or disable WebView
	//     features.
	public WebViewSettings Settings { get; }
	//
	// Summary:
	//     Gets or sets the object that gets focus when a user presses the Directional Pad
	//     (D-pad) up.
	//
	// Returns:
	//     The object that gets focus when a user presses the Directional Pad (D-pad).
	public DependencyObject XYFocusUp { get; set; }
	//
	// Summary:
	//     Gets or sets the object that gets focus when a user presses the Directional Pad
	//     (D-pad) right.
	//
	// Returns:
	//     The object that gets focus when a user presses the Directional Pad (D-pad).
	public DependencyObject XYFocusRight { get; set; }
	//
	// Summary:
	//     Gets or sets the object that gets focus when a user presses the Directional Pad
	//     (D-pad) left.
	//
	// Returns:
	//     The object that gets focus when a user presses the Directional Pad (D-pad).
	public DependencyObject XYFocusLeft { get; set; }
	//
	// Summary:
	//     Gets or sets the object that gets focus when a user presses the Directional Pad
	//     (D-pad) down.
	//
	// Returns:
	//     The object that gets focus when a user presses the Directional Pad (D-pad).
	public DependencyObject XYFocusDown { get; set; }
	//
	// Summary:
	//     Identifies the AllowedScriptNotifyUris dependency property.
	//
	// Returns:
	//     The identifier for the AllowedScriptNotifyUris dependency property.
	public static DependencyProperty AllowedScriptNotifyUrisProperty { get; }
	//
	// Summary:
	//     Gets a value that you can use to set the AllowedScriptNotifyUris property to
	//     indicate that any page can fire ScriptNotify events to this WebView.
	//
	// Returns:
	//     The safe list of URIs that are permitted to fire ScriptNotify events.
	public static IList<Uri> AnyScriptNotifyUri { get; }
	//
	// Summary:
	//     Identifies the DataTransferPackage dependency property.
	//
	// Returns:
	//     The identifier for the DataTransferPackage dependency property.
	public static DependencyProperty DataTransferPackageProperty { get; }
	//
	// Summary:
	//     Identifies the Source dependency property.
	//
	// Returns:
	//     The identifier for the Source dependency property.
	public static DependencyProperty SourceProperty { get; }
	//
	// Summary:
	//     Identifies the CanGoBack dependency property.
	//
	// Returns:
	//     The identifier for the CanGoBack dependency property.
	public static DependencyProperty CanGoBackProperty { get; }
	//
	// Summary:
	//     Identifies the CanGoForward dependency property.
	//
	// Returns:
	//     The identifier for the CanGoForward dependency property.
	public static DependencyProperty CanGoForwardProperty { get; }
	//
	// Summary:
	//     Identifies the DefaultBackgroundColor dependency property.
	//
	// Returns:
	//     The identifier for the DefaultBackgroundColor dependency property.
	public static DependencyProperty DefaultBackgroundColorProperty { get; }
	//
	// Summary:
	//     Identifies the DocumentTitle dependency property.
	//
	// Returns:
	//     The identifier of the DocumentTitle dependency property.
	public static DependencyProperty DocumentTitleProperty { get; }
	//
	// Summary:
	//     Identifies the ContainsFullScreenElement dependency property.
	//
	// Returns:
	//     The identifier for the ContainsFullScreenElement dependency property.
	public static DependencyProperty ContainsFullScreenElementProperty { get; }
	//
	// Summary:
	//     Gets the default threading behavior of WebView instances in the current app.
	//
	// Returns:
	//     The default threading behavior of WebView instances in the current app.
	public static WebViewExecutionMode DefaultExecutionMode { get; }
	//
	// Summary:
	//     Identifies the XYFocusDown dependency property.
	//
	// Returns:
	//     The identifier for the XYFocusDown dependency property.
	public static DependencyProperty XYFocusDownProperty { get; }
	//
	// Summary:
	//     Identifies the XYFocusLeft dependency property.
	//
	// Returns:
	//     The identifier for the XYFocusLeft dependency property.
	public static DependencyProperty XYFocusLeftProperty { get; }
	//
	// Summary:
	//     Identifies the XYFocusRight dependency property.
	//
	// Returns:
	//     The identifier for the XYFocusRight dependency property.
	public static DependencyProperty XYFocusRightProperty { get; }
	//
	// Summary:
	//     Identifies the XYFocusUp dependency property.
	//
	// Returns:
	//     The identifier for the XYFocusUp dependency property.
	public static DependencyProperty XYFocusUpProperty { get; }
	//
	// Summary:
	//     Occurs when top-level navigation completes and the content loads into the WebView
	//     control or when an error occurs during loading.
	public event LoadCompletedEventHandler LoadCompleted;
	//
	// Summary:
	//     Occurs when the WebView cannot complete the navigation attempt.
	public event WebViewNavigationFailedEventHandler NavigationFailed;
	//
	// Summary:
	//     Occurs when the content contained in the WebView control passes a string to the
	//     application by using JavaScript.
	public event NotifyEventHandler ScriptNotify;
	//
	// Summary:
	//     Occurs when the WebView has started loading new content.
	public event TypedEventHandler<WebView, WebViewContentLoadingEventArgs> ContentLoading;
	//
	// Summary:
	//     Occurs when the WebView has finished parsing the current HTML content.
	public event TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs> DOMContentLoaded;

	//
	// Summary:
	//     Occurs when a frame in the WebView has started loading new content.
	public event TypedEventHandler<WebView, WebViewContentLoadingEventArgs> FrameContentLoading;
	//
	// Summary:
	//     Occurs when a frame in the WebView has finished parsing its current HTML content.
	public event TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs> FrameDOMContentLoaded;
	//
	// Summary:
	//     Occurs when a frame in the WebView has finished loading its content.
	public event TypedEventHandler < WebView, WebViewNavigationCompletedEventArgs > FrameNavigationCompleted;
	//
	// Summary:
	//     Occurs before a frame in the WebView navigates to new content.
	public event TypedEventHandler < WebView, WebViewNavigationStartingEventArgs > FrameNavigationStarting;
	//
	// Summary:
	//     Occurs periodically while the WebView executes JavaScript, letting you halt the
	//     script.
	public event TypedEventHandler < WebView, WebViewLongRunningScriptDetectedEventArgs > LongRunningScriptDetected;
	//
	// Summary:
	//     Occurs when the WebView has finished loading the current content or if navigation
	//     has failed.
	public event TypedEventHandler < WebView, WebViewNavigationCompletedEventArgs > NavigationCompleted;
	//
	// Summary:
	//     Occurs before the WebView navigates to new content.
	public event TypedEventHandler < WebView, WebViewNavigationStartingEventArgs > NavigationStarting;
	//
	// Summary:
	//     Occurs when the WebView shows a warning page for content that was reported as
	//     unsafe by SmartScreen Filter.
	public event TypedEventHandler<WebView, object> UnsafeContentWarningDisplaying
	{
		add;
		remove;
	}
	//
	// Summary:
	//     Occurs when the WebView attempts to download an unsupported file.
	public event TypedEventHandler < WebView, WebViewUnviewableContentIdentifiedEventArgs > UnviewableContentIdentified;
	//
	// Summary:
	//     Occurs when the status of whether the WebView currently contains a full screen
	//     element or not changes.
	public event TypedEventHandler<WebView, object> ContainsFullScreenElementChanged
	{
		add;
		remove;
	}
	//
	// Summary:
	//     Occurs when a user performs an action in a WebView that causes content to be
	//     opened in a new window.
	public event TypedEventHandler < WebView, WebViewNewWindowRequestedEventArgs > NewWindowRequested;
	//
	// Summary:
	//     Occurs when an action in a WebView requires that permission be granted.
	public event TypedEventHandler < WebView, WebViewPermissionRequestedEventArgs > PermissionRequested;
	//
	// Summary:
	//     Occurs when an attempt is made to navigate to a Uniform Resource Identifier (URI)
	//     using a scheme that WebView doesn't support.
	public event TypedEventHandler < WebView, WebViewUnsupportedUriSchemeIdentifiedEventArgs > UnsupportedUriSchemeIdentified;
	//
	// Summary:
	//     Occurs when a WebView runs with an ExecutionMode of **SeparateProcess**, and
	//     the separate process is lost.
	public event TypedEventHandler < WebView, WebViewSeparateProcessLostEventArgs > SeparateProcessLost;
	//
	// Summary:
	//     Occurs when an HTTP request has been made.
	public event TypedEventHandler < WebView, WebViewWebResourceRequestedEventArgs > WebResourceRequested;
	//
	// Summary:
	//     Initializes a new instance of the WebView class with the specified execution
	//     mode.
	//
	// Parameters:
	//   executionMode:
	//     A value of the enumeration that indicates whether the WebView hosts content on
	//     the UI thread or a non-UI thread.
	public extern WebView([In] WebViewExecutionMode executionMode);
	//
	// Summary:
	//     Initializes a new instance of the WebView class.
	public extern WebView();
	//
	// Summary:
	//     Executes the specified script function from the currently loaded HTML, with specific
	//     arguments.
	//
	// Parameters:
	//   scriptName:
	//     The name of the script function to invoke.
	//
	//   arguments:
	//     A string array that packages arguments to the script function.
	//
	// Returns:
	//     The result of the script invocation.
	public extern string InvokeScript([In] string scriptName, [In] string[] arguments);
	public extern void Navigate([In] Uri source);
	//
	// Summary:
	//     Loads the specified HTML content as a new document.
	//
	// Parameters:
	//   text:
	//     The HTML content to display in the WebView control.
	public extern void NavigateToString([In] string text);
	//
	// Summary:
	//     Navigates the WebView to the next page in the navigation history.
	public extern void GoForward();
	//
	// Summary:
	//     Navigates the WebView to the previous page in the navigation history.
	public extern void GoBack();

	//
	// Summary:
	//     Reloads the current content in the WebView.
	public extern void Refresh();

	//
	// Summary:
	//     Halts the current WebView navigation or download.
	public extern void Stop();

	//
	// Summary:
	//     Creates an image of the current WebView contents and writes it to the specified
	//     stream.
	//
	// Parameters:
	//   stream:
	//     The stream to write the image to.
	//
	// Returns:
	//     An asynchronous action to await the capture operation.
	[RemoteAsync]
	public extern IAsyncAction CapturePreviewToStreamAsync([In] IRandomAccessStream stream);

	[RemoteAsync]
	public extern IAsyncOperation<string> InvokeScriptAsync([In] string scriptName, [In] IEnumerable<string> arguments);

	//
	// Summary:
	//     Asynchronously gets a DataPackage that contains the selected content within the
	//     WebView.
	//
	// Returns:
	//     When this method completes, it returns the selected content as a DataPackage.
	[RemoteAsync]
	public extern IAsyncOperation<DataPackage> CaptureSelectedContentToDataPackageAsync();

	public extern void NavigateToLocalStreamUri([In] Uri source, [In] IUriToStreamResolver streamResolver);

	//
	// Summary:
	//     Creates a URI that you can pass to NavigateToLocalStreamUri.
	//
	// Parameters:
	//   contentIdentifier:
	//     A unique identifier for the content the URI is referencing. This defines the
	//     root of the URI.
	//
	//   relativePath:
	//     The path to the resource, relative to the root.
	//
	// Returns:
	//     The URI created by combining and normalizing the *contentIdentifier* and *relativePath*.
	public extern Uri BuildLocalStreamUri([In] string contentIdentifier, [In] string relativePath);

	//
	// Summary:
	//     Navigates the WebView to a URI with a POST request and HTTP headers.
	//
	// Parameters:
	//   requestMessage:
	//     The details of the HTTP request.
	public extern void NavigateWithHttpRequestMessage([In] HttpRequestMessage requestMessage);

	//
	// Summary:
	//     Sets the input focus to the WebView.
	//
	// Parameters:
	//   value:
	//     A value that indicates how the focus was set.
	//
	// Returns:
	//     **true** if focus was set; otherwise, **false**.
	public extern bool Focus([In] FocusState value);

	//
	// Summary:
	//     Adds a native Windows Runtime object as a global parameter to the top level document
	//     inside of a WebView.
	//
	// Parameters:
	//   name:
	//     The name of the object to expose to the document in the WebView.
	//
	//   pObject:
	//     The object to expose to the document in the WebView.
	public extern void AddWebAllowedObject([In] string name, [In] object pObject);

	//
	// Summary:
	//     Returns the deferred permission request with the specified Id.
	//
	// Parameters:
	//   id:
	//     The Id of the deferred permission request.
	//
	// Returns:
	//     The deferred permission request with the specified Id.
	public extern WebViewDeferredPermissionRequest DeferredPermissionRequestById([In] uint id);

	//
	// Summary:
	//     Clears the WebView 's cache and **IndexedDB** data.
	//
	// Returns:
	//     An asynchronous action to await the clear operation.
	[RemoteAsync]
	public static extern IAsyncAction ClearTemporaryWebDataAsync();
}