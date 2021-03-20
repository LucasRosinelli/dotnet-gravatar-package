using System;
using Xunit;

namespace Gravatar.UnitTests
{
    public class GravatarExtensionsTests
    {
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData("   ", 0)]
        [InlineData(null, 100)]
        [InlineData("", 100)]
        [InlineData(" ", 100)]
        [InlineData("   ", 100)]
        [InlineData(null, 2049)]
        [InlineData("", 2049)]
        [InlineData(" ", 2049)]
        [InlineData("   ", 2049)]
        public void ToGravatar_WithEmptyOrNullOrWhiteSpaceEmail_ShouldIgnoreSizeAndReturnEmptyUrl(string email, int size)
        {
            // Arrange

            // Act
            string gravatarUrl = email.ToGravatar(size);

            // Assert
            Assert.Empty(gravatarUrl);
        }

        [Theory]
        [InlineData("admin@example.org", "96614ec98aa0c0d2ee75796dced6df54")]
        [InlineData("test@example.com", "55502f40dc8b7c769880b10874abc9d0")]
        [InlineData("me@example.com", "2e0d5407ce8609047b8255c50405d7b1")]
        [InlineData("contact@example.org", "edd6d88634e06a9f5181178e907b5405")]
        public void ToGravatar_WithEmail_ShouldCreateGravatarUrl(string email, string expectedHash)
        {
            // Arrange
            const string GravatarAvatarEndpoint = "https://www.gravatar.com/avatar/";
            const int GravatarDefaultSize = 80;

            // Act
            string gravatarUrl = email.ToGravatar();

            // Assert
            Assert.Equal($"{GravatarAvatarEndpoint}{expectedHash}?s={GravatarDefaultSize}", gravatarUrl);
        }

        [Theory]
        [InlineData("admin@example.org", 5, "96614ec98aa0c0d2ee75796dced6df54")]
        [InlineData("test@example.com", 20, "55502f40dc8b7c769880b10874abc9d0")]
        [InlineData("me@example.com", 1, "2e0d5407ce8609047b8255c50405d7b1")]
        [InlineData("contact@example.org", 2048, "edd6d88634e06a9f5181178e907b5405")]
        public void ToGravatar_WithEmailAndValidSize_ShouldCreateGravatarUrl(string email, int size, string expectedHash)
        {
            // Arrange
            const string GravatarAvatarEndpoint = "https://www.gravatar.com/avatar/";

            // Act
            string gravatarUrl = email.ToGravatar(size);

            // Assert
            Assert.Equal($"{GravatarAvatarEndpoint}{expectedHash}?s={size}", gravatarUrl);
        }

        [Theory]
        [InlineData("admin@example.org", 0)]
        [InlineData("test@example.com", 2049)]
        public void ToGravatar_WithEmailAndInvalidSize_ThrowsArgumentException(string email, int size)
        {
            // Arrange

            // Act & Assert
            Assert.Throws<ArgumentException>(() => email.ToGravatar(size));
        }
    }
}
