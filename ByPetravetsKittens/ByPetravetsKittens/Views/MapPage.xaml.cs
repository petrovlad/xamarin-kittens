using ByPetravetsKittens.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ByPetravetsKittens.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        private void HandleMarkerClicked(object sender, PinClickedEventArgs e)
        {
            var vm = (BindingContext as UsersViewModel);
            var pin = (Pin) sender;
            Navigation.PushAsync(new DetailsPage(vm.Users.First(u => u.Nickname == pin.Label)));
        }
    }
}