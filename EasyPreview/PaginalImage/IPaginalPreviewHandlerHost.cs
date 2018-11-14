using System;

namespace EasyPreview.PaginalImage
{
    public interface IPaginalPreviewHandlerHost
    {
        /// <summary>
        /// Pages number
        /// </summary>
        short PagesNumber { get; }

        /// <summary>
        /// Current page number
        /// </summary>
        short CurrentPageNumber { get; }

        /// <summary>
        /// Goes to the next page
        /// </summary>
        void NextPage();

        /// <summary>
        /// Goes to the previous page
        /// </summary>
        void PreviousPage();

        /// <summary>
        /// Goes to the page with specified number
        /// </summary>
        /// <param name="page_number">page number</param>
        /// <exception cref="ArgumentOutOfRangeException">In case if page number is wrong</exception>
        void GoToPage(short page_number);
    }

}