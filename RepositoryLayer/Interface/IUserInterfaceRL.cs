﻿using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserInterfaceRL
    {
       public UserEntity UserRegitrations(UserRegistration userRegistration);
    }
}
