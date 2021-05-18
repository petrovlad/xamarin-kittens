using System;
using System.ComponentModel;
using System.Globalization;
using ColorPicker;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ByPetravetsKittens.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            // Fonts.SelectedIndex = 0;
            Language.SelectedIndex = 0;
            Size.Value = Convert.ToDouble(Application.Current.Resources["FontSize"]);
            DarkModeSwitch.IsToggled = Application.Current.UserAppTheme == OSAppTheme.Dark;
            
            // ColorPicker.SelectedColor = (Color) Application.Current.Resources["BackgroundColor"];
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
        }

        private async void HandleLogOutClicked(object sender, EventArgs e)
        {
            // Navigation.InsertPageBefore(new SignInPage(), this);
            // await Navigation.PopAsync();
            await Navigation.PopToRootAsync();
            Application.Current.MainPage = new NavigationPage(new SignInPage());
        }

        private void HandleFontSizeChange(object sender, ValueChangedEventArgs e)
        {
            Size.Value = Math.Round(Size.Value);
            Application.Current.Resources["FontSize"] = Convert.ToInt32(Size.Value);
        }

        private void HandleFontChange(object sender, EventArgs e)
        {
            // Application.Current.Resources["FontFamily"] = Fonts.SelectedItem;
        }

        private void HandleLanguageChange(object sender, EventArgs e)
        {
            var langCulture = Language.SelectedIndex == 0 ? "en" : "ru";
            Application.Current.Resources["Language"] = langCulture;
            LocalizationResourceManager.Current.SetCulture(
               CultureInfo.GetCultureInfo(langCulture)
            );
        }

        private void HandleColorChange(object sender, PropertyChangedEventArgs e)
        {
            var colorCircle = (ColorCircle) sender;
            Application.Current.Resources["BackgroundColor"] = colorCircle.SelectedColor;
        }

        private void HandleDarkModeToggle(object sender, ToggledEventArgs e)
        {
            if (DarkModeSwitch.IsToggled)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
            }
        }
    }
}
