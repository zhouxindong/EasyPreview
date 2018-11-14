using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasyPreview.Host
{
    public class PreviewHandlerHostFactory
    {
        /// <summary>
        /// from the extension of filename to find Preview Handler
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="preview_handler_host_mapping"></param>
        /// <returns></returns>
        public static PreviewHandlerHostBase Create(
            string file_name,
            List<Tuple<PreviewHandlerHostBase, List<string>, bool>> preview_handler_host_mapping
            )
        {
            PreviewHandlerHostBase preview_handler_host = null;

            string extension = Path.GetExtension(file_name.ToLower());

            if (!string.IsNullOrEmpty(extension))
            {
                string low_pointless_ext = extension.ToLower().Replace(@".", string.Empty);

                foreach (var mapping in preview_handler_host_mapping)
                {
                    if (mapping.Item2 != null)
                        foreach (var ext in mapping.Item2)
                        {
                            if (low_pointless_ext == ext)
                            {
                                preview_handler_host = mapping.Item1;
                                break;
                            }
                        }

                    if (preview_handler_host != null)
                        break;
                }
            }

            if (preview_handler_host == null)
            {
                var default_handler_host_mapping = preview_handler_host_mapping.FirstOrDefault(t => t.Item3);
                if (default_handler_host_mapping != null)
                    preview_handler_host = default_handler_host_mapping.Item1;
            }

            return preview_handler_host;
        }
    }
}