using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByPetravetsKittens.Models;
using ByPetravetsKittens.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ByPetravetsKittens.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditLocation : ContentPage
    {
        private readonly User _user;
        private Pin _pin;
        public EditLocation()
        {
            InitializeComponent();
        }

        public EditLocation(User user)
        {
            InitializeComponent();
            _user = user;
            btnSave.IsEnabled = false;
        }

        private void Map_OnMapClicked(object sender, MapClickedEventArgs e)
        {
            _pin = null;
            Map.Pins.Clear();
            _pin = new Pin()
            {
                Label = "",
                Type = PinType.Place,
                Position = new Position(e.Position.Latitude, e.Position.Longitude)
            };
            Map.Pins.Add(_pin);
            btnSave.IsEnabled = true;
        }

        private async void HandleSaveLocationClicked(object sender, EventArgs e)
        {
            try
            {
                _user.Position= _pin.Position;
                var storageService = DependencyService.Get<IStorageService>();
                await storageService.UpdateUser(_user);
                await DisplayAlert("Success", "User updated", "OK");
            } catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}