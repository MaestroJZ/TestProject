using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestProject.Data;
using TestProject.Models.Dto;
using TestProject.Repository.IRepository;

namespace TestProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private string secretKey;
        public UserRepository(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = "oeimvg4n492938fnh2nf2o3nf92oikenf293nf842nf2qefi";
        }
        public bool isUniqueUser(string login)
        {
            var user = _db.Users.FirstOrDefault(u => u.Login == login);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Users.FirstOrDefault(u => u.Login.ToLower() == loginRequestDTO.Login.ToLower()
            && u.Password == loginRequestDTO.Password);
            if (user == null) {
                LoginResponseDTO notFound = new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
                return Task.FromResult(notFound);
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return Task.FromResult(loginResponseDTO);

        }
    }
}
