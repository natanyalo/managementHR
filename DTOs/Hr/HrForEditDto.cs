using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace managementHR.DTOs.Hr
{
    public class HrForEditDto
    {
        public int Age { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

    }
}
