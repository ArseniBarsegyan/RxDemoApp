using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using RxUIDemoApp.Models;
using RxUIDemoApp.Services;

namespace RxUIDemoApp.ViewModels
{
    public class RestPageViewModel : BaseViewModel
    {
        private int _currentId = 1;
        private CompositeDisposable _compositeDisposable;

        public RestPageViewModel()
        {
            UrlPathSegment = "REST demo";
            People = new ObservableCollection<Human>();

            StartDownload = ReactiveCommand.Create(UpdatePeople);
            StopDownload = ReactiveCommand.Create(() =>
            {
                _compositeDisposable.Dispose();
            });
            ClearList = ReactiveCommand.Create(() =>
            {
                People.Clear();
                _currentId = 1;
            });
        }

        public ObservableCollection<Human> People { get; }

        public ReactiveCommand<Unit, Unit> StartDownload { get; private set; }
        public ReactiveCommand<Unit, Unit> StopDownload { get; private set; }
        public ReactiveCommand<Unit, Unit> ClearList { get; private set; }
        
        private void UpdatePeople()
        {
            _compositeDisposable = new CompositeDisposable();
            _compositeDisposable.Add(Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(async human =>
                {
                    if (_currentId <= 10)
                    {
                        return await RestService.Get(_currentId);
                    }
                    return null;
                })
                .Subscribe(async result =>
                    {
                        if (result != null)
                        {
                            People.Add(await result);
                        }

                        _currentId++;
                    },
                    error =>
                    {
                        Debug.WriteLine($"!!! ERROR !!! : {error}");
                    }));
        }
    }
}