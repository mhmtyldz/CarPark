using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.User.Models
{
    public class ContactAddRequestModel
    {
        [Required(ErrorMessage = "Required")]
        [DisplayName("NameSurname")]
        public string NameSurname { get; set; }
        public string EmailAddress
        {
            get; set;
        }
    }
}
