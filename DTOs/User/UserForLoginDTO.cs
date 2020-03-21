using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace managementHR.DTOs.User
{
    public class UserForLoginDTO
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "you have specify password")]
        public string UserName { get; set; }
    }
}
