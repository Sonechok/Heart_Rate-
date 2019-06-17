using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using MiBand2SDK;
using MiBand2SDK.Components;
using Windows.ApplicationModel.Background;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace App3
{

    public sealed class CheckHeartRateInBackgroundTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _defferal;
        private static MiBand2SDK.MiBand2 band = new MiBand2SDK.MiBand2();

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _defferal = taskInstance.GetDeferral();

            if (await band.ConnectAsync())
            {
                // You will receive heart rate measurements from the band
                await band.HeartRate.SubscribeToHeartRateNotificationsAsync((sender, args) =>
                {
                    int currentHeartRate = args.CharacteristicValue.ToArray()[1];
                    System.Diagnostics.Debug.WriteLine($"Current heartrate from background task is {currentHeartRate} bpm ");
                });
            }
        }

        public static async void RegisterAndRunAsync()
        {
            var taskName = typeof(CheckHeartRateInBackgroundTask).Name;
            IBackgroundTaskRegistration checkHeartRateInBackground = BackgroundTaskRegistration.AllTasks.Values.FirstOrDefault(t => t.Name == taskName);

            if (band.IsConnected() && band.Identity.IsAuthenticated())
            {
                if (checkHeartRateInBackground != null)
                    checkHeartRateInBackground.Unregister(true);

                var deviceTrigger = new DeviceUseTrigger();
                var deviceInfo = await band.Identity.GetPairedBand();

                var taskBuilder = new BackgroundTaskBuilder
                {
                    Name = taskName,
                    TaskEntryPoint = typeof(CheckHeartRateInBackgroundTask).ToString(),
                    IsNetworkRequested = false
                };

                taskBuilder.SetTrigger(deviceTrigger);
                BackgroundTaskRegistration task = taskBuilder.Register();

                await deviceTrigger.RequestAsync(deviceInfo.Id);
            }
        }
    }

    public class BitRate
    {
        public String Time
        {
            get;
            set;
        }
        public int Bit
        {
            get;
            set;
        }
    }
    public sealed partial class MainPage : Page
    {
        MiBand2 band;
        DispatcherTimer timer;
        bool IsRun = false;
        List<BitRate> HearBits;
        int counter;

        public MainPage()
        {
            this.InitializeComponent();

            band = new MiBand2();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            
        }

        private async void Timer_Tick(object sender, object e)
        {
            if (IsRun)
            {
                counter++;
                BitRate br = new BitRate();
                br.Time = counter.ToString();
                int cur = await GetHeart();
                br.Bit = cur;
                if (cur > 0)
                {
                    HearBits.Add(br);
                    tbHr.Text = cur.ToString();
                    tbCount.Text = counter.ToString();
                }

            }
        }

        public void StartCardio()
        {
            counter = 0;
            HearBits = new List<BitRate>();
            IsRun = true;
            timer.Start();
           
           
        }

        public void StopCardio()
        {
            IsRun = false;
            timer.Stop();
            (LineChart.Series[0] as LineSeries).ItemsSource = HearBits;
        }

        public async Task<int> GetHeart()
        {
            return await band.HeartRate.GetHeartRateAsync();
        }

        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRun)
            {
                btnStart.Content = "Stop";
                if (await band.ConnectAsync())
                {
                    if (await band.Identity.AuthenticateAsync())
                    {
                        StartCardio();
                    }
                }
            }
            else
            {
                btnStart.Content = "Start";
                StopCardio();
            }

            
            //if (await band.ConnectAsync())
            //{
            //    if (await band.Identity.AuthenticateAsync())
            //    {
            //        tbHr.Text = (await GetHeart()).ToString();
            //    }
            //}

            //CheckHeartRateInBackgroundTask.RegisterAndRunAsync();
        }
    }
}


