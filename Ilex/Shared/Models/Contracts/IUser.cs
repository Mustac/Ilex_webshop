using System;

namespace Ilex.Shared.Models.Contracts
{
    public interface IUser
    {
        string City { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string HashedPassword { get; set; }
        int Id { get; set; }
        string LastName { get; set; }
        string Role { get; set; }
        string PhoneNumber { get; set; }
        int PostNum { get; set; }
        string Street { get; set; }
        DateTime DateCreated { get; set; }
    }
}