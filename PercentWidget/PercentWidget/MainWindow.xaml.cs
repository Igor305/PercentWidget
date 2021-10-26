using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        public MainWindow()
        {
            try
            {

                InitializeComponent();
                this.Left = SystemParameters.PrimaryScreenWidth - this.Width;

                getPercent();

                aTimer = new Timer(21600000);
                aTimer.Elapsed += (o,e) => getPercent();
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void getPercent()
        {
            double percent;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                DataTable dt_user = Select(appSettings["Select"]);

                percent = Math.Truncate(Double.Parse(dt_user.Rows[0][0].ToString()));
            }
            catch
            {
                percent = 0;
            }

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
        }

        private DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);
            sqlConnection.Open();                                           
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          
            sqlCommand.CommandText = selectSQL;                             
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); 
            sqlDataAdapter.Fill(dataTable);                                 
                                                                            
            return dataTable;
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
