using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace EasyPreview.PreviewHandler
{
    /// <summary>
    /// Provides a bare-bones implementation of System.Runtime.InteropServices.IStream that wraps an System.IO.Stream.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    internal class COMStream : IStream
    {
        private readonly Stream _inner_stream;

        /// <summary>
        /// Initialises a new instance of the StreamWrapper class, using the specified System.IO.Stream.
        /// </summary>
        /// <param name="inner"></param>
        public COMStream(Stream inner)
        {
            _inner_stream = inner;
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="ppstm"></param>
        public void Clone(out IStream ppstm)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="grf_commit_flags"></param>
        public void Commit(int grf_commit_flags)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="pstm"></param>
        /// <param name="cb"></param>
        /// <param name="pcb_read"></param>
        /// <param name="pcb_written"></param>
        public void CopyTo(IStream pstm, long cb, IntPtr pcb_read, IntPtr pcb_written)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="lib_offset"></param>
        /// <param name="cb"></param>
        /// <param name="dw_lock_type"></param>
        public void LockRegion(long lib_offset, long cb, int dw_lock_type)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Reads a sequence of bytes from the underlying System.IO.Stream.
        /// </summary>
        /// <param name="pv"></param>
        /// <param name="cb"></param>
        /// <param name="pcb_read"></param>
        public void Read(byte[] pv, int cb, IntPtr pcb_read)
        {
            long bytes_read = _inner_stream.Read(pv, 0, cb);
            if (pcb_read != IntPtr.Zero) Marshal.WriteInt64(pcb_read, bytes_read);
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public void Revert()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Advances the stream to the specified position.
        /// </summary>
        /// <param name="dlib_move"></param>
        /// <param name="dw_origin"></param>
        /// <param name="plib_new_position"></param>
        public void Seek(long dlib_move, int dw_origin, IntPtr plib_new_position)
        {
            long pos = _inner_stream.Seek(dlib_move, (System.IO.SeekOrigin)dw_origin);
            if (plib_new_position != IntPtr.Zero) Marshal.WriteInt64(plib_new_position, pos);
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="lib_new_size"></param>
        public void SetSize(long lib_new_size)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns details about the stream, including its length, type and name.
        /// </summary>
        /// <param name="pstatstg"></param>
        /// <param name="grf_stat_flag"></param>
        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grf_stat_flag)
        {
            pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG
            {
                cbSize = _inner_stream.Length,
                type = 2,
                pwcsName = (_inner_stream is FileStream) ? ((FileStream) _inner_stream).Name : string.Empty
            };
            // stream type
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="lib_offset"></param>
        /// <param name="cb"></param>
        /// <param name="dw_lock_type"></param>
        public void UnlockRegion(long lib_offset, long cb, int dw_lock_type)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Writes a sequence of bytes to the underlying System.IO.Stream.
        /// </summary>
        /// <param name="pv"></param>
        /// <param name="cb"></param>
        /// <param name="pcb_written"></param>
        public void Write(byte[] pv, int cb, IntPtr pcb_written)
        {
            _inner_stream.Write(pv, 0, cb);
            if (pcb_written != IntPtr.Zero) Marshal.WriteInt64(pcb_written, (Int64)cb);
        }
    }

}