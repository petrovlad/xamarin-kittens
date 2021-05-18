using ByPetravetsKittens.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ByPetravetsKittens.Services
{
    public interface IStorageService
    {
        ObservableCollection<User> GetUsers();
        Task AddUser(User user);
        Task UpdateUser(User user);
    }
}
