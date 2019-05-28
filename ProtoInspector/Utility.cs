using System;
using System.Text;
using Fiddler;

namespace yy.ProtoInspector
{
    internal static class Utility
    {
        public static string ToHexString(this byte[] ba)
        {
            if (ba == null || ba.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] HexToByteArray(this String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static byte[] DecompressBody(HTTPHeaders headers, byte[] ba)
        {
            var copy = (byte[])ba.Clone();
            Fiddler.Utilities.utilDecodeHTTPBody(headers, ref copy);
            return copy;
        }
    }
}
