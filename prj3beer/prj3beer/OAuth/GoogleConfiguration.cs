using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.OAuth
{
    class GoogleConfiguration
    {
        public static readonly string ClientId = "921981992033-84pr5h9a07dikuklcthe90ng7uvur6ak.apps.googleusercontent.com";

        public static readonly string Scope = "email";

        public static readonly string ClientSecret = "LArhm6K2LIEHpesgzx34kWMBUAVIjQ37DjxuAj2Vvk8=";

        public static readonly string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";

        public static readonly string RedirectUrl = "<your redirect url>";

        public static readonly string AcessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";

        public static bool IsUsingNativeUI = true;
    }
}
