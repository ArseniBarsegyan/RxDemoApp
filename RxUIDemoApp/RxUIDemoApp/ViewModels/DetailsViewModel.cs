using RxUIDemoApp.Models;

namespace RxUIDemoApp.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        private readonly SearchResults _searchResults;

        public DetailsViewModel(SearchResults searchResults)
        {
            UrlPathSegment = $"Details";
            _searchResults = searchResults;
        }

        public string Description => _searchResults.Description;
    }
}