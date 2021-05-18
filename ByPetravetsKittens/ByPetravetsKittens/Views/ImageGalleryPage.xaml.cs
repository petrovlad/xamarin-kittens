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
    public partial class ImageGalleryPage : ContentPage
    {
        private IList<string> _images;

        public IList<string> Images
        {
            get => _images;
            set
            {
                _images = value;
                OnPropertyChanged();
            }
        }

        public ImageGalleryPage(IList<string> images, int selectedImage)
        {
            _images = images;

            InitializeComponent();            

            Carousel.CurrentItem = images[selectedImage];
        }
    }
}