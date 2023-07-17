using Core.Entities.Identity;

namespace Core.Interfaces.Service.Identity
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}