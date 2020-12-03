namespace UnknownPleasures
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    public partial class MainWindow : Window
    {
        private const int ImageSize = 462;
        private List<List<Point>> lines;

        public MainWindow()
        {
            this.Title = "Unknown Pleasures";

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(154) };
            timer.Tick += delegate
            {
                this.GenerateImage(null, null);
            };

            timer.Start();

            this.InitializeComponent();
            this.DrawingCanvas.Height = ImageSize;
            this.DrawingCanvas.Width = ImageSize;

            this.Loaded += this.GenerateImage;
        }

        private void GenerateImage(object sender, RoutedEventArgs e)
        {
            this.lines = new List<List<Point>>();
            this.DrawingCanvas.Children.Clear();
            this.GeneratePoints();
            this.DrawPoints();
        }

        private void GeneratePoints()
        {
            var step = 10;
            var random = new Random();
            for (var stepIndex = step; stepIndex <= ImageSize - step; stepIndex += step)
            {
                var linePoints = new List<Point>();
                for (var pointIndex = step; pointIndex <= ImageSize - step; pointIndex += step)
                {
                    var distanceToCenter = Math.Abs(pointIndex - ImageSize / 2);
                    var variance = Math.Max(ImageSize / 2 - 77 - distanceToCenter, 0);
                    var point = new Point(pointIndex, stepIndex - random.Next(0, variance / 2));
                    linePoints.Add(point);
                }

                this.lines.Add(linePoints);
            }
        }

        private void DrawPoints()
        {
            var fillBrush = new SolidColorBrush { Color = Color.FromArgb(255, 255, 255, 255) };
            var strokeBrush = new SolidColorBrush { Color = Color.FromArgb(255, 0, 0, 0) };

            foreach (var currentLine in this.lines)
            {
                var line = new Polyline
                {
                    Fill = strokeBrush,
                    StrokeThickness = 1.54,
                    Stroke = fillBrush,
                    StrokeEndLineCap = PenLineCap.Round
                };

                foreach (var currentPoint in currentLine)
                {
                    line.Points?.Add(currentPoint);
                }

                this.DrawingCanvas.Children.Add(line);
            }
        }
    }
}