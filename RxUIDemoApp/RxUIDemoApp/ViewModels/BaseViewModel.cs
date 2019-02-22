using System.Reactive;
using ReactiveUI;
using Splat;

namespace RxUIDemoApp.ViewModels
{
    public class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment { get; protected set; }
        public IScreen HostScreen { get; }

        public BaseViewModel()
        {
            HostScreen = Locator.Current.GetService<IScreen>();
            Router = new RoutingState();
        }

        #region routing
        public RoutingState Router { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; protected set; }
        #endregion
    }
}
