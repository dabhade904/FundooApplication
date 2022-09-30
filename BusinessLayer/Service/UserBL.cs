using BusinessLayer.Interface;
using CommanLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserInterfaceRL userInterfaceRL;
        public UserBL(IUserInterfaceRL userInterfaceRL)
        {
            this.userInterfaceRL = userInterfaceRL;
        }
        public UserEntity UserRegitrations(UserRegistration userRegistration)
        {
            try
            {
                return userInterfaceRL.UserRegitrations(userRegistration);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
