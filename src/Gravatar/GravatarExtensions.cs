using System;
using System.Security.Cryptography;
using System.Text;

namespace Gravatar
{
    /// <summary>
    /// Extension methods for converting an email to a Gravatar URL.
    /// </summary>
    public static class GravatarExtensions
    {
        private const int SizeMinimum = 1;
        private const int SizeMaximum = 2048;

        /// <summary>
        /// Convert an email to a Gravatar URL.
        /// </summary>
        /// <param name="email">The email. There is no validation to ensure a valid email. <see langword="null" />, empty <see cref="string"/> and blank spaces email result in an empty URL (<see cref="string.Empty"/>).</param>
        /// <param name="size">The Gravatar size. Defaults to 80. Minimum = 1, Maximum = 2048.</param>
        /// <returns>The Gravatar URL.</returns>
        /// <exception cref="ArgumentException">Validation failure.</exception>
        public static string ToGravatar(this string email, int size = 80)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return string.Empty;
            }

            Validate(size);

            const string GravatarAvatarEndpoint = "https://www.gravatar.com/avatar/";
            const string HexadecimalStringFormat = "x2";

            using (var md5 = MD5.Create())
            {
                var emailAsciiBytes = Encoding.ASCII.GetBytes(email);
                var hashedBytes = md5.ComputeHash(emailAsciiBytes);

                var hashedHexStringBuilder = new StringBuilder();
                foreach (var hashedByte in hashedBytes)
                {
                    hashedHexStringBuilder.Append(hashedByte.ToString(HexadecimalStringFormat));
                }

                return $"{GravatarAvatarEndpoint}{hashedHexStringBuilder}?s={size:0}";
            }
        }

        /// <summary>
        /// Validate Gravatar parameters.
        /// </summary>
        /// <param name="size">The Gravatar size.</param>
        private static void Validate(int size)
        {
            if (size < SizeMinimum || size > SizeMaximum)
            {
                throw new ArgumentException($"The size must be a value between {SizeMinimum} and {SizeMaximum}.", nameof(size));
            }
        }
    }
}
