using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using BitMiracle.LibTiff.Classic;
using EasyPreview.Resources;

namespace EasyPreview.PaginalImage
{
    public class CachedLibTiffImage : IPaginalImageFile
    {
        /// <summary>
        /// Converted to disk pages of multipaged TIF
        /// </summary>
        private readonly Dictionary<short, Tuple<string, bool, string>> _page_files;

        private static volatile object _locker = new object();

        /// <summary>
        /// TIF file name
        /// </summary>
        public string FileName { get; protected set; }

        private Bitmap _bitmap;

        private Thread _thread;

        /// <summary>
        /// GDI+ bitmap
        /// </summary>
        public Bitmap CurrentPage => _bitmap ?? (_bitmap = Convert());

        private short _pages_number = -1;

        private bool _disposed;

        /// <summary>
        /// Pages number
        /// </summary>
        public short PagesNumber
        {
            get
            {
                if (_pages_number < 0)
                    _pages_number = GetPagesNumber(this.FileName);
                return _pages_number;
            }
        }

        /// <summary>
        /// Current page number
        /// </summary>
        public short CurrentPageNumber { get; private set; }

        public CachedLibTiffImage(string file_name)
        {
            FileName = file_name;
            CurrentPageNumber = 0;
            _page_files = new Dictionary<short, Tuple<string, bool, string>>();
            ConvertAll();
        }

        public Bitmap GetNextPage()
        {
            if (CurrentPageNumber < PagesNumber - 1)
                CurrentPageNumber++;
            else
                CurrentPageNumber = 0;

            _bitmap = Convert();

            return _bitmap;
        }

        public Bitmap GetPreviousPage()
        {
            if (CurrentPageNumber == 0)
                CurrentPageNumber = (short)(PagesNumber - 1);
            else
                CurrentPageNumber--;

            _bitmap = Convert();

            return _bitmap;
        }

        public Bitmap GoToPage(short page_number)
        {
            if (page_number > PagesNumber - 1)
                throw new ArgumentOutOfRangeException(
                    "page_number",
                     Messages.ExceedeingPageNumber
                );

            CurrentPageNumber = page_number;
            _bitmap = Convert();
            return _bitmap;
        }

        private void ConvertAll()
        {
            var thread_start = new ThreadStart(
                delegate
                {
                    for (short i = 0; i < PagesNumber; i++)
                    {
                        Convert32Bit(i);
                    }
                }
                );
            _thread = new Thread(thread_start);
            _thread.Start();
        }

        private Bitmap Convert()
        {
            Convert32Bit(CurrentPageNumber);

            if (_page_files.ContainsKey(CurrentPageNumber))
            {
                while (_page_files[CurrentPageNumber].Item2)
                {
                    Thread.Sleep(100);
                }
                return (Bitmap)Image.FromFile(_page_files[CurrentPageNumber].Item1);
            }

            return null;
        }

        private void Convert32Bit(short current_page_number)
        {
            try
            {
                if (!File.Exists(FileName))
                    throw new FileNotFoundException(Messages.FileNotFound, FileName);

                string temp_file_name = Path.GetTempFileName();
                var locked_file_name = new Tuple<string, bool, string>(
                    temp_file_name,
                    true, // lock tuple
                    string.Empty
                    );

                lock (_locker)
                {
                    if (_page_files.ContainsKey(current_page_number))
                        return;

                    _page_files.Add(
                        current_page_number,
                        locked_file_name
                        );
                }

                using (Tiff tif = Tiff.Open(FileName, "r"))
                {
                    tif.SetDirectory(current_page_number);

                    // Find the width and height of the image
                    FieldValue[] value = tif.GetField(TiffTag.IMAGEWIDTH);
                    int width = value[0].ToInt();

                    value = tif.GetField(TiffTag.IMAGELENGTH);
                    int height = value[0].ToInt();

                    // Read the image into the memory buffer
                    int[] raster = new int[height * width];
                    if (!tif.ReadRGBAImage(width, height, raster))
                        throw new Exception(Messages.CantReadFile + ": " + FileName);

                    var bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);

                    Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                    BitmapData bmpdata = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
                    byte[] bits = new byte[bmpdata.Stride * bmpdata.Height];

                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        int raster_offset = y * bitmap.Width;
                        int bits_offset = (bitmap.Height - y - 1) * bmpdata.Stride;

                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            int rgba = raster[raster_offset++];
                            bits[bits_offset++] = (byte)((rgba >> 16) & 0xff);
                            bits[bits_offset++] = (byte)((rgba >> 8) & 0xff);
                            bits[bits_offset++] = (byte)(rgba & 0xff);
                            bits[bits_offset++] = (byte)((rgba >> 24) & 0xff);
                        }
                    }

                    Marshal.Copy(bits, 0, bmpdata.Scan0, bits.Length);
                    bitmap.UnlockBits(bmpdata);


                    bitmap.Save(_page_files[current_page_number].Item1);

                    _page_files[current_page_number] = new Tuple<string, bool, string>(
                        temp_file_name,
                        false, // unlock
                        string.Empty
                        );
                }
            }
            catch (Exception exception)
            {
                string temp_file_name = Path.GetTempFileName();
                string user_error_message = Messages.ErrorOccured + ": " + exception.Message;
                var error_bitmap = CreateBitmapWithText(user_error_message);
                error_bitmap.Save(temp_file_name);
                var tuple = new Tuple<string, bool, string>(
                        temp_file_name,
                        false,
                        user_error_message
                        );
                if (_page_files.ContainsKey(current_page_number))
                    _page_files[current_page_number] = tuple;
                else
                    _page_files.Add(current_page_number, tuple);
            }
        }

        private Bitmap CreateBitmapWithText(string error_text)
        {
            const float size = 10;

            string err_text = error_text.Replace("\n", " ");

            var bitmap = new Bitmap(err_text.Length * (int)size, (int)size * 2);

            var font = new Font(FontFamily.GenericMonospace, size);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                TextRenderer.DrawText(
                    g,
                    err_text,
                    font,
                    Point.Empty,
                    Color.Black,
                    SystemColors.Control
                );
            }

            return bitmap;
        }

        private short GetPagesNumber(string file_name)
        {
            using (Tiff image = Tiff.Open(file_name, "r"))
                return GetPagesNumber(image);
        }

        private short GetPagesNumber(Tiff image)
        {
            short page_count = 0;
            do
            {
                ++page_count;
            } while (image.ReadDirectory());

            return page_count;
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
                _bitmap?.Dispose();

                if (_thread != null && _thread.IsAlive)
                    _thread.Abort();
            }

            _disposed = true;
        }
        #endregion
    }

}