using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Google.GData.Client;
using Google.Contacts;
using Google.GData.Contacts;
using Google.GData.Extensions;

namespace MyContacts.Library
{
    public class ContactManagerGoogle : ContactManagerBase
    {
        public override List<EContact> GetContacts(string pCodeForAccessToken)
        {
            List<EContact> vListOfContacts = new List<EContact>();
            EContact vEContact = null;
            string vParameters = string.Empty;
            byte[] vByteArray = null;
            Stream vPostStream = null;
            string vResponseFromServer = string.Empty;

            try
            {
                // Get access token
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(LocalConstants.GetGoogleTokenUrl);      // https://accounts.google.com/o/oauth2/token
                webRequest.Method = "POST";

                vParameters = string.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code",
                    pCodeForAccessToken, LocalConstants.GetClientID, LocalConstants.GetGoogleClientSecret, LocalConstants.GetRedirectUrl);

                vByteArray = Encoding.UTF8.GetBytes(vParameters);
                webRequest.ContentType = LocalConstants.GetRequestContentType; 
                webRequest.ContentLength = vByteArray.Length;

                vPostStream = webRequest.GetRequestStream();
                vPostStream.Write(vByteArray, 0, vByteArray.Length);
                vPostStream.Close();                

                WebResponse response = webRequest.GetResponse();
                vPostStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(vPostStream);
                vResponseFromServer = reader.ReadToEnd();

                dynamic vToken = JObject.Parse(vResponseFromServer);

                OAuth2Parameters oAuthparameters = new OAuth2Parameters()
                {
                    Scope = LocalConstants.GetGoogleScopeContactsUrl,
                    AccessToken = vToken.access_token,
                    RefreshToken = vToken.refresh_token
                };

                RequestSettings vSettings = new RequestSettings(string.Format("<var>{0}</var>", LocalConstants.GetGoogleAuthApplication), oAuthparameters);
                ContactsRequest vContactsRequest = new ContactsRequest(vSettings);

                ContactsQuery vContactsQuery = new ContactsQuery(ContactsQuery.CreateContactsUri("default"));
                vContactsQuery.NumberToRetrieve = 5000;

                Feed<Contact> feed = vContactsRequest.Get<Contact>(vContactsQuery);

                if (feed !=null && feed.Entries != null)
                {
                    foreach (Contact contact in feed.Entries)
                    {
                        foreach (EMail email in contact.Emails)
                        {
                            vEContact = new EContact();
                            vEContact.FullName = string.IsNullOrEmpty(contact.Title) ? email.Address : contact.Title;
                            vEContact.Email = email.Address;
                            vListOfContacts.Add(vEContact);
                        }
                    }
                }
            }
            catch(Exception vException)
            {
                throw vException;
            }

            return vListOfContacts;
        }
        
        public override List<EContact> GetContacts()
        {
            return null;
        }
    }
}