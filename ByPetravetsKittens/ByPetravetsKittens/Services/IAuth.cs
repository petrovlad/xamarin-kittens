using System.Threading.Tasks;

namespace ByPetravetsKittens.Services
{
    public interface IAuth
    {
        Task<string> SignUpWithEmail(string email, string password);
    }
}
