using System;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.XamForms;
using RxUIDemoApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace RxUIDemoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDemoPage : ReactiveContentPage<EventDemoViewModel>
    {
        public EventDemoPage()
        {
            InitializeComponent();
            Observable.FromEventPattern(x => ClickCounter.Clicked += x,
                    x => ClickCounter.Clicked -= x)
                .Throttle(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(async target => { await DisplayAlert("Event", "Event handled", "Ok"); });
        }
    }
}