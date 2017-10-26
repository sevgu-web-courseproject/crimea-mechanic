﻿using System.Threading.Tasks;
using BusinessLogic.Objects.User;
using Microsoft.Owin.Security.OAuth;

namespace BusinessLogic.Managers.Abstraction
{
    public interface IUserManager
    {
        Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context);

        Task Register(RegisterUserDto dto);
    }
}
