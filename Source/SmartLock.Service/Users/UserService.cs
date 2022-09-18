using Microsoft.IdentityModel.Tokens;
using SmartLock.Access.Users;
using SmartLock.Common.Constants;
using SmartLock.Common.Exceptions;
using SmartLock.Model;
using SmartLock.Model.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartLock.DataLogic.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(UserCredentialDto userCredential)
        {
            var encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(userCredential.Password));
            var user = await _userRepository.GetByUserNameAsync(userCredential.UserName);

            if (user == null || user.Password != encodedPassword) 
                throw new SmartLockApiException("Invalid username or password");

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        private static string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim(Jwt.ID, user.Id.ToString()),
                    new Claim(Jwt.ROLES, String.Join(',', user.UserOfficeRoleMappings.Select(x => x.RoleId))),
                    new Claim(Jwt.IS_ADMIN, user.IsAdmin.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
