using kanban_websocket_back.Data;
using kanban_websocket_back.Jwt;
using kanban_websocket_back.Models.Login;
using kanban_websocket_back.Tokens;
using kanban_websocket_back.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kanban_websocket_back.Controllers
{
    [AllowAnonymousAttribute]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly LoginValidation _loginValidation = new LoginValidation();
        private readonly Token _token = new Token();


        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] UserLogin user,
            [FromServices] postgresContext context
        )
        {
            try
            {
                var login = user.Email;
                var password = user.Password;

                if (string.IsNullOrEmpty(login))
                    return Unauthorized(new { message = "Username is required" });

                if (string.IsNullOrEmpty(password))
                    return Unauthorized(new { message = "Password is required" });

                var encryptedPassword = _loginValidation.NewEncrypt(password, 3);

                var usuario = await context.Users.FirstOrDefaultAsync(x => x.Email == login);

                if (usuario == null)
                    return Unauthorized(new { message = "Usuário e/ou senha incorretos!" });

                if (usuario.Email == login && usuario.Password == encryptedPassword | usuario.Password == password)
                {
                    var usuarioID = usuario.Id;
                    var name = usuario.Name;
                    var email = usuario.Email;
                    var token = new JwtTokenBuilder()
                                            .AddSecurityKey(JwtSecurityKey.Create(_token.JwtKey()))
                                            .AddSubject(login)
                                            .AddClaim("id", usuarioID.ToString())
                                            .AddClaim("email", login)
                                            .AddExpiry(_token.JwtExpiry("User"))
                                            .AddIssuer(_token.JwtIssuer())
                                            .AddAudience(_token.JwtAudience())
                                            .Build();
                    return StatusCode(200, new { token.Value, usuarioID, name, email });
                }
                return Unauthorized(new { message = "Usuário e/ou senha incorretos!" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível realizar o login" });
            }
        }
    }
}
