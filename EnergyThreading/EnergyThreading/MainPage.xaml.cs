using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public MainPage()
        {
            this.InitializeComponent();
            if (instance == null)
            {
                // pass time of day & amount of houses to instance
                instance = new Instance(MyFrame);
                instance.initialize();
                TotalDemand.Text = instance.totalDemand.ToString();

            }
            _timeOfDay = checkTime();
        }

        public void amountOfHouses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            instance.update();
            int houseAmount = (int)e.AddedItems[0];
            if (houses.SelectedItem != null)
            {
                //ComboBoxItem cbi1 = (ComboBoxItem)(sender as ComboBox).SelectedItem;  
                ComboBoxItem cbi = (ComboBoxItem)houses.SelectedItem;
                int selectedAmount = (int)cbi.Content;
            }
        }

        /*private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            instance.update();
            houses.Text = instance.totalDemand.ToString();
        }*/

        public TimeSpan checkTime()
        {
            DateTime _startStopwatch;
            _startStopwatch = DateTime.Now;
            TimeSpan elapsed = DateTime.Now - _startStopwatch;
            if (timePicker.SelectedTime != null)
            {
                TimeSpan currentTimeOfDay = (TimeSpan)(elapsed + timePicker.SelectedTime);
                return currentTimeOfDay;
            }
            else
            {
                return TimeSpan.Zero;
            }

        }

        public void Button_Click_Singlethread(object sender, RoutedEventArgs e)
        {
           instance.update();
           TotalDemand.Text = instance.totalDemand.ToString();
           TotalSupply.Text = instance.getCity.storedEnergy.ToString();
        }
        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click_Multithread(object sender, RoutedEventArgs e)
        {
            if (instance.getCity.getSingleThread == true)
            {
                instance.getCity.setSingleThread(false);
            }
            else
            {
                instance.getCity.setSingleThread(true);
            }
            while (!instance.getCity.getSingleThread)
            {
                await Task.Delay(100);
            }
        }

        private async void newbutton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                instance.getCity.produceEnergyForHouses();
            });
        }
    }
}
