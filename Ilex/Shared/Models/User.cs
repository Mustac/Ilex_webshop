using System;

namespace Ilex.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string HashedPassword { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int PostNum { get; set; }
        public DateTime DateCreated { get; set; }
        public string Role { get; set; }
    }
}
