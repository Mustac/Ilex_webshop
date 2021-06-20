using Ilex.Shared.ModelDTOs.Contracts;
using Ilex.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Shared.ModelDTOs.Account
{
    public class UserRegistrationDTO : IUserRegistrationDTO
    {
        [Required(ErrorMessage ="Email adresa je obavezna")]
        [EmailAddress(ErrorMessage = "Unesite ispravnu Email adresu")]
        [StringLength(30, ErrorMessage ="Email adresa je predugacka")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(maximumLength:20, ErrorMessage = "Ime mora biti od 3 do 20 znamenki", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(maximumLength: 20, ErrorMessage = "Prezime mora biti od 3 do 20 znamenki", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ulica je obavezna")]
        [StringLength(maximumLength: 200, ErrorMessage = "Ulica mora biti od 3 do 200 znamenki", MinimumLength = 3)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Mjesto je obavezno")]
        [StringLength(maximumLength: 50, ErrorMessage = "Mjesto mora biti od 3 do 50 znamenki", MinimumLength = 3)]
        public string City { get; set; }
        [Required(ErrorMessage = "Poštanski broj je je obaveznan")]
        [Range(1000, 999999)]
        public int PostNum { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezana")]
        [StringLength(maximumLength: 20, ErrorMessage = "Lozinka mora biti od 4 do 20 znamenki", MinimumLength = 3)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Lozinke se ne podudaraju")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }


    }
}
