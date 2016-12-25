namespace WPF_Radial_Menu
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public partial class RadialMenu
    {
        public static readonly DependencyProperty SegmentCountProperty = DependencyProperty.Register(nameof(SegmentCount), typeof(Int32), typeof(RadialMenu), new PropertyMetadata(1, OnSegmentCountChanged));

        public static readonly DependencyProperty RingCountProperty = DependencyProperty.Register(nameof(RingCount), typeof(Int32), typeof(RadialMenu), new PropertyMetadata(1, OnRingCountChanged));

        private Point _lastMouseDownPoint;

        public RadialMenu()
        {
            this.InitializeComponent();
        }

        public event EventHandler<RadialMenuField> Click;

        public Int32 SegmentCount
        {
            get { return (Int32)this.GetValue(SegmentCountProperty); }
            set { this.SetValue(SegmentCountProperty, value); }
        }

        public Int32 RingCount
        {
            get { return (Int32)this.GetValue(RingCountProperty); }
            set { this.SetValue(RingCountProperty, value); }
        }

        protected String[,] Labels { get; set; }

        protected void OnSizeChanged(Object sender, SizeChangedEventArgs e)
        {
            this.UpdateUserInterface();
        }

        protected void UpdateUserInterface()
        {
            var size = Math.Min(this.ActualWidth, this.ActualHeight);
            this.Root.Width = this.Root.Height = size;
            this.Root.Children.Clear();
            for (var i = 0; i < this.RingCount; ++i)
            {
                this.Root.Children.Add(new Ellipse()
                {
                    Margin = new Thickness(size / this.RingCount / 2 * i),
                    StrokeThickness = 2,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Transparent,
                });
            }

            if (this.SegmentCount >= 2)
            {
                for (var i = 0; i <= this.SegmentCount; ++i)
                {
                    this.Root.Children.Add(new Line()
                    {
                        X1 = size / 2,
                        Y1 = size / 2,
                        X2 = (Math.Sin(Math.PI * 2 / this.SegmentCount * i) * (size / 2)) + (size / 2),
                        Y2 = (Math.Cos(Math.PI * 2 / this.SegmentCount * i) * (size / 2)) + (size / 2),
                        StrokeThickness = 2,
                        Stroke = Brushes.Black,
                    });
                }
            }

            this.Labels = new String[this.SegmentCount, this.RingCount];
            for (var s = 0; s < this.SegmentCount; ++s)
            {
                for (var r = 0; r < this.RingCount; ++r)
                {
                    this.Labels[s, r] = new RadialMenuField(r, s).ToString();
                }
            }

            for (var s = 0; s < this.SegmentCount; ++s)
            {
                for (var r = 0; r < this.RingCount; ++r)
                {
                    var tb = new TextBlock()
                    {
                        Text = this.Labels[s, r],
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    this.Root.Children.Add(tb);
                    tb.UpdateLayout();
                    tb.Margin = new Thickness(
                            (Math.Sin(Math.PI * 2 / this.SegmentCount * (s + 0.5)) * (size / 2) / this.RingCount * (r + 0.5)) + (size / 2) - (tb.ActualWidth / 2),
                            (Math.Cos(Math.PI * 2 / this.SegmentCount * (s + 0.5)) * (size / 2) / this.RingCount * (r + 0.5)) + (size / 2) - (tb.ActualHeight / 2),
                            0,
                            0);
                }
            }
        }

        protected Double Normalize(Double value, Double circle)
        {
            while (value < 0)
            {
                value += circle;
            }

            while (value >= circle)
            {
                value -= circle;
            }

            return value;
        }

        protected RadialMenuField GetFieldFromPoint(Point p)
        {
            var size = Math.Min(this.ActualWidth, this.ActualHeight) / 2;
            var distance = Math.Sqrt(((p.X - size) * (p.X - size)) + ((p.Y - size) * (p.Y - size)));
            return new RadialMenuField(
                (Int32)(distance / size * this.RingCount),
                (Int32)(this.Normalize(-Math.Atan2((p.Y - size) / distance, (p.X - size) / distance) + (Math.PI / 2), Math.PI * 2) / 2 / Math.PI * this.SegmentCount));
        }

        private static void OnSegmentCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as RadialMenu;
            if (control == null)
            {
                return;
            }

            var val = (Int32)e.NewValue;
            if (val <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(RadialMenu.SegmentCount), $"{nameof(RingCount)} musst be greater or equal 1.");
            }

            control.UpdateUserInterface();
        }

        private static void OnRingCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as RadialMenu;
            if (control == null)
            {
                return;
            }

            var val = (Int32)e.NewValue;
            if (val <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(RadialMenu.SegmentCount), $"{nameof(RingCount)} musst be greater or equal 1.");
            }

            control.UpdateUserInterface();
        }

        private void OnMouseDown(Object sender, MouseButtonEventArgs e)
        {
            this._lastMouseDownPoint = e.GetPosition(this.Root);
        }

        private void OnMouseUp(Object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(this.Root);
            var field = this.GetFieldFromPoint(pos);
            if (!field.Equals(this.GetFieldFromPoint(this._lastMouseDownPoint)) || !this.DeltaCompare(pos, this._lastMouseDownPoint, 3))
            {
                return;
            }

            this.Click?.Invoke(this, field);
        }

        private Boolean DeltaCompare(Point p1, Point p2, Double delta)
        {
            return (p1.X < p2.X + delta && p1.X > p2.X - delta)
                && (p1.Y < p2.Y + delta && p1.Y > p2.Y - delta);
        }
    }
}
