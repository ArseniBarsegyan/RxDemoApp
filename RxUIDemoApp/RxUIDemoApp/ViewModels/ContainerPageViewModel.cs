namespace RxUIDemoApp.ViewModels
{
    public class ContainerPageViewModel : BaseViewModel
    {
        public ColorViewModel ColorViewModel { get; }
        public RestPageViewModel RestViewModel { get; }
        public SearchPageViewModel SearchPageViewModel { get; }
        public EventDemoViewModel EventDemoViewModel { get; }

        public ContainerPageViewModel()
        {
            UrlPathSegment = "Tabbed page";
            ColorViewModel = new ColorViewModel();
            RestViewModel = new RestPageViewModel();
            SearchPageViewModel = new SearchPageViewModel();
            EventDemoViewModel = new EventDemoViewModel();
        }
    }
}