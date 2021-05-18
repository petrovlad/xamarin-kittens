using ByPetravetsKittens.Models;
using ByPetravetsKittens.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ByPetravetsKittens.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        private readonly AvatarService _avatarService = new AvatarService();
        private string _avatarUrl;

        public SignUpPage()
        {
            InitializeComponent();

            AvatarUrl = _avatarService.GetDefaultAvatarUrl();
            // Utils.FillListWithEnumValues<OccupationType>(Occupation.Items);
            // Occupation.SelectedIndex = 0;
            // Utils.FillListWithEnumValues<MobType>(FavouriteMob.Items);
            // FavouriteMob.SelectedIndex = 0;
            // Utils.FillListWithEnumValues<PrivilegeType>(Privilege.Items);
            // Privilege.SelectedIndex = 0;
        }

        public string AvatarUrl
        {
            get => _avatarUrl;
            set
            {
                _avatarUrl = value;
                OnPropertyChanged(nameof(AvatarUrl));
            }
        }

        private async void HandleSignUpClick(object sender, EventArgs e)
        {
            try
            {
                var auth = DependencyService.Get<IAuth>();
                var userId = await auth.SignUpWithEmail(Email.Text, Password.Text);

                var avatarData = await FetchAvatarData(AvatarUrl);
                var mediaService = DependencyService.Get<IMediaService>();
                var avatarUrl = await mediaService.UploadAvatar(userId, avatarData);

                var user = new User
                {
                    Id = userId,
                    Email = Email.Text,
                    Password = Password.Text,
                    Nickname = Nickname.Text,
                    // Occupation = (OccupationType)Enum.GetValues(typeof(OccupationType)).GetValue(Occupation.SelectedIndex),
                    // FavouriteMob = (MobType)Enum.GetValues(typeof(MobType)).GetValue(FavouriteMob.SelectedIndex),
                    FavouriteServerAddress = FavouriteServerAddress.Text,
                    // Privilige = (PrivilegeType)Enum.GetValues(typeof(PrivilegeType)).GetValue(Privilege.SelectedIndex),
                    RealworldName = RealworldName.Text,
                    Country = Country.Text,
                    City = City.Text,
                    Age = Utils.ParseInt(Age.Text),
                    AvatarUrl = avatarUrl
                };

                var storageService = DependencyService.Get<IStorageService>();
                await storageService.AddUser(user);

                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            } catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task<byte[]> FetchAvatarData(string avatarUrl)
        {
            using (var client = new WebClient())
            {
                return await client.DownloadDataTaskAsync(avatarUrl);
            }
        }

        private async void HandleNicknameChanged(object sender, EventArgs e)
        {
            AvatarUrl = await _avatarService.GetAvatarUrlByNickname(Nickname.Text);
        }
    }
}