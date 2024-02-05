using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IIABackend
{
    /// <summary>
    /// JWT Token Manger
    /// </summary>
    public class JWTTokenBuilder
    {
        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="phoneNumber">Phone Number</param>
        /// <param name="id">id</param>
        /// <param name="isAdmin">isAdmin</param>
        /// <param name="membershipStatus">membershipStatus</param>
        /// <param name="chapterId">Chapter Id</param>
        /// <returns>Token</returns>
        public static LoginMetadata GenerateJwtToken(string phoneNumber, string id, bool isAdmin, string membershipStatus, int chapterId)
        {
            // generate token that is valid always
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("TokenSecret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("phoneNumber", phoneNumber), new Claim("id", id), new Claim("isAdmin", isAdmin.ToString()), new Claim("membershipStatus", membershipStatus), new Claim("chapterId", chapterId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LoginMetadata(id, phoneNumber, membershipStatus, chapterId, isAdmin, tokenString);
        }

        /// <summary>
        /// SMS Sending function
        /// </summary>
        /// <param name="phoneNumber">phoneNumber</param>
        /// <param name="msg">msg</param>
        /// <returns>Token</returns>
        public static string SendSMS(string phoneNumber, string msg)
        {
            string url = "http://bhashsms.com/api/sendmsg.php?user=" + Environment.GetEnvironmentVariable("otpUserName") + "&pass=" + Environment.GetEnvironmentVariable("otpPassword") + "&sender=IIANHO&phone=" + phoneNumber + "&text=Dear%20" + msg + "%2C%20Wish%20you%20very%20Happy%20Birthday.God%20bless%20you%20with%20good%20luck%2C%20happiness%2C%20good%20health%20and%20success%20in%20every%20walk%20of%20your%20life.%20President%20IIA&priority=ndnd&stype=normal";
            string strResponce = GetResponse(url);
            return strResponce;
        }

        /// <summary>
        /// End SMS Sending function
        /// Get Response function
        /// </summary>
        /// <param name="smsURL">smsURL</param>
        /// <returns>Token</returns>
        public static string GetResponse(string smsURL)
        {
            try
            {
                WebClient objWebClient = new WebClient();
                System.IO.StreamReader reader = new System.IO.StreamReader(objWebClient.OpenRead(smsURL));
                string resultHTML = reader.ReadToEnd();
                return resultHTML;
            }
            catch (Exception)
            {
                return "Fail";
            }
        }

        /// <summary>
        /// Generate OTP JWT Token
        /// </summary>
        /// <param name="phoneNumber">Phone Number</param>
        /// <returns>Token</returns>
        public static string GenerateOTPJwtToken(string phoneNumber)
        {
            Random generator = new Random();
            int r = generator.Next(100000, 1000000);
            string otp = r.ToString();
            string smsResult = SendSMS(phoneNumber, otp);

            if (smsResult == "Fail")
            {
                return "error";
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("TokenSecret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("phoneNumber", phoneNumber), new Claim("otp", otp) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        /// <summary>
        /// Validate given token
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="otp">OTP</param>
        /// <returns>Id if valid, null otherwise</returns>
        public static bool ValidateOTPJwtToken(string token, string phoneNumber, string otp)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("TokenSecret"));
            try
            {
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    },
                    out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string phoneNumberFromToken = jwtToken.Claims.First(x => x.Type == "phoneNumber").Value;
                string otpFromToken = jwtToken.Claims.First(x => x.Type == "otp").Value;
                if (otpFromToken == otp && phoneNumberFromToken == phoneNumber)
                {
                    return true;
                }

                // return account id from JWT token if validation successful
                return false;
            }
            catch
            {
                // return null if validation fails
                return false;
            }
        }

        /// <summary>
        /// Validate given OTPtoken
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>Id if valid, null otherwise</returns>
        public static LoginMetadata ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("TokenSecret"));
            try
            {
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    },
                    out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                var phoneNumber = jwtToken.Claims.First(x => x.Type == "phoneNumber").Value;
                var isAdmin = jwtToken.Claims.First(x => x.Type == "isAdmin").Value == bool.TrueString;
                var membershipStatus = jwtToken.Claims.First(x => x.Type == "membershipStatus").Value;
                var chapterId = jwtToken.Claims.First(x => x.Type == "chapterId").Value;

                // return account id from JWT token if validation successful
                return new LoginMetadata(userId, phoneNumber, membershipStatus, int.Parse(chapterId), isAdmin, token);
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
