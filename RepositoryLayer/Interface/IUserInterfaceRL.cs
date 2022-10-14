using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserInterfaceRL
    {
        public UserEntity UserRegistration(Registration registration);
        public string Login(UserLogin userLogin);
        public string ForgetPassword(string emailId);
        public bool ResetPassword(string emailId, string newPassword, string confirmPassword);
        public string EncryptPassword(string password);

    }
}
