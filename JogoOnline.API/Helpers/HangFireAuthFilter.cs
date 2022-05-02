using Hangfire.Dashboard;

namespace JogoOnline.API.Helpers
{
    public class HangFireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}