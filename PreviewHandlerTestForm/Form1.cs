using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EasyPreview.Kernel;

namespace PreviewHandlerTestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private object _handler;
        private string _file_folder = @"C:\Works\Git_Repo\EasyPreview\PreviewHandlerTestForm\TestDoc\test";
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Preview(string extension)
        {
            var clsid = PreviewHandler.GetCLSIDFromExtensionShellEx(extension);
            if (clsid == string.Empty)
            {
                MessageBox.Show("no clsid to find!");
                return;
            }

            _handler = PreviewHandler.CreateObjectByCLSID(clsid);
            if (_handler == null)
            {
                MessageBox.Show("create com object by clsid failed!");
                return;
            }

            var file_name = _file_folder + extension;
            var init_success = PreviewHandler.InitHandlerObjectWithFile(_handler, file_name);
            if (!init_success)
            {
                MessageBox.Show("initialize com object with file name failed!");
                return;
            }

            IPreviewHandler handler = _handler as IPreviewHandler;
            if (handler == null)
            {
                MessageBox.Show("cast com object to IPreviewHandler failed");
                return;
            }

            Rectangle r = _panel_preview.ClientRectangle;
            handler.SetWindow(_panel_preview.Handle, ref r);

            handler.DoPreview();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseHandler();
        }

        private void ReleaseHandler()
        {
            if (_handler == null) return;
            try
            {
                ((IPreviewHandler)_handler).Unload();
                Marshal.ReleaseComObject(_handler);
            }
            catch (Exception)
            {
            }
        }

        private void _panel_preview_Resize(object sender, EventArgs e)
        {

            if (_handler != null && _handler is IPreviewHandler)
            {
                // update the preview handler's bounds to match the control's
                Rectangle r = _panel_preview.ClientRectangle;

                ((IPreviewHandler)_handler).SetRect(ref r);
                //((IPreviewHandler)_handler).DoPreview();
            }
            else
            {
                _panel_preview.Refresh();
            }
        }

        private void _btn_preview_Click(object sender, EventArgs e)
        {
            ReleaseHandler();
            var sel_ext = _cmb_extensions.Text;
            if (sel_ext == string.Empty) return;
            Preview(sel_ext);
        }
    }
}
