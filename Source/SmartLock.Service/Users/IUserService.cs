using SmartLock.Model;
using SmartLock.Model.Dto;

namespace SmartLock.DataLogic.Users
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(UserCredentialDto userCredential);
    }
}
