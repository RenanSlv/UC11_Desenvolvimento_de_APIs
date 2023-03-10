using Chapter.Interfaces;
using Chapter.ViewModels;
using Chapter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Chapter.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;

        public LoginController(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }

        /// <summary>
        /// Método que controla o acesso para login
        /// </summary>
        /// <param name="login">Dados dos usuario : email e senha</param>
        /// <returns>Token de acesso</returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel login) 
        {
            Usuario usuarioEncontrado = _iUsuarioRepository.Login(login.Email, login.Senha);
            if (usuarioEncontrado == null) 
            { 
                return Unauthorized(new { msg = "E-mail e/ou senha inválidos"});
            }

            var minhasClaims = new[]
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, usuarioEncontrado.Id.ToString()),
                new Claim(ClaimTypes.Role, usuarioEncontrado.Tipo)
            };

            var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao"));

            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var meuToken = new JwtSecurityToken(
                issuer: "chapter.webapi",
                audience: "chapter.webapi",
                claims: minhasClaims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credenciais
            );

            return Ok( new {token = new JwtSecurityTokenHandler().WriteToken(meuToken)});

        }
    }
}
