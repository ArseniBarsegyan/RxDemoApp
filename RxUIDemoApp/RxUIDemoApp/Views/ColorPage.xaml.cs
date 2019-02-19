using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.XamForms;
using RxUIDemoApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RxUIDemoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPage : ReactiveContentPage<ColorViewModel>
    {
        private CompositeDisposable _compositeDisposable;

        public ColorPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            _compositeDisposable = new CompositeDisposable();
            _compositeDisposable.Add(this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, vm => vm.RedColor, c => c.RedSelector.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, vm => vm.GreenColor, c => c.GreenSelector.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, vm => vm.BlueColor, c => c.BlueSelector.Value)
                    .DisposeWith(disposable);

                // UI should be updated in main thread
                this.WhenAnyValue(x => x.ViewModel.Color)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(observer =>
                    {
                        ColorDisplay.BackgroundColor =
                            Color.FromRgba(observer.R, observer.G, observer.B, observer.A);
                    })
                    .DisposeWith(disposable);
            }));
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _compositeDisposable.Dispose();
            base.OnDisappearing();
        }
    }
}