using BusinessLayer.Interfaces;
using DatabaseLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;   
        }
        public User AddUser(UserPostModel user)
        {
            try
            {
              return this.userRL.AddUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
