namespace Ilex.Shared.ModelDTOs.Contracts
{
    public interface IUserRegistrationDTO
    {
      
        string ConfirmPassword { get; set; }
        string Email { get; set; }
        string Address { get; set; }
        string City { get; set; }
        int PostNum { get; set; }
        string PhoneNumber { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
    }
}