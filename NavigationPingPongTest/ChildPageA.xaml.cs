using System.Diagnostics;

namespace NavigationPingPongTest;

public partial class ChildPageA : ContentPage
{
    private static uint _instanceCounter = 0;
	public ChildPageA()
	{
		_instanceCounter++;
#if USE_SINGLETON
        Debug.Assert(_instanceCounter <= 1, "Expecting only one instance of this class.");
#endif
		InitializeComponent();
	}
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // If the loop is running, and we try to close the app, it's
        // possible to cause an exception by trying to navigate when
        // the main window handle has already disposed, so we check.
        if (App.Current?.MainPage?.Handler != null)
        {
            await Task.Delay(App.PING_PONG_INTERVAL);
            int tries = 1;
            retry:
            try
            {
                await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                if (tries == 1) App.ReportError(ex, sender: nameof(ChildPageA));
                if (tries++ < 5)
                {
                    goto retry;
                }
                else throw new AggregateException(ex);
            }
        }
    }
}