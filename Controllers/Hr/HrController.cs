using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using managementHR.Data;
using managementHR.DTOs.Hr;
using managementHR.Models.profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace managementHR.Controllers.Hr
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HrController : ControllerBase
    {
        private readonly DataContext _dataCotext;
        public HrController(DataContext dataContext)
        {
            _dataCotext = dataContext;
        }

        [HttpGet]
        public IActionResult GetProfile()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = _dataCotext.ProfileHr.Where(t => t.UserId.ToString() == userId).FirstOrDefault();
            if (profile != null)
                return Ok(profile);
            else
                return BadRequest();
        }
        [HttpPost]
        public IActionResult EditProfile(HrForEditDto hrForEditDto)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var DBprofile = this._dataCotext.ProfileHr.Where(p => p.UserId.ToString() == userId).FirstOrDefault();
            if (DBprofile == null)
                addNewProfile(hrForEditDto);
            else
                EditProfile( DBprofile,hrForEditDto);
            _dataCotext.SaveChanges();
            return Ok();
        }
        private void addNewProfile(HrForEditDto hrForEditDto)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = new ProfileHr
            {
                Age = hrForEditDto.Age,
                UserName = hrForEditDto.UserName,
                UserId = Int32.Parse(userId),
                Gender = hrForEditDto.Gender,
                Date = hrForEditDto.Date,
                Email = hrForEditDto.Email,
                ImagePath = hrForEditDto.ImagePath,
                Phone = hrForEditDto.Phone,
            };
            _dataCotext.ProfileHr.Add(profile);
         
        }
        private void EditProfile(ProfileHr profileHr, HrForEditDto hrForEditDto)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            profileHr.Age = hrForEditDto.Age;
            profileHr.UserName = hrForEditDto.UserName;
            profileHr.UserId = Int32.Parse(userId);
            profileHr.Date = hrForEditDto.Date;
            profileHr.Email = hrForEditDto.Email;
            profileHr.ImagePath = hrForEditDto.ImagePath;
            profileHr.Phone = hrForEditDto.Phone;
            
            
        }
    }
}