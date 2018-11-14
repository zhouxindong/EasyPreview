using System;
using System.Drawing;
using System.Drawing.Imaging;
using EasyPreview.Resources;

namespace EasyPreview.PaginalImage
{
    public class PaginalImage : IPaginalImageFile
    {
        public string FileName { get; }

        private Bitmap _bitmap;

        private bool _disposed;

        /// <summary>
        /// GDI+ bitmap
        /// </summary>
        public Bitmap CurrentPage => _bitmap ?? (_bitmap = LoadImage());

        /// <summary>
        /// Pages number
        /// </summary>
        public short PagesNumber
        {
            get
            {
                try
                {
                    return (short)_bitmap.GetFrameCount(FrameDimension.Page);
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }

        /// <summary>
        /// Current page number
        /// </summary>
        public short CurrentPageNumber { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PaginalImage(string file_name)
        {
            FileName = file_name;
            CurrentPageNumber = 0;
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~PaginalImage()
        {
            Dispose(false);
        }

        /// <summary>
        /// Get next page
        /// </summary>
        /// <returns></returns>
        public Bitmap GetNextPage()
        {
            if (CurrentPageNumber < PagesNumber - 1)
                CurrentPageNumber++;
            else
                CurrentPageNumber = 0;

            _bitmap.SelectActiveFrame(FrameDimension.Page, CurrentPageNumber);

            return _bitmap;
        }

        /// <summary>
        /// Get previous page
        /// </summary>
        /// <returns></returns>
        public Bitmap GetPreviousPage()
        {
            if (CurrentPageNumber == 0)
                CurrentPageNumber = (short)(PagesNumber - 1);
            else
                CurrentPageNumber--;

            _bitmap.SelectActiveFrame(FrameDimension.Page, CurrentPageNumber);

            return _bitmap;
        }

        /// <summary>
        /// Go to the specified page
        /// </summary>
        /// <param name="page_number">page number</param>
        /// <returns></returns>
        public Bitmap GoToPage(short page_number)
        {
            if (page_number > PagesNumber - 1)
                throw new ArgumentOutOfRangeException(
                    "page_number",
                    Messages.ExceedeingPageNumber
                );

            CurrentPageNumber = page_number;

            _bitmap.SelectActiveFrame(FrameDimension.Page, page_number);

            return _bitmap;
        }

        /// <summary>
        /// Load image from file
        /// </summary>
        /// <returns></returns>
        private Bitmap LoadImage()
        {
            return (Bitmap)Image.FromFile(FileName);
        }

        #region Implementation of IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_bitmap != null)
                    _bitmap.Dispose();
            }

            _disposed = true;
        }
        #endregion
    }
}