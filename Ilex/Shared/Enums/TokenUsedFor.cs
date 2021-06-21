using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Shared.Enums
{
    public enum TokenusedFor : ushort
    {
        UserConfirmation = 0,
        UserPasswordReset = 1,
        FacebookLogin = 2
    }
}
