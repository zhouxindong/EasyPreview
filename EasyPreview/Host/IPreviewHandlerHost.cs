namespace EasyPreview.Host
{
    /// <summary>
    /// interface for those implement IPreviewHandler COM class
    /// </summary>
    public interface IPreviewHandlerHost
    {
        bool Open(string file_name);
    }
}