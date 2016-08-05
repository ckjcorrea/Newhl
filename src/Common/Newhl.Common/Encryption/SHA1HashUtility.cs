using System;
using System.Security.Cryptography;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// Salted password hashing with PBKDF2-SHA1.
    /// </summary>
    public class SHA1HashUtility
    {
        // The following constants may be changed without breaking existing hashes.

        /// <summary>
        /// The byte size of the salt
        /// </summary>
        public const int SaltByteSize = 24;

        /// <summary>
        /// The byte size of the hash
        /// </summary>
        public const int HashByteSize = 24;

        /// <summary>
        /// The default number of iterations to hash
        /// </summary>
        public const int Pbkdf2Iterations = 1000;

        /// <summary>
        /// The default constructor for this utility
        /// </summary>
        public SHA1HashUtility() : this(Pbkdf2Iterations) { }

        /// <summary>
        /// A constructor that defines the number of iterations to hash
        /// </summary>
        /// <param name="iterations">Number of Iterations to Hash</param>
        public SHA1HashUtility(int iterations)
        {
            this.Iterations = iterations;
        }

        /// <summary>
        /// Gets the salt generated when hashing a password
        /// </summary>
        public byte[] Salt { get; private set; }

        /// <summary>
        /// Gets the number of iterations used to hash a password
        /// </summary>
        public int Iterations { get; private set; }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }

        /// <summary>
        /// Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }

        /// <summary>
        /// Generate a salt using the .Net RNGCryptoServiceProvider
        /// </summary>
        /// <returns>The salt as a byte array</returns>
        private byte[] GenerateSalt()
        {
            byte[] retVal;

            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            retVal = new byte[SaltByteSize];
            csprng.GetBytes(retVal);

            return retVal;
        }

        /// <summary>
        /// Creates a salted PBKDF2 hash of the password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash of the password.</returns>
        public string HashPassword(string password)
        {
            this.Salt = this.GenerateSalt();

            // Hash the password and encode the parameters
            byte[] hash = PBKDF2(password, this.Salt, this.Iterations, HashByteSize);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">The salted password to check.</param>
        /// <param name="correctHash">A hash of the correct password.</param>
        /// <param name="salt">The salt used to precede the password</param>
        /// <param name="iterations">The number of iterations to hash</param>
        /// <returns>True if the password is correct. False otherwise.</returns>
        public static bool ValidatePassword(string password, string correctHash, byte[] salt, int iterations)
        {
            byte[] convertedCorrectHash = Convert.FromBase64String(correctHash);

            byte[] testHash = PBKDF2(password, salt, iterations, convertedCorrectHash.Length);
            return SlowEquals(convertedCorrectHash, testHash);
        }
    }
}
