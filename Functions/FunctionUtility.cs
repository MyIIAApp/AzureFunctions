using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace IIABackend
{
    /// <summary>
    /// News Create function
    /// </summary>
    public static class FunctionUtility
    {
        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="req">HTTP request</param>
        /// <returns>Tokenobject</returns>
        public static LoginMetadata ValidateToken(HttpRequest req)
        {
            var authToken = req.Headers["Authorization"];

            if (string.IsNullOrEmpty(authToken))
            {
                return null;
            }

            return JWTTokenBuilder.ValidateJwtToken(authToken);
        }

        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="req">HTTP request</param>
        /// <returns>Tokenobject</returns>
        public static LoginMetadata ValidateToken(string req)
        {
            var authToken = req;

            if (string.IsNullOrEmpty(authToken))
            {
                return null;
            }

            return JWTTokenBuilder.ValidateJwtToken(authToken);
        }

        /// <summary>
        /// Validate OTP Token
        /// </summary>
        /// <param name="req">HTTP request</param>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="otp">OTP</param>
        /// <returns>boolean</returns>
        public static bool ValidateOTPToken(HttpRequest req, string phoneNumber, string otp)
        {
            var authToken = req.Headers["Authorization"];

            if (string.IsNullOrEmpty(authToken))
            {
                return false;
            }

            return JWTTokenBuilder.ValidateOTPJwtToken(authToken, phoneNumber, otp);
        }

        /// <summary>
        /// sha256 hash calculator
        /// </summary>
        /// <param name="rawData">Input string to be hashed</param>
        /// <returns>hashed value</returns>
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA512 sha256Hash = SHA512.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder hashedValue = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    hashedValue.Append(bytes[i].ToString("x2"));
                }

                return hashedValue.ToString();
            }
        }

        /// <summary>
        /// string Encoder
        /// </summary>
        /// <param name="input">Input string to be encoded</param>
        /// <returns>encoded value</returns>
        public static string StringEncoder(string input)
        {
            string result = string.Empty;
            foreach (char c in input)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    result += c;
                }
            }

            return result;
        }

        /// <summary>
        /// HTML Decoder
        /// </summary>
        /// <param name="html">Input string to be decoded</param>
        /// <returns>encoded value</returns>
        public static string HTMLDecoder(string html)
        {
            var array = html.Split(';');
            var result = string.Empty;
            foreach (var c in array)
            {
                result += Convert.ToChar(int.Parse(c.Length > 2 ? c.Substring(2) : "160"));
            }

            return result;
        }

        /// <summary>
        /// HTML Encoder
        /// </summary>
        /// <param name="input">Input string to be encoded</param>
        /// <returns>encoded value</returns>
        public static string HTMLEncoder(string input)
        {
            char[] chars = HttpUtility.HtmlEncode(input).ToCharArray();
            StringBuilder result = new StringBuilder(input.Length + (int)(input.Length * 0.1));

            foreach (char c in chars)
            {
                int value = Convert.ToInt32(c);
                result.AppendFormat("&#{0};", value);
            }

            return result.ToString();
        }

        /// <summary>
        /// Convert String to Date Time
        /// </summary>
        /// <param name="input">Input string to be encoded</param>
        /// <returns>encoded value</returns>
        public static DateTime? ConvertDate(string input)
        {
            DateTime? result = null;
            if (!string.IsNullOrEmpty(input))
            {
                result = Convert.ToDateTime(input);
            }

            return result;
        }

        /// <summary>
        /// Delete Invoice
        /// </summary>
        /// <param name="userId"> User Id</param>
        /// <param name="expiryYea">Expiry Year</param>
        public static void DeletePayemnt(int userId, int expiryYea)
        {
            if (expiryYea > 0)
            {
                UserProfile userProfile = Database.GetUserProfile(userId, null, null);
                userProfile.MembershipExpiryYears = userProfile.MembershipExpiryYears.Replace("," + expiryYea.ToString(), string.Empty);
                userProfile.MembershipExpiryYears = userProfile.MembershipExpiryYears.Replace(expiryYea.ToString(), string.Empty);
                string[] arr = userProfile.MembershipExpiryYears.Split(",");
                Array.Sort(arr, StringComparer.InvariantCulture);
                DateTime curr = DateTime.Now;
                userProfile.MembershipCurrentExpiryYear = int.Parse(arr.Length >= 1 ? arr[arr.Length - 1] != string.Empty ? arr[arr.Length - 1] : "1900" : "1900");
                string expiryYear = userProfile.MembershipCurrentExpiryYear.ToString();
                if (arr[arr.Length - 1] != string.Empty)
                {
                    userProfile.ProfileStatus = curr.Month <= 3 ? int.Parse(expiryYear) >= curr.Year ? 5 : int.Parse(expiryYear) == curr.Year - 1 ? 6 : 7 : int.Parse(expiryYear) > curr.Year ? 5 : int.Parse(expiryYear) == curr.Year ? 6 : 7;
                }
                else
                {
                    userProfile.ProfileStatus = 4;
                }

                Database.InsertUpdateUserProfile(userProfile, -2);
            }
        }
    }
}
