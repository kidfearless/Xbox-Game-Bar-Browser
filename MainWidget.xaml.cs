using App1.Models;

using Microsoft.Toolkit.Uwp.UI.Extensions;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Web;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public partial class MainWidget : Page, IDisposable
	{
		protected bool IsWebViewFocused { get; set; }
		protected List<HistoryItem> History { get; set; }
		protected string CurrentURL { get; set; }
		public string HomePage { get; set; } = "https://www.google.com/";
		protected HttpClient Client { get; set; }


		public MainWidget()
		{
			this.History = new();
			this.Client = new();
			this.InitializeComponent();

			this.WebView.LostFocus += this.WebView_LostFocus;
			this.WebView.GotFocus += this.WebView_GotFocus;
			this.WebView.Focus(FocusState.Pointer);

			this.WebView.NavigationFailed += this.WebView_NavigationFailed;
			this.WebView.NavigationCompleted += this.WebView_NavigationCompleted;
			this.WebView.NavigationStarting += this.WebView_NavigationStarting;

			this.BackButton.Click += this.BackButton_Click;
			this.BackButton.PointerPressed += this.BackButton_RightClicked;
			this.BackButton.LostFocus += this.BackButton_LostFocus;
			this.ForwardButton.Click += this.ForwardButton_Click;
			this.RefreshButton.Click += this.RefreshButton_Click;
			this.URLBox.KeyDown += this.URLBox_KeyDown;
		}

		private void BackButton_LostFocus(object sender, RoutedEventArgs e) => this.BackButtonPanel.Visibility = Visibility.Collapsed;

		private void BackButton_RightClicked(object sender, PointerRoutedEventArgs e)
		{
			this.BackButtonPanel.Children.Clear();
			foreach (var item in this.History)
			{
				var button = new Button();
				button.Content = new TextBlock()
				{
					Text = item.Title
				};
				this.BackButtonPanel.Children.Add(button);
			}
			this.BackButtonPanel.Visibility = Visibility.Visible;
		}

		private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args) 
			=> this.URLBox.Text = args.Uri.ToString();

		private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
		{
			this.URLBox.Text = args.Uri.ToString();

			this.History.Add(new()
			{
				Title = sender.DocumentTitle,
				URL = args.Uri.ToString()
			});
		}

		private async void WebView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
		{
			if (e.WebErrorStatus is WebErrorStatus.TemporaryRedirect or WebErrorStatus.MovedPermanently or WebErrorStatus.CertificateIsInvalid)
			{
				HttpResponseMessage response = await this.Client.GetAsync(e.Uri.ToString());
				this.WebView.Navigate(response.Headers.Location);
			}
			if (e.WebErrorStatus is WebErrorStatus.NotFound)
			{
				this.Search(e.Uri.ToString().Replace("https://", "").Replace("/", ""));
			}
		}

		private void URLBox_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == VirtualKey.Enter)
			{
				//if(URLBox.Text is url)
				{
					var url = URLBuilder.Parse(URLBox.Text);
					if (url is not null)
					{
						WebView.Navigate(url);
					}
					else
					{
						this.Search(URLBox.Text);
					}
				}
			}
		}
		private void RefreshButton_Click(object sender, RoutedEventArgs e) => this.WebView.Refresh();

		private void ForwardButton_Click(object sender, RoutedEventArgs e)
		{
			if (WebView.CanGoForward)
			{
				WebView.GoForward();
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (WebView.CanGoBack)
			{
				WebView.GoBack();
			}
		}

		private void WebView_GotFocus(object sender, RoutedEventArgs e) => this.IsWebViewFocused = true;
		private void WebView_LostFocus(object sender, RoutedEventArgs e) => this.IsWebViewFocused = false;

		private void Page_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (this.IsWebViewFocused && e.Key == VirtualKey.Enter)
			{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				this.WebView.InvokeScriptAsync("eval", new string[] { "document.querySelector(\"[type = 'submit']\").click()" });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			}
		}

		void Search(string query)
		{
			var google = "https://www.google.com/search?q=";
			query = HttpUtility.UrlEncode(query);
			WebView.Navigate(new Uri(google + query));
		}

		public void Dispose() => ((IDisposable)this.Client).Dispose();
	}
}
