using System;
using System.Windows;

namespace CycleTimes
{
    public static class ClockHelper
    {
        private const int DEGREES_PER_HOUR = 30;
        private const int DEGREES_PER_MINUTE = 6;
        private const int DEGREES_PER_SECOND = 6;
        private const double DEGREES_OF_HOUR_HAND_PER_MINUTE = 0.5;

        public static Point GetEndPoint(ClockHand clockHand, int handLength, DateTime dateTime, Point origin)
        {
            var hour = dateTime.Hour;
            var minute = dateTime.Minute;
            var second = dateTime.Second;

            switch (clockHand)
            {
                case ClockHand.Hour:
                    return AddXYOffset(GetEndPoint(handLength, 90 - (hour * DEGREES_PER_HOUR + minute * DEGREES_OF_HOUR_HAND_PER_MINUTE)), origin);
                case ClockHand.Minute:
                    return AddXYOffset(GetEndPoint(handLength, 90 - minute * DEGREES_PER_MINUTE), origin);
                case ClockHand.Second:
                    return AddXYOffset(GetEndPoint(handLength, 90 - second * DEGREES_PER_SECOND), origin);
            }

            throw new Exception("Invalid clock hand selection");
        }

        public static Point GetEndPoint(int handLength, double degrees)
        {
            return new Point(handLength * Math.Cos(Math.PI / 180 * degrees), -1 * handLength * Math.Sin(Math.PI / 180 * degrees));
        }

        private static Point AddXYOffset(Point point, Point origin)
        {
            point.X = origin.X + point.X;
            point.Y = origin.Y + point.Y;

            return point;
        }
    }
}
