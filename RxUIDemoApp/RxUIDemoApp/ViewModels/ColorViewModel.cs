using System.Drawing;
using System.Reactive;
using ReactiveUI;

namespace RxUIDemoApp.ViewModels
{
    public class ColorViewModel : BaseViewModel, IScreen
    {
        // Read-write property
        private int redColor;
        public int RedColor
        {
            get => redColor;
            set => this.RaiseAndSetIfChanged(ref redColor, value);
        }

        private int greenColor;
        public int GreenColor
        {
            get => greenColor;
            set => this.RaiseAndSetIfChanged(ref greenColor, value);
        }

        private int blueColor;
        public int BlueColor
        {
            get => blueColor;
            set => this.RaiseAndSetIfChanged(ref blueColor, value);
        }

        // Output property
        private readonly ObservableAsPropertyHelper<Color> _color;
        public Color Color => _color.Value;

        public ColorViewModel()
        {
            Router = new RoutingState();
            GoNext = ReactiveCommand.CreateFromObservable(() => this.HostScreen.Router.Navigate.Execute(new EventDemoViewModel()));
            UrlPathSegment = "Color";
            this.WhenAnyValue(x => x.RedColor, x => x.GreenColor, x => x.BlueColor,
                    (red, green, blue) => Color.FromArgb(255, red, green, blue))
                .ToProperty(this, v => v.Color, out _color);
        }

        public RoutingState Router { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }
    }
}