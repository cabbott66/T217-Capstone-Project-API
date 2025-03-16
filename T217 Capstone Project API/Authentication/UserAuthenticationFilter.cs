using Microsoft.AspNetCore.Mvc.Filters;
using T217_Capstone_Project_API.Repositories.Interfaces;
using T217_Capstone_Project_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace T217_Capstone_Project_API.Authentication
{
    public class UserAuthenticationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repo;

        public UserAuthenticationFilter(IConfiguration configuration, IUserRepository repo)
        {
            _configuration = configuration;
            _repo = repo;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("x-api-key", out var apiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key is missing");
                return;
            }

            var user = _repo.GetUserByApiKeyAsync(apiKey!);

            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult("User not found");
                return;
            }
        }
    }
}
