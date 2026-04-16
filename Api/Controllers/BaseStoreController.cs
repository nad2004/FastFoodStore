using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    /// <summary>
    /// Base controller providing common functionality for all API controllers
    /// </summary>
    [ApiController]
    public abstract class BaseStoreController : ControllerBase
    {
        /// <summary>
        /// Gets the current user ID from the JWT token claims
        /// </summary>
        protected int UserId
        {
            get
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                return userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId) ? userId : 0;
            }
        }

        /// <summary>
        /// Gets the current user's email from the JWT token claims
        /// </summary>
        protected string UserEmail => User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

        /// <summary>
        /// Gets the user's roles
        /// </summary>
        protected IEnumerable<string> Roles
        {
            get
            {
                return User.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? new List<string>();
            }
        }

        /// <summary>
        /// Checks if the user has admin role
        /// </summary>
        protected bool IsAdmin => Roles.Contains("admin");

        /// <summary>
        /// Checks if the user has staff role
        /// </summary>
        protected bool IsStaff => Roles.Contains("staff");

        /// <summary>
        /// Checks if the user is authenticated
        /// </summary>
        protected bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
    }
}
