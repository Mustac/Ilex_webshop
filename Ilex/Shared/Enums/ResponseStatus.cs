using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Shared.Enums
{
    public enum ResponseStatus
    {
        Success = 0,
        Error = 1,
        AccountActivated = 2,
        AccountNotActivated = 3,
        AccountRegistrated = 4,
        AccountRegistrationFailed = 5,
        ServerError = 6,
        EmailOrPasswordAreWrong = 7,
        UserDoesNotExist = 8,
        EmailIsUnavailable = 9,
        ServerOffline = 10,
        EmailSendingError = 11,
        EmailSent = 12
    }
}
