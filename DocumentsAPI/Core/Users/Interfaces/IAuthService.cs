namespace DocumentsAPI.Core.Users.Interfaces
{
    public interface IAuthService
    {
        Task<string> SignUpAsync(string email, string password, string name);
        Task<string> SignInAsync(string email, string password);
    }
}
