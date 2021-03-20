using Xunit;

namespace Gravatar.UnitTests
{
    public class GravatarExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void ToGravatar_WhenCallingStaticClassMethod_WithEmptyOrNullOrWhiteSpaceEmail_ShouldReturnEmptyUrl(string email)
        {
            // Arrange

            // Act
            string gravatarUrl = email.ToGravatar();

            // Assert
            Assert.Empty(gravatarUrl);
        }

        [Theory]
        [InlineData("admin@example.org", "96614ec98aa0c0d2ee75796dced6df54")]
        [InlineData("test@example.com", "55502f40dc8b7c769880b10874abc9d0")]
        public void ToGravatar_WhenCallingAsStringExtensionMethod_WithEmail_ShouldCreateGravatarUrl(string email, string expectedHash)
        {
            // Arrange
            const string GravatarAvatarEndpoint = "https://www.gravatar.com/avatar/";

            // Act
            string gravatarUrl = email.ToGravatar();

            // Assert
            Assert.Equal($"{GravatarAvatarEndpoint}{expectedHash}", gravatarUrl);
        }
    }
}
