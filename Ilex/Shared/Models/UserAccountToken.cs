using Ilex.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Shared.Models
{
    public class UserAccountToken
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }
        public TokenusedFor TokenUsedFor { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
