using System.Diagnostics;

namespace NavigationPingPongTest
{
    public partial class MainPage : ContentPage
    {
        private static uint _instanceCounter = 0;
        private int _debugCount;

        public MainPage()
        {
            _instanceCounter++;
            Debug.Assert(_instanceCounter <= 1, "Expecting only one instance of this class.");
            InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            await Task.Delay(App.PING_PONG_INTERVAL);
            int tries = 1;
            retry:
            try
            {
                Debug.WriteLine($"Count = {_debugCount++}");
                await Shell.Current.GoToAsync(nameof(ChildPageA));
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                if (tries == 1) App.ReportError(ex);
                if (tries++ < 5)
                {
                    goto retry;
                }
                else throw new AggregateException(ex);
            }
        }
    }
}
