using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Shared.ModelDTOs.Account
{
    public class UserActivationDTO
    {
        public string Email { get; set; }
        public int Code { get; set; }
    }
}
