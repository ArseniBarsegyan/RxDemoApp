using System.Collections.ObjectModel;
using ReactiveUI;
using RxUIDemoApp.Models;

namespace RxUIDemoApp.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        public SearchPageViewModel()
        {
            UrlPathSegment = "Search demo";
            SearchResults = new ObservableCollection<SearchResults>();

            GoNext = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new DetailsViewModel(SelectedSearchResults)));
        }

        public ObservableCollection<SearchResults> SearchResults { get; }

        private SearchResults selectedSearchResults;
        public SearchResults SelectedSearchResults
        {
            get => selectedSearchResults;
            set => this.RaiseAndSetIfChanged(ref selectedSearchResults, value);
        }
    }
}