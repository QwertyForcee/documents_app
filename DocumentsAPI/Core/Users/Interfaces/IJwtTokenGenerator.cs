using DocumentsAPI.Core.Users.Models;

namespace DocumentsAPI.Core.Users.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string Generate(User user);
    }
}
