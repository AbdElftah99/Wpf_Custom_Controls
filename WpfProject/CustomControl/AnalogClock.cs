using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfProject.CustomControl
{
    public delegate void TimeChangedEventHandler(object sender, TimeChangedEventArgs e);
    public class AnalogClock : Control
    {
        private Line hourHand;
        private Line minuteHand;
        private Line secondHand;

        private static readonly DependencyProperty ShowSecondsProperty =
            DependencyProperty.Register("ShowSeconds", typeof(bool), typeof(AnalogClock), new PropertyMetadata(true));

        private static readonly RoutedEvent TimeChangedEvent =
            EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(TimeChangedEventHandler), typeof(AnalogClock));

        public event TimeChangedEventHandler TimeChanged
        {
            add => AddHandler(TimeChangedEvent, value);
            remove => RemoveHandler(TimeChangedEvent, value);
        }

        public bool ShowSeconds
        {
            get => (bool)GetValue(ShowSecondsProperty);
            set => SetValue(ShowSecondsProperty, value);
        }

        static AnalogClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock),
                new FrameworkPropertyMetadata(typeof(AnalogClock)));
        }



        public override void OnApplyTemplate()
        {
            hourHand = Template.FindName("PART_HourHand", this) as Line;
            minuteHand = Template.FindName("PART_MinuteHand", this) as Line;
            secondHand = Template.FindName("PART_SecondHand", this) as Line;

            UpdateHandAngles(DateTime.Now);

            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (s, e) =>
            {
                OnTimeChanged(DateTime.Now);
            };
            timer.Start();

            base.OnApplyTemplate();
        }

        public virtual void OnTimeChanged(DateTime time)
        {
            UpdateHandAngles(time);
            TimeChangedEventArgs args = new(TimeChangedEvent, this, time);
            RaiseEvent(args);
        }

        private void UpdateHandAngles(DateTime time)
        {
            hourHand.RenderTransform = new RotateTransform((time.Hour / 12.0) * 360, 0.5, 0.5);
            minuteHand.RenderTransform = new RotateTransform((time.Minute / 60.0) * 360, 0.5, 0.5);
            secondHand.RenderTransform = new RotateTransform((time.Second / 60.0) * 360, 0.5, 0.5);
        }
    }

    public class TimeChangedEventArgs : RoutedEventArgs
    {
        public DateTime Time { get; }
        public TimeChangedEventArgs(RoutedEvent routedEvent, object source, DateTime time) : base(routedEvent, source)
        {
            Time = time;
        }
    }
}
