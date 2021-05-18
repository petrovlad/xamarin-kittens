using Firebase.Database;
using Firebase.Database.Query;
using ByPetravetsKittens.Models;
using ByPetravetsKittens.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByPetravetsKittens.Droid.Services
{
    class FirebaseDatabaseStorage : IStorageService
    {
        private const string UsersTableName = "users";
        private readonly FirebaseClient client;

        public FirebaseDatabaseStorage()
        {
            client = new FirebaseClient("https://by-petravets-kittens-default-rtdb.firebaseio.com/");
        }

        public async Task AddUser(User user)
        {
            await client
                .Child(UsersTableName)
                .PostAsync(user);
        }

        public ObservableCollection<User> GetUsers()
        {
            return client
                 .Child(UsersTableName)
                 .OrderBy("Nickname")
                 .AsObservable<User>()
                 .AsObservableCollection();
        }

        public async Task UpdateUser(User user)
        {
            var userToUpdate = (await client.Child(UsersTableName)
                .OnceAsync<User>())
                .FirstOrDefault(u => u.Object.Id == user.Id);

            await client
                .Child(UsersTableName)
                .Child(userToUpdate.Key)
                .PutAsync(user);
        }
    }
}