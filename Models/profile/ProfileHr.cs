using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace managementHR.Models.profile
{
    public class ProfileHr
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int Age { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

    }
}
