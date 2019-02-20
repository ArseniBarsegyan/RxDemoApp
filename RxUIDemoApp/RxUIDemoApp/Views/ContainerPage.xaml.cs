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
                this.OneWayBind(this.ViewModel, vm => vm.ColorViewModel, c => c.ColorDemoPage.ViewModel)
                    .DisposeWith(disposables);
                this.OneWayBind(this.ViewModel, vm => vm.RestViewModel, c => c.RestDemoPage.ViewModel)
                    .DisposeWith(disposables);
                this.OneWayBind(this.ViewModel, vm => vm.SearchPageViewModel, c => c.SearchDemoPage.ViewModel)
                    .DisposeWith(disposables);
                this.OneWayBind(this.ViewModel, vm => vm.EventDemoViewModel, c => c.EventDemoPage.ViewModel)
                    .DisposeWith(disposables);
            });
        }
    }
}