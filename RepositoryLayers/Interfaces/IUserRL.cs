using DatabaseLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.UserInterface
{
    public interface IUserRL
    {
        public User AddUser(UserPostModel user);
        public User GetUser(int userid);
        public string LoginUser(string email, string password);
        public bool ForgetPassword(string email);
        public bool ChangePassword(string email,PasswordValidation valid);

    }
}
