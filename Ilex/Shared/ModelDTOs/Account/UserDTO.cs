using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Shared.ModelDTOs.Account
{
    public class UserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(maximumLength: 20, ErrorMessage = "Ime mora biti od 3 do 20 znamenki", MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(maximumLength: 20, ErrorMessage = "Prezime mora biti od 3 do 20 znamenki", MinimumLength = 3)]
        public string LastName { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Ulica je obavezna")]
        [StringLength(maximumLength: 200, ErrorMessage = "Ulica mora biti od 3 do 200 znamenki", MinimumLength = 3)]
        public string Street { get; set; }
        [Required(ErrorMessage = "Mjesto je obavezno")]
        [StringLength(maximumLength: 50, ErrorMessage = "Mjesto mora biti od 3 do 50 znamenki", MinimumLength = 3)]
        public string City { get; set; }
        [Required(ErrorMessage = "Poštanski broj je je obaveznan")]
        [Range(1000, 999999)]
        public int PostNum { get; set; }

    }
}
