using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace CycleTimes
{
    public partial class MainWindow : Window
    {
        private const string CENTRAL_STANDARD_TIME_ID = "Central Standard Time";
        private const int HOUR_HAND_LENGTH = 50;
        private const int MINUTE_HAND_LENGTH = 75;
        private const int SECOND_HAND_LENGTH = 75;

        private readonly Point ORIGIN = new Point(0, 100);
        private readonly TimeZoneInfo _centralZone = TimeZoneInfo.FindSystemTimeZoneById(CENTRAL_STANDARD_TIME_ID);

        DispatcherTimer _timer = new DispatcherTimer();
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _timer.Tick += _timer_Tick; ;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            var centralTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _centralZone);

            var hourXY = ClockHelper.GetEndPoint(ClockHand.Hour, HOUR_HAND_LENGTH, centralTime, ORIGIN);
            hourHand.Data = new LineGeometry(ORIGIN, hourXY);

            var minuteXY = ClockHelper.GetEndPoint(ClockHand.Minute, MINUTE_HAND_LENGTH, centralTime, ORIGIN);
            minuteHand.Data = new LineGeometry(ORIGIN, minuteXY);

            var secondXY = ClockHelper.GetEndPoint(ClockHand.Second, SECOND_HAND_LENGTH, centralTime, ORIGIN);
            secondHand.Data = new LineGeometry(ORIGIN, secondXY);

            cst.Text = centralTime.ToLongTimeString() + " CST";
        }
    }
}