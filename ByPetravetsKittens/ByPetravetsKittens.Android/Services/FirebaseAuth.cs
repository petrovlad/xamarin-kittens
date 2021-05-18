using Android.App;
using Android.Content;
using Android.OS;
using ByPetravetsKittens.Services;
using System.Threading.Tasks;

namespace ByPetravetsKittens.Droid.Services
{
    class FirebaseAuth : IAuth
    {
        private readonly Firebase.Auth.FirebaseAuth _instance = Firebase.Auth.FirebaseAuth.Instance;

        public async Task<string> SignInWithEmail(string email, string password)
        {
            var user = await _instance.SignInWithEmailAndPasswordAsync(email, password);
            return user.User.Uid;
        }

        public async Task<string> SignUpWithEmail(string email, string password)
        {
            var user = await _instance.CreateUserWithEmailAndPasswordAsync(email, password);
            return user.User.Uid;
        }
    }
}