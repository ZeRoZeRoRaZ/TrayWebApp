using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace TrayWebApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();
			string url = ConfigurationManager.AppSettings.Get("appUrl");
			string icoUrl = Path.Combine(url, "favicon.ico");
			SetTrayIcon(icoUrl);
			Reload.Command = Browser.ReloadCommand;
			Browser.Address = url;
			Browser.TitleChanged += Browser_TitleChanged;
		}

		private void Browser_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Title = Browser.Title;
			NotifyIcon.ToolTip = Browser.Title;
		}

		private void SetTrayIcon(string icoUrl)
		{
			var w = (HttpWebRequest)HttpWebRequest.Create(icoUrl);
			w.AllowAutoRedirect = true;
			var r = (HttpWebResponse)w.GetResponse();

			Bitmap ico;
			using (Stream s = r.GetResponseStream())
			{
				ico = (Bitmap)Image.FromStream(s);
			}

			var icon = System.Drawing.Icon.FromHandle(ico.GetHicon());
			NotifyIcon.Icon = icon;

			Icon = Imaging.CreateBitmapSourceFromHBitmap(ico.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
		}
	}


}
