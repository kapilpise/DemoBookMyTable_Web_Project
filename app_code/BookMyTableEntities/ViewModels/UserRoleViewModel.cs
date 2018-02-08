using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableEntities.ViewModels
{
  public  class UserRoleViewModel
    {
        public int RoleId { get; set; }

        public int UserId { get; set; }

        public string RoleDesc { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        public byte[] ts { get; set; }
    }
}
