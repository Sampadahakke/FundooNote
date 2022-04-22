using DatabaseLayer;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entity;
using RepositoryLayer.FundoNoteContext;
using RepositoryLayer.Services;
using RepositoryLayer.UserInterface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.UserClass
{
    public class UserRL : IUserRL
    {
        FundoContext fundo;
        public IConfiguration Configuration { get; }

        public UserRL(FundoContext fundo, IConfiguration configuration)
        {
            this.fundo = fundo;
            this.Configuration = configuration;
        }

        public User AddUser(UserPostModel user)
        {
            try
            {
                User user1 = new User();
                user1.userID = new User().userID;
                user1.firstName = user.firstName;
                user1.lastName = user.lastName;
                user1.email = user.email;
                user1.password = user.password;
                user1.registerdDate = DateTime.Now;
                user1.address = user.address;
                fundo.Users.Add(user1);
                fundo.SaveChanges();    
                return user1;
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LoginUser(string email, string password)
        {
            try
            {
                var result = fundo.Users.Where(u => u.email == email && u.password == password).FirstOrDefault();
                if (result == null)
                {
                    return null;
                }
                return GetJWTToken(email, result.userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Generate JwT token
        public static string GetJWTToken(string email, int UserID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userId",UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //public bool ForgetPassword(string email)
        //{
        //    try
        //    {
        //        var result = fundo.Users.FirstOrDefault(u => u.email == email);
        //        if (result == null)
        //        {
        //            return false;
        //        }
        //        // Addd message Queue
        //        MessageQueue queue;
        //        if (MessageQueue.Exists(@".\Private$\FundooQueue"))
        //        {
        //            queue = new MessageQueue(@".\Private$\FundooQueue");
        //        }
        //        else
        //        {
        //            queue = MessageQueue.Create(@".\Private$\FundooQueue");
        //        }
        //        Message myMessage = new Message();
        //        myMessage.Formatter = new BinaryMessageFormatter();
        //        myMessage.Body = GetJWTToken(email, result.userID);
        //        queue.Send(myMessage);
        //        Message msg = queue.Receive();
        //        msg.Formatter = new BinaryMessageFormatter();
        //        EmailService.SendMail(email, myMessage.Body.ToString());
        //        queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
        //        queue.BeginReceive();
        //        queue.Close();
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //public string ChangePassword(string email,string password)
        //{
        //    try
        //    {
        //        var userDetails = fundo.Users.Where(x => x.email == email).FirstOrDefault();
        //        userDetails.password=password;
        //        int result = fundo.SaveChanges();
        //        if (result > 0)
        //        {
        //            return "Password changed successfully";
        //        }
        //        else
        //        {
        //            return "Failed";
        //        }
               
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
   





























