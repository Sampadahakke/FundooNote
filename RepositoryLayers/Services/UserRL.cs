using DatabaseLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using RepositoryLayer.FundoNoteContext;
using RepositoryLayer.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.UserClass
{
    public class UserRL : IUserRL
    {
        FundoContext fundo;
        private readonly IConfiguration configuration;

        public UserRL(FundoContext fundo, IConfiguration configuration)
        {
            this.fundo = fundo;
            this.configuration = configuration;
        }
        public User AddUser(UserPostModel user)
        {
            try
            {
                User user1 = new User();
                user1.userID = new Entity.User().userID;
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
    }
}
