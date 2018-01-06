using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyContacts.Library
{
    public class LocalConstants
    {
        private const string CLIENT_ID = "CLIENT_ID";
        private const string REDIRECT_URL = "REDIRECT_URL";
        private const string GOOGLE_ACCOUNT_URL = "GOOGLE_ACCOUNT_URL";
        private const string GOOGLE_SCOPE_URL = "GOOGLE_SCOPE_URL";
        private const string GOOGLE_CLIENT_SECRET = "GOOGLE_CLIENT_SECRET";
        private const string GOOGLE_TOKEN_URL = "GOOGLE_TOKEN_URL";
        private const string GOOGLE_SCOPE_CONTACTS_URL = "GOOGLE_TOKEN_URL";
        private const string GOOGLE_AUTH_APPLICATION = "GOOGLE_AUTH_APPLICATION";
        private const string REQUEST_CONTENT_TYPE = "REQUEST_CONTENT_TYPE";
        private const string SEPARATOR = "SEPARATOR";
        private const string GOOGLE_EXPORT_FILE_NAME = "GOOGLE_EXPORT_FILE_NAME";

        public static string GetClientID
        {
            get
            {
                return ConfigurationManager.AppSettings[CLIENT_ID];
            }
        }
        public static string GetRedirectUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[REDIRECT_URL];
            }
        }
        public static string GetGoogleAccountUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[GOOGLE_ACCOUNT_URL];
            }
        }
        public static string GetGoogleScopeUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[GOOGLE_SCOPE_URL];
            }
        }
        public static string GetGoogleClientSecret
        {
            get
            {
                return ConfigurationManager.AppSettings[GOOGLE_CLIENT_SECRET];
            }
        }
        public static string GetGoogleTokenUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[GOOGLE_TOKEN_URL];
            }
        }
        public static string GetGoogleScopeContactsUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[GOOGLE_SCOPE_CONTACTS_URL];
            }
        }
        public static string GetGoogleAuthApplication
        {
            get
            {
                return ConfigurationManager.AppSettings[GOOGLE_AUTH_APPLICATION];
            }
        }
        public static string GetRequestContentType
        {
            get
            {
                return ConfigurationManager.AppSettings[REQUEST_CONTENT_TYPE];
            }
        }
        public static char GetSeparator
        {
            get
            {
                return Convert.ToChar(ConfigurationManager.AppSettings[SEPARATOR]);
            }
        }
        public static string GetGoogleContactExportFileName
        {
            get
            {
                return ConfigurationManager.AppSettings[GOOGLE_EXPORT_FILE_NAME];
            }
        }
    }
}