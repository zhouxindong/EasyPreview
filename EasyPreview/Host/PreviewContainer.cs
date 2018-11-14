using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EasyPreview.PaginalImage;

namespace EasyPreview.Host
{
    public partial class PreviewContainer : UserControl, IPaginalPreviewHandlerHost
    {
        public DisplayType DisplayType { get; set; }

        private string _source_doc;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] 
        private PreviewHandlerHostBase PreviewHandlerHostControl
        {
            get
            {
                var controls = this.Controls.Find("previewHandlerHost", true);

                if (controls.Any())
                    return (PreviewHandlerHostBase)controls[0];
             
                return null;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] 
        public List<Tuple<PreviewHandlerHostBase, List<string>, bool>> PreviewHostsMapping { get; set; }

        public PreviewContainer()
        {
            InitializeComponent();

            InitializeDefaultPreviewHandlerHost();

            DisplayType = DisplayType.DisplayIcon;
        }

        private void InitializeDefaultPreviewHandlerHost()
        {
            PreviewHostsMapping = DefaultPreviewHostsMapping.PreviewHostsMapping;
        }

        public void CreateLink(string source_doc)
        {
            _source_doc = source_doc;

            if (DisplayType == DisplayType.DisplayIcon)
            {
                pictureBox.Image = Icon.ExtractAssociatedIcon(source_doc).ToBitmap();
                previewHandlerHost.Visible = false;
                pictureBox.Visible = true;
            }
            else
            {
                pictureBox.Visible = false;
                Controls.RemoveByKey("previewHandlerHost");

                if (System.IO.File.Exists(source_doc))
                {
                    var preview_handler_host = PreviewHandlerHostFactory.Create(source_doc, PreviewHostsMapping);
                    preview_handler_host.Visible = false;
                    preview_handler_host.Name = "previewHandlerHost";
                    preview_handler_host.Dock = DockStyle.Fill;
                    preview_handler_host.Open(source_doc);
                    Controls.Add(preview_handler_host);
                    preview_handler_host.Visible = true;
                }
            }
        }

        #region Implementation of IPaginalPreviewHandlerHost

        /// <summary>
        /// Pages number
        /// </summary>
        public short PagesNumber
        {
            get
            {
                if (PreviewHandlerHostControl is IPaginalPreviewHandlerHost)
                    return ((IPaginalPreviewHandlerHost)PreviewHandlerHostControl).PagesNumber;

                return 1;
            }
        }

        /// <summary>
        /// Current page number
        /// </summary>
        public short CurrentPageNumber
        {
            get
            {
                if (PreviewHandlerHostControl is IPaginalPreviewHandlerHost)
                    return ((IPaginalPreviewHandlerHost)PreviewHandlerHostControl).CurrentPageNumber;
                return 0;
            }
        }

        /// <summary>
        /// Go to the next page
        /// </summary>
        public void NextPage()
        {
            if (PreviewHandlerHostControl is IPaginalPreviewHandlerHost)
                ((IPaginalPreviewHandlerHost)PreviewHandlerHostControl).NextPage();
        }

        /// <summary>
        /// Go to the previous page
        /// </summary>
        /// <returns></returns>
        public void PreviousPage()
        {
            if (PreviewHandlerHostControl is IPaginalPreviewHandlerHost)
                ((IPaginalPreviewHandlerHost)PreviewHandlerHostControl).PreviousPage();
        }

        /// <summary>
        /// Go to the page with specified number
        /// </summary>
        /// <param name="page_number">page number/номер страницы</param>
        /// <exception cref="ArgumentOutOfRangeException">If page number is out of the range/В случае, если номер страницы задан неверно</exception>
        public void GoToPage(short page_number)
        {
            if (PreviewHandlerHostControl is IPaginalPreviewHandlerHost)
                ((IPaginalPreviewHandlerHost)PreviewHandlerHostControl).GoToPage(page_number);
        }

        #endregion

    }

    public enum DisplayType
    {
        DisplayIcon,
        DisplayContent
    }
}
