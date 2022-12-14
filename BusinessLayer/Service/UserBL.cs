using BusinessLayer.Interface;
using CommanLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Security.Cryptography;

namespace BusinessLayer.Service
{
    public class UserBL : IUserInterfaceBL
    {
        private readonly IUserInterfaceRL userInterfaceRL;
        public UserBL(IUserInterfaceRL userInterfaceRL)
        {
            this.userInterfaceRL = userInterfaceRL;
        }
        public UserEntity UserRegistration(Registration registration)
        {
            try
            {
                return userInterfaceRL.UserRegistration(registration);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                return userInterfaceRL.Login(userLogin);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ForgetPassword(string emailId)
        {
            try
            {
                return userInterfaceRL.ForgetPassword(emailId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string emailId, string newPassword, string confirmPassword)
        {
            try
            {
                return userInterfaceRL.ResetPassword(emailId, newPassword, confirmPassword);
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
