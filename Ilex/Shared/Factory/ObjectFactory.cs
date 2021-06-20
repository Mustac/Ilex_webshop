using Ilex.Shared.ModelDTOs.Account;
using Ilex.Shared.ModelDTOs.Contracts;
using Ilex.Shared.Models;
using Ilex.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Shared.Factory
{
    public static class ObjectFactory
    {
        public static User NewUser()
        {
            return new User();
        }

        public static IUserRegistrationDTO NewUserRegistrationDTO()
        {
            return new UserRegistrationDTO();
        }
    }
}
