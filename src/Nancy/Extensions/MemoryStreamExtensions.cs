﻿namespace Nancy.Extensions
{
    using System;
    using System.IO;

    internal static class MemoryStreamExtensions
    {
        public static ArraySegment<byte> GetBufferSegment(this MemoryStream stream)
        {
#if DOTNET5_4
            ArraySegment<byte> buffer;
            if (stream.TryGetBuffer(out buffer))
            {
                return buffer;
            }
#endif
            var bytes = stream.GetBytes();

            return new ArraySegment<byte>(bytes, 0, bytes.Length);
        }

        private static byte[] GetBytes(this MemoryStream stream)
        {
#if DOTNET5_4
            return stream.ToArray(); // This is all we have if TryGetBuffer fails in GetBufferSegment
#else
            return stream.GetBuffer();
#endif
        }
    }
}
