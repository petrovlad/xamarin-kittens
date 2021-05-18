using ByPetravetsKittens.Resources.Localization;
using ByPetravetsKittens.Services;
using ByPetravetsKittens.Views;
using System;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ByPetravetsKittens
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            LocalizationResourceManager.Current.Init(Language.ResourceManager);

            MainPage = new NavigationPage(new SignInPage());
            //MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {
        }
    }
}
