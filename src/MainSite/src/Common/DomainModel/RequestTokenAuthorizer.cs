using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// A class representing a request tokens authorization
    /// </summary>
    public class RequestTokenAuthorizer
    {

        /// <summary>
        /// A static Random instance to take advantage of seeding across requests
        /// </summary>
        private static Random randomNumberGenerator;

        /// <summary>
        /// Generate a random number using the static randomNumberGenerate and a specified start and end range
        /// </summary>
        /// <param name="rangeStart">The range start to start the generation from</param>
        /// <param name="rangeEnd">The range end for the generation</param>
        /// <returns>A random number</returns>
        private static int GenerateRandomNumber(int rangeStart, int rangeEnd)
        {
            if (RequestTokenAuthorizer.randomNumberGenerator == null)
            {
                RequestTokenAuthorizer.randomNumberGenerator = new Random();
            }

            return RequestTokenAuthorizer.randomNumberGenerator.Next(rangeStart, rangeEnd);            
        }

        /// <summary>
        /// The default random number start
        /// </summary>
        private const int RandomStart = 1000;

        /// <summary>
        /// The default random number end
        /// </summary>
        private const int RandomEnd = 9999;

        /// <summary>
        /// Generate a random number and use it to populate the Verifier Code
        /// </summary>
        public static string GenerateVerifierCode()
        {
            return RequestTokenAuthorizer.GenerateRandomNumber(RequestTokenAuthorizer.RandomStart, RequestTokenAuthorizer.RandomEnd).ToString();
        }
    }
}
