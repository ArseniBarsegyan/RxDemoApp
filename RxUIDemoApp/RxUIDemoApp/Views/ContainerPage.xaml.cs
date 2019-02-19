using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;
using RxUIDemoApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace RxUIDemoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContainerPage : ReactiveTabbedPage<ContainerPageViewModel>
    {
        public ContainerPage()
        {
            InitializeComponent();
            this.ViewModel = new ContainerPageViewModel();
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(this.ViewModel, x => x.ColorViewModel, x => x.ColorDemoPage.ViewModel)
                    .DisposeWith(disposables);
                this.OneWayBind(this.ViewModel, x => x.RestViewModel, x => x.RestDemoPage.ViewModel)
                    .DisposeWith(disposables);
                this.OneWayBind(this.ViewModel, x => x.SearchPageViewModel, x => x.SearchDemoPage.ViewModel)
                    .DisposeWith(disposables);
            });
        }
    }
}