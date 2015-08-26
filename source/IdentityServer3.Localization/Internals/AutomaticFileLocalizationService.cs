using System.Globalization;
using Thinktecture.IdentityServer.Core.Services.Contrib.Resources;

namespace Thinktecture.IdentityServer.Core.Services.Contrib.Internals
{
    internal class AutomaticFileLocalizationService : ILocalizationService
    {
        public string GetString(string category, string id)
        {
            var resourceFileLocalizationService = new ResourceFileLocalizationService(CultureInfo.CurrentCulture);
            return resourceFileLocalizationService.GetString(category, id);
        }
    }
}