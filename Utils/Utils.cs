using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace B2Emulator
{
    public class Utils
    {
        /// <summary>
        /// Checks for the presence of a valid header, given a header dictionary and the name of the header.
        /// If found in the dictionary and valid, the header will be returned. If not found in the dictionary
        /// or found but invalid, an error will be added to the "errorList" parameter and null will be returned.
        /// </summary>
        /// <param name="headerDict">The header dictionary to check.</param>
        /// <param name="headerName">The name of the header being retrieved.</param>
        /// <param name="errorList">A list of errors to which to add any errors encountered while retrieving
        /// the header.</param>
        public static string GetHeader(IHeaderDictionary headerDict, string headerName, List<string> errorList)
        {
            var headers = headerDict[headerName];

            if (headers.Count != 1)
            {
                errorList.Add(String.Format("Request must have one {0} header.", headerName));
                return null;
            }
            else
            {
                var headerValue = headers[0];

                if (headerValue.Length == 0)
                {
                    errorList.Add($"\"{headerName}\" header must be at least one character long.");
                    return null;
                }
                else
                {
                    return headerValue;
                }
            }
        }

        public static long DateTimeNowMs()
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds);
        }
    }
}