using ReactiveUI.XamForms;
using RxUIDemoApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace RxUIDemoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ReactiveContentPage<DetailsViewModel>
    {
        public DetailsPage()
        {
            InitializeComponent();
        }
    }
}