using System.Collections.Generic;
using System.Linq;
using Thinktecture.IdentityServer.Core.Resources;
using Thinktecture.IdentityServer.Core.Services.Contrib;
using Xunit;
using Xunit.Sdk;

namespace Unittests
{
    public class OneLocalizationToRuleThemAllServiceTests
    {
        [Theory]
        [InlineData("nb-NO")]
        public void ShouldGetLocalizedMessages(string culture)
        {
            AssertTranslationExists(culture, _possibleMessageIds, "Messages");
            AssertTranslationExists(culture, _possibleEventIds, "Events");
            AssertTranslationExists(culture, _possibleScopeIds, "Scopes");
        }
        
        [Theory(Skip = "Bug in idsrv default localization service. Enable when fixed")]
        [InlineData("")] // <-- This means using IdentityServers DefaultLocalizationService
        public void ShouldGetIdServersLocalizedMessages(string culture)
        {
            AssertTranslationExists(culture, _possibleMessageIds, "Messages");
            AssertTranslationExists(culture, _possibleEventIds, "Events");
            AssertTranslationExists(culture, _possibleScopeIds, "Scopes");
        }

        private static void AssertTranslationExists(string culture, IEnumerable<string> ids, string category)
        {
            var options = new LocaleOptions
            {
                Locale = culture
            };
            var service = new GlobalizedLocalizationService(options);
            var notFoundTranslations = new List<string>();
            foreach (var scopeId in ids)
            {
                var localizedString = service.GetString(category, scopeId);
                if (string.IsNullOrEmpty(localizedString))
                {
                    var errormsg = string.Format("Could not find translation of Id '{0}' in {1}", scopeId,
                        string.IsNullOrEmpty(culture) ? "IdentityServers internals" : culture);
                    notFoundTranslations.Add(errormsg);

                }
                else
                {
                    Assert.NotEqual("", localizedString.Trim());    
                }
                
            }
            if (notFoundTranslations.Any())
            {
                var concated = notFoundTranslations.Aggregate((x, y) => x + ", " + y);
                throw new AssertActualExpectedException("Some translation", "NOTHING!", concated );
            }
        }

        private readonly IEnumerable<string> _possibleMessageIds = new List<string>
        {
            MessageIds.ClientIdRequired,
            MessageIds.ExternalProviderError,
            MessageIds.InvalidUsernameOrPassword,
            MessageIds.Invalid_scope,
            MessageIds.MissingClientId,
            MessageIds.MissingToken,
            MessageIds.MustSelectAtLeastOnePermission,
            MessageIds.NoExternalProvider,
            MessageIds.NoMatchingExternalAccount,
            MessageIds.NoSignInCookie,
            MessageIds.NoSubjectFromExternalProvider,
            MessageIds.PasswordRequired,
            MessageIds.SslRequired,
            MessageIds.Unauthorized_client,
            MessageIds.UnexpectedError,
            MessageIds.UnsupportedMediaType,
            MessageIds.Unsupported_response_type,
            MessageIds.UsernameRequired
        };

        private readonly IEnumerable<string> _possibleEventIds = new List<string>
        {
            EventIds.ExternalLoginFailure,
            EventIds.ExternalLoginSuccess,
            EventIds.LocalLoginFailure,
            EventIds.LocalLoginSuccess,
            EventIds.LogoutEvent,
            EventIds.PartialLogin,
            EventIds.PartialLoginComplete,
            EventIds.PreLoginFailure,
            EventIds.PreLoginSuccess
        };


        private readonly IEnumerable<string> _possibleScopeIds = new List<string>
        {
            ScopeIds.Address_DisplayName,
            ScopeIds.All_claims_DisplayName,
            ScopeIds.Email_DisplayName,
            ScopeIds.Offline_access_DisplayName,
            ScopeIds.Openid_DisplayName,
            ScopeIds.Phone_DisplayName,
            ScopeIds.Profile_Description,
            ScopeIds.Profile_DisplayName,
            ScopeIds.Roles_DisplayName
        };

    }
}
