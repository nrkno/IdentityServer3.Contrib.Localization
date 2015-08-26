using System.Linq;
using Thinktecture.IdentityServer.Core.Services.Contrib;
using Xunit;

namespace Unittests
{
    public class AvailableTranslations
    {
        [Theory]
        [InlineData("Default")]
        [InlineData("Auto")]
        [InlineData("pirate")]
        [InlineData("de-DE")]
        [InlineData("es-AR")]
        [InlineData("fr-FR")]
        [InlineData("nb-NO")]
        [InlineData("sv-SE")]
        [InlineData("tr-TR")]
        [InlineData("ro-RO")]
        [InlineData("nl-NL")]
        [InlineData("zh-CN")]
        [InlineData("da-DK")]
        [InlineData("ru-RU")]
        [InlineData("pt-BR")]
        [InlineData("cs-CZ")]
        public void ContainsLocales(string locale)
        {
            Assert.Contains(GlobalizedLocalizationService.GetAvailableLocales(), s => s.Equals(locale));
        }

        [Fact]
        public void HasCorrectCount()
        {
            Assert.Equal(16, GlobalizedLocalizationService.GetAvailableLocales().Count());
        }
    }
}
