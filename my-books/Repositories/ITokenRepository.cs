using Microsoft.AspNetCore.Identity;

namespace my_books.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
