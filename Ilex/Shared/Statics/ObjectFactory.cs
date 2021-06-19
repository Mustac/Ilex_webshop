using Ilex.Shared.Models;
using Ilex.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Shared.Statics
{
    public static class ObjectFactory
    {
        public static IUser NewUser()
        {
            return new User();
        }
    }
}
