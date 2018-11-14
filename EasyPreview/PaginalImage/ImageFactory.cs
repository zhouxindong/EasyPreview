using System;
using System.IO;
using EasyPreview.Resources;

namespace EasyPreview.PaginalImage
{
    public static class ImageFactory
    {
        public static IPaginalImageFile Create(string file_name)
        {
            if (!File.Exists(file_name))
                throw new FileNotFoundException(Messages.FileNotFound, file_name);

            string extension = Path.GetExtension(file_name.ToLower());

            if (string.IsNullOrEmpty(extension))
                throw new ArgumentException(Messages.FileWithoutExtension, file_name);

            string low_pointless_ext = extension.ToLower().Replace(@".", string.Empty);

            IPaginalImageFile image;

            if (low_pointless_ext == "tif" || low_pointless_ext == "tiff")
            {
                try
                {
                    // first of all will try to use native .net image classes
                    // because they work much faster then CachedLibTiffImage
                    image = new PaginalImage(file_name);
                    // tyr to load image into memory
                }
                catch
                {
                    image = new CachedLibTiffImage(file_name);
                }
            }
            else
                image = new PaginalImage(file_name);

            return image;
        }
    }
}