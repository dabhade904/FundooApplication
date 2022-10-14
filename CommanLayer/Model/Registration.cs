using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.Model
{
    public class Registration
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailId { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
