using System.Security.Claims;

namespace ELearningPortal.Helpers
{
    // Small helpers to hash/verify passwords and to read current logged-in user info from JWT claims.
    public static class CommonHelper
    {
        // -------- Password (BCrypt) --------
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword)) return false;
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch
            {
                // hashedPassword is not a valid BCrypt hash (e.g. old plain text) -> not verified
                return false;
            }
        }

        // -------- Claim readers (use these inside controllers / services) --------
        // Example (inside any Controller):  var userId = User.GetUserId();
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var val = user.FindFirst("UserId")?.Value;
            return int.TryParse(val, out var id) ? id : 0;
        }

        public static int GetBranchId(this ClaimsPrincipal user)
        {
            var val = user.FindFirst("BranchId")?.Value;
            return int.TryParse(val, out var id) ? id : 0;
        }

        public static string GetBranchName(this ClaimsPrincipal user)
        {
            return user.FindFirst("BranchName")?.Value ?? string.Empty;
        }

        public static string GetFullName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        }

        public static string GetRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        }

        public static string GetProfileImage(this ClaimsPrincipal user)
        {
            return user.FindFirst("ProfileImage")?.Value ?? string.Empty;
        }
    }
}
