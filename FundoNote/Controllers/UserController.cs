using BusinessLayer.Interfaces;
using DatabaseLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundoNoteContext;
using System;
using System.Linq;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser(UserPostModel user)
        {
            try
            {
                var result = this.userBL.AddUser(user);
                if (result != null)
                {
                  return this.Ok(new { success = true, message = $"Registration Successful {user.email}" });
                }
                return this.BadRequest(new { success = false, message = $"Registration failed {user.email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

