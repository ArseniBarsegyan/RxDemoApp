using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using RxUIDemoApp.Models;
using RxUIDemoApp.Services;

namespace RxUIDemoApp.ViewModels
{
    public class RestPageViewModel : BaseViewModel
    {
        private long _currentId;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public RestPageViewModel()
        {
            UrlPathSegment = "REST demo";
            People = new ObservableCollection<Human>();
            selectedHuman = new Human();

            StartDownload = ReactiveCommand.CreateFromTask(task =>
            {
                return Task.Run(() =>
                {
                    UpdatePeople();
                }, _cancellationTokenSource.Token);
            });
            StopDownload = ReactiveCommand.Create(() =>
            {
                _compositeDisposable.Dispose();
                _cancellationTokenSource.Cancel();
            });
            ClearList = ReactiveCommand.Create(() =>
            {
                _compositeDisposable = new CompositeDisposable();
                People.Clear();
                Interlocked.Exchange(ref _currentId, 0);
                _cancellationTokenSource = new CancellationTokenSource();
            });
        }

        private Human selectedHuman;
        public Human SelectedHuman
        {
            get => selectedHuman;
            set => this.RaiseAndSetIfChanged(ref selectedHuman, value);
        }

        public ObservableCollection<Human> People { get; }

        /// <summary>
        /// Executing this command create new task which get single record from database.
        /// </summary>
        public ReactiveCommand<Unit, Unit> StartDownload { get; private set; }
        /// <summary>
        /// Clear subscriptions and stop all tasks.
        /// </summary>
        public ReactiveCommand<Unit, Unit> StopDownload { get; private set; }
        /// <summary>
        /// Reset list so you should be able to download again.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ClearList { get; private set; }
        
        private void UpdatePeople()
        {
            var subscription = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(async human =>
                {
                    if (Interlocked.Read(ref _currentId) <= 10)
                    {
                        Interlocked.Increment(ref _currentId);
                        return await RestService.Get(_currentId);
                    }
                    return null;
                })
                .Subscribe(async result =>
                {
                    if (result.Result != null)
                    {
                        People.Add(await result);
                    }
                }, error => { Debug.WriteLine($"!!! ERROR !!! : {error}"); });
            _compositeDisposable.Add(subscription);
        }
    }
}