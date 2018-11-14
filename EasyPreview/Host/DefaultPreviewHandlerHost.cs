using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EasyPreview.Kernel;
using EasyPreview.Resources;
using Microsoft.Win32;

namespace EasyPreview.Host
{
    /// <summary>
    /// Use the Host from Registry 
    /// </summary>
    public class DefaultPreviewHandlerHost : PreviewHandlerHostBase
    {
        private object _handler;        // COM object implement IPreviewHandler
        private Guid _handler_guid;     // COM object CLSID
        private Stream _handler_stream; // the content stream for preview

        private string _error_message;
        private string ErrorMessage
        {
            set
            {
                _error_message = value;
                Invalidate();	// repaint the control
            }
        }

        /// <summary>
        /// Gets or sets the background colour of this PreviewHandlerHost.
        /// </summary>
        [DefaultValue("White")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        /// <summary>
        /// Initialialises a new instance of the PreviewHandlerHost class.
        /// </summary>
        public DefaultPreviewHandlerHost()
        {
            _handler_guid = Guid.Empty;
            BackColor = Color.White;
            Size = new Size(320, 240);

            ErrorMessage = Messages.FileNotLoaded; 

            // enable transparency
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the PreviewHandlerHost and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            UnloadPreviewHandler();

            if (_handler != null)
            {
                Marshal.FinalReleaseComObject(_handler);
                _handler = null;
                GC.Collect();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Unloads the preview handler hosted in this PreviewHandlerHost and closes the file stream.
        /// </summary>
        public void UnloadPreviewHandler()
        {
            try
            {
                if (_handler is IPreviewHandler)
                {
                    try
                    {
                        // explicitly unload the content
                        ((IPreviewHandler)_handler).Unload();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception) { }

            if (_handler_stream != null)
            {
                _handler_stream.Close();
                _handler_stream = null;
            }

            _handler_guid = new Guid();
        }


        private static Guid GetPreviewHandlerGUID(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return Guid.Empty;

            var ext = Registry.ClassesRoot.OpenSubKey(Path.GetExtension(filename));
            if (ext == null) return Guid.Empty;

            // open the key that indicates the GUID of the preview handler type
            var test = ext.OpenSubKey("shellex\\{8895b1c6-b41f-4c1c-a562-0d564250836f}");
            if (test != null) return new Guid(Convert.ToString(test.GetValue(null)));

            // sometimes preview handlers are declared on key for the class
            var class_name = Convert.ToString(ext.GetValue(null));
            test = Registry.ClassesRoot.OpenSubKey(class_name + "\\shellex\\{8895b1c6-b41f-4c1c-a562-0d564250836f}");
            return test != null ? new Guid(Convert.ToString(test.GetValue(null))) : Guid.Empty;
        }

        /// <summary>
        /// Paints the error message text on the PreviewHandlerHost control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_error_message != string.Empty)
            {
                // paint the error message
                TextRenderer.DrawText(
                    e.Graphics,
                    Messages.FileNotLoaded,
                    Font,
                    ClientRectangle,
                    ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
                );
            }
        }

        /// <summary>
        /// Resizes the hosted preview handler when this PreviewHandlerHost is resized.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_handler is IPreviewHandler)
            {
                var r = ClientRectangle;
                ((IPreviewHandler)_handler).SetRect(ref r);
            }
            else
            {
                Refresh();
            }
        }

        /// <summary>
        /// Opens the specified file using the appropriate preview handler and displays the result in this PreviewHandlerHost.
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns></returns>
        public override bool Open(string file_name)
        {
            UnloadPreviewHandler();

            if (string.IsNullOrEmpty(file_name))
            {
                ErrorMessage = Messages.FileNotLoaded;
                return false;
            }

            // try to get GUID for the preview handler
            var guid = GetPreviewHandlerGUID(file_name);
            ErrorMessage = "";

            if (guid != Guid.Empty)
            {
                try
                {
                    if (guid != _handler_guid)
                    {
                        _handler_guid = guid;

                        // need to instantiate a different COM type (file format has changed)
                        if (_handler != null) Marshal.FinalReleaseComObject(_handler);

                        // use reflection to instantiate the preview handler type
                        var com_type = Type.GetTypeFromCLSID(_handler_guid);
                        _handler = Activator.CreateInstance(com_type);
                    }

                    if (_handler is IInitializeWithFile)
                    {
                        // some handlers accept a filename
                        ((IInitializeWithFile)_handler).Initialize(file_name, 0);
                    }
                    else if (_handler is IInitializeWithStream)
                    {
                        if (File.Exists(file_name))
                        {
                            // other handlers want an IStream (in this case, a file stream)
                            _handler_stream = File.Open(file_name, FileMode.Open);
                            var stream = new ComStream(_handler_stream);
                            ((IInitializeWithStream)_handler).Initialize(stream, 0);
                        }
                        else
                        {
                            ErrorMessage = Messages.FileNotFound;
                        }
                    }

                    if (_handler is IPreviewHandler)
                    {
                        // bind the preview handler to the control's bounds and preview the content
                        Rectangle r = ClientRectangle;
                        ((IPreviewHandler)_handler).SetWindow(Handle, ref r);
                        ((IPreviewHandler)_handler).DoPreview();

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = Messages.PreviewError + ":\n" + ex.Message;
                }
            }
            else
            {
                ErrorMessage = Messages.NoPreviewAvailable;
            }

            return false;
        }
    }
}