using ByPetravetsKittens.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ByPetravetsKittens.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        private readonly IAuth _auth; 

        public SignInPage()
        {
            InitializeComponent();

            _auth = DependencyService.Resolve<IAuth>();
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            try
            {
                //var userId = await _auth.SignInWithEmail(Email.Text, Password.Text);
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            } catch (Exception error)
            {
                // await DisplayAlert("Error", error.Message, "OK");
            }
        }

        private void OnSignUpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}