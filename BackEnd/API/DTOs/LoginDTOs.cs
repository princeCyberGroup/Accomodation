using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{

    public class LoginDTOs
    {
        //{
        //  "emailId": "prince.11901773@lpu.in",
        //  "password": "prince.5678"
        // }

        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailId { get; set; }

        [MinLength(6)]
        [PasswordPropertyText]
        [Required(ErrorMessage = "Please enter a valid password")]
        public string Password { get; set; }
    }
}