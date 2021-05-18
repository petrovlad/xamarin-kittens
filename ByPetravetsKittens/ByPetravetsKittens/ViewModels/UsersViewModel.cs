using ByPetravetsKittens.Models;
using ByPetravetsKittens.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ByPetravetsKittens.ViewModels
{
    class UsersViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public UsersViewModel()
        {
            _users = DependencyService.Get<IStorageService>().GetUsers();
         //   _users = new ObservableCollection<User>
         //   {
         //       new User()
         //       {
         //           Id = "1",
         //            Email=  "Ilya@email.com",
         //            Password =  "password",
         //            Nickname =   "Ilya",
         //            Occupation =  OccupationType.MapArt,
         //            FavouriteMob =   MobType.Creeper,
         //            FavouriteServerAddress =  "mc.hypixel.net",
         //            Privilige =  PrivilegeType.VipPlus,
         //            RealworldName =  "Ilya",
         //            Country =  "Belarus",
         //            City =  "Minsk",
         //            Age =  19,
         //            AvatarUrl =  "https://crafatar.com/avatars/c06f89064c8a49119c29ea1dbd1aab82?size=150&overlay",
         //            ImageUrls = new List<string>()
         //            {
         //                         "https://i.picsum.photos/id/429/150/300.jpg?hmac=Bh9wghd3kGclhTp0AmpMBonpCFpzEUCNGecLL7Sn7bI",
         //"https://i.picsum.photos/id/22/300/300.jpg?hmac=hpHOD_a9-i6VAVANAhXPGG6fEyYK_-TNpMOM6aBQfgE",
         //"https://i.picsum.photos/id/1012/300/150.jpg?hmac=eSEwHIgJQmTwGj3bbFui3Klo81t4jYYr61PxbqSht_0",
         //"https://i.picsum.photos/id/429/150/300.jpg?hmac=Bh9wghd3kGclhTp0AmpMBonpCFpzEUCNGecLL7Sn7bI",
         //"https://i.picsum.photos/id/22/300/300.jpg?hmac=hpHOD_a9-i6VAVANAhXPGG6fEyYK_-TNpMOM6aBQfgE",
         //"https://i.picsum.photos/id/1012/300/150.jpg?hmac=eSEwHIgJQmTwGj3bbFui3Klo81t4jYYr61PxbqSht_0",
         //            },
         //            VideoUrl = "https://bit.ly/swswift"
         //       },
         //       new User()
         //       {
         //           Id = "2",
         //            Email=  "Ilya@email.com",
         //            Password =  "password",
         //            Nickname =   "Ilya",
         //            Occupation =  OccupationType.MapArt,
         //            FavouriteMob =   MobType.Creeper,
         //            FavouriteServerAddress =  "mc.hypixel.net",
         //            Privilige =  PrivilegeType.VipPlus,
         //            RealworldName =  "Ilya",
         //            Country =  "Belarus",
         //            City =  "Minsk",
         //            Age =  19,
         //            AvatarUrl =  "https://crafatar.com/avatars/c06f89064c8a49119c29ea1dbd1aab82?size=150&overlay",
         //            ImageUrls = new List<string>()
         //            {
         //                         "https://i.picsum.photos/id/429/150/300.jpg?hmac=Bh9wghd3kGclhTp0AmpMBonpCFpzEUCNGecLL7Sn7bI",
         //"https://i.picsum.photos/id/22/300/300.jpg?hmac=hpHOD_a9-i6VAVANAhXPGG6fEyYK_-TNpMOM6aBQfgE",
         //"https://i.picsum.photos/id/1012/300/150.jpg?hmac=eSEwHIgJQmTwGj3bbFui3Klo81t4jYYr61PxbqSht_0",
         //            }
         //       }
         //   };
        }
    }
}
