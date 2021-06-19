namespace Ilex.Shared.Models.Contracts
{
    public interface IUser
    {
        string Address { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string HashedPassword { get; set; }
        int Id { get; set; }
        string LastName { get; set; }
        string Role { get; set; }
    }
}