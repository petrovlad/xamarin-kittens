using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Maps;

namespace ByPetravetsKittens.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Nickname { get; set; }
        public string FavouriteServerAddress { get; set; }
        public string RealworldName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Age { get; set; }

        public string AvatarUrl { get; set; }

        public Position Position { get; set; } = new Position(0, 0);
        public IList<string> ImageUrls { get; set; } = Enumerable.Empty<string>().ToList();
        public string VideoUrl { get; set; }
    }
}
