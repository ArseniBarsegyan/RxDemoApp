using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI.XamForms;
using RxUIDemoApp.ViewModels;
using Xamarin.Forms;

namespace RxUIDemoApp.Views
{
    public partial class SearchDemoPage : ReactiveContentPage<SearchPageViewModel>
    {
        private CompositeDisposable _compositeDisposable;

        public SearchDemoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            _compositeDisposable = new CompositeDisposable();
            _compositeDisposable
                .Add(Observable.FromEventPattern<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(
                        x => SearchBar.TextChanged += x,
                        x => SearchBar.TextChanged -= x)
                    .Throttle(TimeSpan.FromSeconds(2), TaskPoolScheduler.Default)
                    .Select(args => args.EventArgs.NewTextValue)
                    .Subscribe(text =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            ResultsStackLayout.Children.Add(new Label { Text = text });
                        });
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
