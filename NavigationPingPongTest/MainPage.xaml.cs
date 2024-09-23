using System.Diagnostics;
using System.Text;

namespace NavigationPingPongTest
{
    public partial class MainPage : ContentPage
    {
        private static uint _instanceCounter = 0;
        private int _debugCount = 0;
        Stopwatch _stopwatch = new Stopwatch();
        public MainPage()
        {
            _instanceCounter++;
            Debug.Assert(_instanceCounter <= 1, "Expecting only one instance of this class.");
            InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            if (App.Current?.MainPage?.Handler != null)
            {
                int tries = 1;
                retry:
                try
                {
                    var builder = new StringBuilder();
                    if (_debugCount > 0)
                    {
                        builder.Append($@"[{_stopwatch.Elapsed.MinusPingPongIntervals():mm\:ss\.fff}] ");
                    }
                    _debugCount++;
                    builder.Append($"Count = {_debugCount}");
                    var message = builder.ToString();
                    Debug.WriteLine(message);
                    labelMessage.Text = builder.ToString();
                    await Task.Delay(App.PING_PONG_INTERVAL);
                    _stopwatch.Restart();
                    await Shell.Current.GoToAsync(nameof(ChildPageA));
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    if (tries == 1)
                    {
                       labelError.Text = App.ReportError(ex, sender: nameof(MainPage));
                    }
                    if (tries++ < 5)
                    {
                        goto retry;
                    }
                    else throw new AggregateException(ex);
                }
            }
        }
    }
}
