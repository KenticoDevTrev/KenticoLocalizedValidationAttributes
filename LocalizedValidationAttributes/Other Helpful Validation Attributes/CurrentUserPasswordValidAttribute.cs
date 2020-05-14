using Generic.Repositories.Interfaces;
using Generic.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Generic.Attributes
{
    public class CurrentUserPasswordValidAttribute : LocalizedValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value is string)
            {
                string Password = value.ToString();
                IUserService _UserService = DependencyResolver.Current.GetService<IUserService>();
                IUserRepository _UserRepo = DependencyResolver.Current.GetService<IUserRepository>();
                var CurrentUser = _UserRepo.GetUserByUsername(HttpContext.Current.User.Identity.Name);
                return _UserService.ValidateUserPassword(CurrentUser, Password);
            }
            else
            {
                return false;
            }
        }
    }
}