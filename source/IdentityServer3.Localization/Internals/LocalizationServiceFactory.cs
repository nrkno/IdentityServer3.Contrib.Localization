using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace IdentityServer3.Core.Services.Contrib.Internals
{
    internal static class LocalizationServiceFactory
    {
        
        public static ILocalizationService Create(LocaleOptions options, IDictionary<string, object> env)
        {
            var locale = options.GetLocale(env);

            var isLanguage = locale.Length == 2;
            if (isLanguage)
            {
                locale = TranslateLanguageToLocale(locale.ToLower());
            }

            CultureInfo cultureInfo;

            try
            {
                cultureInfo = new CultureInfo(locale);
            }
            catch (CultureNotFoundException)
            {
                cultureInfo = new CultureInfo(Constants.enUS);
            }
            catch (ArgumentNullException)
            {
                cultureInfo = new CultureInfo(Constants.enUS);
            }

            var inner = new ResourceFileLocalizationService(cultureInfo);

            return new FallbackDecorator(inner, options.FallbackLocalizationService);

        }

        private static string TranslateLanguageToLocale(string language)
        {
            string stuff = "";
            LanguageToLocales.TryGetValue(language, out stuff);
            return stuff;
        }

        private static readonly IDictionary<string, string> LanguageToLocales = new Dictionary<string, string>
            {
                {"ar", "ar-SA"},
                {"da", "da-DK"},
                {"de", "de-DE"},
                {"es", "es-ES"},
                {"fi", "fi-FI"},
                {"fr", "fr-FR"},
                {"it", "it-IT"},
                {"nb", "nb-NO"},
                {"nl", "nl-NL"},
                {"pl", "pl-PL"},
                {"pt", "pt-BR"},
                {"ro", "ro-RO"},
                {"ru", "ru-RU"},
                {"sk", "sk-SK"},
                {"sv", "sv-SE"},
                {"tr", "tr-TR"}
            };
    }
}
