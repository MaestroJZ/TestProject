using TestProject.Models.Dto;

namespace TestProject.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUniqueUser(string login);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
