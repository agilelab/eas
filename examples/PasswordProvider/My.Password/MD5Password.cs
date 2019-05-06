using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAS.Explorer;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Text;

namespace My.Passwords
{
    public class MD5Password : IPasswordProvider
    {
        #region IPasswordProvider 成员

        public byte[] Encrypt(string password)
        {
            byte[] result = Encoding.Default.GetBytes(password.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            byte[] buffer = new byte[64];
            Buffer.BlockCopy(output, 0, buffer, 0, 16);
            return buffer;
        }

        public bool Verify(string password, byte[] key)
        {
            byte[] result = Encoding.Default.GetBytes(password.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            byte[] buffer = new byte[64];
            Buffer.BlockCopy(output, 0, buffer, 0, 16);
            return EAS.Security.Bytes.Equals(buffer, key);
        }

        #endregion
    }
}
