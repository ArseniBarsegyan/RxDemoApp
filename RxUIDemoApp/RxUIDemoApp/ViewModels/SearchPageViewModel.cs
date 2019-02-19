using System.Collections.ObjectModel;
using RxUIDemoApp.Models;

namespace RxUIDemoApp.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        public SearchPageViewModel()
        {
            UrlPathSegment = "Search demo";
            SearchResults = new ObservableCollection<SearchResults>();
        }

        public ObservableCollection<SearchResults> SearchResults { get; }
    }
}