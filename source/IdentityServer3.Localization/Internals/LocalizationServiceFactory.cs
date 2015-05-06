using System.Collections.Generic;
using System.Globalization;
using Thinktecture.IdentityServer.Core.Services.Default;

namespace Thinktecture.IdentityServer.Core.Services.Contrib.Internals
{
    internal static class LocalizationServiceFactory
    {
        public static readonly IDictionary<string, ILocalizationService> AvailableLocalizationServices;

        static LocalizationServiceFactory()
        {
            AvailableLocalizationServices = new Dictionary<string, ILocalizationService>
            {
                {Constants.Default, new DefaultLocalizationService()},
                {Constants.Pirate, new PirateLocalizationService()}
            };
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.deDE));
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.esAR));
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.frFR));
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.nbNO));
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.svSE));
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.trTR));
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.roRO));
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.nlNL));
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.zhCN));
            AvailableLocalizationServices.Add(CreateResourceBased(Constants.ruRU));
        }

        public static ILocalizationService Create(LocaleOptions options)
        {
            var inner = AvailableLocalizationServices[options.Locale];
            if (options.FallbackLocalizationService != null)
            {
                return new FallbackDecorator(inner, options.FallbackLocalizationService);
            }
            return new FallbackDecorator(inner, AvailableLocalizationServices[Constants.Default]);
        }

        private static KeyValuePair<string, ILocalizationService> CreateResourceBased(string locale)
        {
            return new KeyValuePair<string, ILocalizationService>(locale, new ResourceFileLocalizationService(new CultureInfo(locale)));
        } 
    }
}
