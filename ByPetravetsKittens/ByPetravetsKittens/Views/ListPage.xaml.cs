using ByPetravetsKittens.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ByPetravetsKittens.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

        private async void HandleUserTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetailsPage((((TappedEventArgs)e).Parameter as User)));
        }
    }
}