using System.Linq;
using System.Windows.Forms;

namespace EasyPreview.Host
{
    /// <summary>
    /// Use for HTML file
    /// </summary>
    public class BrowserPreviewHandlerHost : PreviewHandlerHostBase
    {
        private WebBrowser _browser;
        private WebBrowser WebBrowser => _browser ?? (_browser = new WebBrowser
        {
            Dock = DockStyle.Fill,
            Name = "webbrowser",
            ScrollBarsEnabled = true
        });

        public override bool Open(string file_name)
        {
            WebBrowser.Navigate(file_name, false);

            if (!Controls.Find("webbrowser", true).Any())
                Controls.Add(WebBrowser);

            return true;
        }
    }
}