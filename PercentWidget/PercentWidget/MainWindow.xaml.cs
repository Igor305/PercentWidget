using System;
using System.Net.Http;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PercentWidget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Timer aTimer;
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;

            getPercent();

            aTimer = new Timer(21600000);
            aTimer.Elapsed += (s,e) => getPercent();
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private async void getPercent()
        {
            double percent;
            try
            {
                string response = await client.GetStringAsync("http://10.13.153.10/api/percent");
                percent = Double.Parse(response);

                this.Dispatcher.Invoke(() =>
                {
                    if (percent > 98)
                    {
                        Gauge.FromColor = Color.FromRgb(64, 117, 62);
                        Gauge.ToColor = Color.FromRgb(30, 199, 24);
                    }
                    if (percent <= 98)
                    {
                        Gauge.FromColor = Color.FromRgb(173, 5, 16);
                        Gauge.ToColor = Color.FromRgb(255, 0, 17);

                        if (percent == 0)
                        {
                            Gauge.GaugeBackground = Brushes.LightSlateGray;
                        }
                    }
                    Gauge.Value = percent;
                    TextB.Text = $"{percent}%";
                });
            }
            catch
            {
                this.Dispatcher.Invoke(() =>
                {

                    percent = 0;

                    Gauge.Value = percent;
                    TextB.Text = $"{percent}%";
                });
            }
        }

        private void Btn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }      
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            wind.Close();
        }
    }
}
