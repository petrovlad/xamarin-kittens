using ByPetravetsKittens.Models;
using ByPetravetsKittens.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ByPetravetsKittens.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        private bool _isVideoSelected = false;
        private User _user;
        public ObservableCollection<ImageSource> Images { get; set; } = new ObservableCollection<ImageSource>();
        private List<string> _imagesToDelete = new List<string>();
        private List<string> _uploadedImages;
        private List<FileResult> _addedImages = new List<FileResult>();
        private FileResult _addedVideo;
        public Command DeleteImageCommand { get; set; }


        public EditPage(User user)
        {
            DeleteImageCommand = new Command(HandleDeleteImage);

            InitializeComponent();

            // Utils.FillListWithEnumValues<OccupationType>(Occupation.Items);
            // Occupation.SelectedIndex = Array.IndexOf(Enum.GetValues(typeof(OccupationType)), user.Occupation);
            // Utils.FillListWithEnumValues<MobType>(FavouriteMob.Items);
            // FavouriteMob.SelectedIndex = Array.IndexOf(Enum.GetValues(typeof(MobType)), user.FavouriteMob);
            // Utils.FillListWithEnumValues<PrivilegeType>(Privilege.Items);
            // Privilege.SelectedIndex = Array.IndexOf(Enum.GetValues(typeof(PrivilegeType)), user.Privilige);
            FavouriteServerAddress.Text = user.FavouriteServerAddress;
            RealworldName.Text = user.RealworldName;
            Country.Text = user.Country;
            City.Text = user.City;
            Age.Text = user.Age.ToString();
            Latitude.Text = user.Position.Latitude.ToString();
            Longitude.Text = user.Position.Longitude.ToString();
            _uploadedImages = new List<string>(user.ImageUrls);

            _isVideoSelected = user.VideoUrl != null;
            ToggleVideoButtonText();

            foreach (var imageUrl in user.ImageUrls)
            {
                Images.Add(ImageSource.FromUri(new Uri(imageUrl)));
            }

            SetDisplayedImagesHeight();            

            _user = user;            
        }

        private void ToggleVideoButtonText()
        {
            Video.Text = _isVideoSelected ? "Delete video" : "Add video";
        }

        private void SetDisplayedImagesHeight()
        {
            DisplayedImages.HeightRequest = 75 * Images.Count() + 75;
        }

        private async void HandleAddImageClicked(object sender, EventArgs e)
        {
            var image = await MediaPicker.PickPhotoAsync();
            if (image != null)
            {
                _addedImages.Add(image);
                Images.Add(ImageSource.FromFile(image.FullPath));
                SetDisplayedImagesHeight();
            }
        }

        private void HandleDeleteImage(object image)
        {
            var imageIndex = Images.IndexOf(image as ImageSource);

            if (imageIndex == -1) return;

            if (imageIndex >= _uploadedImages.Count())
            {
                _addedImages.RemoveAt(imageIndex - _uploadedImages.Count());
            }
            else
            {
                var deletedImage = _uploadedImages[imageIndex];
                _uploadedImages.RemoveAt(imageIndex);
                _imagesToDelete.Add(deletedImage);
            }

            Images.RemoveAt(imageIndex);
            SetDisplayedImagesHeight();
        }

        private async void HandleToggleVideoClicked(object sender, EventArgs e)
        {
            if (!_isVideoSelected)
            {
                var video = await MediaPicker.PickVideoAsync();
                if (video != null)
                {
                    _addedVideo = video;
                    _isVideoSelected = true;
                }
            }
            else
            {
                _isVideoSelected = false;
                _addedVideo = null;
            }

            ToggleVideoButtonText();
        }

        private async void HandleSaveClicked(object sender, EventArgs e)
        {
            try
            {
                var mediaService = DependencyService.Get<IMediaService>();
                await mediaService.DeleteImages(_imagesToDelete);
                var imageUrls = await mediaService.UploadImages(_user.Id, _addedImages);

                var newVideoUrl = _user.VideoUrl;
                if (_addedVideo != null)
                {
                    if (_user.VideoUrl != null)
                    {
                        await mediaService.DeleteVideo(_user.VideoUrl);
                    }

                    newVideoUrl = await mediaService.UploadVideo(_user.Id, _addedVideo);
                }

                // _user.Occupation = (OccupationType)Enum.GetValues(typeof(OccupationType)).GetValue(Occupation.SelectedIndex);
                // _user.FavouriteMob = (MobType)Enum.GetValues(typeof(MobType)).GetValue(FavouriteMob.SelectedIndex);
                // _user.Privilige = (PrivilegeType)Enum.GetValues(typeof(PrivilegeType)).GetValue(Privilege.SelectedIndex);
                _user.FavouriteServerAddress = FavouriteServerAddress.Text;
                _user.RealworldName = RealworldName.Text;
                _user.Country = Country.Text;
                _user.City = City.Text;
                _user.Age = Utils.ParseInt(Age.Text);
                _user.Position = new Position(Utils.ParseInt(Latitude.Text), Utils.ParseInt(Longitude.Text));

                _user.ImageUrls = (new List<string>(_uploadedImages)).Concat(imageUrls).ToList();
                _user.VideoUrl = newVideoUrl;

                var storageService = DependencyService.Get<IStorageService>();
                await storageService.UpdateUser(_user);

                _addedVideo = null;
                _addedImages.Clear();
                _imagesToDelete.Clear();
                _uploadedImages = new List<string>(_user.ImageUrls);

                await DisplayAlert("Success", "User updated", "OK");
            } catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

        }
    }
}