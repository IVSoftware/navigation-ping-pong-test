using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NavigationPingPongTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
        public Stopwatch Stopwatch { get;  } = new Stopwatch();
#if __ANDROID__
        public static TimeSpan PING_PONG_INTERVAL = TimeSpan.FromSeconds(5); 
#else
        public static TimeSpan PING_PONG_INTERVAL = TimeSpan.FromSeconds(1);
#endif
        private static uint _comExceptionCount = 0;
        public static void ReportError(Exception ex, string sender, [CallerMemberName] string? caller = null)
        {
            _comExceptionCount++;
            Debug.WriteLine
                ($"{ex.GetType().Name} Count={_comExceptionCount} Caller={sender}.{caller}{Environment.NewLine}{ex.Message}");
        }
    }
    static partial class Extensions
    {
        public static TimeSpan MinusPingPongInterval(this TimeSpan interval) =>
            interval - App.PING_PONG_INTERVAL;
    }
}
