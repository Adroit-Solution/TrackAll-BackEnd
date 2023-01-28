using System.Security.Claims;

namespace Test_Series.Services
{
    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserServices(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetId()
        {
            return httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated()
        {
            return httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
