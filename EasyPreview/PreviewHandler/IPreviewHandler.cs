using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EasyPreview.PreviewHandler
{
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid(PreviewHandler.CLSID)]
    public interface IPreviewHandler
    {
        void SetWindow(IntPtr hwnd, ref Rectangle rect);
        void SetRect(ref Rectangle rect);
        void DoPreview();
        void Unload();
        void SetFocus();
        void QueryFocus(out IntPtr phwnd);

        [PreserveSig]
        uint TranslateAccelerator(ref Message pmsg);
    }
}