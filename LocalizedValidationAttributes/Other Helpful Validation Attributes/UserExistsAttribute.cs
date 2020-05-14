using Generic.Repositories.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Generic.Attributes
{
    /// <summary>
    /// Is valid if the user exists, can be either on the User's Guid, ID, Username or Email
    /// </summary>
    public class UserExistsAttribute : LocalizedValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value is Guid)
            {
                Guid UserGuid = (Guid)value;
                IUserRepository _UserRepo = DependencyResolver.Current.GetService<IUserRepository>();
                return _UserRepo.GetUserID(UserGuid).HasValue;
            } else if (value is int)
            {
                int UserID = (int)value;
                IUserRepository _UserRepo = DependencyResolver.Current.GetService<IUserRepository>();
                return _UserRepo.GetUserByID(UserID) != null;
            } else if (value is string)
            {
                string UsernameOrEmail = (string)value;
                IUserRepository _UserRepo = DependencyResolver.Current.GetService<IUserRepository>();
                return _UserRepo.GetUserByUsername(UsernameOrEmail) != null || _UserRepo.GetUserByEmail(UsernameOrEmail) != null;
            }
            return false;
        }
    }
}