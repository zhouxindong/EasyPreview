using System.Windows.Forms;

namespace EasyPreview.Host
{
    /// <summary>
    /// Base class for all preview handler hosts
    /// <remarks>This class is not abstract because VS can't show derived controls in design mode
    /// if they are derived from asbtract class.</remarks>
    /// </summary>
    public class PreviewHandlerHostBase : Control, IPreviewHandlerHost
    {
        #region Implementation of IPreviewHandlerHost
        public virtual bool Open(string file_name)
        {
            return false;
        }
        #endregion
    }
}