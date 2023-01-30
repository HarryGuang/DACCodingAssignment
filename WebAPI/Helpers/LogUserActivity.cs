using WebAPI.Extensions;
using WebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();

            var userRepository = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var user = await userRepository.GetUserByIdAsync(userId);
            user.LastActive = DateTime.UtcNow;
            await userRepository.UpdateUserAsync(user);
        }
    }
}
