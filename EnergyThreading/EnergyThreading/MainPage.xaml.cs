using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EnergyThreading
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Instance instance;
        public TimeSpan _timeOfDay;
        private Stopwatch stopwatch1 = new Stopwatch();
        private Stopwatch stopwatch2 = new Stopwatch();
        public MainPage()
        {
            this.InitializeComponent();
            if (instance == null)
            {
                // pass amount of houses and start supply to instance
                instance = new Instance(MyFrame, 100, 0);
                instance.initialize();
                TotalDemandResult.Text = instance.totalDemand.ToString();
                TotalSupplyResult.Text = instance.getCity.generator.powerSupply.ToString();
                HousesAmountResult.Text = instance.getCity.getHouses().Count.ToString() + " houses";

                if (!instance.getCity.getSingleThread) {
                    ThreadingTypeText.Text = "MultiThread";
                } else
                {
                    ThreadingTypeText.Text = "SingleThread";
                }
                
            }
        }
        
        public void amountOfHouses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (houses.SelectedItem != null)
            {
                var selectedAmount =(int)houses.SelectedItem;
                instance.getCity.createHouses(selectedAmount);
                HousesAmountResult.Text = instance.getCity.getHouses().Count.ToString() + " houses";

                instance.totalDemand = instance.getCity.calculateTotalDemand();
                TotalDemandResult.Text = instance.totalDemand.ToString();
            }
        }

        public void Button_Click_Next_Day(object sender, RoutedEventArgs e)
        {
            int amountOfHouses = instance.getCity.getHouses().Count;
            Boolean singleThread = instance.getCity.getSingleThread;
            float remainingSupply = instance.getCity.generator.powerSupply;

            instance = new Instance(MyFrame, amountOfHouses, remainingSupply);
            instance.initialize();
            TotalDemandResult.Text = instance.totalDemand.ToString();
            TotalSupplyResult.Text = instance.getCity.generator.powerSupply.ToString();

            if (!singleThread)
            {
                instance.getCity.setSingleThread(false);
                ThreadingTypeText.Text = "MultiThread";
            }
            else
            {
                instance.getCity.setSingleThread(true);
                ThreadingTypeText.Text = "SingleThread";
            }

        }
        
        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Button_Click_Change_Threading(object sender, RoutedEventArgs e)
        {
            if (instance.getCity.getSingleThread == true)
            {
                instance.getCity.setSingleThread(false);
                ThreadingTypeText.Text = "MultiThread";
            }
            else
            {
                instance.getCity.setSingleThread(true);
                ThreadingTypeText.Text = "SingleThread";
            }
        }

        private async void Button_Click_ProducePower(object sender, RoutedEventArgs e)
        {
            stopwatch1.Restart();

            await Task.Run(() =>
            {
                instance.getCity.produceEnergyForHouses();
            });
            
            stopwatch1.Stop();
            double elapsedGen = stopwatch1.Elapsed.TotalMilliseconds;
            generateTimerResult.Text = elapsedGen.ToString() + "ms";
            TotalSupplyResult.Text = instance.getCity.generator.powerSupply.ToString();
            stopwatch1.Reset();

        }

        private void Button_Click_SupplyPower(object sender, RoutedEventArgs e)
        {
            stopwatch2.Start();
            instance.update();
            stopwatch2.Stop();
            double elapsedSupply = stopwatch2.Elapsed.TotalMilliseconds;
            supplyTimerResult.Text = elapsedSupply.ToString() + "ms";
            stopwatch2.Reset();

            TotalSupplyResult.Text = instance.getCity.generator.powerSupply.ToString();
            instance.totalDemand = instance.getCity.calculateTotalDemand();
            TotalDemandResult.Text = instance.totalDemand.ToString();
        }
    }
}
