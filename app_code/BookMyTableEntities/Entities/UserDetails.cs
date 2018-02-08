using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMyTableEntities.Entities
{
    public class UserDetails
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string PassWord { get; set; }

        public string LastName { get; set; }

        public bool Success { get; set; }
    }
}