using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

namespace prj3beer.OAuth
{
    public static class OAuthAuthenticatorHelper
    {
        private static OAuth2Authenticator oAuth2Authenticator;
        public static OAuth2Authenticator AuthenticationState { get; private set; }

        public static OAuth2Authenticator CreateOAuth2(OAuth2ProviderType socialLoginProvider)
        {
            switch(socialLoginProvider)
            {
                case OAuth2ProviderType.GOOGLE:
                    oAuth2Authenticator = new OAuth2Authenticator(
                        clientId: GoogleConfiguration.ClientId,
                        clientSecret: GoogleConfiguration.ClientSecret,
                        scope: GoogleConfiguration.Scope,
                        authorizeUrl: new Uri(GoogleConfiguration.AuthorizeUrl),
                        redirectUrl: new Uri(GoogleConfiguration.RedirectUrl),
                        getUsernameAsync: null,
                        isUsingNativeUI: GoogleConfiguration.IsUsingNativeUI,
                        accessTokenUrl: new Uri(GoogleConfiguration.AcessTokenUrl))
                    {
                        AllowCancel = true,
                        ShowErrors = false,
                        ClearCookiesBeforeLogin = true
                    };
                    break;
            }
            AuthenticationState = oAuth2Authenticator;
            return oAuth2Authenticator;
        }
    }
}
