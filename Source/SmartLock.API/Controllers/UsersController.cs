using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLock.DataLogic.Users;
using SmartLock.Model.Dto;
using Swashbuckle.Swagger.Annotations;

namespace SmartLock.Controllers
{
    [ApiController]
    [Route("api/users")]
    [ApiVersion("1")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userDataLogic;
        public UsersController(IUserService userDataLogic)
        {
            _userDataLogic = userDataLogic;
        }

        [HttpPost, Route("login")]
        [AllowAnonymous]
        [SwaggerOperation(OperationId = "login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserCredentialDto userCredential)
        {
            return Ok(await _userDataLogic.AuthenticateAsync(userCredential));
        }
    }
}