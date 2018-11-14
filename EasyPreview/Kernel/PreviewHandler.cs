using System;
using System.IO;
using Microsoft.Win32;

namespace EasyPreview.Kernel
{
    public class PreviewHandler
    {
        // The GUID of IPreviewHandler
        public const string CLSID = "8895b1c6-b41f-4c1c-a562-0d564250836f";
        public static Guid PreviewHandlerGuid = new Guid(CLSID);

        /// <summary>
        /// This method returns the GUID of supported preview handler associated strExtension
        /// </summary>
        /// <param name="preview_handler_guid"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetCLSIDFromExtensionShellEx(Guid preview_handler_guid, string extension)
        {
            var clsid = string.Empty;
            var sub_key = $@"{extension}\ShellEx\{preview_handler_guid:B}";
            using (var hk_preview_handler = Registry.ClassesRoot.OpenSubKey(
                sub_key))
            {
                if (hk_preview_handler != null)
                {
                    clsid = hk_preview_handler.GetValue(string.Empty).ToString();
                }
            }
            return clsid;
        }

        public static string GetCLSIDFromExtensionShellEx(string extension)
        {
            return GetCLSIDFromExtensionShellEx(PreviewHandlerGuid, extension);
        }

        /// <summary>
        /// Create a COM instance, it is like CoCreateInstance in C++
        /// </summary>
        /// <returns></returns>
        public static object CreateObjectByCLSID(string clsid)
        {
            var new_guid = new Guid(clsid);

            // Get the type of the COM interface specified by CLSID(Guid)
            var handler_type = Type.GetTypeFromCLSID(new_guid, true);
            return Activator.CreateInstance(handler_type);
        }

        /// <summary>
        /// Initialize COM object with a filename which content can be preivew later
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="file_name"></param>
        public static bool InitHandlerObjectWithFile(object obj, string file_name)
        {
            var file_init = obj as IInitializeWithFile;
            if (file_init == null) return false;
            file_init.Initialize(file_name, 0);
            return true;
        }

        public static bool InitHandlerObjectWithStream(object obj, string file_name)
        {
            var stream_init = obj as IInitializeWithStream;
            if (stream_init == null) return false;
            var stream = new FileStream(file_name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ComStream com_stream = new ComStream(stream);
            stream_init.Initialize(com_stream, 0);
            return true;
        }
    }
}