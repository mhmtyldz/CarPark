using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.User.Models
{
    public class ContactRequestModel
    {
        [Required(ErrorMessage = "Required")]
        [DisplayName("NameSurname")]
        public string NameSurname { get; set; }
        public int Age { get; set; }
        public string EmailAddress { get; set; }
    }
    
}
