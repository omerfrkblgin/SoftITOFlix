using Microsoft.AspNetCore.Identity;

namespace SoftITOFlix.Models
{
    public class SoftITOFlixRole : IdentityRole<long>
    {
        public SoftITOFlixRole(string roleName) : base(roleName)
        {

        }
        public SoftITOFlixRole()
        {

        }
    }
}
