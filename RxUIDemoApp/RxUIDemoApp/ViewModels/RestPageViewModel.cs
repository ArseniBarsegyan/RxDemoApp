using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using RxUIDemoApp.Models;
using RxUIDemoApp.Services;

namespace RxUIDemoApp.ViewModels
{
    public class RestPageViewModel : BaseViewModel
    {
        private int _currentId = 1;
        private CompositeDisposable _compositeDisposable;

        private ObservableCollection<Human> _people;
        public ObservableCollection<Human> People => _people;

        public ReactiveCommand<Unit, Unit> StartDownload { get; private set; }
        public ReactiveCommand<Unit, Unit> StopDownload { get; private set; }
        public ReactiveCommand<Unit, Unit> ClearList { get; private set; }

        public RestPageViewModel()
        {
            UrlPathSegment = "REST demo";
            _people = new ObservableCollection<Human>();
            StartDownload = ReactiveCommand.CreateFromTask(async x => await Task.Run(() =>
            {
                UpdatePeople();
            }));
            StopDownload = ReactiveCommand.CreateFromTask(async x => await Task.Run(() =>
            {
                _compositeDisposable.Dispose();
            }));
            ClearList = ReactiveCommand.CreateFromTask(async x => await Task.Run(() =>
            {
                _people.Clear();
                _currentId = 1;
            }));
        }

        private void UpdatePeople()
        {
            _compositeDisposable = new CompositeDisposable();
            _compositeDisposable.Add(Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(async human => await RestService.Get(_currentId))
                .ObserveOn(RxApp.TaskpoolScheduler)
                .Subscribe(async result =>
                    {
                        if (result != null)
                        {
                            _people.Add(await result);
                        }

                        _currentId++;
                    },
                    error => { Debug.WriteLine("!!! --- REST download Error --- !!!"); }));
        }
    }
}