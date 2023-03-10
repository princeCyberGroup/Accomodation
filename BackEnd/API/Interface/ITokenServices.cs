using API.Entity;

namespace API.Interface
{
    public interface ITokenServices
    {
        string CreateToken(AppUserRegister user);
    }
}