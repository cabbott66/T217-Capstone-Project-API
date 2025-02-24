using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Repositories;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Authentication
{
    public class UserAuthenticationFilterAdmin : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repo;

        public UserAuthenticationFilterAdmin(IConfiguration configuration, IUserRepository repo)
        {
            _configuration = configuration;
            _repo = repo;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key is missing");
                return;
            }

            var user = await _repo.GetUserByApiKeyAsync(extractedApiKey);

            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult("User not found");
                return;
            }
            else if (user.UserID != 1)
            {
                context.Result = new UnauthorizedObjectResult("User not authorized");
                return;
            }
        }
    }
}
