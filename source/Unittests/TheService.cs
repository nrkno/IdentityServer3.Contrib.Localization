using FakeItEasy;
using IdentityServer3.Core.Resources;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Contrib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Sdk;
using Constants = IdentityServer3.Core.Constants;
using IdSrvConstants = IdentityServer3.Core.Constants.LocalizationCategories;

namespace Unittests
{
    public class TheService
    {
        public TheService()
        {
            Debug.Listeners.Add(new DefaultTraceListener());
        }

        [Theory]
        [InlineData("ar")]
        [InlineData("ar-SA")]
        [InlineData("cs-CZ")]
        [InlineData("de")]
        [InlineData("de-DE")]
        [InlineData("da")]
        [InlineData("da-DK")]
        [InlineData("es")]
        [InlineData("es-AR")]
        [InlineData("es-ES")]
        [InlineData("fi")]
        [InlineData("fi-FI")]
        [InlineData("fr")]
        [InlineData("fr-FR")]
        [InlineData("it")]
        [InlineData("it-IT")]
        [InlineData("nb")]
        [InlineData("nb-NO")]
        [InlineData("nl-NL")]
        [InlineData("pl")]
        [InlineData("pl-PL")]
        [InlineData("pt")]
        [InlineData("pt-BR")]
        [InlineData("ro")]
        [InlineData("ro-RO")]
        [InlineData("ru")]
        [InlineData("ru-RU")]
        [InlineData("sk")]
        [InlineData("sk-SK")]
        [InlineData("sv")]
        [InlineData("sv-SE")]
        [InlineData("tr")]
        [InlineData("tr-TR")]
        [InlineData("zh-CN")]
        [InlineData("zh-TW")]
        public void HasTranslationsForAllPublicIds(string availableCulture)
        {
            AssertTranslationExists(availableCulture, TestHelper.GetAllMessageIds(), IdSrvConstants.Messages);
            AssertTranslationExists(availableCulture, TestHelper.GetAllEventIds(), IdSrvConstants.Events);
            AssertTranslationExists(availableCulture, TestHelper.GetAllScopeIds(), IdSrvConstants.Scopes);
        }

        [Fact]
        public void UsesFallbackWhenIsSet()
        {
            const string someid = "SomeId";
            var mock = new Fake<ILocalizationService>();
            mock.CallsTo(loc => loc.GetString(IdSrvConstants.Messages, someid)).Returns("fallbackValue");

            var localeOptions = new LocaleOptions
            {
                LocaleProvider = env => "nb-NO",
                FallbackLocalizationService = mock.FakedObject
            };
            var envServiceMock = new Fake<OwinEnvironmentService>();
            var service = new GlobalizedLocalizationService(envServiceMock.FakedObject, localeOptions);

            var result = service.GetString(IdSrvConstants.Messages, someid);
            Assert.Equal("fallbackValue", result);
        }

        [Theory]
		[InlineData(null)]
        [InlineData("")]
        [InlineData("notexisting")]
        public void UnknownLocalesUseDefaultLocale(string locale)
        {
            var options = new LocaleOptions
            {
                LocaleProvider = env => locale
            };

            var envServiceMock = new Fake<OwinEnvironmentService>().FakedObject;

            var service = new GlobalizedLocalizationService(envServiceMock, options);

            var resource = service.GetString(IdSrvConstants.Messages, MessageIds.MissingClientId);

            Assert.Equal("client_id is missing", resource);
        }

        [Fact]
        public void FetchesResourceWhenUsingLocaleProviderFunc()
        {
            var options = new LocaleOptions
            {
                LocaleProvider = env => "nb-NO"
            };
            var envServiceMock = new Fake<OwinEnvironmentService>().FakedObject;

            var service = new GlobalizedLocalizationService(envServiceMock, options);
            var norwegianString = service.GetString(IdSrvConstants.Messages, MessageIds.MissingClientId);

            Assert.Equal("ClientId mangler", norwegianString);
        }

        [Fact]
        public void FetchesResrouceBasedOnOwinEnvironment()
        {
            IDictionary<string, object> environment = new Dictionary<string, object>();
            environment["UserSettings.Language"] = "nb-NO";
            var owinEnvironmentService = new OwinEnvironmentService(environment);
            var options = new LocaleOptions
            {
                LocaleProvider = env => env["UserSettings.Language"].ToString()
            };

            var service = new GlobalizedLocalizationService(owinEnvironmentService, options);
            var norwegianString = service.GetString(IdSrvConstants.Messages, MessageIds.MissingClientId);

            Assert.Equal("ClientId mangler", norwegianString);
        }

