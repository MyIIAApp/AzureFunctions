using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Data;
using ExcelDataReader;
using System.IO;
using System;

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
        
        
    }
}
