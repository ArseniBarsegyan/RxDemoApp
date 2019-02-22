using ReactiveUI;
using ReactiveUI.XamForms;
using RxUIDemoApp.ViewModels;
using RxUIDemoApp.Views;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RxUIDemoApp
{
    public partial class App : Application, IScreen
    {
        public RoutingState Router { get; set; }

        public App()
        {
            InitializeComponent();

            this.Router = new RoutingState();
            // Register ourselves as the Screen instance
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            // Register view models
            Locator.CurrentMutable.Register(() => new SearchDemoPage(), typeof(IViewFor<SearchPageViewModel>));
            Locator.CurrentMutable.Register(() => new ColorPage(), typeof(IViewFor<ColorViewModel>));
            Locator.CurrentMutable.Register(() => new RestPage(), typeof(IViewFor<RestPageViewModel>));
            Locator.CurrentMutable.Register(() => new ContainerPage(), typeof(IViewFor<ContainerPageViewModel>));
            Locator.CurrentMutable.Register(() => new EventDemoPage(), typeof(IViewFor<EventDemoViewModel>));
            Locator.CurrentMutable.Register(() => new DetailsPage(), typeof(IViewFor<DetailsViewModel>));

            // Navigate
            this.Router.Navigate.Execute(new ContainerPageViewModel());
            MainPage = new RoutedViewHost();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
