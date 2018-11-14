using System;
using System.Collections.Generic;

namespace EasyPreview.Host
{
    public static class DefaultPreviewHostsMapping
    {
        public static List<Tuple<PreviewHandlerHostBase, List<string>, bool>> PreviewHostsMapping { get; set; }

        static DefaultPreviewHostsMapping()
        {
            PreviewHostsMapping = new List<Tuple<PreviewHandlerHostBase, List<string>, bool>>();

            PreviewHandlerHostBase default_handler_host = new DefaultPreviewHandlerHost();
            var default_handler_host_mapping = new Tuple<PreviewHandlerHostBase, List<string>, bool>(
                default_handler_host,
                null,
                true
            );
            PreviewHostsMapping.Add(default_handler_host_mapping);

            PreviewHandlerHostBase browsert_handler_host = new BrowserPreviewHandlerHost();
            var browser_handler_host_mapping = new Tuple<PreviewHandlerHostBase, List<string>, bool>(
                browsert_handler_host,
                new List<string>() { "xml", "htm", "html" },
                false
            );
            PreviewHostsMapping.Add(browser_handler_host_mapping);

            PreviewHandlerHostBase image_handler_host = new ImagePreviewHandlerHost();
            var image_handler_host_mapping = new Tuple<PreviewHandlerHostBase, List<string>, bool>(
                image_handler_host,
                new List<string>() { "tif", "tiff", "jpg", "jpeg", "bmp", "gif", "png" },
                false
            );

            PreviewHostsMapping.Add(image_handler_host_mapping);
        }
    }
}