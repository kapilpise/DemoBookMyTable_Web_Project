using BookMyTableEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingWebService.Repository
{
    public interface IUserManangement
    {
        UserDetails GetUserDetails(string userName, string Password);
    }
}
