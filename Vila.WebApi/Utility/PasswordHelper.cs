using System.Security.Cryptography;
using System.Text;

namespace Vila.WebApi.Utility
{
    public static class PasswordHelper
    {
        public static string EncodePasswordMd5(string pass)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string;
            return BitConverter.ToString(encodedBytes);
        }
        public static string EncodeProSecurity(string pass)
        {
            var first = EncodePasswordMd5(pass);
            var second = EncodePasswordMd5(first);
            return EncodePasswordMd5(second);
        }
    }
}
