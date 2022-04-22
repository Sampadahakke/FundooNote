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
        FundoContext fundo;
        IUserBL userBL;
        public UserController(IUserBL userBL, FundoContext fundo)
        {
            this.userBL = userBL;
            this.fundo = fundo;
    }
        [HttpPost("register")]
        public ActionResult RegisterUser(UserPostModel user)
        {
            try
            {
                var getUserData = fundo.Users.FirstOrDefault(u => u.email == user.email);
                if (getUserData != null)
                {
                    return this.Ok(new { success = false, message = $"{user.email} is Already Exists" });
                }
                this.userBL.AddUser(user);
                return this.Ok(new { success = true, message = $"Registration Successfull { user.email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Login/{email}/{password}")]
        public ActionResult LoginUser(string email, string password)
        {
            try
            {
                var result = this.userBL.LoginUser(email,password);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Login Successful {result}" });
                }
                return this.BadRequest(new { success = false, message = $"Login failed {result}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPut("ChangePassword")]
        //public ActionResult ChangePassword(string email, string password)
        //{
        //    try
        //    {
        //        var result = this.userBL.ChangePassword(email, password);

        //        if (result != null)
        //        {
        //            return this.Ok(new { success = true, message = $"{result}" });
        //        }
        //        return this.BadRequest(new { success = false, message = $"{result}" });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}

