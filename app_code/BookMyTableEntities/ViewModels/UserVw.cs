using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableEntities.ViewModels
{
    public class UserVw
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public int orderid { get; set; }
        public int IsApproved { get; set; }
    }
}
