using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// A utility class for performing an MD5 hash on a string.
    /// </summary>
    public class MD5HashUtility
    {
        /// <summary>
        /// Compute an MD5 Hash
        /// </summary>
        /// <param name="inVal">Value to be hashed</param>
        /// <returns>MD5 Hashed string</returns>
        public static string HashString(string inVal)
        {
            string retVal = string.Empty;

            MD5CryptoServiceProvider md5Service = new MD5CryptoServiceProvider();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(inVal);
            byte[] hash = md5Service.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            retVal = sb.ToString();

            return retVal;
        }
    }
}
