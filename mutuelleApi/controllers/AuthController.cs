using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using mutuelleApi.dtos;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.controllers
{
    public class AuthController(IMapper mapper, IUnitOfWork uow, IConfiguration configuration) : BaseController
    {
        private readonly IUnitOfWork uow = uow;
        private readonly IMapper mapper = mapper;
        private readonly IConfiguration configuration = configuration;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthRequestDto request)
        {
            var utilisateur = await uow.UtilisateurRepository.GetByLoginAsync(request.Login);

            if (utilisateur is null)
            {
                return NotFound("Utilisateur non trouvé!");
            }

            if (
                utilisateur.MotDePasse is not null &&
                utilisateur.ClesMotDePasse is not null &&
                request.Password is not null &&
                !MatchPasswordHash(request.Password, utilisateur.MotDePasse, utilisateur.ClesMotDePasse)
            )
            {
                return BadRequest("Le mot de passe est invalide");
            }


            var authResponseDto = new AuthResponseDto();
            authResponseDto.Token = CreateJWT(utilisateur, 1);
            authResponseDto.RefreshToken = CreateJWT(utilisateur, 24);

            return Ok(authResponseDto);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var utilisateur = await uow.UtilisateurRepository.GetByIdAsync(GetUserId());

            if (utilisateur is null)
            {
                return NotFound("Cet utilisateur n'existe pas!");
            }

            var utilisateurDto = mapper.Map<UtilisateurDto>(utilisateur);
            return Ok(utilisateurDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Password(int id, ChangePasswordRequest request)
        {
            var utilisateur = await uow.UtilisateurRepository.GetByIdAsync(id);
            if (utilisateur is null)
            {
                return NotFound("Utilisateur introuvable");
            }

            
            if (
                utilisateur.MotDePasse is not null &&
                utilisateur.ClesMotDePasse is not null &&
                request.MotDePasse is not null &&
                request.AncienMotDePasse is not null &&
                MatchPasswordHash(request.AncienMotDePasse, utilisateur.MotDePasse, utilisateur.ClesMotDePasse)
            )
            {
                var hmac = new HMACSHA512(utilisateur.ClesMotDePasse);
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.MotDePasse));
                utilisateur.MotDePasse = passwordHash;
            }

            await uow.SaveAsync();
            return Ok();
        }


        private string CreateJWT(Utilisateur utilisateur, int expiration)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("La clé secrète pour le JWT est introuvable ou vide.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(utilisateur.Login))
            {
                claims.Add(new Claim(ClaimTypes.Name, utilisateur.Login));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString()));

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(expiration),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool MatchPasswordHash(string passworText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passworText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (password[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }
    }
}