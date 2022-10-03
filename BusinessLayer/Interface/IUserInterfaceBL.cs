using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserInterfaceBL
    {
        public UserEntity UserRegistration(Registration registration);
        public string Login(UserLogin userLogin);
    }
}
