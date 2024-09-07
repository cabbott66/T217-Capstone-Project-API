using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Repositories;

namespace T217_Capstone_Project_API.Authentication
{
    public class UserAuthenticationFilterAdmin : IAuthorizationFilter
    {
        private readonly UserAuthRepository _repo = new UserAuthRepository();
        private readonly IConfiguration _configuration;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                                 .Any(x => x.GetType() == typeof(AllowAnonymousAttribute));

            if (hasAllowAnonymous) return;

            if (!context.HttpContext.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key is missing");
                return;
            }

            User user = _repo.GetUserByApiKey(extractedApiKey);

            if (user.UserID != 1)
            {
                context.Result = new UnauthorizedObjectResult("User not authorized");
                return;
            }
        }

        private bool AuthenticateUser(User user, string password)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password);
        }
    }
}
