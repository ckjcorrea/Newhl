/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace Newhl.Common.Utilities
{
    public class Utils
    {
        private const string HtmlTagPattern = "<.*?>";
        /// <summary>
        /// Clean all HTML tags out of the given string
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string StripHtml(string inputString)
        {
            string retVal = string.Empty;

            if (inputString != null)
            {
                retVal = Regex.Replace(inputString, @"<(.|\n)*?>", string.Empty);
            }

            return retVal;
        }
        /// <summary>
        /// Clean all javascript tags out of the given string 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string StripJavascript(string inputString)
        {
            string retVal = inputString;

            int scriptStart = retVal.IndexOf("<script>");

            if (scriptStart > -1)
            {
                int scriptEnd = retVal.IndexOf("</script>");
                retVal.Remove(scriptStart, (scriptEnd + 9) - scriptStart);
            }

            return retVal;
        }

        /// <summary>
        /// Url Encode a string, also replace spaces with underscores and
        /// . with %2E, for some reason . cause problems with mvc routes
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncodeForUrl(string source)
        {
            string retVal = source.Replace(" ", "_");
            retVal = retVal.Replace(".", ";;");
            return WebUtility.UrlEncode(retVal);
        }

        /// <summary>
        /// Url Decode a string, also replace with underscores with spaces and
        /// . with %2E, for some reason . cause problems with mvc routes
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DecodeFromUrl(string source)
        {
            string retVal = WebUtility.UrlDecode(source);
            retVal = retVal.Replace(";;", ".");
            retVal = retVal.Replace("_", " ");
            return retVal;
        }

        public static DateTime DetermineStartOfWeek(DateTime targetDate)
        {
            DateTime retVal = targetDate;

            switch (targetDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:                    
                    break;
                case DayOfWeek.Monday:
                    retVal = targetDate.AddDays(-1);
                    break;
                case DayOfWeek.Tuesday:
                    retVal = targetDate.AddDays(-2);
                    break;
                case DayOfWeek.Wednesday:
                    retVal = targetDate.AddDays(-3);
                    break;
                case DayOfWeek.Thursday:
                    retVal = targetDate.AddDays(-4);
                    break;
                case DayOfWeek.Friday:
                    retVal = targetDate.AddDays(-5);
                    break;
                case DayOfWeek.Saturday:
                    retVal = targetDate.AddDays(-6);
                    break;
            }

            return retVal;
        }
    }
}
