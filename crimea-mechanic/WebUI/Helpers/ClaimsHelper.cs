using System.Security.Claims;
using System.Security.Principal;
using WebUI.ProjectConstants;

namespace WebUI.Helpers
{
    public static class ClaimsHelper
    {
        public static string GetDeclineReason(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimsConstants.ClaimDeclineReason);

            return claim?.Value ?? string.Empty;
        }
    }
}