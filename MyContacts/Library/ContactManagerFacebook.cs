using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyContacts.Library
{
    public class ContactManagerFacebook : ContactManagerBase
    {
        public override List<EContact> GetContacts()
        {
            throw new NotImplementedException();
        }

        public override List<EContact> GetContacts(string pCodeForAccessToken)
        {
            return null;
        }
    }
}