        [Theory]
        [InlineData("ar")]
        [InlineData("ar-SA")]
        [InlineData("cs-CZ")]
        [InlineData("de")]
        [InlineData("de-DE")]
        [InlineData("da")]
        [InlineData("da-DK")]
        [InlineData("es")]
        [InlineData("es-AR")]
        [InlineData("es-ES")]
        [InlineData("fi")]
        [InlineData("fi-FI")]
        [InlineData("fr")]
        [InlineData("fr-FR")]
        [InlineData("it")]
        [InlineData("it-IT")]
        [InlineData("nb")]
        [InlineData("nb-NO")]
        [InlineData("nl-NL")]
        [InlineData("pl")]
        [InlineData("pl-PL")]
        [InlineData("pt")]
        [InlineData("pt-BR")]
        [InlineData("ro")]
        [InlineData("ro-RO")]
        [InlineData("ru")]
        [InlineData("ru-RU")]
        [InlineData("sk")]
        [InlineData("sk-SK")]
        [InlineData("sv")]
        [InlineData("sv-SE")]
        [InlineData("tr")]
        [InlineData("tr-TR")]
        [InlineData("zh-CN")]
        [InlineData("zh-TW")]

        public void ContainsLocales(string locale)
        {
            var env = new Dictionary<string, object>();
            var options = new LocaleOptions
            {
                LocaleProvider = e => locale
            };
            var global = new GlobalizedLocalizationService(new OwinEnvironmentService(env), options);
            var resource = global.GetString(IdSrvConstants.Messages, MessageIds.MissingClientId);

            Assert.NotEqual("client_id is missing", resource);
        }

        [Theory]
        [InlineData("en-GB")]
        [InlineData("en-US")]
        public void ContainsEnglishLocalesAsWell(string locale)
        {
            var env = new Dictionary<string, object>();
            var options = new LocaleOptions
            {
                LocaleProvider = e => locale
            };
            var global = new GlobalizedLocalizationService(new OwinEnvironmentService(env), options);
            var resource = global.GetString(IdSrvConstants.Messages, MessageIds.MissingClientId);

            Assert.Equal("client_id is missing", resource);
        }

        private static void AssertTranslationExists(string culture, IEnumerable<string> ids, string category)
        {
            var options = new LocaleOptions
            {
                LocaleProvider = env => culture
            };
            var envServiceMock = new Fake<OwinEnvironmentService>().FakedObject;

            var service = new GlobalizedLocalizationService(envServiceMock, options);
            var notFoundTranslations = new List<string>();

            foreach (var id in ids)
            {
                var localizedString = service.GetString(category, id);
                if (string.IsNullOrEmpty(localizedString))
                {
                    var errormsg = string.Format("Could not find translation of Id '{0}' in {1}", id,
                        string.IsNullOrEmpty(culture) ? "IdentityServers internals" : culture);
                    notFoundTranslations.Add(errormsg);

                }
                else
                {
                    string message = string.Format("{0} - {1}:{2} - {3}", culture, category, id, localizedString);
                    Trace.WriteLine(message);
                    Debug.WriteLine(message);
                    Console.WriteLine(message);
                    Assert.NotEqual("", localizedString.Trim());
                }

            }
            if (notFoundTranslations.Any())
            {
                var concated = notFoundTranslations.Aggregate((x, y) => x + ", " + y);
                throw new AssertActualExpectedException("Some translation", "NOTHING!", concated);
            }
        }
    }

    internal static class TestHelper
    {
        public static IEnumerable<string> GetAllMessageIds()
        {
            return typeof(MessageIds).GetFields().Select(m => m.GetRawConstantValue().ToString());
        }

        public static IEnumerable<string> GetAllEventIds()
        {
            return typeof(EventIds).GetFields().Select(m => m.GetRawConstantValue().ToString());
        }

        public static IEnumerable<string> GetAllScopeIds()
        {
            return typeof(ScopeIds).GetFields().Select(m => m.GetRawConstantValue().ToString());
        }
    }
}
