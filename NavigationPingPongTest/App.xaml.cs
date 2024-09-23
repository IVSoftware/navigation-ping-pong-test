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
        public static TimeSpan PING_PONG_INTERVAL = TimeSpan.FromSeconds(1);
        private static uint _comExceptionCount = 0;
        public static void ReportError(Exception ex, [CallerMemberName] string? caller = null)
        {
            _comExceptionCount++;
            Debug.WriteLine
                ($"{ex.GetType().Name} Count={_comExceptionCount} Caller={caller}{Environment.NewLine}{ex.Message}");
        }
    }
}
