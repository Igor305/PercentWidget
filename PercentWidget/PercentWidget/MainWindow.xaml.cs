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
        private static bool createdNew = true;

        public MainWindow()
        {  
            using (System.Threading.Mutex instanceMutex = new System.Threading.Mutex(true, @"Global\ControlPanel", out createdNew))
            {
                if (!createdNew)
                {
                    Application.Current.Shutdown();
                    return;
                }
            }

            InitializeComponent();

            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;

            getPercent();

            aTimer = new Timer(21600000);
            aTimer.Elapsed += (s,e) => getPercent();
            aTimer.AutoReset = true;
            aTimer.Enabled = true;  
        }

        private void setZeroPercent()
        {
            Gauge.AnimationsSpeed = TimeSpan.FromMilliseconds(100);
            Gauge.Value = 0;
            TextB.Text = "";
        }

        private async void getPercent()
        {
            double percent;
            try
            {              
                string response = await client.GetStringAsync("http://widget-percent-service.avrora.lan/api/percent");
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

        private void update(object sender, RoutedEventArgs e)
        {
            setZeroPercent();
            getPercent();
        }

        private void fix(object sender, RoutedEventArgs e)
        {
            if (!wind.Topmost)
            {
                menuFixation.Header = "Відкріпити поверх всіх вікон";
                wind.Topmost = true;
                return;
            }

            if (wind.Topmost)
            {
                menuFixation.Header = "Закріпити поверх всіх вікон";
                wind.Topmost = false;
                return;
            }
        } 
        
        private void changeSize(object sender, RoutedEventArgs e)
        {
            if (wind.Width == 200)
            {
                wind.Height = 140;
                wind.Width = 140;
                Gauge.Height = 115;
                Gauge.Width = 115;
                elipse1.Height = 105;
                elipse1.Width = 105;
                elipse2.Height = 115;
                elipse2.Width = 115;
                elipse2Effect.ShadowDepth = 4;
                elipse3.Height = 120;
                elipse3.Width = 120;
                elipse3Effect.ShadowDepth = 4;
                TextB.FontSize = 35;

                menuSize.Header = "Збільшити розмір";
                return;
            }

            if (wind.Width == 140)
            {
                wind.Height = 200;
                wind.Width = 200;
                Gauge.Height = 153;
                Gauge.Width = 153;
                elipse1.Height = 140;
                elipse1.Width = 140;
                elipse2.Height = 160;
                elipse2.Width = 160;
                elipse2Effect.ShadowDepth = 7;
                elipse3.Height = 170;
                elipse3.Width = 170;
                elipse3Effect.ShadowDepth = 7;
                TextB.FontSize = 50;

                menuSize.Header = "Зменшити розмір";
                return;
            }
        }

        private void close(object sender, RoutedEventArgs e)
        {
            wind.Close();
        }

        private void Btn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

            if (e.LeftButton == MouseButtonState.Released)
            {
                if (this.Left < 0)
                {
                    this.Left = 0;
                }
                if (this.Top < 0)
                {
                    this.Top = 0;
                }
                
                if (this.Left + this.Width > SystemParameters.VirtualScreenWidth)
                {
                    this.Left = SystemParameters.VirtualScreenWidth - this.Width;
                }
                if (this.Top + this.Height > SystemParameters.VirtualScreenHeight)
                {
                    this.Top = SystemParameters.VirtualScreenHeight - this.Height;
                }
            }
        }
    }
}