using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyContacts.Library
{
    public abstract class ContactManagerBase
    {
        public abstract List<EContact> GetContacts(string pCodeForAccessToken);
        public abstract List<EContact> GetContacts();
    }
}