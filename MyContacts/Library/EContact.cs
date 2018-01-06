using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyContacts.Library
{
    public class EContact
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public Uri Photo { get; set; }
    }
}