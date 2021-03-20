using System.Security.Cryptography;
using System.Text;

namespace Gravatar
{
    /// <summary>
    /// Extension methods for converting an email to a Gravatar URL.
    /// </summary>
    public static class GravatarExtensions
    {
        /// <summary>
        /// Convert an email to a Gravatar URL.
        /// </summary>
        /// <remarks>
        /// There is no validation to ensure a valid email.
        /// <see langword="null" />, empty <see cref="string"/> and blank spaces result in an empty URL (<see cref="string.Empty"/>).
        /// </remarks>
        /// <param name="email">The email.</param>
        /// <returns>The Gravatar URL.</returns>
        public static string ToGravatar(this string email)
        {
            const string GravatarAvatarEndpoint = "https://www.gravatar.com/avatar/";
            const string HexadecimalStringFormat = "x2";

            if (string.IsNullOrWhiteSpace(email))
            {
                return string.Empty;
            }

            using (var md5 = MD5.Create())
            {
                var emailAsciiBytes = Encoding.ASCII.GetBytes(email);
                var hashedBytes = md5.ComputeHash(emailAsciiBytes);

                var hashedHexStringBuilder = new StringBuilder();
                foreach (var hashedByte in hashedBytes)
                {
                    hashedHexStringBuilder.Append(hashedByte.ToString(HexadecimalStringFormat));
                }

                return $"{GravatarAvatarEndpoint}{hashedHexStringBuilder}";
            }
        }
    }
}
