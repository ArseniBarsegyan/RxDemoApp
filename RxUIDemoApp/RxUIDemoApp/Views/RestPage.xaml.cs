using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.XamForms;
using RxUIDemoApp.Models;
using RxUIDemoApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RxUIDemoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestPage : ReactiveContentPage<RestPageViewModel>
    {
        public RestPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, vm => vm.People, c => c.ResultsList.ItemsSource)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, vm => vm.StartDownload, c => c.StartDownloadButton);
                this.BindCommand(ViewModel, vm => vm.StopDownload, c => c.StopDownloadButton);
                this.BindCommand(ViewModel, vm => vm.ClearList, c => c.ClearButton);
            });
        }
    }
}