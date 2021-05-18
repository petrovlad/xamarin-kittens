using ByPetravetsKittens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.CommunityToolkit.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ByPetravetsKittens.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        private readonly User _user;
        private IList<string> _images = Enumerable.Empty<string>().ToList();

        public IList<string> Images
        {
            get => _images;
            set
            {
                _images = value;
                OnPropertyChanged(nameof(Images));
            }
        }

        public DetailsPage(User user)
        {
            InitializeComponent();

            _user = user;
            UpdateDisplayedData();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateDisplayedData();
        }

        private void UpdateDisplayedData()
        {
            // Avatar.Source = ImageSource.FromUri(new Uri(_user.AvatarUrl));
            Nickname.Text = _user.Nickname;
            // Occupation.Text = _user.Occupation.ToString();
            // Privilege.Text = _user.Privilige.ToString();
            // FavouriteMob.Text = _user.FavouriteMob.ToString();
            FavouriteServerAddress.Text = _user.FavouriteServerAddress;
            RealworldName.Text = _user.RealworldName;
            Country.Text = _user.Country;
            City.Text = _user.City;
            Age.Text = _user.Age.ToString();

            if (_user.ImageUrls.Count() != 0)
            {
                Images = _user.ImageUrls.Take(4).ToList();
                ImagesPreview.IsVisible = true;
            }
            else
            {
                ImagesPreview.IsVisible = false;
            }

            if (_user.VideoUrl != null)
            {
                Video.Source = MediaSource.FromUri(_user.VideoUrl);
                Video.IsVisible = true;
            }
            else
            {
                Video.IsVisible = false;
            }
        }

        private async void HandleEditClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage(_user));
        }

        public async void HandleImageTapped(object sender, EventArgs e)
        {
            var imageIndex = Images.IndexOf((((TappedEventArgs)e).Parameter as string));
            await Navigation.PushAsync(new ImageGalleryPage(_user.ImageUrls, imageIndex));
        }
    }
}
