using System.Security.Claims;

namespace SmBeachApp.Extensions
{
    public static class HttpExtensions
    {
        public static string GetCurrentUser(this HttpRequest request)
        {
            try
            {
                string result = request.HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }

                return "Anonimous Web";
            }
            catch
            {
                return null;
            }
        }
    }
}
